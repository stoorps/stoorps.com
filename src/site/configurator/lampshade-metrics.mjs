import { strandOpenArea } from "../../../models/lampshade/waves.mjs";
import { validateOpening } from "../../../models/lampshade/cells.mjs";
import { surfacePlan } from "../../../models/lampshade/surface.mjs";
export function openArea(p) {
  if (p.fit_test || Object.values(p).some((v) => !Number.isFinite(v)))
    return null;
  if (p.pattern === 2) return strandOpenArea(p, surfacePlan(p).circumference);
  if (!p.pattern || (!p.cell_cut_inside && !p.cell_cut_outside)) return 0;
  let nodes;
  try {
    nodes = validateOpening(p);
  } catch {
    return null;
  }
  const plan = surfacePlan(p),
    w = plan.width,
    h = plan.cellHeight;
  let sx = (w * p.cell_scale) / 2,
    sy = (h * p.cell_scale) / 2;
  if (!p.cell_cut_outside) {
    const width = plan.minCircumference / plan.cols;
    const maxR =
      Math.max(p.bottom_diameter, p.middle_diameter, p.top_diameter) / 2 +
      p.ripple_depth;
    const margin =
      p.thickness *
      Math.hypot(1, (((p.twist * Math.PI) / 180) * maxR) / p.height);
    const size = Math.min(width - margin, (h - p.thickness) / p.cell_aspect);
    if (size <= 0) return null;
    sx = ((size / width) * w) / 2;
    sy = (size * p.cell_aspect) / 2;
  }
  const poly = nodes.map(([x, y]) => [x * sx, y * sy]);
  const minX = Math.min(...poly.map((q) => q[0])),
    maxX = Math.max(...poly.map((q) => q[0]));
  const minY = Math.min(...poly.map((q) => q[1])),
    maxY = Math.max(...poly.map((q) => q[1]));
  const band = p.thickness / 2;
  function contains(x, y) {
    if (
      x < minX - band ||
      x > maxX + band ||
      y < minY - band ||
      y > maxY + band
    )
      return false;
    let inside = false,
      dist = Infinity;
    for (let i = 0, j = poly.length - 1; i < poly.length; j = i++) {
      const [ax, ay] = poly[j],
        [bx, by] = poly[i];
      if (ay > y !== by > y && x < ((bx - ax) * (y - ay)) / (by - ay) + ax)
        inside = !inside;
      if (p.cell_cut_outside) {
        const dx = bx - ax,
          dy = by - ay,
          t = Math.max(
            0,
            Math.min(1, ((x - ax) * dx + (y - ay) * dy) / (dx * dx + dy * dy)),
          );
        dist = Math.min(dist, Math.hypot(x - ax - t * dx, y - ay - t * dy));
      }
    }
    return p.cell_cut_outside
      ? dist <= band || (!p.cell_cut_inside && inside)
      : inside;
  }
  let open = 0;
  const nx = 96,
    ny = 192;
  for (let j = 0; j < ny; j++)
    for (let i = 0; i < nx; i++) {
      const x = ((i + 0.5) / nx) * w,
        y = ((j + 0.5) / ny) * 2 * h;
      let hit = false;
      for (let row = -2; row <= 3 && !hit; row++)
        for (let col = -2; col <= 2 && !hit; col++)
          hit = contains(
            x - (col + 0.5 + p.cell_offset * (((row % 2) + 2) % 2)) * w,
            y - (row + 0.5) * h,
          );
      if (p.cell_cut_outside ? !hit : hit) open++;
    }
  return (100 * open) / (nx * ny);
}
