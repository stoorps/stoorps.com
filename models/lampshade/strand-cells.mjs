import { strandPlan } from "./cells.mjs";

// Fuse one column in 2D, then reuse its tessellation around the shade. Only
// the column joins and mounting hardware require 3D Boolean operations.
export function strandCells(
  p,
  layout,
  radius,
  { CrossSection, Manifold, Mesh },
  own,
) {
  const { cols, rows, width, height, points } = strandPlan(p, layout);
  const temporary = [];
  const keep = (x) => (temporary.push(x), x);
  try {
    const contour = keep(new CrossSection([points]));
    const outer = keep(contour.offset(p.thickness / 2, "Round", 2, 12));
    let material = outer;
    if (p.cell_cut_inside) {
      const inner = keep(contour.offset(-p.thickness / 2, "Round", 2, 12));
      if (inner.isEmpty())
        throw new Error(
          "The strand thickness fills the cell centre. Reduce thickness or increase cell size.",
        );
      material = keep(outer.subtract(inner));
    }
    const repeats = Array.from({ length: rows }, (_, row) =>
      keep(
        material.translate([
          (0.5 + p.cell_offset * (row % 2)) * width,
          (row + 0.5) * height,
        ]),
      ),
    );
    const column = keep(CrossSection.union(repeats));
    const clip = keep(
      keep(CrossSection.square([width * 8, p.height])).translate([
        -width * 3,
        0,
      ]),
    );
    const clipped = keep(column.intersect(clip));
    const flat = keep(clipped.extrude(p.thickness));
    const length = Math.min(
      3,
      width / 4,
      p.ripple_depth > 0 ? (width * cols) / Math.max(1, p.lobes) / 12 : 3,
    );
    const refined = keep(flat.refineToLength(length));
    const template = refined.getMesh();
    const pieces = [];
    for (let col = 0; col < cols; col++) {
      const positions = new Float32Array(template.numVert * 3);
      for (let i = 0; i < template.numVert; i++) {
        const x = template.vertProperties[i * template.numProp],
          z = template.vertProperties[i * template.numProp + 1],
          depth = template.vertProperties[i * template.numProp + 2];
        const angle =
          (2 * Math.PI * (col + x / width)) / cols +
          (((p.twist * Math.PI) / 180) * z) / p.height;
        const r = radius(angle, z) + depth - p.thickness / 2;
        positions.set([r * Math.cos(angle), r * Math.sin(angle), z], i * 3);
      }
      pieces.push(
        own(
          new Manifold(
            new Mesh({
              numProp: 3,
              vertProperties: positions,
              triVerts: template.triVerts,
            }),
          ),
        ),
      );
    }
    return pieces;
  } finally {
    for (const item of temporary.reverse()) item.delete();
  }
}
