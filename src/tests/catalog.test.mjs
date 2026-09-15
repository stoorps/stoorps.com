import test from "node:test";
import assert from "node:assert/strict";
import { readModels, validateCatalog } from "../tools/model-catalog.mjs";
test("discovers real model-scoped manifests without a central model list", async () => {
  const models = await readModels();
  assert.ok(models.length >= 1);
  const bilresa = models.find((m) => m.catalog.id === "bilresa");
  assert.ok(bilresa);
  assert.equal(bilresa.crate, "model-bilresa");
  assert.equal(bilresa.catalog.parts.length, 3);
});
test("rejects broken metadata before it reaches the site", async () => {
  const model = (await readModels()).find((m) => m.catalog.id === "bilresa");
  const valid = model.catalog;
  for (const value of [
    { ...valid, id: "wrong" },
    { ...valid, revision: 0 },
    { ...valid, unknown: "x" },
    { ...valid, parameters: [{ ...valid.parameters[0], default: 99 }] },
    { ...valid, parameters: [{ ...valid.parameters[0], step: 0 }] },
    { ...valid, parameters: [valid.parameters[0], valid.parameters[0]] },
    { ...valid, parts: [{ ...valid.parts[0], color: 0x1000000 }] },
    { ...valid, parts: [valid.parts[0], valid.parts[0]] },
  ])
    assert.throws(() => validateCatalog(value, "bilresa"));
});
