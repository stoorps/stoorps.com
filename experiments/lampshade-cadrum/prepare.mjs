import { readCatalog } from "../../src/tools/catalog-yaml.mjs";
import { readFile, writeFile } from "node:fs/promises";
const catalog = await readCatalog("models/lampshade/catalog.yml");
const p = {
  ...Object.fromEntries(catalog.parameters.map((x) => [x.key, x.default])),
  ripple_depth: 1,
  twist: 0,
};
const spacing = Math.max(32 - (26 * p.density) / 100, 2 * p.thickness + 2);
const cols = Math.round(
  (2 *
    Math.PI *
    (Math.min(p.bottom_diameter, p.middle_diameter, p.top_diameter) / 2 -
      p.ripple_depth)) /
    spacing,
);
const rows = Math.round(p.height / spacing),
  width =
    (2 *
      Math.PI *
      (Math.min(p.bottom_diameter, p.middle_diameter, p.top_diameter) / 2 -
        p.ripple_depth)) /
    cols,
  height = p.height / rows;
const size = Math.min(width - p.thickness, height - p.thickness);
const sx = size / width,
  sy = size / height;
await writeFile(
  "artifacts/lampshade-comparison/input.json",
  JSON.stringify({
    ...p,
    cols,
    rows,
    hole: [
      [0.44 * sx, 0],
      [0, 0.44 * sy],
      [-0.44 * sx, 0],
      [0, -0.44 * sy],
    ],
  }),
);
