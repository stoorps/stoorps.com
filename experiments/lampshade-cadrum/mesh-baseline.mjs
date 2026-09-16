import { readFile, writeFile } from "node:fs/promises";
import Module from "manifold-3d";
import {
  connectedCells,
  latticeLayout,
} from "../../models/lampshade/cells.mjs";
import { profile } from "../../models/lampshade/reference.mjs";
import { BufferGeometry, BufferAttribute } from "three";
import {
  toCreasedNormals,
  mergeVertices,
} from "three/addons/utils/BufferGeometryUtils.js";
import { applySurfaceNormals } from "../../src/site/configurator/surface-normals.mjs";
const p = JSON.parse(
  await readFile("artifacts/lampshade-comparison/input.json", "utf8"),
);
const wasm = await Module();
wasm.setup();
const start = performance.now();
const radius = (a, z) =>
  profile(p, z / p.height) +
  p.ripple_depth *
    Math.sin(p.lobes * (a - (((p.twist * Math.PI) / 180) * z) / p.height));
const raw = connectedCells(p, latticeLayout(p), radius);
const solid = new wasm.Manifold(
  new wasm.Mesh({
    numProp: 3,
    vertProperties: new Float32Array(raw.vertices.flat()),
    triVerts: new Uint32Array(raw.faces.flat()),
  }),
);
const mesh = solid.getMesh();
const generationSeconds = (performance.now() - start) / 1000;
const geometry = new BufferGeometry();
geometry.setAttribute("position", new BufferAttribute(mesh.vertProperties, 3));
geometry.setIndex(new BufferAttribute(mesh.triVerts, 1));
const smooth = new Float32Array(mesh.vertProperties.length);
for (let i = 0; i < smooth.length; i += 3) {
  const [x, y, z] = mesh.vertProperties.slice(i, i + 3),
    a = Math.atan2(y, x),
    r = Math.hypot(x, y),
    offset = r - radius(a, z);
  if (Math.abs(Math.abs(offset) - p.thickness / 2) > 0.0001) continue;
  const da = (p.ripple_depth * p.lobes * Math.cos(p.lobes * a)) / r,
    dz = (radius(a, z + 0.0001) - radius(a, z - 0.0001)) / 0.0002;
  const n = [
      Math.cos(a) + da * Math.sin(a),
      Math.sin(a) - da * Math.cos(a),
      -dz,
    ],
    l = Math.hypot(...n);
  for (let k = 0; k < 3; k++) smooth[i + k] = (Math.sign(offset) * n[k]) / l;
}
geometry.setAttribute("surfaceNormal", new BufferAttribute(smooth, 3));
const final = toCreasedNormals(geometry, Math.PI / 4);
applySurfaceNormals(final);
const compact = mergeVertices(final, 1e-5);
const rounded = (a) => Array.from(a, (x) => +x.toFixed(5));
await writeFile(
  "artifacts/lampshade-comparison/manifold.json",
  JSON.stringify({
    generationSeconds,
    volume: solid.volume(),
    positions: rounded(compact.attributes.position.array),
    normals: rounded(compact.attributes.normal.array),
    indices: [...compact.index.array],
    rows: p.rows,
    totalRows: p.rows,
  }),
);
console.log({
  generationSeconds,
  triangles: mesh.triVerts.length / 3,
  volume: solid.volume(),
});
solid.delete();
