import * as THREE from "three";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";
import "./style.css";
const viewport = document.querySelector("#viewport");
const renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setPixelRatio(Math.min(devicePixelRatio, 2));
viewport.append(renderer.domElement);
const scene = new THREE.Scene();
scene.background = new THREE.Color("#e8eee8");
const camera = new THREE.PerspectiveCamera(38, 1, 0.1, 5000);
camera.up.set(0, 0, 1);
camera.position.set(150, -240, 250);
const controls = new OrbitControls(camera, renderer.domElement);
controls.enableDamping = true;
scene.add(new THREE.HemisphereLight(0xffffff, 0x657f70, 3));
const light = new THREE.DirectionalLight(0xffffff, 3);
light.position.set(30, -50, 100);
scene.add(light);
const materials = [0x548f7c, 0xe4dfc5, 0xc68b57].map(
  (color) =>
    new THREE.MeshStandardMaterial({ color, roughness: 0.5, metalness: 0.08 }),
);
let meshes = [];
new ResizeObserver(() => {
  let w = viewport.clientWidth,
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
let id = 0;
const pending = new Map();
const request = (type, data = {}) =>
  new Promise((resolve, reject) => {
    const n = ++id;
    pending.set(n, { resolve, reject });
    worker.postMessage({ id: n, type, ...data });
  });
worker.onmessage = ({ data }) => {
  const p = pending.get(data.id);
  pending.delete(data.id);
  data.error ? p?.reject(new Error(data.error)) : p?.resolve(data);
};
worker.onerror = (e) => {
  pending.forEach((p) => p.reject(new Error(e.message)));
  pending.clear();
};
const form = document.querySelector("form"),
  select = document.querySelector("#part"),
  explode = document.querySelector("#explode"),
  status = document.querySelector("#status"),
  metrics = document.querySelector("#metrics");
const step = document.querySelector("#step"),
  stl = document.querySelector("#stl");
let revision = 0,
  builtRevision = -1,
  timer,
  busy = false,
  queued;
function exportState() {
  step.disabled = builtRevision !== revision;
  stl.disabled = step.disabled || select.value === "-1";
}
function display() {
  const chosen = Number(select.value);
  meshes.forEach((m, i) => {
    m.visible = chosen === -1 || chosen === i;
    m.position.set(0, 0, 0);
    if (explode.checked && chosen === -1) {
      if (i === 1) m.position.z = 25;
      if (i === 2) m.position.y = 30;
    }
  });
  exportState();
  document.querySelector("#export-note").textContent =
    chosen === -1
      ? "STEP includes all three solids. Select one part to export a printable STL."
      : "STL uses millimetres. Preview separation does not change exported coordinates.";
}
select.onchange = display;
explode.onchange = display;
async function build(params, version) {
  if (busy) {
    queued = { params, version };
    return;
  }
  busy = true;
  try {
    const data = await request("build-bilresa", { params });
    if (version !== revision) return;
    meshes.forEach((m) => {
      scene.remove(m);
      m.geometry.dispose();
    });
    meshes = data.parts.map((p, i) => {
      const g = new THREE.BufferGeometry();
      g.setAttribute("position", new THREE.BufferAttribute(p.positions, 3));
      g.setAttribute("normal", new THREE.BufferAttribute(p.normals, 3));
      g.setIndex(new THREE.BufferAttribute(p.indices, 1));
      g.computeBoundingBox();
      const m = new THREE.Mesh(g, materials[i]);
      scene.add(m);
      return m;
    });
    const box = meshes[0].geometry.boundingBox,
      center = box.getCenter(new THREE.Vector3()),
      size = box.getSize(new THREE.Vector3());
    const direction = camera.position.clone().sub(controls.target).normalize();
    controls.target.copy(center);
    camera.position
      .copy(center)
      .addScaledVector(direction, Math.max(size.length() * 1.65, 220));
    builtRevision = version;
    display();
    status.textContent = `Three parts rebuilt in ${data.milliseconds.toFixed(0)} ms`;
    metrics.textContent = `${size.x.toFixed(2)} × ${size.y.toFixed(2)} × ${size.z.toFixed(2)} mm · Build ${version}`;
  } catch (e) {
    if (version === revision) {
      status.textContent = e.message;
      metrics.textContent = "Previous preview retained.";
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
  queued = null;
  exportState();
  if (!form.checkValidity()) {
    status.textContent = "Use whole numbers from 0 to 8.";
    return;
  }
  status.textContent = "Rebuilding three parts…";
  const version = revision,
    params = [...form.elements].map((e) => e.valueAsNumber);
  timer = setTimeout(() => build(params, version), 150);
}
form.oninput = schedule;
form.onsubmit = (e) => e.preventDefault();
for (const button of [step, stl])
  button.onclick = async () => {
    const version = revision,
      part = Number(select.value),
      format = button.id;
    button.disabled = true;
    try {
      const { bytes } = await request("export-bilresa", { part, format });
      const url = URL.createObjectURL(
        new Blob([bytes], { type: "application/octet-stream" }),
      );
      const a = document.createElement("a");
      a.href = url;
      a.download = `bilresa-${["assembly", "body", "blanking-plate", "cover-top"][part + 1]}.${format}`;
      a.click();
      setTimeout(() => URL.revokeObjectURL(url), 1000);
    } catch (e) {
      if (version === revision) status.textContent = e.message;
    } finally {
      exportState();
    }
  };
schedule();
