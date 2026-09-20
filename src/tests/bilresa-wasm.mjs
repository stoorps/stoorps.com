import strictAssert from "node:assert/strict";
import { readModels } from "../tools/model-catalog.mjs";
import { readFile, writeFile, mkdir } from "node:fs/promises";
import init, {
  Model,
  inspect_step,
  catalog_json,
} from "../generated/models/bilresa/model.js";
import { assert, close } from "./geometry.mjs";
await init({
  module_or_path: await readFile(
    new URL("../generated/models/bilresa/model_bg.wasm", import.meta.url),
  ),
});
await mkdir("artifacts/bilresa", { recursive: true });
const reports = [];
for (const [left, right] of [
  [1, 1],
  [2, 0],
  [0, 2],
  [0, 0],
  [3, 1],
  [1, 3],
  [4, 0],
]) {
  const start = performance.now(),
    model = new Model(
      JSON.stringify({ num_switches_left: left, num_switches_right: right }),
    ),
    parts = [];
  for (let i = 0; i < 3; i++) {
    const p = model.part(i),
      step = p.step(),
      stl = p.stl();
    const parsed = JSON.parse(inspect_step(step));
    assert(parsed.solids.length === 1, "One solid per part");
    close(parsed.solids[0].volume, p.volume(), 1e-4, "STEP roundtrip");
    await writeFile(`artifacts/bilresa/${left}-${right}-${i}.step`, step);
    await writeFile(`artifacts/bilresa/${left}-${right}-${i}.stl`, stl);
    parts.push({
      index: i,
      volume: p.volume(),
      triangles: p.indices().length / 3,
    });
    p.free();
  }
  const combined = model.step();
  assert(
    JSON.parse(inspect_step(combined)).solids.length === 3,
    "Three solids in assembly STEP",
  );
  await writeFile(`artifacts/bilresa/${left}-${right}-assembly.step`, combined);
  reports.push({ left, right, parts, milliseconds: performance.now() - start });
  model.free();
}
for (const args of [
  [-1, 1],
  [1.5, 1],
  [9, 0],
  [NaN, 1],
]) {
  let rejected = false;
  try {
    new Model(
      JSON.stringify({
        num_switches_left: args[0],
        num_switches_right: args[1],
      }),
    );
  } catch {
    rejected = true;
  }
  assert(rejected, "Reject invalid counts");
}
await writeFile(
  "artifacts/bilresa/wasm-validation.json",
  JSON.stringify(reports, null, 2),
);
console.log(JSON.stringify(reports, null, 2));

const catalog = JSON.parse(catalog_json());
assert(
  catalog.id === "bilresa" && catalog.revision === 2,
  "Model metadata exported",
);

const contract = (m) => ({
  id: m.id,
  revision: m.revision,
  parameters: m.parameters.map(({ key, default: value, min, max, step }) => ({
    key,
    default: value,
    min,
    max,
    step,
  })),
  parts: m.parts.map((p) => p.id),
});
strictAssert.deepEqual(
  contract(catalog),
  contract(
    (await readModels()).find((m) => m.catalog.id === "bilresa").catalog,
  ),
  "Compiled Rust and source YAML geometry settings match",
);

const variants = [
  {
    sp_width: 75,
    sp_height: 80,
    sp_depth: 6,
    num_switches_left: 0,
    num_switches_right: 0,
  },
  {
    sp_width: 120,
    sp_height: 120,
    sp_depth: 10,
    num_switches_left: 4,
    num_switches_right: 4,
  },
  { sp_width: 100, sp_height: 80, num_switches_left: 0, num_switches_right: 4 },
  { sp_width: 75, sp_height: 120, num_switches_left: 4, num_switches_right: 0 },
  {
    switch_tolerance: 0.4,
    sp_tolerance: 0.4,
    bracket_rail_tolerance: 0.4,
    cover_tolerance: 0.2,
    mag_xy_tolerance: 0.3,
    mag_adhesive_tolerance: 0.8,
    asm_wall_thickness: 4,
    switch_pitch: 8,
  },
  {
    switch_tolerance: 0.05,
    sp_tolerance: 0.1,
    bracket_rail_tolerance: 0.1,
    cover_tolerance: 0.02,
    mag_xy_tolerance: 0.01,
    mag_adhesive_tolerance: 0.1,
  },
];
await mkdir("artifacts/bilresa-parameters", { recursive: true });
for (const [index, params] of variants.entries()) {
  const model = new Model(JSON.stringify(params));
  const assembly = JSON.parse(inspect_step(model.step()));
  strictAssert.equal(assembly.solids.length, 3);
  const subset = JSON.parse(
    inspect_step(model.step_selected(new Uint32Array([0, 2]))),
  );
  strictAssert.equal(subset.solids.length, 2);
  strictAssert.throws(() => model.step_selected(new Uint32Array([])));
  strictAssert.throws(() => model.step_selected(new Uint32Array([0, 0])));
  const measures = JSON.parse(model.measurements_json());
  strictAssert.equal(measures[0].value, params.sp_width ?? 85.6);
  for (let part = 0; part < 3; part++) {
    const p = model.part(part);
    strictAssert.ok(p.volume() > 0);
    await writeFile(
      `artifacts/bilresa-parameters/${index}-${part}.step`,
      p.step(),
    );
    await writeFile(
      `artifacts/bilresa-parameters/${index}-${part}.stl`,
      p.stl(),
    );
    p.free();
  }
  model.free();
}
for (const params of [
  { num_switches_left: 5 },
  { sp_width: 74 },
  { sp_height: 121 },
  { sp_depth: 10.01 },
  { cover_tolerance: 0.001 },
  { sp_width: 90.123 },
  { unknown: 1 },
])
  strictAssert.throws(() => new Model(JSON.stringify(params)));
console.log(
  "Expanded parameters: six boundary combinations, subset STEP, measurements and invalid values PASS",
);
