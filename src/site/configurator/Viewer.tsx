import { useEffect, useRef, useState } from "react";
import * as THREE from "three";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";
import type { ModelDefinition } from "../models/types";
import type { PartMesh } from "./protocol";
type View = {
  update: (parts: PartMesh[], colors: readonly number[]) => void;
  display: (part: number, exploded: boolean) => void;
  reset: () => void;
  rotate: (x: number, y: number) => void;
};
export function Viewer({
  model,
  parts,
  selected,
  exploded,
}: {
  model: ModelDefinition;
  parts: PartMesh[];
  selected: number;
  exploded: boolean;
}) {
  const host = useRef<HTMLDivElement>(null),
    view = useRef<View | null>(null);
  const [error, setError] = useState("");
  useEffect(() => {
    const node = host.current!;
    let renderer: THREE.WebGLRenderer;
    try {
      renderer = new THREE.WebGLRenderer({ antialias: true });
    } catch {
      setError(
        "This browser cannot display the 3D preview. You can still configure and download the parts.",
      );
      return;
    }
    renderer.setPixelRatio(Math.min(devicePixelRatio, 2));
    node.append(renderer.domElement);
    renderer.domElement.setAttribute("aria-hidden", "true");
    const scene = new THREE.Scene();
    scene.background = new THREE.Color("#e6ede6");
    const camera = new THREE.PerspectiveCamera(38, 1, 0.1, 5000);
    camera.up.set(0, 0, 1);
    const controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.minDistance = 30;
    controls.maxDistance = 3000;
    scene.add(new THREE.HemisphereLight(0xffffff, 0x657f70, 3));
    const light = new THREE.DirectionalLight(0xffffff, 3);
    light.position.set(30, -50, 100);
    scene.add(light);
    let meshes: THREE.Mesh<THREE.BufferGeometry, THREE.MeshStandardMaterial>[] =
      [];
    let size = new THREE.Vector3(182, 90, 23),
      center = new THREE.Vector3();
    function clear() {
      meshes.forEach((m) => {
        scene.remove(m);
        m.geometry.dispose();
        m.material.dispose();
      });
      meshes = [];
    }
    function reset() {
      controls.target.copy(center);
      camera.position
        .copy(center)
        .add(
          new THREE.Vector3(150, -240, 250)
            .normalize()
            .multiplyScalar(Math.max(size.length() * 1.65, 220)),
        );
      controls.update();
    }
    const api: View = {
      update(data, colors) {
        clear();
        meshes = data.map((p, i) => {
          const geometry = new THREE.BufferGeometry();
          geometry.setAttribute(
            "position",
            new THREE.BufferAttribute(p.positions, 3),
          );
          geometry.setAttribute(
            "normal",
            new THREE.BufferAttribute(p.normals, 3),
          );
          geometry.setIndex(new THREE.BufferAttribute(p.indices, 1));
          geometry.computeBoundingBox();
          const mesh = new THREE.Mesh(
            geometry,
            new THREE.MeshStandardMaterial({
              color: colors[i],
              roughness: 0.5,
              metalness: 0.08,
            }),
          );
          scene.add(mesh);
          return mesh;
        });
        const box = meshes[0]?.geometry.boundingBox;
        if (box) {
          box.getSize(size);
          box.getCenter(center);
        }
        reset();
      },
      display(selectedPart, separate) {
        meshes.forEach((m, i) => {
          m.visible = selectedPart === -1 || selectedPart === i;
          m.position.set(0, 0, 0);
          if (separate && selectedPart === -1) {
            if (i === 1) m.position.z = 25;
            if (i === 2) m.position.y = 30;
          }
        });
      },
      reset,
      rotate(x, y) {
        const offset = camera.position.clone().sub(controls.target);
        offset.applyAxisAngle(new THREE.Vector3(0, 0, 1), x);
        if (y) offset.applyAxisAngle(new THREE.Vector3(1, 0, 0), y);
        camera.position.copy(controls.target).add(offset);
        controls.update();
      },
    };
    view.current = api;
    reset();
    const resize = new ResizeObserver(() => {
      const { width, height } = node.getBoundingClientRect();
      renderer.setSize(width, height);
      camera.aspect = width / Math.max(height, 1);
      camera.updateProjectionMatrix();
    });
    resize.observe(node);
    const onLost = (e: Event) => {
      e.preventDefault();
      setError(
        "The 3D preview lost its graphics connection. Reload the page to restore it.",
      );
    };
    renderer.domElement.addEventListener("webglcontextlost", onLost);
    renderer.setAnimationLoop(() => {
      controls.update();
      renderer.render(scene, camera);
    });
    return () => {
      view.current = null;
      resize.disconnect();
      renderer.setAnimationLoop(null);
      controls.dispose();
      clear();
      renderer.domElement.removeEventListener("webglcontextlost", onLost);
      renderer.dispose();
      renderer.forceContextLoss();
      renderer.domElement.remove();
    };
  }, []);
  useEffect(() => {
    if (parts.length)
      view.current?.update(
        parts,
        model.parts.map((p) => p.color),
      );
    view.current?.display(selected, exploded);
  }, [parts, model]);
  useEffect(() => {
    view.current?.display(selected, exploded);
  }, [selected, exploded]);
  return (
    <section className="viewer" aria-label="Interactive 3D preview">
      <div className="viewer-top">
        <span className="eyebrow">
          {selected === -1 ? "Three-part assembly" : model.parts[selected].name}
        </span>
        <span>Drag to orbit · Scroll to zoom</span>
      </div>
      <div
        ref={host}
        className="viewer-canvas"
        tabIndex={0}
        role="group"
        aria-label="3D model. Use arrow keys to rotate; press Home to reset."
        onKeyDown={(e) => {
          if (
            [
              "ArrowLeft",
              "ArrowRight",
              "ArrowUp",
              "ArrowDown",
              "Home",
            ].includes(e.key)
          ) {
            e.preventDefault();
            if (e.key === "Home") view.current?.reset();
            else
              view.current?.rotate(
                e.key === "ArrowLeft"
                  ? 0.15
                  : e.key === "ArrowRight"
                    ? -0.15
                    : 0,
                e.key === "ArrowUp" ? 0.15 : e.key === "ArrowDown" ? -0.15 : 0,
              );
          }
        }}
      />
      {error && (
        <p className="viewer-error" role="alert">
          {error}
        </p>
      )}
      <div className="viewer-bottom">
        <span>Preview separation never changes your downloads.</span>
        <button className="quiet-button" onClick={() => view.current?.reset()}>
          Reset view
        </button>
      </div>
    </section>
  );
}
