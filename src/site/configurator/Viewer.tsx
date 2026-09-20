import { useEffect, useRef, useState } from "react";
import * as THREE from "three";
import { creasedNormals } from "./creased-normals.mjs";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";
import type { ModelDefinition } from "../models/types";
import { applySurfaceNormals } from "./surface-normals.mjs";
import { outlineStyle } from "./outline-style.mjs";
import { ModelArtwork } from "../components/ModelArtwork";
import { PartsPanel } from "./PartsPanel";
import type { Measurement, PartMesh } from "./protocol";
type Mode = "solid" | "outline";
type Angle = "face" | "top" | "side";
type Settings = {
  visible: boolean[];
  exploded: boolean;
  doorAngle: number;
  mode: Mode;
  dimensions: boolean;
  transparency: number;
  angle: Angle | null;
};
type BulbEnvelope = { radius: number; length: number; z: number };
type View = {
  envelope: (value?: BulbEnvelope) => void;
  update: (
    parts: PartMesh[],
    measurements: Measurement[],
    colors: readonly number[],
  ) => void;
  display: (settings: Settings) => void;
  reset: () => void;
  rotate: (x: number, y: number) => void;
};
export function Viewer({
  model,
  parts,
  visible,
  exploded,
  measurements,
  bulbEnvelope,
  setExploded,
  included,
  setVisible,
  setIncluded,
  exporting,
  generating = false,
  motion,
}: {
  model: ModelDefinition;
  parts: PartMesh[];
  visible: boolean[];
  exploded: boolean;
  measurements: Measurement[];
  bulbEnvelope?: BulbEnvelope;
  setExploded: (value: boolean) => void;
  included: boolean[];
  setVisible: (value: boolean[]) => void;
  setIncluded: (value: boolean[]) => void;
  exporting: boolean;
  generating?: boolean;
  motion?: { angle: number; limit: string; valid: boolean };
}) {
  const [doorOpen, setDoorOpen] = useState(false);
  useEffect(() => {
    setDoorOpen(false);
  }, [parts]);
  const host = useRef<HTMLDivElement>(null),
    overlay = useRef<SVGSVGElement>(null),
    view = useRef<View | null>(null);
  const [error, setError] = useState(""),
    [mode, setMode] = useState<Mode>(model.default_view ?? "outline"),
    [measurementPreference, setMeasurementPreference] = useState<
      boolean | null
    >(null),
    [transparency, setTransparency] = useState(outlineStyle.transparency),
    [angle, setAngle] = useState<Angle | null>(null);
  const [previewReady, setPreviewReady] = useState(false);
  const partsButton = useRef<HTMLButtonElement>(null);
  const [partsOpen, setPartsOpen] = useState(false);
  const [partsPosition, setPartsPosition] = useState({ top: 0, left: 0 });
  function positionParts() {
    const rect = partsButton.current?.getBoundingClientRect();
    if (rect)
      setPartsPosition({
        top: rect.bottom + 8,
        left: Math.max(16, Math.min(rect.right - 320, window.innerWidth - 336)),
      });
  }
  useEffect(() => {
    if (!partsOpen) return;
    window.addEventListener("resize", positionParts);
    window.addEventListener("scroll", positionParts, true);
    return () => {
      window.removeEventListener("resize", positionParts);
      window.removeEventListener("scroll", positionParts, true);
    };
  }, [partsOpen]);
  const [transparencyPosition, setTransparencyPosition] = useState({
    top: 0,
    left: 0,
  });
  const dimensions = measurementPreference ?? true;
  useEffect(() => {
    const node = host.current!,
      svg = overlay.current!;
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
    let bulb:
      THREE.Mesh<THREE.CylinderGeometry, THREE.MeshBasicMaterial> | undefined;
    function envelope(value?: BulbEnvelope) {
      if (bulb) {
        scene.remove(bulb);
        bulb.geometry.dispose();
        bulb.material.dispose();
        bulb = undefined;
      }
      if (!value) return;
      bulb = new THREE.Mesh(
        new THREE.CylinderGeometry(
          value.radius,
          value.radius,
          value.length,
          48,
          1,
          true,
        ),
        new THREE.MeshBasicMaterial({
          color: 0xe5af55,
          transparent: true,
          opacity: 0.3,
          depthWrite: false,
          side: THREE.DoubleSide,
          wireframe: true,
        }),
      );
      bulb.rotation.x = Math.PI / 2;
      bulb.position.z = value.z;
      scene.add(bulb);
    }
    scene.background = new THREE.Color(outlineStyle.background);
    const ortho = new THREE.OrthographicCamera(-150, 150, 100, -100, 0.1, 5000);
    ortho.up.set(0, 1, 0);
    const camera = ortho;
    let controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.minDistance = 30;
    controls.maxDistance = 3000;
    controls.minZoom = 0.2;
    controls.maxZoom = 10;
    scene.add(new THREE.HemisphereLight(0xffffff, 0x657f70, 3));
    const light = new THREE.DirectionalLight(0xffffff, 3);
    light.position.set(30, -50, 100);
    scene.add(light);
    const gridSource = new THREE.GridHelper(2000, 200, 0x3e7169, 0x2a564f);
    (gridSource.material as THREE.Material).dispose();
    const gridMaterial = new THREE.ShaderMaterial({
      vertexColors: true,
      transparent: true,
      depthWrite: false,
      toneMapped: false,
      uniforms: {
        radius: { value: new THREE.Vector2(180, 110) },
        opacity: { value: 1 },
      },
      vertexShader: `varying vec3 gridColor; varying vec2 gridPoint;
        void main(){gridColor=color;gridPoint=position.xz;gl_Position=projectionMatrix*modelViewMatrix*vec4(position,1.0);}`,
      fragmentShader: `uniform float opacity; uniform vec2 radius; varying vec3 gridColor; varying vec2 gridPoint;
        void main(){float fade=1.0-smoothstep(.4,1.0,length(gridPoint/radius));gl_FragColor=vec4(gridColor,fade*.8*opacity);
        #include <colorspace_fragment>
        }`,
    });
    const grid = new THREE.LineSegments(gridSource.geometry, gridMaterial);
    grid.renderOrder = -1;
    grid.rotation.x = Math.PI / 2;
    grid.position.z = -0.5;
    scene.add(grid);
    let objects: {
      mesh: THREE.Mesh<
        THREE.BufferGeometry,
        THREE.MeshStandardMaterial | THREE.MeshBasicMaterial
      >;
      silhouette: THREE.Mesh<THREE.BufferGeometry, THREE.ShaderMaterial>;
      solid: THREE.MeshStandardMaterial;
      flat: THREE.MeshBasicMaterial;
      hiddenEdge: THREE.LineSegments<
        THREE.BufferGeometry,
        THREE.LineBasicMaterial
      >;
      edge: THREE.LineSegments<THREE.BufferGeometry, THREE.LineBasicMaterial>;
    }[] = [];
    let data: PartMesh[] = [],
      modelMeasures: Measurement[] = [],
      initialized = false,
      fadeStart = 0,
      width = 1,
      height = 1;
    let settings: Settings = {
      visible: [],
      exploded: false,
      doorAngle: 0,
      mode: model.default_view ?? "outline",
      dimensions: false,
      transparency: 35,
      angle: null,
    };
    const center = new THREE.Vector3(),
      size = new THREE.Vector3(182, 90, 23);
    let annotations: {
      measurement: Measurement;
      group: SVGGElement;
      line: SVGPathElement;
      text: SVGTextElement;
    }[] = [];
    function bounds() {
      const box = new THREE.Box3();
      data.forEach((p, i) => {
        if (settings.visible[i]) {
          const b = p.bounds;
          box.union(
            new THREE.Box3(
              new THREE.Vector3(...(b.slice(0, 3) as [number, number, number])),
              new THREE.Vector3(...(b.slice(3, 6) as [number, number, number])),
            ),
          );
        }
      });
      return box;
    }
    function annotationBuild() {
      svg.replaceChildren();
      annotations = [];
      if (!settings.dimensions) return;
      const box = bounds();
      if (box.isEmpty()) return;
      const a = box.min,
        b = box.max,
        gap = 12;
      const measures: Measurement[] = [
        {
          label: "Width",
          approximate: true,
          value: b.x - a.x,
          from: [a.x, a.y - gap, b.z],
          to: [b.x, a.y - gap, b.z],
        },
        {
          label: model.id !== "bilresa" ? "Depth" : "Height",
          approximate: true,
          value: b.y - a.y,
          from: [b.x + gap, a.y, b.z],
          to: [b.x + gap, b.y, b.z],
        },
        {
          label: model.id !== "bilresa" ? "Height" : "Depth",
          approximate: true,
          value: b.z - a.z,
          from: [a.x - gap, a.y, a.z],
          to: [a.x - gap, a.y, b.z],
        },
      ];
      if (settings.angle === "top") {
        measures[0].from[2] += gap;
        measures[0].to[2] += gap;
      }
      if (settings.angle === "side") {
        measures[1].from[2] += gap;
        measures[1].to[2] += gap;
      }
      if (
        (!settings.angle || settings.angle === "face") &&
        settings.visible.filter(Boolean).length > 1 &&
        settings.visible[0]
      )
        measures.push(
          ...modelMeasures.map((m) => ({
            ...m,
            from: [
              m.from[0] +
                (m.from[0] === m.to[0]
                  ? (modelMeasures[0]?.value || 0) * 0.3
                  : 0),
              m.from[1] -
                (m.from[1] === m.to[1]
                  ? (modelMeasures[1]?.value || 0) * 0.3
                  : 0),
              b.z + 1,
            ] as [number, number, number],
            to: [
              m.to[0] +
                (m.from[0] === m.to[0]
                  ? (modelMeasures[0]?.value || 0) * 0.3
                  : 0),
              m.to[1] -
                (m.from[1] === m.to[1]
                  ? (modelMeasures[1]?.value || 0) * 0.3
                  : 0),
              b.z + 1,
            ] as [number, number, number],
          })),
        );
      for (const measurement of measures) {
        const ns = "http://www.w3.org/2000/svg",
          group = document.createElementNS(ns, "g"),
          line = document.createElementNS(ns, "path"),
          text = document.createElementNS(ns, "text");
        text.textContent = `${measurement.label} ${measurement.approximate ? "≈ " : ""}${Number(measurement.value.toFixed(2))} mm`;
        text.setAttribute("text-anchor", "middle");
        group.append(line, text);
        svg.append(group);
        annotations.push({ measurement, group, line, text });
      }
    }
    function resizeCamera() {
      const aspect = width / height;
      const extent = Math.max(size.x / aspect, size.y, size.z, 80) * 0.72;
      ortho.left = -extent * aspect;
      ortho.right = extent * aspect;
      ortho.top = extent;
      ortho.bottom = -extent;
      ortho.updateProjectionMatrix();
    }
    let cameraDistance = 1000;
    let fitting = false;
    let defaultFraming = true;
    const leaveDefaultFraming = () => {
      defaultFraming = false;
    };
    controls.addEventListener("start", leaveDefaultFraming);
    function fit() {
      fitting = true;
      const box = bounds();
      if (!box.isEmpty()) {
        box.getSize(size);
        box.getCenter(center);
      }
      cameraDistance = Math.max(1000, size.length() * 2);
      ortho.far = cameraDistance * 4;
      controls.maxDistance = cameraDistance * 3;
      controls.target.copy(center);
      resizeCamera();
      if (settings.angle) {
        ortho.zoom = 1;
        const direction =
          settings.angle === "face"
            ? model.id === "mini-rack-sideboard"
              ? new THREE.Vector3(0, -1, 0)
              : new THREE.Vector3(0, 0, 1)
            : settings.angle === "top"
              ? model.id === "mini-rack-sideboard"
                ? new THREE.Vector3(0, 0, 1)
                : new THREE.Vector3(0, 1, 0)
              : new THREE.Vector3(1, 0, 0);
        ortho.up.set(
          0,
          (
            model.id === "mini-rack-sideboard"
              ? settings.angle === "top"
              : settings.angle === "face"
          )
            ? 1
            : 0,
          (
            model.id === "mini-rack-sideboard"
              ? settings.angle === "top"
              : settings.angle === "face"
          )
            ? 0
            : 1,
        );
        ortho.position.copy(center).addScaledVector(direction, cameraDistance);
        ortho.updateProjectionMatrix();
      } else {
        const preset =
          model.camera?.[
            matchMedia("(max-width: 760px)").matches ? "mobile" : "desktop"
          ];
        ortho.zoom = preset?.zoom ?? 1;
        ortho.up.set(0, 0, 1);
        ortho.position
          .copy(center)
          .add(
            new THREE.Vector3(...(preset?.direction ?? outlineStyle.direction))
              .normalize()
              .multiplyScalar(cameraDistance),
          );
        ortho.lookAt(center);
        const pan = preset?.pan ?? [0, 0];
        const offset = new THREE.Vector3(1, 0, 0)
          .applyQuaternion(ortho.quaternion)
          .multiplyScalar((-pan[0] * (ortho.right - ortho.left)) / ortho.zoom)
          .add(
            new THREE.Vector3(0, 1, 0)
              .applyQuaternion(ortho.quaternion)
              .multiplyScalar(
                (pan[1] * (ortho.top - ortho.bottom)) / ortho.zoom,
              ),
          );
        controls.target.add(offset);
        ortho.position.add(offset);
        ortho.updateProjectionMatrix();
      }
      refreshOrbitBasis();
      fitting = false;
    }
    function clear() {
      for (const {
        mesh,
        edge,
        hiddenEdge,
        silhouette,
        solid,
        flat,
      } of objects) {
        scene.remove(mesh, edge, hiddenEdge, silhouette);
        mesh.geometry.dispose();
        solid.dispose();
        flat.dispose();
        silhouette.material.dispose();
        edge.geometry.dispose();
        edge.material.dispose();
        hiddenEdge.material.dispose();
      }
      objects = [];
    }
    let decorationFadeStart = 0;
    let gridPlane: Angle = "face";
    let gridFadeStart = 0;
    let explodeProgress = 0;
    let doorProgress = 0;
    let doorTransition: { start: number; from: number; to: number } | null =
      null;
    let explodeTransition: { start: number; from: number; to: number } | null =
      null;
    function positionExplosion(now: number) {
      if (doorTransition) {
        const t = reduced.matches
          ? 1
          : Math.min(1, (now - doorTransition.start) / 650);
        doorProgress = THREE.MathUtils.lerp(
          doorTransition.from,
          doorTransition.to,
          t * t * (3 - 2 * t),
        );
        if (t === 1) doorTransition = null;
      }
      if (explodeTransition) {
        const t = reduced.matches
          ? 1
          : Math.min(1, (now - explodeTransition.start) / 400);
        const ease = t * t * (3 - 2 * t);
        explodeProgress = THREE.MathUtils.lerp(
          explodeTransition.from,
          explodeTransition.to,
          ease,
        );
        if (t === 1) explodeTransition = null;
      }
      objects.forEach(({ mesh, edge, hiddenEdge, silhouette }, i) => {
        const part = data[i];
        const theta = (-(part.hand || 0) * doorProgress * Math.PI) / 180;
        const pivot = part.pivot || [0, 0];
        const offset = part.explode || [0, i === 2 ? 30 : 0, i === 1 ? 25 : 0];
        mesh.rotation.z = theta;
        mesh.position.set(
          pivot[0] -
            Math.cos(theta) * pivot[0] +
            Math.sin(theta) * pivot[1] +
            offset[0] * explodeProgress,
          pivot[1] -
            Math.sin(theta) * pivot[0] -
            Math.cos(theta) * pivot[1] +
            offset[1] * explodeProgress,
          offset[2] * explodeProgress,
        );
        for (const outline of [edge, hiddenEdge, silhouette]) {
          outline.position.copy(mesh.position);
          outline.rotation.copy(mesh.rotation);
        }
        mesh.updateMatrixWorld();
      });
    }
    let transition: {
      start: number;
      from: THREE.Quaternion;
      to: THREE.Quaternion;
      fromTarget: THREE.Vector3;
      toTarget: THREE.Vector3;
      fromZoom: number;
      toZoom: number;
    } | null = null;
    function display(next: Settings) {
      const changedAngle = settings.angle !== next.angle;
      if (settings.doorAngle !== next.doorAngle) {
        const now = performance.now();
        positionExplosion(now);
        doorTransition = { start: now, from: doorProgress, to: next.doorAngle };
      }
      if (settings.exploded !== next.exploded) {
        const now = performance.now();
        // Reverse from the current position, even between rendered frames.
        positionExplosion(now);
        explodeTransition = {
          start: now,
          from: explodeProgress,
          to: next.exploded ? 1 : 0,
        };
      }
      settings = next;
      controls.enableRotate = true;
      objects.forEach(
        ({ mesh, edge, hiddenEdge, silhouette, solid, flat }, i) => {
          mesh.visible = !!next.visible[i];
          edge.visible = !!next.visible[i] && next.mode !== "solid";
          if (edge.visible && !edge.geometry.getAttribute("position")) {
            edge.geometry.dispose();
            edge.geometry = new THREE.EdgesGeometry(
              mesh.geometry,
              outlineStyle.edgeAngle,
            );
            hiddenEdge.geometry = edge.geometry;
          }
          mesh.material = next.mode === "solid" ? solid : flat;
          flat.color.set(outlineStyle.background);
          edge.material.color.set(outlineStyle.line);
          hiddenEdge.visible = edge.visible;
          silhouette.visible = edge.visible;
          silhouette.material.uniforms.color.value.copy(edge.material.color);
        },
      );
      positionExplosion(performance.now());
      if (next.angle && changedAngle) {
        const from = ortho.quaternion.clone(),
          fromTarget = controls.target.clone(),
          fromZoom = ortho.zoom;
        fit();
        transition =
          changedAngle &&
          !matchMedia("(prefers-reduced-motion: reduce)").matches
            ? {
                start: performance.now(),
                from,
                to: ortho.quaternion.clone(),
                fromTarget,
                toTarget: controls.target.clone(),
                fromZoom,
                toZoom: ortho.zoom,
              }
            : null;
      }
      annotationBuild();
    }
    function onOrbitChange() {
      if (fitting || transition || !settings.angle) return;
      const direction = camera.position
        .clone()
        .sub(controls.target)
        .normalize();
      const expected =
        settings.angle === "face"
          ? model.id === "mini-rack-sideboard"
            ? new THREE.Vector3(0, -1, 0)
            : new THREE.Vector3(0, 0, 1)
          : settings.angle === "top"
            ? model.id === "mini-rack-sideboard"
              ? new THREE.Vector3(0, 0, 1)
              : new THREE.Vector3(0, 1, 0)
            : new THREE.Vector3(1, 0, 0);
      if (direction.dot(expected) < 0.99999) {
        settings.angle = null;
        setAngle(null);
      }
    }
    controls.addEventListener("change", onOrbitChange);
    function refreshOrbitBasis() {
      // OrbitControls caches camera.up when constructed. Rebuild after a view
      // transition so dragging uses the new screen plane rather than old poles.
      const target = controls.target.clone();
      controls.dispose();
      controls = new OrbitControls(camera, renderer.domElement);
      controls.target.copy(target);
      controls.enableDamping = true;
      controls.minDistance = 30;
      controls.maxDistance = 3000;
      controls.minZoom = 0.2;
      controls.maxZoom = 10;
      controls.update();
      controls.addEventListener("change", onOrbitChange);
      controls.addEventListener("start", leaveDefaultFraming);
    }
    const api: View = {
      envelope,
      update(next, measures, colors) {
        clear();
        data = next;
        modelMeasures = measures;
        objects = data.map((p, i) => {
          let geometry = new THREE.BufferGeometry();
          geometry.setAttribute(
            "position",
            new THREE.BufferAttribute(p.positions, 3),
          );
          geometry.setAttribute(
            "normal",
            new THREE.BufferAttribute(p.normals, 3),
          );
          geometry.setIndex(new THREE.BufferAttribute(p.indices, 1));
          if (p.surfaceNormals)
            geometry.setAttribute(
              "surfaceNormal",
              new THREE.BufferAttribute(p.surfaceNormals, 3),
            );
          if (model.id === "lampshade") {
            const indexed = geometry;
            geometry = creasedNormals(
              indexed,
              p.rounded ? Math.PI * 0.47 : Math.PI / 4,
            );
            indexed.dispose();
            applySurfaceNormals(geometry);
          }
          const mesh = new THREE.Mesh<
            THREE.BufferGeometry,
            THREE.MeshStandardMaterial | THREE.MeshBasicMaterial
          >(
            geometry,
            new THREE.MeshStandardMaterial({
              color: colors[i],
              roughness: 0.65,
              metalness: 0.04,
              polygonOffset: true,
              polygonOffsetFactor: 1,
              polygonOffsetUnits: 1,
            }),
          );
          const edge = new THREE.LineSegments(
            new THREE.BufferGeometry(),
            new THREE.LineBasicMaterial({ color: 0x254f40 }),
          );
          const silhouette = new THREE.Mesh(
            geometry,
            new THREE.ShaderMaterial({
              uniforms: {
                color: { value: new THREE.Color(0x254f40) },
                opacity: { value: 1 },
                thickness: { value: 0.0015 },
              },
              vertexShader: `uniform float thickness;
              void main(){vec4 clip=projectionMatrix*modelViewMatrix*vec4(position,1.0);vec4 inner=projectionMatrix*modelViewMatrix*vec4(position-normal,1.0);gl_Position=clip+normalize(clip-inner)*thickness*clip.w;}`,
              fragmentShader: `uniform vec3 color;uniform float opacity;
              void main(){gl_FragColor=vec4(color,opacity);
              #include <colorspace_fragment>
              }`,
              side: THREE.BackSide,
              toneMapped: false,
            }),
          );
          const hiddenEdge = new THREE.LineSegments(
            edge.geometry,
            new THREE.LineBasicMaterial({
              color: 0xc3e3cc,
              transparent: true,
              depthTest: false,
              depthWrite: false,
              opacity: 0,
              toneMapped: false,
            }),
          );
          mesh.renderOrder = 0;
          edge.renderOrder = 1;
          silhouette.renderOrder = 1;
          hiddenEdge.renderOrder = 2;
          scene.add(mesh, edge, hiddenEdge, silhouette);
          const solid = mesh.material as THREE.MeshStandardMaterial;
          const flat = new THREE.MeshBasicMaterial({
            color: 0xe6ede6,
            polygonOffset: true,
            polygonOffsetFactor: 1,
            polygonOffsetUnits: 1,
            toneMapped: false,
          });
          return { mesh, edge, hiddenEdge, silhouette, solid, flat };
        });
        display(settings);
        if (!initialized) {
          fit();
          initialized = true;
        }
        fadeStart = performance.now();
        setPreviewReady(true);
      },
      display,
      reset() {
        defaultFraming = true;
        transition = null;
        settings.angle = null;
        setAngle(null);
        fit();
        annotationBuild();
      },
      rotate(x, y) {
        defaultFraming = false;
        settings.angle = null;
        setAngle(null);
        const offset = camera.position.clone().sub(controls.target);
        offset.applyAxisAngle(new THREE.Vector3(0, 0, 1), x);
        if (y) offset.applyAxisAngle(new THREE.Vector3(1, 0, 0), y);
        camera.position.copy(controls.target).add(offset);
        controls.update();
      },
    };
    view.current = api;
    const mobile = matchMedia("(max-width: 760px)");
    const viewer = node.closest<HTMLElement>(".viewer")!;
    const title = node
      .closest(".model-page")
      ?.querySelector<HTMLElement>(".model-title");
    const stage = node.parentElement!;
    const toolbar = viewer.querySelector<HTMLElement>(".viewer-toolbar")!;
    const topGroups = [toolbar];
    const regions = title ? [title, ...topGroups] : topGroups;
    let extension = -1;
    const projected = new THREE.Vector3();
    function updateOverlap() {
      const boundary = mobile.matches && title ? title : viewer;
      const nextExtension = Math.max(
        0,
        stage.getBoundingClientRect().top -
          boundary.getBoundingClientRect().top,
      );
      if (Math.abs(nextExtension - extension) > 0.5) {
        extension = nextExtension;
        viewer.style.setProperty("--canvas-extension", `${extension}px`);
      }
      const toolbarHeight =
        toolbar.getBoundingClientRect().bottom -
        viewer.getBoundingClientRect().top;
      const backdropHeight = `${Math.max(0, toolbarHeight)}px`;
      if (
        viewer.style.getPropertyValue("--toolbar-backdrop-height") !==
        backdropHeight
      )
        viewer.style.setProperty("--toolbar-backdrop-height", backdropHeight);
      const canvasRect = node.getBoundingClientRect();
      const boxes = data.flatMap((part, index) => {
        if (!settings.visible[index] || !objects[index]) return [];
        let left = Infinity,
          top = Infinity,
          right = -Infinity,
          bottom = -Infinity;
        for (let corner = 0; corner < 8; corner++) {
          projected.set(
            part.bounds[corner & 1 ? 3 : 0],
            part.bounds[corner & 2 ? 4 : 1],
            part.bounds[corner & 4 ? 5 : 2],
          );
          projected
            .applyMatrix4(objects[index].mesh.matrixWorld)
            .project(camera);
          if (
            !Number.isFinite(projected.x + projected.y + projected.z) ||
            Math.abs(projected.z) > 1
          )
            continue;
          const x =
            canvasRect.left + ((projected.x + 1) * canvasRect.width) / 2;
          const y =
            canvasRect.top + ((1 - projected.y) * canvasRect.height) / 2;
          left = Math.min(left, x);
          right = Math.max(right, x);
          top = Math.min(top, y);
          bottom = Math.max(bottom, y);
        }
        return [{ left, right, top, bottom }];
      });
      regions.forEach((region) => {
        if (
          (!mobile.matches && region === title) ||
          (mobile.matches && region.classList.contains("viewer-tools"))
        ) {
          region.removeAttribute("data-model-under");
          return;
        }
        const rect = region.getBoundingClientRect();
        // A small exit margin prevents flicker when an edge grazes a control.
        const margin = region.hasAttribute("data-model-under") ? 8 : 0;
        const overlaps = boxes.some(
          (box) =>
            box.right > rect.left - margin &&
            box.left < rect.right + margin &&
            box.bottom > rect.top - margin &&
            box.top < rect.bottom + margin,
        );
        if (overlaps !== region.hasAttribute("data-model-under"))
          region.toggleAttribute("data-model-under", overlaps);
      });
    }
    const resize = new ResizeObserver(() => {
      ({ width, height } = node.getBoundingClientRect());
      width = Math.max(width, 1);
      height = Math.max(height, 1);
      renderer.setSize(width, height);
      svg.setAttribute("viewBox", `0 0 ${width} ${height}`);
      if (initialized && defaultFraming && !settings.angle) fit();
      else resizeCamera();
    });
    resize.observe(node);
    const lost = (e: Event) => {
      e.preventDefault();
      setError(
        "The preview lost its graphics connection. Reload to restore it.",
      );
    };
    renderer.domElement.addEventListener("webglcontextlost", lost);
    const reduced = matchMedia("(prefers-reduced-motion: reduce)");
    renderer.setAnimationLoop(() => {
      positionExplosion(performance.now());
      controls.enabled = !transition;
      if (transition) {
        const t = reduced.matches
          ? 1
          : Math.min(1, (performance.now() - transition.start) / 350);
        const ease = t * t * (3 - 2 * t);
        ortho.quaternion.slerpQuaternions(transition.from, transition.to, ease);
        controls.target.lerpVectors(
          transition.fromTarget,
          transition.toTarget,
          ease,
        );
        ortho.position
          .set(0, 0, cameraDistance)
          .applyQuaternion(ortho.quaternion)
          .add(controls.target);
        ortho.up.set(0, 1, 0).applyQuaternion(ortho.quaternion);
        ortho.zoom = THREE.MathUtils.lerp(
          transition.fromZoom,
          transition.toZoom,
          ease,
        );
        ortho.updateProjectionMatrix();
        if (t === 1) {
          refreshOrbitBasis();
          transition = null;
          decorationFadeStart = performance.now();
        }
      } else controls.update();
      const direction = camera.position
        .clone()
        .sub(controls.target)
        .normalize();
      const cutoff = gridPlane === "face" ? 0.99999 : 0.99995;
      const plane: Angle =
        Math.abs(direction.y) > cutoff
          ? "top"
          : Math.abs(direction.x) > cutoff
            ? "side"
            : "face";
      if (plane !== gridPlane) {
        gridPlane = plane;
        gridFadeStart = performance.now();
      }
      const gridBounds = bounds();
      if (!gridBounds.isEmpty()) {
        const midpoint = gridBounds.getCenter(new THREE.Vector3()),
          span = gridBounds.getSize(new THREE.Vector3());
        grid.rotation.set(Math.PI / 2, 0, 0);
        grid.position.set(midpoint.x, midpoint.y, gridBounds.min.z - 0.5);
        const radius = new THREE.Vector2(span.x, span.y);
        if (gridPlane === "top") {
          grid.rotation.set(0, 0, 0);
          grid.position.set(midpoint.x, gridBounds.min.y - 0.5, midpoint.z);
          radius.set(span.x, span.z);
        }
        if (gridPlane === "side") {
          grid.rotation.set(0, 0, Math.PI / 2);
          grid.position.set(gridBounds.min.x - 0.5, midpoint.y, midpoint.z);
          radius.set(span.y, span.z);
        }
        gridMaterial.uniforms.radius.value.copy(
          radius.multiplyScalar(0.85).addScalar(30),
        );
      }
      grid.visible = !gridBounds.isEmpty() && !transition;
      const decorationAlpha = transition
        ? 0
        : reduced.matches
          ? 1
          : Math.min(1, (performance.now() - decorationFadeStart) / 140);
      gridMaterial.uniforms.opacity.value =
        decorationAlpha *
        (reduced.matches
          ? 1
          : Math.min(1, (performance.now() - gridFadeStart) / 140));
      const alpha = reduced.matches
        ? 1
        : Math.min(1, (performance.now() - fadeStart) / 200);
      for (const { mesh, edge, hiddenEdge, silhouette } of objects) {
        const through =
          settings.mode === "solid" ? 0 : settings.transparency / 100;
        mesh.material.transparent = alpha < 1 || settings.mode !== "solid";
        mesh.material.opacity = alpha * (1 - through);
        hiddenEdge.material.opacity = alpha * through;
        edge.material.transparent = alpha < 1 || settings.mode !== "solid";
        edge.material.opacity = alpha;
        silhouette.material.uniforms.opacity.value = alpha;
        silhouette.material.transparent =
          alpha < 1 || settings.mode !== "solid";
      }
      renderer.render(scene, camera);
      updateOverlap();
      for (const { measurement: m, group, line, text } of annotations) {
        const a = new THREE.Vector3(...m.from).project(camera),
          b = new THREE.Vector3(...m.to).project(camera);
        const x1 = ((a.x + 1) * width) / 2,
          y1 = ((1 - a.y) * height) / 2,
          x2 = ((b.x + 1) * width) / 2,
          y2 = ((1 - b.y) * height) / 2;
        const length = Math.hypot(x2 - x1, y2 - y1);
        group.style.opacity = String(decorationAlpha);
        const hidden =
          !!transition ||
          ![x1, y1, x2, y2, a.z, b.z, length].every(Number.isFinite) ||
          length < 25 ||
          Math.abs(a.z) > 1 ||
          Math.abs(b.z) > 1;
        group.style.display = hidden ? "none" : "";
        if (hidden) continue;
        const nx = (-(y2 - y1) / length) * 4,
          ny = ((x2 - x1) / length) * 4;
        line.setAttribute(
          "d",
          `M${x1} ${y1}L${x2} ${y2}M${x1 - nx} ${y1 - ny}L${x1 + nx} ${y1 + ny}M${x2 - nx} ${y2 - ny}L${x2 + nx} ${y2 + ny}`,
        );
        const tx = (x1 + x2) / 2,
          ty = (y1 + y2) / 2;
        let degrees = (Math.atan2(y2 - y1, x2 - x1) * 180) / Math.PI;
        if (degrees > 90) degrees -= 180;
        if (degrees < -90) degrees += 180;
        text.setAttribute("x", String(tx));
        text.setAttribute("y", String(ty - 8));
        text.setAttribute("transform", `rotate(${degrees} ${tx} ${ty})`);
      }
    });
    return () => {
      view.current = null;
      resize.disconnect();
      viewer.style.removeProperty("--canvas-extension");
      viewer.style.removeProperty("--toolbar-backdrop-height");
      regions.forEach((region) => region.removeAttribute("data-model-under"));
      renderer.setAnimationLoop(null);
      controls.dispose();
      envelope();
      clear();
      grid.geometry.dispose();
      (grid.material as THREE.Material).dispose();
      svg.replaceChildren();
      renderer.domElement.removeEventListener("webglcontextlost", lost);
      renderer.dispose();
      renderer.forceContextLoss();
      renderer.domElement.remove();
    };
  }, []);
  useEffect(() => {
    view.current?.display({
      visible,
      exploded,
      doorAngle: doorOpen && !exploded && !generating ? motion?.angle || 0 : 0,
      mode,
      dimensions,
      angle,
      transparency,
    });
  }, [
    visible,
    exploded,
    mode,
    dimensions,
    angle,
    transparency,
    doorOpen,
    motion?.angle,
    generating,
  ]);
  useEffect(() => {
    if (parts.length)
      view.current?.update(
        parts,
        measurements,
        model.parts.map((p) => p.color),
      );
  }, [parts, measurements, model]);
  useEffect(() => {
    view.current?.envelope(
      !generating && visible[0] ? bulbEnvelope : undefined,
    );
  }, [
    bulbEnvelope?.radius,
    bulbEnvelope?.length,
    bulbEnvelope?.z,
    generating,
    visible[0],
  ]);
  const shown = visible.filter(Boolean).length;
  return (
    <section
      className={`viewer viewer-${mode}`}
      style={{ viewTransitionName: `model-${model.id}` }}
      aria-label="Interactive 3D preview"
    >
      <div className="viewer-toolbar-backdrop" aria-hidden="true" />
      <div className="viewer-toolbar">
        <div className="view-modes" role="group" aria-label="Preview style">
          {(["outline", "solid"] as Mode[]).map((v) => (
            <button
              key={v}
              aria-pressed={mode === v}
              onClick={() => {
                setMode(v);
              }}
            >
              {v[0].toUpperCase() + v.slice(1)}
            </button>
          ))}
        </div>

        <div
          className="orientation-controls"
          role="group"
          aria-label="View orientation"
        >
          {(["face", "top", "side"] as Angle[]).map((v) => (
            <button
              key={v}
              aria-pressed={angle === v}
              onClick={() => setAngle(v)}
            >
              {v === "face" ? "Front" : v === "top" ? "Top" : "Side"}
            </button>
          ))}
        </div>

        <div className="viewer-tools">
          {motion && (
            <button
              aria-label={doorOpen ? "Close doors" : "Open doors"}
              aria-pressed={doorOpen}
              disabled={generating || !motion.valid || motion.angle <= 0}
              title={`${motion.angle.toFixed(1)}° — ${motion.limit}`}
              onClick={() => {
                setExploded(false);
                setDoorOpen(!doorOpen);
              }}
            >
              <span>{doorOpen ? "Close" : "Open"}</span>
            </button>
          )}
          <button
            className="measurements-button"
            aria-label="Measurements"
            title="Measurements"
            aria-pressed={dimensions}
            onClick={() => setMeasurementPreference(!dimensions)}
          >
            <svg className="tool-icon" viewBox="0 0 24 24" aria-hidden="true">
              <path d="M3 7h18v10H3zM7 7v5m5-5v3m5-3v5" />
            </svg>
            <span className="tool-label">Measurements</span>
          </button>
          <span
            className="explode-control"
            title="Separate parts in the preview"
          >
            <button
              aria-label="Explode"
              title="Explode"
              aria-pressed={exploded}
              onClick={() => {
                setDoorOpen(false);
                setExploded(!exploded);
              }}
            >
              <svg className="tool-icon" viewBox="0 0 24 24" aria-hidden="true">
                <path d="m12 2 2.2 6 6-3-2.4 6 4.2 3-6.4 1 .8 7-4.4-4.8L7 22l.5-7-6-1L6 10 3 5l6 3Z" />
              </svg>
              <span className="tool-label">Explode</span>
            </button>
          </span>
          <button
            className="parts-tool-button"
            aria-label="Parts"
            title="Parts"
            ref={partsButton}
            popoverTarget="viewer-parts"
            aria-expanded={partsOpen}
            onClick={positionParts}
          >
            <svg className="tool-icon" viewBox="0 0 24 24" aria-hidden="true">
              <path d="m12 3 10 5-10 5L2 8Zm-10 9 10 5 10-5M2 16l10 5 10-5" />
            </svg>
            <span className="tool-label">Parts</span>
          </button>
          <button
            className="transparency-button"
            aria-label="Transparency"
            title="Transparency"
            disabled={mode === "solid"}
            popoverTarget="transparency-popover"
            onClick={(event) => {
              const rect = event.currentTarget.getBoundingClientRect();
              setTransparencyPosition({
                top: rect.bottom + 8,
                left: Math.max(
                  16,
                  Math.min(rect.right - 280, window.innerWidth - 296),
                ),
              });
            }}
          >
            <svg className="tool-icon" viewBox="0 0 24 24" aria-hidden="true">
              <circle cx="12" cy="12" r="9" />
              <path d="M12 3v18M12 6l5 5m-5-1 8 8m-8-3 5 5" />
            </svg>
            <span className="tool-label">Transparency</span>
          </button>
          <button
            className="fit-view-button"
            onClick={() => view.current?.reset()}
            title="Reset view"
            aria-label="Reset view"
          >
            <svg className="tool-icon" viewBox="0 0 24 24" aria-hidden="true">
              <path d="M3 10a9 9 0 1 1 2 8M3 4v6h6" />
            </svg>
            <span>Reset view</span>
          </button>
        </div>
      </div>
      <div
        id="transparency-popover"
        popover="auto"
        className="transparency-popover"
        style={transparencyPosition}
      >
        <label className="transparency-control">
          <span>
            Transparency <output>{transparency}%</output>
          </span>
          <input
            aria-label="Object transparency"
            type="range"
            min="0"
            max="100"
            step="5"
            value={transparency}
            onChange={(e) => setTransparency(Number(e.target.value))}
          />
        </label>
      </div>
      <div
        id="viewer-parts"
        className="viewer-parts-popover"
        popover="auto"
        style={partsPosition}
        onToggle={(event) => setPartsOpen(event.newState === "open")}
      >
        <div className="parts-popover-heading">
          <strong>Model parts</strong>
          <button
            className="icon-button"
            aria-label="Close parts"
            popoverTarget="viewer-parts"
            popoverTargetAction="hide"
          >
            ×
          </button>
        </div>
        <PartsPanel
          model={model}
          visible={visible}
          included={included}
          setVisible={setVisible}
          setIncluded={setIncluded}
          disabled={exporting}
        />
      </div>
      <div
        className="viewer-stage"
        data-generating={generating || undefined}
        aria-busy={generating}
      >
        <div
          className={`viewer-loading-art${previewReady ? " is-ready" : ""}`}
          aria-hidden="true"
        >
          <ModelArtwork model={model} />
        </div>
        <div
          ref={host}
          className="viewer-canvas"
          tabIndex={generating ? -1 : 0}
          aria-hidden={generating || undefined}
          role="group"
          aria-label="3D model. Arrow keys rotate; Home resets the view."
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
                  e.key === "ArrowUp"
                    ? 0.15
                    : e.key === "ArrowDown"
                      ? -0.15
                      : 0,
                );
            }
          }}
        />
        <svg
          ref={overlay}
          className="measurement-overlay"
          aria-label="Model dimensions"
        />
        {!generating && !shown && (
          <p className="viewer-empty">
            All parts are hidden. Use the eyes in the parts list to show them.
          </p>
        )}
        {generating && (
          <div className="viewer-generating" role="status" aria-live="polite">
            <span className="viewer-spinner" aria-hidden="true" />
            <span>Generating your model…</span>
          </div>
        )}
      </div>
      {error && (
        <p className="viewer-error" role="alert">
          {error}
        </p>
      )}
    </section>
  );
}
