import { surfacePlan } from "../../models/lampshade/surface.mjs";
import test from "node:test";
import assert from "node:assert/strict";
import { readFile } from "node:fs/promises";
import { readCatalog } from "../tools/catalog-yaml.mjs";
import Module from "manifold-3d";
import { BufferGeometry, BufferAttribute, IcosahedronGeometry } from "three";
import { mergeVertices } from "three/addons/utils/BufferGeometryUtils.js";
import { creasedNormals } from "../site/configurator/creased-normals.mjs";
import { toCreasedNormals } from "three/addons/utils/BufferGeometryUtils.js";
import { applySurfaceNormals } from "../site/configurator/surface-normals.mjs";
import {
  applyNodes,
  presetNodes,
  validateOpening,
  connectedCells,
  cellContactWarning,
} from "../../models/lampshade/cells.mjs";
import {
  latticeLayout,
  sampleSections,
  profile,
} from "../../models/lampshade/reference.mjs";
const catalog = await readCatalog("models/lampshade/catalog.yml");
const defaults = Object.fromEntries(
  catalog.parameters.map((p) => [p.key, p.default]),
);
// Run model behaviour checks against the deployed Rust/WASM ABI. Snapshot
// results and release WASM handles so long suites do not accumulate kernel memory.
const compiled = await import("../generated/models/lampshade/model.js");
await compiled.default({
  module_or_path: await readFile(
    "src/generated/models/lampshade/model_bg.wasm",
  ),
});
const module = {
  Model: class {
    constructor(json) {
      const model = new compiled.Model(json);
      try {
        this.parts = catalog.parts.map((_, i) => {
          const part = model.part(i);
          try {
            const values = Object.fromEntries(
              [
                "positions",
                "indices",
                "normals",
                "surface_normals",
                "bounds",
                "volume",
                "stl",
              ].map((k) => [k, part[k]()]),
            );
            return Object.fromEntries(
              Object.entries(values).map(([key, value]) => [key, () => value]),
            );
          } finally {
            part.free();
          }
        });
      } finally {
        model.free();
      }
    }
    part(i) {
      return this.parts[i];
    }
  },
};
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

test("polygon and curved openings create connected printable shades", () => {
  for (const count of [3, 4, 5, 6, 8]) {
    const p = applyNodes(
      { ...defaults, density: 65 },
      presetNodes(count, 0, count === 6),
    );
    const m = new module.Model(JSON.stringify(p));
    m.parts.forEach(check);
  }
});
test("cell editor rejects crossings and out-of-bounds handles", () => {
  const cross = applyNodes(defaults, [
    { x: -0.8, y: -0.8, hx: 0, hy: 0, smooth: 0 },
    { x: 0.8, y: 0.8, hx: 0, hy: 0, smooth: 0 },
    { x: -0.8, y: 0.8, hx: 0, hy: 0, smooth: 0 },
    { x: 0.8, y: -0.8, hx: 0, hy: 0, smooth: 0 },
  ]);
  assert.throws(() => validateOpening(cross));
  assert.throws(() =>
    validateOpening({ ...defaults, cell_0_smooth: 1, cell_0_hx: 1 }),
  );
});
test("layout proportions, rotated openings and staggered rows remain connected", () => {
  for (const extra of [
    { cell_aspect: 0.5, cell_offset: 0, cell_rotation: 45 },
    { cell_aspect: 2, cell_offset: 1, cell_rotation: -30 },
  ])
    build({ ...extra, density: 60 }).parts.forEach(check);
});

test("cell panels weld into a closed mesh before Boolean operations", () => {
  const p = {
    ...defaults,
    bottom_diameter: 200,
    middle_diameter: 256,
    top_diameter: 100,
    density: 100,
  };
  const { vertices, faces } = connectedCells(
    p,
    latticeLayout(p),
    (_a, z) => 50 + 50 * Math.sin((Math.PI * z) / p.height),
  );
  const s = new checker.Manifold(
    new checker.Mesh({
      numProp: 3,
      vertProperties: Float32Array.from(vertices.flat()),
      triVerts: Uint32Array.from(faces.flat()),
    }),
  );
  try {
    assert.equal(s.status(), "NoError");
    assert.ok(s.volume() > 0);
    const components = s.decompose();
    assert.equal(components.length, 1);
    components.forEach((c) => c.delete());
  } finally {
    s.delete();
  }
});

test("plain drum keeps round openings round and staggers whole rows without gaps", () => {
  const p = applyNodes(
    {
      ...defaults,
      bottom_diameter: 180,
      middle_diameter: 180,
      top_diameter: 180,
      ripple_depth: 0,
      twist: 0,
      density: 51,
    },
    presetNodes(6, 0, true),
  );
  for (const offset of [0, 0.35, 0.5, 1]) {
    p.cell_offset = offset;
    const layout = latticeLayout(p),
      width = (2 * Math.PI * 90) / layout.cells,
      height = p.height / layout.rows;
    const mesh = connectedCells(p, layout, () => 90);
    const s = new checker.Manifold(
      new checker.Mesh({
        numProp: 3,
        vertProperties: Float32Array.from(mesh.vertices.flat()),
        triVerts: Uint32Array.from(mesh.faces.flat()),
      }),
    );
    assert.equal(s.status(), "NoError", `watertight at offset ${offset}`);
    s.delete();
    const contours = [new Map(), new Map()];
    for (const face of mesh.faces) {
      const pts = face.map((i) => mesh.vertices[i]);
      const radii = pts.map(([x, y]) => Math.hypot(x, y));
      if (Math.max(...radii) - Math.min(...radii) < 1) continue;
      for (let i = 0; i < 3; i++) {
        if (radii[i] < 90) continue;
        const [x, y, z] = pts[i],
          row = Math.floor(z / height);
        if (row > 1 || z % height < 1e-6 || height - (z % height) < 1e-6)
          continue;
        let u =
          ((Math.atan2(y, x) + 2 * Math.PI) % (2 * Math.PI)) * 90 -
          offset * (row % 2) * width;
        if (u < 0 || u > width) continue;
        contours[row].set(`${u.toFixed(6)},${z.toFixed(6)}`, [
          u,
          z - row * height,
        ]);
      }
    }
    for (const points of contours) {
      assert.ok(points.size > 30);
      const radii = [...points.values()].map(([x, z]) =>
        Math.hypot(x - width / 2, z - height / 2),
      );
      assert.ok(
        Math.max(...radii) / Math.min(...radii) < 1.04,
        `round at offset ${offset}`,
      );
    }
  }
});

test("cutaway combinations produce connected rings, filled cells and a solid wall", () => {
  const p = {
    ...defaults,
    bottom_diameter: 180,
    middle_diameter: 180,
    top_diameter: 180,
    ripple_depth: 0,
    twist: 0,
    density: 35,
    cell_cut_outside: 1,
  };
  const ring = new module.Model(JSON.stringify(p));
  ring.parts.forEach(check);
  const filled = new module.Model(JSON.stringify({ ...p, cell_cut_inside: 0 }));
  filled.parts.forEach(check);
  assert.ok(filled.part(0).volume() > ring.part(0).volume());
  const noCuts = build({ cell_cut_inside: 0, cell_cut_outside: 0 });
  assert.ok(
    Math.abs(noCuts.part(0).volume() - build({ pattern: 0 }).part(0).volume()) <
      0.01,
  );
  const round = applyNodes(p, presetNodes(6, 0, true));
  new module.Model(JSON.stringify({ ...round, cell_scale: 1.4 })).parts.forEach(
    check,
  );
});

test("outside cutaway warns about separation and rejects disconnected material", () => {
  const p = {
    ...defaults,
    cell_cut_outside: 1,
    cell_scale: 0.5,
    density: 25,
    ripple_depth: 0,
    twist: 0,
  };
  assert.match(cellContactWarning(p), /overlap/);
  assert.equal(cellContactWarning({ ...p, cell_cut_outside: 0 }), "");
  assert.throws(
    () => new module.Model(JSON.stringify(p)),
    /neighbouring shapes must overlap/,
  );
});

test("ripples do not fold the perforated cell surfaces into reversed triangles", () => {
  for (const ripple_depth of [0, 1, 4]) {
    const p = { ...defaults, ripple_depth, twist: 0 };
    const radius = (a, z) =>
      profile(p, z / p.height) + ripple_depth * Math.sin(p.lobes * a);
    const { vertices, faces } = connectedCells(p, latticeLayout(p), radius);
    let surfaceTriangles = 0;
    for (const face of faces) {
      const [a, b, c] = face.map((i) => vertices[i]);
      const layers = [a, b, c].map(
        (v) => Math.hypot(v[0], v[1]) - radius(Math.atan2(v[1], v[0]), v[2]),
      );
      const side = layers[0] > 0 ? 1 : -1;
      // Skip the radial walls around openings and the top/bottom rims.
      if (!layers.every((r) => Math.abs(r - (side * p.thickness) / 2) < 1e-7))
        continue;
      const u = b.map((v, k) => v - a[k]),
        v = c.map((v, k) => v - a[k]);
      const nx = u[1] * v[2] - u[2] * v[1],
        ny = u[2] * v[0] - u[0] * v[2];
      assert.ok(
        side * (nx * a[0] + ny * a[1]) > 0,
        `Folded surface at ripple depth ${ripple_depth}`,
      );
      for (const [start, end] of [
        [a, b],
        [b, c],
        [c, a],
      ]) {
        const angleA = Math.atan2(start[1], start[0]),
          angleB = Math.atan2(end[1], end[0]);
        const delta = Math.atan2(
          Math.sin(angleB - angleA),
          Math.cos(angleB - angleA),
        );
        const angle = angleA + delta / 2,
          z = (start[2] + end[2]) / 2;
        const r = radius(angle, z) + (side * p.thickness) / 2;
        const error = Math.hypot(
          (start[0] + end[0]) / 2 - r * Math.cos(angle),
          (start[1] + end[1]) / 2 - r * Math.sin(angle),
        );
        assert.ok(
          error <= 0.0201,
          `Chord deviates ${error} mm at ripple ${ripple_depth}`,
        );
      }
      if (ripple_depth === 1) {
        // Connectivity and winding alone miss long, skinny triangles that
        // visibly crease after wrapping. Compare against the smooth surface.
        const center = a.map((value, k) => (value + b[k] + c[k]) / 3);
        const angle = Math.atan2(center[1], center[0]),
          z = center[2];
        const r = radius(angle, z) + (side * p.thickness) / 2;
        const angularSlope = (p.lobes * Math.cos(p.lobes * angle)) / r;
        const heightSlope =
          (radius(angle, z + 0.0001) - radius(angle, z - 0.0001)) / 0.0002;
        const expected = [
          Math.cos(angle) + angularSlope * Math.sin(angle),
          Math.sin(angle) - angularSlope * Math.cos(angle),
          -heightSlope,
        ];
        const nz = u[0] * v[1] - u[1] * v[0];
        const alignment =
          (side * (nx * expected[0] + ny * expected[1] + nz * expected[2])) /
          Math.hypot(nx, ny, nz) /
          Math.hypot(...expected);
        assert.ok(
          alignment > 0.99,
          `Cell surface creases away from its profile: ${alignment}`,
        );
      }
      surfaceTriangles++;
    }
    assert.ok(surfaceTriangles > 0);
  }
  // Check the reported configuration after the reinforcing rings and mount
  // have been joined, including the exported mesh's connectivity.
  build({ ripple_depth: 1, twist: 0 }).parts.forEach(check);
});

test("analytic preview normals smooth skins without rounding hole walls", () => {
  const part = build({ ripple_depth: 1, twist: 0 }).part(0);
  const indexed = new BufferGeometry();
  indexed.setAttribute("position", new BufferAttribute(part.positions(), 3));
  indexed.setAttribute(
    "surfaceNormal",
    new BufferAttribute(part.surface_normals(), 3),
  );
  indexed.setIndex(new BufferAttribute(part.indices(), 1));
  const geometry = toCreasedNormals(indexed, Math.PI / 4);
  const before = geometry.getAttribute("normal").array.slice();
  const analytic = geometry.getAttribute("surfaceNormal").array;
  applySurfaceNormals(geometry);
  const after = geometry.getAttribute("normal").array;
  let changed = 0,
    hard = 0;
  for (let i = 0; i < after.length; i += 9) {
    // Mixed inner/outer directions identify opening-wall faces.
    const dot = (j, k) =>
      [0, 1, 2].reduce(
        (sum, c) => sum + analytic[i + j + c] * analytic[i + k + c],
        0,
      );
    if (dot(0, 3) < -0.5 || dot(0, 6) < -0.5) {
      assert.deepEqual(after.slice(i, i + 9), before.slice(i, i + 9));
      hard++;
    }
    if (
      after.slice(i, i + 9).some((v, k) => Math.abs(v - before[i + k]) > 1e-6)
    ) {
      assert.deepEqual(after.slice(i, i + 9), analytic.slice(i, i + 9));
      changed++;
    }
  }
  assert.ok(changed > 100);
  assert.ok(hard > 100);
  geometry.dispose();
  indexed.dispose();
});

test("Rust backend preserves reference adapter bounds, volume and measurements", async () => {
  const { createModule } = await import("../../models/lampshade/reference.mjs");
  const reference = createModule(catalog);
  await reference.initialize();
  const params = JSON.stringify({ ...defaults, fit_test: 1 });
  const before = new reference.Model(params);
  const after = new compiled.Model(params);
  try {
    assert.deepEqual(
      JSON.parse(after.measurements_json()),
      JSON.parse(before.measurements_json()),
    );
    assert.equal(JSON.parse(compiled.catalog_json()).backend, "manifold");
    for (let i = 0; i < catalog.parts.length; i++) {
      const a = before.part(i),
        b = after.part(i);
      try {
        for (const [j, value] of a.bounds().entries())
          assert.ok(Math.abs(value - b.bounds()[j]) < 0.001);
        assert.ok(Math.abs(a.volume() - b.volume()) / a.volume() < 0.001);
      } finally {
        a.free();
        b.free();
      }
    }
  } finally {
    before.free();
    after.free();
  }
});

test("mounting supports stay inside the curved shade wall", () => {
  for (const overrides of [
    {},
    { ripple_depth: 6, twist: -90, curve: 0, density: 35 },
    {
      bottom_diameter: 180,
      middle_diameter: 180,
      top_diameter: 180,
      ripple_depth: 0,
      twist: 0,
      cell_cut_outside: 1,
      density: 35,
    },
  ]) {
    const p = { ...defaults, ...overrides };
    const part = build(overrides).part(0);
    check(part);
    const vertices = part.positions();
    const zm = p.height - p.mount_depth - p.plate_thickness;
    let checked = 0;
    for (let i = 0; i < vertices.length; i += 3) {
      const [x, y, z] = vertices.slice(i, i + 3);
      if (z < zm - 6 - 0.001 || z > zm - 3 + 0.001) continue;
      const a = Math.atan2(y, x);
      const outer =
        profile(p, z / p.height) +
        p.thickness / 2 +
        p.ripple_depth *
          Math.sin(
            p.lobes * (a - (((p.twist * Math.PI) / 180) * z) / p.height),
          );
      assert.ok(
        Math.hypot(x, y) <= outer + 0.03,
        `Mount protrudes by ${Math.hypot(x, y) - outer} mm`,
      );
      checked++;
    }
    assert.ok(checked > 0);
  }
});

test("cell normalisation fills both axes independently of drawing size and position", async () => {
  const { opening } = await import("../../models/lampshade/cells.mjs");
  const original = presetNodes(4);
  const small = original.map((n) => ({
    ...n,
    x: n.x * 0.25 + 0.1,
    y: n.y * 0.5 - 0.2,
  }));
  const a = opening(applyNodes(defaults, original));
  const b = opening(applyNodes(defaults, small));
  a.forEach((q, i) =>
    q.forEach((v, k) => assert.ok(Math.abs(v - b[i][k]) < 1e-10)),
  );
  for (const k of [0, 1]) {
    assert.equal(Math.max(...b.map((q) => q[k])), 1);
    assert.equal(Math.min(...b.map((q) => q[k])), -1);
  }
  assert.throws(
    () =>
      opening(
        applyNodes(
          defaults,
          original.map((n) => ({ ...n, y: 0 })),
        ),
      ),
    /Separate/,
  );
  const p = { ...defaults, density: 25, ripple_depth: 0, twist: 0 };
  const first = new module.Model(JSON.stringify(applyNodes(p, original))).part(
    0,
  );
  const second = new module.Model(JSON.stringify(applyNodes(p, small))).part(0);
  check(first);
  check(second);
  assert.ok(Math.abs(first.volume() - second.volume()) < 0.01);
});

test("curved overlapping rings avoid the excessive uniform subdivision mesh", () => {
  const p = {
    ...defaults,
    cell_cut_outside: 1,
    cell_scale: 1.7,
    cell_0_smooth: 1,
    cell_0_hy: 0.293,
    cell_1_smooth: 1,
    cell_1_hx: -0.293,
    cell_2_smooth: 1,
    cell_2_hy: -0.293,
    cell_3_smooth: 1,
    cell_3_hx: 0.293,
    cell_3_y: 0.28,
  };
  const model = new module.Model(JSON.stringify(p));
  model.parts.forEach(check);
  const part = model.part(0);
  assert.ok(
    part.indices().length / 3 <
      2000 * surfacePlan(p).cols * surfacePlan(p).rows,
    "keep the surface-spaced pattern within a bounded mesh budget",
  );
});

test("ripple depth follows profile, wall and mounting clearance", async () => {
  const { maxRippleDepth } = await import("../../models/lampshade/cells.mjs");
  assert.equal(maxRippleDepth(defaults), 30);
  for (const overrides of [
    {},
    { bottom_diameter: 100, middle_diameter: 200, top_diameter: 160 },
    { thickness: 4, clamp_diameter: 54 },
    { bottom_diameter: 290, middle_diameter: 290, top_diameter: 290 },
  ]) {
    const p = { ...defaults, ...overrides, fit_test: 1 };
    const max = maxRippleDepth(p);
    new module.Model(JSON.stringify({ ...p, ripple_depth: max })).parts.forEach(
      check,
    );
    assert.throws(
      () =>
        new module.Model(
          JSON.stringify({
            ...p,
            ripple_depth: Number((max + 0.1).toFixed(1)),
          }),
        ),
      /Ripple depth must be at most/,
    );
  }
  build({ pattern: 0, ripple_depth: 10 }).parts.forEach(check);
});

test("rounded strands change exported geometry while retaining connected printable rings", () => {
  const p = {
    density: 20,
    height: 100,
    bottom_diameter: 120,
    middle_diameter: 120,
    top_diameter: 120,
    ripple_depth: 0,
    twist: 0,
    cell_cut_outside: 1,
    cell_cut_inside: 1,
  };
  const square = build(p).part(0);
  const rounded = build({ ...p, cell_rounded: 1 }).part(0);
  check(rounded);
  assert.ok(rounded.volume() < square.volume() * 0.95);
  assert.ok(rounded.volume() > square.volume() * 0.65);
  assert.ok(rounded.stl().length > 84);
});

test("indexed crease shading agrees with the reference without expanding every triangle", () => {
  const shape = new IcosahedronGeometry(20, 3);
  shape.deleteAttribute("normal");
  shape.deleteAttribute("uv");
  const input = mergeVertices(shape);
  const reference = toCreasedNormals(input, Math.PI / 4);
  const actual = creasedNormals(input, Math.PI / 4);
  assert.ok(
    actual.getAttribute("position").count <
      reference.getAttribute("position").count / 2,
  );
  const normal = actual.getAttribute("normal");
  const expected = reference.getAttribute("normal");
  for (let i = 0; i < actual.index.count; i++) {
    const j = actual.index.getX(i);
    for (const axis of ["getX", "getY", "getZ"])
      assert.ok(Math.abs(normal[axis](j) - expected[axis](i)) < 1e-5);
  }
  input.dispose();
  shape.dispose();
  actual.dispose();
  reference.dispose();
});

test("wave strands sweep into one printable shade and preserve the adapter", () => {
  const p = {
    pattern: 2,
    wave_arch: 0,
    wave_width: 40,
    wave_height: 8,
    height: 100,
    bottom_diameter: 120,
    middle_diameter: 120,
    top_diameter: 120,
    ripple_depth: 0,
    twist: 0,
    wave_width: 47,
  };
  const result = build(p);
  check(result.part(0));
  check(result.part(1));
  assert.ok(result.part(0).indices().length / 3 < 700000);
  assert.throws(
    () =>
      build({
        ...p,
        wave_0_y: 0,
        wave_1_y: 0,
        wave_2_y: 0,
        wave_3_y: 0,
        wave_0_slope: 0,
        wave_1_slope: 0,
        wave_2_slope: 0,
        wave_3_slope: 0,
      }),
    /some height/,
  );
  assert.throws(() => build({ ...p, wave_2_x: 0.1 }), /ordered/);
});

test("wave strands stay connected through pronounced ripple and twist", () => {
  const result = build({
    pattern: 2,
    wave_arch: 0,
    wave_width: 40,
    wave_height: 8,
    height: 245,
    bottom_diameter: 160,
    middle_diameter: 160,
    top_diameter: 160,
    lobes: 4,
    ripple_depth: 35,
    twist: 45,
  });
  check(result.part(0));
  assert.ok(result.part(0).indices().length / 3 < 900000);
  assert.throws(
    () =>
      build({
        pattern: 2,
        wave_arch: 0,
        wave_width: 40,
        wave_height: 8,
        height: 245,
        bottom_diameter: 160,
        middle_diameter: 160,
        top_diameter: 160,
        lobes: 24,
        ripple_depth: 35,
        twist: 45,
      }),
    /tight bends|folds/,
  );
});

test("strand merge replaces unsafe dense spacing and remains printable", () => {
  const result = build({
    pattern: 2,
    wave_arch: 0,
    wave_width: 40,
    wave_height: 8,
    height: 245,
    bottom_diameter: 160,
    middle_diameter: 160,
    top_diameter: 160,
    lobes: 4,
    ripple_depth: 35,
    twist: 45,
    wave_width: 16.4,
    wave_merge: 100,
    wave_height: 2.5,
    wave_spacing: 1,
    wave_offset: 0.5,
    wave_0_slope: 0,
    wave_2_slope: 0,
  });
  const shade = result.part(0);
  check(shade);
  // This 97-layer case has substantially more exposed surface than the 4.5 mm wave.
  assert.ok(shade.indices().length / 3 < 6000000);
  assert.ok(shade.volume() > 20000 && shade.volume() < 300000);
  assert.ok(Math.abs(shade.bounds()[5] - shade.bounds()[2] - 245) < 0.1);
});

test("half-sine arches fuse their trough corners into a printable shade", () => {
  const result = build({
    pattern: 2,
    height: 100,
    bottom_diameter: 120,
    middle_diameter: 120,
    top_diameter: 120,
    ripple_depth: 0,
    twist: 0,
    thickness: 1.2,
    wave_width: 10,
    wave_height: 10,
    wave_arch: 1,
  });
  check(result.part(0));
  check(result.part(1));
  assert.ok(result.part(0).volume() > 5000);
});
