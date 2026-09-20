import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import { PDFDocument } from "pdf-lib";
import * as wasm from "../generated/models/mini-rack-sideboard/model.js";
import {
  solveMotion,
  validateBattenSpacing,
  polygonDistance,
} from "../../models/mini-rack-sideboard/motion.mjs";
import {
  fabricationFiles,
  definitionGroups,
  footCutFaces,
} from "../../models/mini-rack-sideboard/fabrication.mjs";
await wasm.default({
  module_or_path: readFileSync(
    new URL(
      "../generated/models/mini-rack-sideboard/model_bg.wasm",
      import.meta.url,
    ),
  ),
});
const defaults = Object.fromEntries(
  JSON.parse(wasm.catalog_json()).parameters.map((p) => [p.key, p.default]),
);
const model = new wasm.Model("{}");
const assembly = JSON.parse(model.assembly_json());

test("default assembly has separate stock pieces and valid machined solids", () => {
  assert.equal(model.part_count(), 102);
  assert.equal(new Set(assembly.parts.map((p) => p.id)).size, 102);
  assert.equal(assembly.metrics.battenCountPerDoor, 31);
  assert.equal(assembly.metrics.battenLength, 616);
  assert.equal(assembly.parts.filter((p) => p.group === "feet").length, 16);
  validateBattenSpacing(assembly);
  for (let i = 0; i < model.part_count(); i++) {
    const part = model.part(i);
    try {
      assert.ok(part.volume() > 0);
      assert.ok([...part.positions()].every(Number.isFinite));
      assert.ok(part.indices().length > 0);
    } finally {
      part.free();
    }
  }
  assert.equal(
    assembly.hardware.find((h) => /dowel/i.test(h.name)).quantity,
    124,
  );
  assert.ok(assembly.parts.every((p) => p.explode.some((x) => x !== 0)));
});

test("batten count and dowel schedule follow changed door path and requested gap", () => {
  const changed = new wasm.Model(JSON.stringify({ sd_batten_gap: 15 }));
  try {
    const a = JSON.parse(changed.assembly_json());
    validateBattenSpacing(a);
    assert.ok(a.metrics.battenCountPerDoor < 31);
    assert.equal(
      a.parts.filter(
        (p) => p.id.includes("batten") && p.group.endsWith("-door"),
      ).length,
      a.metrics.battenCountPerDoor * 2,
    );
    assert.equal(
      a.hardware.find((h) => /dowel/i.test(h.name)).quantity,
      a.metrics.battenCountPerDoor * 4,
    );
    assert.notEqual(changed.part_count(), 102);
  } finally {
    changed.free();
  }
});

test("motion honours hardware stop and physical obstructions", () => {
  assert.deepEqual(solveMotion(assembly), {
    angle: 110,
    limit: "Hardware opening limit",
    valid: true,
  });
  const limited = structuredClone(assembly);
  limited.metrics.hardwareLimit = 40;
  assert.equal(solveMotion(limited).angle, 40);
  const blocked = structuredClone(assembly);
  blocked.metrics.pivotLeft[0] += 10;
  blocked.metrics.pivotRight[0] -= 10;
  blocked.metrics.pivotLeft[1] -= 12;
  blocked.metrics.pivotRight[1] -= 12;
  const motion = solveMotion(blocked);
  assert.ok(motion.angle < 20);
  assert.ok(motion.valid);
  const closed = structuredClone(assembly);
  closed.metrics.swingClearance = 20;
  assert.equal(solveMotion(closed).valid, false);
  assert.equal(
    polygonDistance(
      [
        [0, 0],
        [1, 0],
        [1, 1],
        [0, 1],
      ],
      [
        [2, 0],
        [3, 0],
        [3, 1],
        [2, 1],
      ],
    ),
    1,
  );
});

test("all four exploded foot bundles separate their individual battens", () => {
  const feet = assembly.parts.filter((p) => p.group === "feet");
  for (let i = 0; i < feet.length; i++) {
    for (let j = i + 1; j < feet.length; j++) {
      const exploded = (p) =>
        p.profile.map((q) => [
          p.frame.o[0] + q.x + p.explode[0],
          p.frame.o[1] + q.y + p.explode[1],
        ]);
      assert.ok(
        polygonDistance(exploded(feet[i]), exploded(feet[j])) > 10,
        `${feet[i].id} overlaps ${feet[j].id} when exploded`,
      );
    }
  }
});

test("foot face guides retain true stock widths and common compound-cut length", () => {
  for (const part of assembly.parts.filter((p) => p.group === "feet")) {
    const faces = footCutFaces(part);
    assert.equal(faces.length, 4);
    faces.forEach((f, i) => {
      assert.ok(Math.abs(f.profile[1].x - part.stock[i % 2]) < 1e-8);
      assert.ok(
        Math.abs(
          f.profile[2].y - f.profile[1].y - Math.hypot(...part.extrusion),
        ) < 1e-8,
      );
    });
    const ys = faces.flatMap((f) => f.profile.map((p) => p.y));
    assert.ok(
      Math.abs(Math.max(...ys) - Math.min(...ys) - part.stock[2]) < 1e-8,
    );
  }
});

test("fabrication pack has physical-scale PDFs, analytic curves and selected cut quantities", async () => {
  const a = { ...assembly, motion: solveMotion(assembly) };
  const files = await fabricationFiles(a, defaults);
  const pdf = await PDFDocument.load(files["templates.pdf"]);
  assert.equal(pdf.getPageCount(), definitionGroups(a.parts).length + 64);
  assert.ok(Math.abs((pdf.getPage(0).getWidth() / 72) * 25.4 - 1330) < 1e-5);
  assert.match(new TextDecoder().decode(files["profiles/base-1.dxf"]), /ARC/);
  assert.match(
    new TextDecoder().decode(files["templates/base-1.svg"]),
    /1330mm/,
  );
  assert.equal(
    Object.keys(files).filter((k) => k.startsWith("foot-cut-guides/")).length,
    64,
  );
  const selected = await fabricationFiles(a, defaults, 1, [0]);
  assert.equal(
    JSON.parse(new TextDecoder().decode(selected["configuration.json"]))
      .selectedParts.length,
    1,
  );
  assert.equal(
    (await PDFDocument.load(selected["templates.pdf"])).getPageCount(),
    1,
  );
  await assert.rejects(() => fabricationFiles(a, defaults, 1, []));
  const step = model.step_selected(new Uint32Array([0]));
  assert.ok(step.length > 1000);
});

test("impossible shelf geometry is rejected", () => {
  assert.throws(() => new wasm.Model(JSON.stringify({ i_shelf_height: 900 })));
});
test.after(() => model.free());
