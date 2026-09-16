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
  for (let i = 0; i < position.count; i += 3) {
    a.fromBufferAttribute(position, i);
    b.fromBufferAttribute(position, i + 1);
    c.fromBufferAttribute(position, i + 2);
    face.crossVectors(b.sub(a), c.sub(a)).normalize();
    directions.forEach((v, j) => v.fromBufferAttribute(surface, i + j));
    if (!directions.every((v) => v.lengthSq() > 0.9 && v.dot(face) > 0.9))
      continue;
    directions.forEach((v, j) => normal.setXYZ(i + j, v.x, v.y, v.z));
  }
  normal.needsUpdate = true;
  geometry.deleteAttribute("surfaceNormal");
}
