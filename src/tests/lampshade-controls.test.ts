import { test } from "node:test";
import assert from "node:assert/strict";
import { getModel } from "../generated/catalog.ts";
import { defaults, type Parameters } from "../site/models/types.ts";
import {
  configurationUrl,
  parametersFromHash,
} from "../site/configurator/share.ts";
import {
  surfacePlan,
  bulbClearance,
  geometryKey,
} from "../../models/lampshade/surface.mjs";
import { openArea } from "../site/configurator/lampshade-metrics.mjs";
const base = defaults(getModel("lampshade"));
test("display modes and bulb guides do not rebuild the printable geometry", () => {
  assert.equal(
    geometryKey(base),
    geometryKey({
      ...base,
      density_mode: 2,
      bulb_length: 85,
      bulb_offset: 15,
      bulb_gap: 12.5,
      bulb_overlay: 1,
    }),
  );
  assert.notEqual(geometryKey(base), geometryKey({ ...base, cell_pitch: 12 }));
  assert.equal(openArea({ ...base, height: NaN }), null);
  assert.equal(bulbClearance({ ...base, bulb_length: NaN }).available, false);
});
test("density follows actual rippled perimeter and mode switching preserves layout", () => {
  const p = {
    ...base,
    height: 245,
    bottom_diameter: 160,
    middle_diameter: 160,
    top_diameter: 160,
    lobes: 4,
    ripple_depth: 35,
    twist: 45,
  };
  const plan = surfacePlan(p);
  assert.equal(plan.cols, 70);
  assert.equal(surfacePlan({ ...p, ripple_depth: 0 }).cols, 45);
  for (const density_mode of [0, 1, 2]) {
    const next = surfacePlan({ ...p, density_mode });
    assert.equal(next.cols, plan.cols);
    assert.equal(next.rows, plan.rows);
  }
  const finer = surfacePlan({ ...p, cell_pitch: 8 });
  assert.ok(finer.cols > plan.cols);
  assert.ok(finer.rows > plan.rows);
});
test("readouts distinguish solid wall, holes, clearance gaps and mount interference", () => {
  assert.equal(openArea({ ...base, pattern: 0 }), 0);
  const area = openArea(base);
  assert.ok(area !== null && area > 0 && area < 100);
  const a = bulbClearance(base),
    b = bulbClearance({ ...base, bulb_gap: base.bulb_gap + 5 });
  assert.ok(a.available);
  assert.ok(Math.abs(a.diameter - b.diameter - 10) < 0.001);
  assert.ok(bulbClearance({ ...base, bulb_offset: 0 }).diameter < a.diameter);
  assert.equal(bulbClearance({ ...base, fit_test: 1 }).available, false);
  const lamp = bulbClearance({ ...base, orientation: 1 });
  assert.equal(lamp.diameter, a.diameter);
});
test("all new controls survive compact links", () => {
  const model = getModel("lampshade");
  const p = {
    ...base,
    density_mode: 2,
    cell_pitch: 12.34,
    bulb_length: 85,
    bulb_offset: 15,
    bulb_gap: 12.5,
    bulb_overlay: 1,
    cell_rounded: 1,
  };
  assert.deepEqual(
    parametersFromHash(
      model,
      new URL(
        configurationUrl(model, p, "https://example.com/designs/lampshade"),
      ).hash,
    ),
    p,
  );
});

test("strand mode, wave nodes and inactive cells survive shared links", () => {
  const model = getModel("lampshade");
  const p = {
    ...base,
    pattern: 2,
    wave_peaks: 18,
    wave_height: 12,
    wave_spacing: 6,
    wave_offset: 0.35,
    wave_nodes: 5,
    wave_4_x: 0.9,
    wave_4_y: -0.3,
    wave_4_slope: 2.5,
    cell_2_hx: 0.23,
  };
  const url = configurationUrl(model, p, "http://localhost/designs/lampshade");
  assert.deepEqual(parametersFromHash(model, new URL(url).hash), p);
  const area = openArea(p);
  assert.ok(area !== null && area > 0 && area < 100);
});

import {
  waveNodes,
  applyWave,
  strandPlan,
  wavePreset,
  waveValue,
} from "../../models/lampshade/waves.mjs";
test("strand width follows widest surface and merge prevents remote-layer contact", () => {
  for (const wave_merge of [1, 50, 100]) {
    const p: Parameters = {
      ...base,
      pattern: 2,
      wave_merge,
      ripple_depth: 0,
      twist: 0,
    };
    const plan = strandPlan(p);
    assert.ok(!plan.error);
    assert.ok(plan.actualMerge! >= plan.minMerge! - 1e-8);
    assert.ok(plan.actualMerge! <= plan.maxMerge! + 1e-8);
    assert.ok(2 * plan.spacing! > p.wave_height + p.thickness);
    assert.ok(
      Math.abs(plan.width! * plan.repeats! - surfacePlan(p).maxCircumference) <
        1e-8,
    );
  }
  const a = strandPlan({ ...base, ripple_depth: 0 });
  const b = strandPlan({ ...base, ripple_depth: 20 });
  assert.ok(b.repeats! > a.repeats!);
  for (const name of ["Smooth", "Soft peaks"]) {
    const n = wavePreset(name);
    for (let i = 0; i <= 100; i++)
      assert.ok(waveValue(n, i / 100) >= 0 && waveValue(n, i / 100) <= 1);
    assert.equal(waveValue(n, 0), 0);
    assert.equal(waveValue(n, 0.5), 1);
  }
});
test("revision four strands migrate without losing inactive cell edits", () => {
  const model = getModel("lampshade");
  const p = parametersFromHash(
    model,
    "#config=AgIEAaUBAjwDPAQ8BgIHBAjeAgmHASKNCiMBJsMFKAEswwUtAS-ACjCNCjIBSAFJC0oBS9gETwFRLVIDUwNYeF54",
  );
  assert.equal(p.pattern, 2);
  assert.equal(p.height, 245);
  assert.ok(p.wave_width >= 4);
  assert.ok(!strandPlan(p).error);
  for (let i = 0; i < p.wave_nodes; i++)
    assert.ok(p[`wave_${i}_y`] >= 0 && p[`wave_${i}_y`] <= 1);
  const url = configurationUrl(model, p, "http://localhost/designs/lampshade");
  assert.deepEqual(parametersFromHash(model, new URL(url).hash), p);
});

test("smooth arch defaults to a true half sine with equal width and height", () => {
  assert.equal(base.wave_width, base.wave_height);
  const nodes = waveNodes(base);
  for (let i = 0; i <= 100; i++)
    assert.ok(
      Math.abs(waveValue(nodes, i / 100) - Math.sin((Math.PI * i) / 100)) <
        1e-12,
    );
  const edited = applyWave(
    base,
    nodes.map((n, i) => (i === 1 ? { ...n, y: 0.8 } : n)),
  );
  assert.equal(edited.wave_arch, 2);
  assert.notEqual(waveValue(waveNodes(edited), 0.25), waveValue(nodes, 0.25));
  const previous = parametersFromHash(
    getModel("lampshade"),
    "#config=AgIFAwEEWQYCCwBSEm5C",
  );
  assert.equal(previous.wave_arch, 0);
  assert.equal(previous.wave_width, 10.6);
  assert.equal(previous.wave_height, 10);
});
