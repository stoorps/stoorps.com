import * as THREE from "three";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";
import "./style.css";
const viewport = document.querySelector("#viewport");
const renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setPixelRatio(Math.min(devicePixelRatio, 2));
viewport.append(renderer.domElement);
const scene = new THREE.Scene();
scene.background = new THREE.Color("#e8eee8");
const camera = new THREE.PerspectiveCamera(38, 1, 0.1, 2000);
camera.up.set(0, 0, 1);
camera.position.set(85, -100, 95);
const controls = new OrbitControls(camera, renderer.domElement);
controls.enableDamping = true;
scene.add(new THREE.HemisphereLight(0xffffff, 0x657f70, 3));
const light = new THREE.DirectionalLight(0xffffff, 3);
light.position.set(30, -50, 100);
scene.add(light);
const material = new THREE.MeshStandardMaterial({
  color: 0x548f7c,
  roughness: 0.48,
  metalness: 0.12,
});
let mesh,
  fitted = false;
new ResizeObserver(() => {
  const w = viewport.clientWidth,
    h = viewport.clientHeight;
  renderer.setSize(w, h);
  camera.aspect = w / h;
  camera.updateProjectionMatrix();
}).observe(viewport);
renderer.setAnimationLoop(() => {
  controls.update();
  renderer.render(scene, camera);
});
const worker = new Worker(new URL("./worker.js", import.meta.url), {
  type: "module",
});
let sequence = 0,
  revision = 0,
  timer,
  busy = false,
  queued = null;
const pending = new Map();
function request(type, extra = {}) {
  return new Promise((resolve, reject) => {
    const id = ++sequence;
    pending.set(id, { resolve, reject });
    worker.postMessage({ id, type, ...extra });
  });
}
worker.onmessage = ({ data }) => {
  const p = pending.get(data.id);
  pending.delete(data.id);
  if (data.error) p?.reject(new Error(data.error));
  else p?.resolve(data);
};
worker.onerror = (e) => {
  for (const p of pending.values()) p.reject(new Error(e.message));
  pending.clear();
};
const status = document.querySelector("#status"),
  metrics = document.querySelector("#metrics");
const buttons = [
  document.querySelector("#step"),
  document.querySelector("#stl"),
];
const form = document.querySelector("form");
function disable() {
  buttons.forEach((b) => (b.disabled = true));
}
async function build(params, version) {
  if (busy) {
    queued = { params, version };
    return;
  }
  busy = true;
  try {
    const data = await request("build", { params });
    if (version !== revision) return;
    const geometry = new THREE.BufferGeometry();
    geometry.setAttribute(
      "position",
      new THREE.BufferAttribute(data.positions, 3),
    );
    geometry.setAttribute("normal", new THREE.BufferAttribute(data.normals, 3));
    geometry.setIndex(new THREE.BufferAttribute(data.indices, 1));
    if (mesh) {
      scene.remove(mesh);
      mesh.geometry.dispose();
    }
    mesh = new THREE.Mesh(geometry, material);
    scene.add(mesh);
    const [w, l, h] = params;
    controls.target.set(w / 2, l / 2, h / 2);
    if (!fitted) {
      camera.position.set(w * 2, -l * 2, h + 75);
      fitted = true;
    }
    status.textContent = `Rebuilt in ${data.milliseconds.toFixed(1)} ms`;
    metrics.textContent = `${(data.volume / 1000).toFixed(3)} cm³ · ${data.indices.length / 3} triangles · Build ${version}`;
    buttons.forEach((b) => (b.disabled = false));
  } catch (e) {
    if (version === revision) {
      status.textContent = e.message;
      metrics.textContent =
        "Fix the dimensions to rebuild. Previous preview retained.";
    }
  } finally {
    busy = false;
    if (queued) {
      const next = queued;
      queued = null;
      build(next.params, next.version);
    }
  }
}
function schedule() {
  clearTimeout(timer);
  revision++;
  disable();
  queued = null;
  const version = revision;
  if (!form.checkValidity()) {
    status.textContent = "Enter dimensions between 0.5 and 200 mm.";
    return;
  }
  status.textContent = "Rebuilding…";
  const params = [...form.elements].map((e) => e.valueAsNumber);
  timer = setTimeout(() => build(params, version), 150);
}
form.addEventListener("input", schedule);
form.addEventListener("submit", (e) => e.preventDefault());
for (const button of buttons)
  button.onclick = async () => {
    const exportRevision = revision;
    button.disabled = true;
    try {
      const { bytes } = await request("export", { format: button.id });
      const url = URL.createObjectURL(
        new Blob([bytes], { type: "application/octet-stream" }),
      );
      const a = document.createElement("a");
      a.href = url;
      a.download = `box-with-hole.${button.id}`;
      a.click();
      setTimeout(() => URL.revokeObjectURL(url), 1000);
    } catch (e) {
      if (revision === exportRevision) status.textContent = e.message;
    } finally {
      if (revision === exportRevision) button.disabled = false;
    }
  };
schedule();
