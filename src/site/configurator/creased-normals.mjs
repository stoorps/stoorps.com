import { BufferAttribute, BufferGeometry } from "three";

// Use the mesh's actual connectivity instead of hashing millions of positions.
// Keep shared vertices, splitting only where a crease needs different normals.
export function creasedNormals(source, angle = Math.PI / 4) {
  const positions = source.getAttribute("position");
  const indices = source.index.array;
  const count = positions.count;
  const heads = new Int32Array(count).fill(-1);
  const next = new Int32Array(indices.length);
  const faces = new Float32Array(indices.length);
  for (let i = 0; i < indices.length; i += 3) {
    const a = indices[i],
      b = indices[i + 1],
      c = indices[i + 2];
    const ux = positions.getX(b) - positions.getX(a),
      uy = positions.getY(b) - positions.getY(a),
      uz = positions.getZ(b) - positions.getZ(a);
    const vx = positions.getX(c) - positions.getX(a),
      vy = positions.getY(c) - positions.getY(a),
      vz = positions.getZ(c) - positions.getZ(a);
    const x = uy * vz - uz * vy,
      y = uz * vx - ux * vz,
      z = ux * vy - uy * vx;
    const length = Math.hypot(x, y, z) || 1;
    faces.set([x / length, y / length, z / length], i);
    for (let j = i; j < i + 3; j++) {
      next[j] = heads[indices[j]];
      heads[indices[j]] = j;
    }
  }
  const output = new BufferGeometry();
  const xyz = [],
    normals = [],
    surface = [];
  const analytic = source.getAttribute("surfaceNormal");
  const result = new Uint32Array(indices.length);
  const threshold = Math.cos(angle);
  for (let vertex = 0; vertex < count; vertex++) {
    const corners = [];
    for (let c = heads[vertex]; c !== -1; c = next[c]) corners.push(c);
    const groups = new Map();
    for (const corner of corners) {
      const f = corner - (corner % 3);
      let x = 0,
        y = 0,
        z = 0;
      const included = [];
      for (let k = 0; k < corners.length; k++) {
        const other = corners[k],
          g = other - (other % 3);
        if (
          faces[f] * faces[g] +
            faces[f + 1] * faces[g + 1] +
            faces[f + 2] * faces[g + 2] >
          threshold
        ) {
          included.push(k);
          x += faces[g];
          y += faces[g + 1];
          z += faces[g + 2];
        }
      }
      const key = included.join(",");
      let id = groups.get(key);
      if (id === undefined) {
        id = xyz.length / 3;
        groups.set(key, id);
        xyz.push(
          positions.getX(vertex),
          positions.getY(vertex),
          positions.getZ(vertex),
        );
        const length = Math.hypot(x, y, z) || 1;
        normals.push(x / length, y / length, z / length);
        if (analytic)
          surface.push(
            analytic.getX(vertex),
            analytic.getY(vertex),
            analytic.getZ(vertex),
          );
      }
      result[corner] = id;
    }
  }
  output.setAttribute(
    "position",
    new BufferAttribute(new Float32Array(xyz), 3),
  );
  output.setAttribute(
    "normal",
    new BufferAttribute(new Float32Array(normals), 3),
  );
  if (analytic)
    output.setAttribute(
      "surfaceNormal",
      new BufferAttribute(new Float32Array(surface), 3),
    );
  output.setIndex(new BufferAttribute(result, 1));
  return output;
}
