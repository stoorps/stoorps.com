import { surfacePlan } from "./surface.mjs";
// Reserve two wall thicknesses plus the reinforcing band's extra 1.3 mm,
// or the mounting collar's clearance, whichever requires more room.
export function maxRippleDepth(p) {
  const radius =
    Math.min(p.bottom_diameter, p.middle_diameter, p.top_diameter) / 2;
  const reserve = Math.max(
    2 * p.thickness + 1.3,
    p.clamp_diameter / 2 + 9 + p.thickness / 2,
  );
  return Math.max(0, Math.floor((radius - reserve + 1e-9) * 10) / 10);
}

export function latticeLayout(p) {
  // Use physical spacing instead of counts, so larger shades get more cells.
  // Keep open space between the strands even at the densest setting.
  const spacing = Math.max(32 - (26 * p.density) / 100, 2 * p.thickness + 2);
  const diameter =
    (p.bottom_diameter + 2 * p.middle_diameter + p.top_diameter) / 4;
  return {
    spacing,
    cells: Math.max(8, Math.round((Math.PI * diameter) / spacing)),
    rows: Math.max(3, Math.round(p.height / spacing)),
  };
}

import { ShapeUtils, Vector2 } from "three";
export const MAX_NODES = 8;
export function presetNodes(count, rotation = 0, smooth = false) {
  return Array.from({ length: count }, (_, i) => {
    const a = (2 * Math.PI * i) / count + rotation;
    return {
      x: +(0.88 * Math.cos(a)).toFixed(3),
      y: +(0.88 * Math.sin(a)).toFixed(3),
      hx: +(-Math.sin(a) * 0.28).toFixed(3),
      hy: +(Math.cos(a) * 0.28).toFixed(3),
      smooth: smooth ? 1 : 0,
    };
  });
}
export function cellNodes(p) {
  return Array.from({ length: p.cell_nodes }, (_, i) => ({
    x: p[`cell_${i}_x`],
    y: p[`cell_${i}_y`],
    hx: p[`cell_${i}_hx`],
    hy: p[`cell_${i}_hy`],
    smooth: p[`cell_${i}_smooth`],
  }));
}
export function applyNodes(p, nodes) {
  const next = { ...p, cell_nodes: nodes.length };
  nodes.forEach((n, i) =>
    Object.entries(n).forEach(([k, v]) => (next[`cell_${i}_${k}`] = v)),
  );
  return next;
}
export function opening(p) {
  const nodes = cellNodes(p),
    points = [];
  for (let i = 0; i < nodes.length; i++) {
    const a = nodes[i],
      b = nodes[(i + 1) % nodes.length];
    const c = [a.x + (a.smooth ? a.hx : 0), a.y + (a.smooth ? a.hy : 0)],
      d = [b.x - (b.smooth ? b.hx : 0), b.y - (b.smooth ? b.hy : 0)];
    const steps = 12;
    for (let j = 0; j < steps; j++) {
      const t = j / steps,
        u = 1 - t;
      points.push([
        u * u * u * a.x +
          3 * u * u * t * c[0] +
          3 * u * t * t * d[0] +
          t * t * t * b.x,
        u * u * u * a.y +
          3 * u * u * t * c[1] +
          3 * u * t * t * d[1] +
          t * t * t * b.y,
      ]);
    }
  }
  const angle = (p.cell_rotation * Math.PI) / 180;
  const rotated = points.map(([x, y]) => [
    x * Math.cos(angle) - y * Math.sin(angle),
    x * Math.sin(angle) + y * Math.cos(angle),
  ]);
  const min = [0, 1].map((k) => Math.min(...rotated.map((q) => q[k])));
  const max = [0, 1].map((k) => Math.max(...rotated.map((q) => q[k])));
  const center = min.map((v, k) => (v + max[k]) / 2);
  const scale = min.map((v, k) => (max[k] - v) / 2);
  if (scale.some((s) => s < 1e-9))
    throw new Error("Separate neighbouring cell nodes.");
  return rotated.map(([x, y]) => [
    (x - center[0]) / scale[0],
    (y - center[1]) / scale[1],
  ]);
}
export function validateOpening(p) {
  const pts = opening(p);
  if (pts.some((pt) => pt.some((v) => !Number.isFinite(v))))
    throw new Error("Cell coordinates must be finite.");
  const nodes = cellNodes(p);
  if (
    nodes.some(
      (n) =>
        Math.abs(n.x) > 1 ||
        Math.abs(n.y) > 1 ||
        (n.smooth &&
          (Math.abs(n.x + n.hx) > 1 ||
            Math.abs(n.x - n.hx) > 1 ||
            Math.abs(n.y + n.hy) > 1 ||
            Math.abs(n.y - n.hy) > 1)),
    )
  )
    throw new Error("Keep nodes and curve handles inside the cell boundary.");
  const cross = (a, b, c) =>
    (b[0] - a[0]) * (c[1] - a[1]) - (b[1] - a[1]) * (c[0] - a[0]);
  const on = (a, b, c) =>
    Math.abs(cross(a, b, c)) < 1e-9 &&
    c[0] >= Math.min(a[0], b[0]) - 1e-9 &&
    c[0] <= Math.max(a[0], b[0]) + 1e-9 &&
    c[1] >= Math.min(a[1], b[1]) - 1e-9 &&
    c[1] <= Math.max(a[1], b[1]) + 1e-9;
  for (let i = 0; i < pts.length; i++) {
    const a = pts[i],
      b = pts[(i + 1) % pts.length];
    if (Math.hypot(a[0] - b[0], a[1] - b[1]) < 1e-6)
      throw new Error("Separate neighbouring cell nodes.");
    for (let j = i + 2; j < pts.length; j++) {
      if (i === 0 && j === pts.length - 1) continue;
      const c = pts[j],
        d = pts[(j + 1) % pts.length];
      if (
        (cross(a, b, c) * cross(a, b, d) < 0 &&
          cross(c, d, a) * cross(c, d, b) < 0) ||
        on(a, b, c) ||
        on(a, b, d) ||
        on(c, d, a) ||
        on(c, d, b)
      )
        throw new Error(
          "Cell edges cannot cross or touch. Move the selected node or shorten its handles.",
        );
    }
  }
  const area =
    pts.reduce((sum, a, i) => {
      const b = pts[(i + 1) % pts.length];
      return sum + a[0] * b[1] - a[1] * b[0];
    }, 0) / 2;
  if (Math.abs(area) < 0.08) throw new Error("Make the cell opening larger.");
  return area < 0 ? pts.reverse() : pts;
}

// Triangulate only contour corners, then restore the shared edge samples.
// Nearly collinear Bezier samples otherwise become zero-area ears. Wrapping
// those ears onto a ripple turns them into overlapping, reversed triangles.
function triangulatePanel(boundary, hole) {
  const points = [...boundary, ...hole];
  const chains = [];
  function corners(loop, offset) {
    const kept = loop
      .map((_, i) => i)
      .filter((i) => {
        const a = loop[(i + loop.length - 1) % loop.length],
          b = loop[i],
          c = loop[(i + 1) % loop.length];
        return (
          Math.abs((b.x - a.x) * (c.y - b.y) - (b.y - a.y) * (c.x - b.x)) >
          1e-10
        );
      });
    kept.forEach((start, k) => {
      const end = kept[(k + 1) % kept.length],
        chain = [offset + start];
      for (
        let i = (start + 1) % loop.length;
        i !== end;
        i = (i + 1) % loop.length
      )
        chain.push(offset + i);
      chain.push(offset + end);
      if (chain.length > 2) chains.push(chain);
    });
    return kept.map((i) => offset + i);
  }
  const outer = corners(boundary, 0),
    inner = corners(hole, boundary.length);
  const indices = [...outer, ...inner];
  let faces = ShapeUtils.triangulateShape(
    outer.map((i) => points[i].clone()),
    [inner.map((i) => points[i].clone())],
  ).map((face) => face.map((i) => indices[i]));
  for (const chain of chains) {
    const a = chain[0],
      b = chain[chain.length - 1];
    const next = [];
    for (const face of faces) {
      const edge = face.findIndex(
        (v, i) =>
          (v === a && face[(i + 1) % 3] === b) ||
          (v === b && face[(i + 1) % 3] === a),
      );
      if (edge < 0) {
        next.push(face);
        continue;
      }
      const ordered = face[edge] === a ? chain : [...chain].reverse();
      const opposite = face[(edge + 2) % 3];
      for (let i = 0; i < ordered.length - 1; i++)
        next.push([ordered[i], ordered[i + 1], opposite]);
    }
    faces = next;
  }
  return faces;
}

// Flip interior diagonals toward a constrained Delaunay triangulation.
// Long, thin triangle fans approximate a curved surface poorly even when their
// winding is correct. Boundary edges stay fixed, preserving welded row seams.
function improveTriangles(
  points,
  triangles,
  width,
  height,
  allowed = () => true,
) {
  const xy = points.map((p) => [p.x * width, p.y * height]);
  const cross = (a, b, c) =>
    (xy[b][0] - xy[a][0]) * (xy[c][1] - xy[a][1]) -
    (xy[b][1] - xy[a][1]) * (xy[c][0] - xy[a][0]);
  function cot(a, b, c) {
    const u = [xy[a][0] - xy[c][0], xy[a][1] - xy[c][1]],
      v = [xy[b][0] - xy[c][0], xy[b][1] - xy[c][1]];
    return (u[0] * v[0] + u[1] * v[1]) / Math.abs(u[0] * v[1] - u[1] * v[0]);
  }
  for (let pass = 0; pass < 100; pass++) {
    const edges = new Map(),
      touched = new Set();
    let changed = false;
    triangles.forEach((face, i) =>
      face.forEach((a, k) => {
        const b = face[(k + 1) % 3],
          c = face[(k + 2) % 3],
          key = `${Math.min(a, b)},${Math.max(a, b)}`;
        if (!edges.has(key)) {
          edges.set(key, { a, b, c, i });
          return;
        }
        const previous = edges.get(key),
          d = previous.c,
          j = previous.i;
        if (touched.has(i) || touched.has(j)) return;
        // These are the two prospective triangles, retaining CCW orientation.
        if (cross(c, d, b) <= 1e-10 || cross(d, c, a) <= 1e-10) return;
        if (cot(a, b, c) + cot(a, b, d) >= -1e-8 || !allowed(c, d)) return;
        triangles[i] = [c, d, b];
        triangles[j] = [d, c, a];
        touched.add(i);
        touched.add(j);
        changed = true;
      }),
    );
    if (!changed) break;
  }
  return triangles;
}

// Each panel has one hole. Adjacent panels share vertex IDs, so the whole
// perforated surface is one closed mesh before any mounting Boolean operations.
export function connectedCells(p, layout, radius) {
  let cols = layout.cells;
  const rows = Math.min(
    Math.max(3, Math.round(layout.rows / p.cell_aspect)),
    Math.floor(p.height / (p.thickness + 0.5)),
  );
  const hole = validateOpening(p);
  const minRadius =
    Math.min(p.bottom_diameter, p.middle_diameter, p.top_diameter) / 2 -
    p.ripple_depth;
  cols = Math.min(
    cols,
    Math.max(8, Math.round((2 * Math.PI * minRadius) / layout.spacing)),
  );
  const height = p.height / rows;
  const maxRadius =
    Math.max(p.bottom_diameter, p.middle_diameter, p.top_diameter) / 2 +
    p.ripple_depth;
  let width, margin;
  // Cap density at the narrowest part so every opening retains a material web.
  do {
    width = (2 * Math.PI * minRadius) / cols;
    const shear =
      (((Math.abs(p.twist) * Math.PI) / 180) * maxRadius) / p.height;
    margin = p.thickness * Math.sqrt(1 + shear * shear);
    if (width > margin + 0.5) break;
    cols--;
  } while (cols >= 8);
  if (cols < 8 || height <= p.thickness + 0.25)
    throw new Error(
      "Reduce strand thickness or twist, or widen the narrowest part of the shade, to leave space for cell openings.",
    );
  const openingSize = Math.min(
    width - margin,
    (height - p.thickness) / p.cell_aspect,
  );
  const sx = openingSize / width,
    sy = (openingSize * p.cell_aspect) / height;
  const h = hole.map(
    ([x, y]) => new Vector2(0.5 + x * sx * 0.5, 0.5 + y * sy * 0.5),
  );
  // Shared edge samples also follow the profile and angular ripples.
  const sub = Math.max(
    2,
    Math.ceil((p.lobes / cols) * 12),
    Math.ceil(height / 3),
  );
  // Both staggered rows must have identical vertices along their shared seam.
  // A shared grid covering every allowed offset prevents T-junctions.
  const seamSegments = Math.ceil(sub / 20) * 20; // offset step is 0.05 cells
  const horizontal = Array.from(
    { length: seamSegments },
    (_, i) => i / seamSegments,
  );
  const boundary = horizontal.map((x) => new Vector2(x, 0));
  for (let i = 0; i < sub; i++) boundary.push(new Vector2(1, i / sub));
  for (const x of horizontal) boundary.push(new Vector2(1 - x, 1));
  for (let i = 0; i < sub; i++) boundary.push(new Vector2(0, 1 - i / sub));
  let triangles = triangulatePanel(boundary, h);
  const local = [...boundary, ...h],
    vertices = [],
    faces = [],
    ids = new Map();
  triangles = improveTriangles(local, triangles, width, height);
  // Refine long template edges before wrapping them onto the curved surface.
  // Every triangle uses the same edge split rule, including the hole walls.
  const midpoints = new Map();
  // Conservative chord-error bound across every repeat, including the
  // steepest profile section and every ripple phase. A common rule preserves
  // matching samples on neighbouring panels. Units are millimetres.
  const profileRange = Math.max(
    Math.abs(p.middle_diameter - p.bottom_diameter),
    Math.abs(p.top_diameter - p.middle_diameter),
  );
  const profileSlope = (profileRange / p.height) * Math.max(1, Math.PI / 2);
  const profileCurvature =
    (p.curve * profileRange * Math.PI ** 2) / p.height ** 2;
  const needsSplit = (a, b) => {
    const dx = local[a].x - local[b].x,
      dz = (local[a].y - local[b].y) * height;
    const angular = Math.abs(
      (2 * Math.PI * dx) / cols + (((p.twist * Math.PI) / 180) * dz) / p.height,
    );
    const phase = Math.abs((p.lobes * 2 * Math.PI * dx) / cols);
    const radialChange = profileSlope * Math.abs(dz) + p.ripple_depth * phase;
    // A partly straight profile has a slope discontinuity at mid-height.
    // For an odd row count that crease falls inside the middle row.
    const ya = (local[a].y - 0.5) * height,
      yb = (local[b].y - 0.5) * height;
    const kinkError =
      rows % 2 && ya * yb < 0
        ? ((((1 - p.curve) *
            Math.abs(
              p.top_diameter - 2 * p.middle_diameter + p.bottom_diameter,
            )) /
            p.height) *
            Math.abs(ya * yb)) /
          (Math.abs(ya) + Math.abs(yb))
        : 0;
    const error =
      kinkError +
      (profileCurvature * dz * dz +
        p.ripple_depth * phase * phase +
        2 * angular * radialChange +
        (maxRadius + p.thickness / 2) * angular * angular) /
        8;
    return error > 0.02 || Math.abs(dz) > 6 || phase > Math.PI / 3;
  };
  const midpoint = (a, b) => {
    const key = `${Math.min(a, b)},${Math.max(a, b)}`;
    if (!midpoints.has(key)) {
      midpoints.set(key, local.length);
      local.push(
        new Vector2(
          (local[a].x + local[b].x) / 2,
          (local[a].y + local[b].y) / 2,
        ),
      );
    }
    return midpoints.get(key);
  };
  for (let pass = 0; pass < 10; pass++) {
    let changed = false;
    const next = [];
    for (const [a, b, c] of triangles) {
      const ab = needsSplit(a, b),
        bc = needsSplit(b, c),
        ca = needsSplit(c, a);
      if (!ab && !bc && !ca) {
        next.push([a, b, c]);
        continue;
      }
      changed = true;
      const x = ab ? midpoint(a, b) : -1,
        y = bc ? midpoint(b, c) : -1,
        z = ca ? midpoint(c, a) : -1;
      if (ab && bc && ca) next.push([a, x, z], [x, b, y], [z, y, c], [x, y, z]);
      else if (ab && bc) next.push([x, b, y], [a, x, c], [x, y, c]);
      else if (bc && ca) next.push([y, c, z], [b, y, a], [y, z, a]);
      else if (ca && ab) next.push([z, a, x], [c, z, b], [z, x, b]);
      else if (ab) next.push([a, x, c], [x, b, c]);
      else if (bc) next.push([b, y, a], [y, c, a]);
      else next.push([c, z, b], [z, a, b]);
    }
    triangles = next;
    if (!changed) break;
  }
  triangles = improveTriangles(
    local,
    triangles,
    width,
    height,
    (a, b) => !needsSplit(a, b),
  );
  function edgeSegments(a, b) {
    if (!needsSplit(a, b)) return [[a, b]];
    const m = midpoint(a, b);
    return [...edgeSegments(a, m), ...edgeSegments(m, b)];
  }
  const holeEdges = h.flatMap((_, i) =>
    edgeSegments(boundary.length + i, boundary.length + ((i + 1) % h.length)),
  );
  const rimEdges = boundary.flatMap((_, i) =>
    edgeSegments(i, (i + 1) % boundary.length),
  );
  function vertex(u, v, side) {
    u = ((u % cols) + cols) % cols;
    const key = `${Math.round(u * 1e8)},${Math.round(v * 1e8)},${side}`;
    if (ids.has(key)) return ids.get(key);
    const z = (v / rows) * p.height,
      a =
        (u / cols) * 2 * Math.PI + (((p.twist * Math.PI) / 180) * z) / p.height;
    const r = radius(a, z) + ((side ? 1 : -1) * p.thickness) / 2;
    const id = vertices.length;
    vertices.push([r * Math.cos(a), r * Math.sin(a), z]);
    ids.set(key, id);
    return id;
  }
  for (let row = 0; row < rows; row++)
    for (let col = 0; col < cols; col++) {
      const indices = [0, 1].map((side) =>
        local.map((q) =>
          vertex(col + q.x + p.cell_offset * (row % 2), row + q.y, side),
        ),
      );
      for (const [a, b, c] of triangles) {
        faces.push(
          [indices[1][a], indices[1][b], indices[1][c]],
          [indices[0][a], indices[0][c], indices[0][b]],
        );
      }
      // Hole contour is CCW: reverse the usual outer boundary side faces.
      for (const [a, b] of holeEdges) {
        faces.push(
          [indices[0][a], indices[1][a], indices[1][b]],
          [indices[0][a], indices[1][b], indices[0][b]],
        );
      }
      for (const [i, j] of rimEdges) {
        const a = local[i],
          b = local[j];
        if (
          (row === 0 && a.y === 0 && b.y === 0) ||
          (row === rows - 1 && a.y === 1 && b.y === 1)
        )
          faces.push(
            [indices[0][i], indices[0][j], indices[1][j]],
            [indices[0][i], indices[1][j], indices[1][i]],
          );
      }
    }
  return { vertices, faces };
}

// Physical repeat spacing for the contour/strand modes. The shape may extend
// beyond a repeat boundary; overlap is deliberate and fused by the generator.
export function strandPlan(p, layout = latticeLayout(p)) {
  const plan = surfacePlan(p);
  const { cols, rows, width, cellHeight: height } = plan;
  return {
    cols,
    rows,
    width,
    height,
    points: validateOpening(p).map(([x, y]) => [
      (x * width * p.cell_scale) / 2,
      (y * height * p.cell_scale) / 2,
    ]),
  };
}

export function cellContactWarning(p) {
  if (!p.cell_cut_outside) return "";
  let plan;
  try {
    plan = strandPlan(p);
  } catch {
    return "";
  }
  const { points, width, height } = plan;
  const cross = (a, b, c) =>
    (b[0] - a[0]) * (c[1] - a[1]) - (b[1] - a[1]) * (c[0] - a[0]);
  const pointDistance = (p, a, b) => {
    const dx = b[0] - a[0],
      dy = b[1] - a[1];
    const t = Math.max(
      0,
      Math.min(
        1,
        ((p[0] - a[0]) * dx + (p[1] - a[1]) * dy) / (dx * dx + dy * dy),
      ),
    );
    return Math.hypot(p[0] - a[0] - t * dx, p[1] - a[1] - t * dy);
  };
  function joins(x, y) {
    const shifted = points.map((p) => [p[0] + x * width, p[1] + y * height]);
    for (let i = 0; i < points.length; i++)
      for (let j = 0; j < shifted.length; j++) {
        const a = points[i],
          b = points[(i + 1) % points.length],
          c = shifted[j],
          d = shifted[(j + 1) % shifted.length];
        if (
          cross(a, b, c) * cross(a, b, d) < 0 &&
          cross(c, d, a) * cross(c, d, b) < 0
        )
          return true;
        if (
          Math.min(
            pointDistance(a, c, d),
            pointDistance(b, c, d),
            pointDistance(c, a, b),
            pointDistance(d, a, b),
          ) <
          p.thickness * 0.9
        )
          return true;
      }
    return false;
  }
  const horizontal = joins(1, 0),
    left = joins(p.cell_offset, 1),
    right = joins(p.cell_offset - 1, 1);
  return (left && right) || (horizontal && (left || right))
    ? ""
    : "Outside is cut away: neighbouring shapes need solid overlap. Increase cell size or thickness, or adjust nodes and row offset. Export also checks the finished connections.";
}
