import * as THREE from 'three';
import { outlineStyle as style } from '../site/configurator/outline-style.mjs';

// CPU projection and depth testing keep static builds independent of a browser/GPU.
export function renderPreview(parts, title, width = 1000, height = 660) {
  const box = new THREE.Box3();
  for (const part of parts) for (let i = 0; i < part.positions.length; i += 3)
    box.expandByPoint(new THREE.Vector3(...part.positions.slice(i, i + 3)));
  if (box.isEmpty()) throw new Error('Cannot render an empty model');
  const center = box.getCenter(new THREE.Vector3());
  const direction = new THREE.Vector3(...style.direction).normalize();
  const camera = new THREE.OrthographicCamera(-1, 1, 1, -1, .1, 10000);
  camera.up.set(0, 0, 1);
  camera.position.copy(center).addScaledVector(direction, Math.max(1000, box.getSize(new THREE.Vector3()).length() * 3));
  camera.lookAt(center);
  camera.updateMatrixWorld();
  const viewBox = new THREE.Box3();
  for (const part of parts) for (let i = 0; i < part.positions.length; i += 3)
    viewBox.expandByPoint(new THREE.Vector3(...part.positions.slice(i, i + 3)).applyMatrix4(camera.matrixWorldInverse));
  const size = viewBox.getSize(new THREE.Vector3()), middle = viewBox.getCenter(new THREE.Vector3());
  const extent = Math.max(size.y, size.x * height / width) * .68;
  camera.left = middle.x - extent * width / height; camera.right = middle.x + extent * width / height;
  camera.top = middle.y + extent; camera.bottom = middle.y - extent;
  camera.updateProjectionMatrix();
  const project = p => {
    const v = p.clone().project(camera);
    return [(v.x + 1) * width / 2, (1 - v.y) * height / 2, v.z];
  };
  const depth = new Float64Array(width * height).fill(Infinity);
  const lines = [];
  const cross = (a, b, x, y) => (b[0] - a[0]) * (y - a[1]) - (b[1] - a[1]) * (x - a[0]);
  for (const part of parts) {
    const vertices = [];
    for (let i = 0; i < part.positions.length; i += 3) vertices.push(new THREE.Vector3(...part.positions.slice(i, i + 3)));
    const projected = vertices.map(project);
    const adjacency = new Map();
    const vertexKey = p => p.toArray().map(v => Math.round(v * 1e5)).join(',');
    for (let i = 0; i < part.indices.length; i += 3) {
      const ids = Array.from(part.indices.slice(i, i + 3));
      const [a, b, c] = ids.map(index => projected[index]);
      const area = cross(a, b, c[0], c[1]);
      if (Math.abs(area) > 1e-9) {
        for (let y = Math.max(0, Math.floor(Math.min(a[1], b[1], c[1]))); y <= Math.min(height - 1, Math.ceil(Math.max(a[1], b[1], c[1]))); y++) {
          for (let x = Math.max(0, Math.floor(Math.min(a[0], b[0], c[0]))); x <= Math.min(width - 1, Math.ceil(Math.max(a[0], b[0], c[0]))); x++) {
            const u = cross(b, c, x + .5, y + .5) / area, v = cross(c, a, x + .5, y + .5) / area, w = 1 - u - v;
            if (u >= 0 && v >= 0 && w >= 0) depth[y * width + x] = Math.min(depth[y * width + x], u * a[2] + v * b[2] + w * c[2]);
          }
        }
      }
      const normal = vertices[ids[1]].clone().sub(vertices[ids[0]]).cross(vertices[ids[2]].clone().sub(vertices[ids[0]])).normalize();
      for (let e = 0; e < 3; e++) {
        const start = vertices[ids[e]], end = vertices[ids[(e + 1) % 3]];
        const key = [vertexKey(start), vertexKey(end)].sort().join('|');
        const edge = adjacency.get(key) || { start, end, normals: [] };
        edge.normals.push(normal); adjacency.set(key, edge);
      }
    }
    const sharp = Math.cos(THREE.MathUtils.degToRad(style.edgeAngle));
    for (const edge of adjacency.values()) {
      const normals = edge.normals;
      if (normals.length === 1 || normals.some(n => n.dot(normals[0]) < sharp) ||
          (normals.some(n => n.dot(direction) > 0) && normals.some(n => n.dot(direction) <= 0)))
        lines.push([project(edge.start), project(edge.end)]);
    }
  }
  const paths = [[], []];
  const point = p => `${p[0].toFixed(2)} ${p[1].toFixed(2)}`;
  for (const [a, b] of lines) {
    const steps = Math.max(1, Math.ceil(Math.hypot(a[0] - b[0], a[1] - b[1])));
    let previous = a, start = a, lastVisible;
    for (let i = 0; i <= steps; i++) {
      const t = i / steps, p = a.map((v, j) => v + (b[j] - v) * t);
      const x = Math.max(0, Math.min(width - 1, Math.floor(p[0]))), y = Math.max(0, Math.min(height - 1, Math.floor(p[1])));
      const visible = p[2] <= depth[y * width + x] + 0.00008;
      if (lastVisible !== undefined && visible !== lastVisible) {
        paths[Number(lastVisible)].push(`M${point(start)}L${point(previous)}`); start = previous;
      }
      lastVisible = visible; previous = p;
    }
    paths[Number(lastVisible)].push(`M${point(start)}L${point(b)}`);
  }
  // Project the front-plane grid through the same camera as the model.
  const modelSize = box.getSize(new THREE.Vector3());
  const radiusX = modelSize.x * .85 + 30, radiusY = modelSize.y * .85 + 30;
  const gridCenter = new THREE.Vector3(center.x, center.y, box.min.z - .5);
  const origin = project(gridCenter);
  const axisX = project(gridCenter.clone().add(new THREE.Vector3(radiusX, 0, 0)));
  const axisY = project(gridCenter.clone().add(new THREE.Vector3(0, radiusY, 0)));
  const matrix = [axisX[0] - origin[0], axisX[1] - origin[1], axisY[0] - origin[0], axisY[1] - origin[1], origin[0], origin[1]].map(v => v.toFixed(4)).join(' ');
  const gridPaths = [];
  const gridLine = (x1, y1, x2, y2) => gridPaths.push(`M${point(project(new THREE.Vector3(center.x + x1, center.y + y1, gridCenter.z)))}L${point(project(new THREE.Vector3(center.x + x2, center.y + y2, gridCenter.z)))}`);
  const spacing = Math.max(10, Math.ceil(Math.max(radiusX, radiusY) / 100 / 10) * 10);
  for (let x = -Math.ceil(radiusX / spacing) * spacing; x <= radiusX; x += spacing) gridLine(x, -radiusY, x, radiusY);
  for (let y = -Math.ceil(radiusY / spacing) * spacing; y <= radiusY; y += spacing) gridLine(-radiusX, y, radiusX, y);
  const grid = `<defs><radialGradient id="grid-fade" gradientUnits="userSpaceOnUse" cx="0" cy="0" r="1" gradientTransform="matrix(${matrix})"><stop offset="0" stop-color="white"/><stop offset=".4" stop-color="white"/><stop offset="1" stop-color="white" stop-opacity="0"/></radialGradient><mask id="grid-mask"><rect width="100%" height="100%" fill="url(#grid-fade)"/></mask></defs><path d="${gridPaths.join('')}" fill="none" stroke="#4c8070" stroke-width="1.2" opacity=".5" mask="url(#grid-mask)"/>`;
  const escape = value => value.replace(/[<>&"']/g, c => ({'<':'&lt;','>':'&gt;','&':'&amp;','"':'&quot;',"'":'&apos;'}[c]));
  return `<svg xmlns="http://www.w3.org/2000/svg" width="${width}" height="${height}" viewBox="0 0 ${width} ${height}" role="img"><title>${escape(title)}</title><rect width="100%" height="100%" fill="${style.background}"/>${grid}<g fill="none" stroke="${style.line}" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><path opacity="${style.transparency / 100}" d="${paths[0].join('')}"/><path opacity=".95" d="${paths[1].join('')}"/></g></svg>\n`;
}
