import test from "node:test";
import assert from "node:assert/strict";
import { readFile } from "node:fs/promises";
import { parse } from "smol-toml";
import Module from "manifold-3d";
import {
  createModule,
  latticeLayout,
  sampleSections,
} from "../../models/lampshade/model.mjs";
const catalog = parse(await readFile("models/lampshade/catalog.toml", "utf8"));
const defaults = Object.fromEntries(
  catalog.parameters.map((p) => [p.key, p.default]),
);
const module = createModule(catalog);
await module.initialize();
const checker = await Module();
checker.setup();
function build(overrides = {}) {
  return new module.Model(JSON.stringify({ ...defaults, ...overrides }));
}
function solid(part) {
  return new checker.Manifold(
    new checker.Mesh({
      numProp: 3,
      vertProperties: part.positions(),
      triVerts: part.indices(),
    }),
  );
}
function check(part) {
  const s = solid(part);
  try {
    assert.equal(s.status(), "NoError");
    assert.ok(s.volume() > 0);
    const pieces = s.decompose();
    assert.equal(pieces.length, 1);
    pieces.forEach((p) => p.delete());
    const stl = part.stl();
    assert.equal(
      stl.length,
      84 + new DataView(stl.buffer).getUint32(80, true) * 50,
    );
    assert.ok(part.normals().every(Number.isFinite));
  } finally {
    s.delete();
  }
}
test("default lattice and rippled shell are connected, positive-volume printable meshes", () => {
  for (const pattern of [0, 1]) {
    const model = build({ pattern });
    model.parts.forEach(check);
    const b = model.part(0).bounds();
    assert.equal(b[2], 0);
    assert.equal(b[5], 180);
  }
});
test("fit-test adapter has the requested aperture and plate thickness and no interference with its collar", () => {
  for (const thread_clearance of [0.1, 0.25, 0.6]) {
    const model = build({ fit_test: 1, thread_clearance });
    model.parts.forEach(check);
    const adapter = model.part(1),
      pos = adapter.positions();
    let inner = Infinity;
    for (let i = 0; i < pos.length; i += 3)
      if (pos[i + 2] > 2.99)
        inner = Math.min(inner, Math.hypot(pos[i], pos[i + 1]));
    assert.ok(Math.abs(inner * 2 - 27.02) < 0.001);
    assert.equal(adapter.bounds()[5], 3);
    assert.equal(adapter.bounds()[2], -8);
    const a = solid(model.part(0)),
      b = solid(adapter),
      intersection = a.intersect(b);
    assert.ok(
      intersection.volume() < 0.001,
      `thread interference: ${intersection.volume()}`,
    );
    intersection.delete();
    a.delete();
    b.delete();
  }
});
test("mount offsets, lamp orientation and profile changes stay connected", () => {
  for (const p of [
    { mount_depth: 0 },
    {
      height: 100,
      mount_depth: 75,
      orientation: 1,
      top_diameter: 200,
      middle_diameter: 120,
      bottom_diameter: 200,
    },
    { pattern: 0, twist: -90, ripple_depth: 6 },
  ])
    build(p).parts.forEach(check);
});
test("print setup changes do not silently alter geometry", () => {
  const a = build({ fit_test: 1 }),
    b = build({
      fit_test: 1,
      nozzle: 0.6,
      material: 3,
      build_x: 200,
      layer_height: 0.3,
    });
  assert.deepEqual(a.part(0).positions(), b.part(0).positions());
});
test("invalid fixtures, unsafe offsets and malformed values reject explicitly", () => {
  for (const p of [
    { fixture_diameter: 40, clamp_diameter: 40 },
    { mount_depth: 120, height: 100 },
    { thickness: NaN },
    { density: 101 },
    { nozzle: 0 },
    { extra: 1 },
  ])
    assert.throws(() => build(p));
});

test("density scales with physical size and produces much finer cells", () => {
  const sparse = latticeLayout({ ...defaults, density: 0 }),
    dense = latticeLayout({ ...defaults, density: 100 });
  assert.ok(dense.cells > 32);
  assert.ok(dense.rows > 14);
  assert.ok(dense.cells > sparse.cells * 3);
  const larger = latticeLayout({
    ...defaults,
    height: 280,
    bottom_diameter: 280,
    middle_diameter: 280,
    top_diameter: 280,
  });
  const normal = latticeLayout(defaults);
  assert.ok(larger.cells > normal.cells);
  assert.ok(larger.rows > normal.rows);
  build({ density: 100 }).parts.forEach(check);
});

test("adaptive sampling retains curved detail and simplifies straight sections", () => {
  const straight = sampleSections((t) => [[0, 0, 180 * t]], 8);
  assert.equal(straight.length, 9);
  const curve = (t) => [
    [2 * Math.sin(16 * Math.PI * t), 4 * Math.cos(2 * Math.PI * t), 180 * t],
  ];
  const samples = sampleSections(curve, 32);
  assert.ok(samples.length < 361);
  for (let i = 1; i < samples.length; i++) {
    const a = samples[i - 1],
      b = samples[i];
    for (let j = 1; j < 20; j++) {
      const f = j / 20,
        actual = curve(a.t + (b.t - a.t) * f)[0];
      const distance = Math.hypot(
        ...actual.map(
          (v, k) => v - a.section[0][k] * (1 - f) - b.section[0][k] * f,
        ),
      );
      assert.ok(distance < 0.055, `Chord deviation ${distance}`);
    }
  }
});
