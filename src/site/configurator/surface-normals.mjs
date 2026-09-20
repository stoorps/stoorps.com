import { Vector3 } from "three";

// The optional analytic normals are carried through de-indexing. Only replace
// complete skin faces: hole walls have opposite inner/outer directions, and
// hardware/join faces have zero markers and retain their creased normals.
export function applySurfaceNormals(geometry) {
  const surface = geometry.getAttribute("surfaceNormal");
  if (!surface) return;
  const position = geometry.getAttribute("position"),
    normal = geometry.getAttribute("normal");
  const a = new Vector3(),
    b = new Vector3(),
    c = new Vector3(),
    face = new Vector3();
  const directions = [new Vector3(), new Vector3(), new Vector3()];
  const indices = geometry.index;
  const count = indices ? indices.count : position.count;
  for (let i = 0; i < count; i += 3) {
    const ids = [0, 1, 2].map((j) => (indices ? indices.getX(i + j) : i + j));
    a.fromBufferAttribute(position, ids[0]);
    b.fromBufferAttribute(position, ids[1]);
    c.fromBufferAttribute(position, ids[2]);
    face.crossVectors(b.sub(a), c.sub(a)).normalize();
    directions.forEach((v, j) => v.fromBufferAttribute(surface, ids[j]));
    if (!directions.every((v) => v.lengthSq() > 0.9 && v.dot(face) > 0.9))
      continue;
    directions.forEach((v, j) => normal.setXYZ(ids[j], v.x, v.y, v.z));
  }
  normal.needsUpdate = true;
  geometry.deleteAttribute("surfaceNormal");
}
