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
    { ...valid, enabled: "false" },
    { ...valid, backend: "unknown" },
    { ...valid, backend: undefined },
    { ...valid, runtime: "mesh" },
    { ...valid, backend: "manifold", formats: ["step"] },
    { ...valid, share_id: 0 },
    { ...valid, parameters: [{ ...valid.parameters[0], share_id: 0 }] },
    {
      ...valid,
      parameters: [
        valid.parameters[0],
        { ...valid.parameters[1], share_id: valid.parameters[0].share_id },
      ],
    },
    { ...valid, unknown: "x" },
    { ...valid, parameters: [{ ...valid.parameters[0], default: 99 }] },
    { ...valid, parameters: [{ ...valid.parameters[0], step: 0 }] },
    { ...valid, parameters: [valid.parameters[0], valid.parameters[0]] },
    { ...valid, parts: [{ ...valid.parts[0], color: 0x1000000 }] },
    { ...valid, parts: [valid.parts[0], valid.parts[0]] },
  ])
    assert.throws(() => validateCatalog(value, "bilresa"));
});

test("camera presets are optional and reject unsafe projection values", async () => {
  const { catalog } = (await readModels())[0];
  const { camera, ...withoutCamera } = catalog;
  assert.doesNotThrow(() => validateCatalog(withoutCamera, catalog.id));
  for (const change of [
    { direction: [0, 0, 0] },
    { direction: [0, 0, 1] },
    { zoom: 0 },
    { pan: [NaN, 0] },
    { pan: [0] },
  ]) {
    assert.throws(() =>
      validateCatalog(
        { ...catalog, camera: { mobile: { ...camera.mobile, ...change } } },
        catalog.id,
      ),
    );
  }
});

test("catalogue backends select separate Rust kernels", async () => {
  const models = await readModels();
  assert.equal(
    models.find((m) => m.catalog.id === "bilresa").catalog.backend,
    "cadrum",
  );
  const shade = models.find((m) => m.catalog.id === "lampshade");
  assert.equal(shade.catalog.backend, "manifold");
  assert.equal(shade.crate, "model-lampshade");
});

test("enabled is optional and disabled models remain available to tooling", async () => {
  const entries = await readModels();
  const { catalog } = entries.find((m) => m.catalog.id === "lampshade");
  assert.equal(catalog.enabled, false);
  const { enabled, ...legacy } = catalog;
  for (const value of [
    legacy,
    { ...catalog, enabled: true },
    { ...catalog, enabled: false },
  ])
    assert.doesNotThrow(() => validateCatalog(value, catalog.id));
  assert.equal(
    entries.find((m) => m.catalog.id === "bilresa").catalog.enabled,
    true,
  );
});

test("YAML references splice section lists and support local anchors and nested references", async () => {
  const { mkdtemp, writeFile, mkdir, rm } = await import("node:fs/promises");
  const { tmpdir } = await import("node:os");
  const path = await import("node:path");
  const { readCatalog } = await import("../tools/catalog-yaml.mjs");
  const root = await mkdtemp(path.join(tmpdir(), "catalog-yaml-"));
  try {
    await mkdir(path.join(root, "catalog"));
    const main = path.join(root, "catalog.yml");
    await writeFile(main, "parameters:\n  - $ref: catalog/section.yml\n");
    await writeFile(
      path.join(root, "catalog/section.yml"),
      "- &base {key: a, min: 0, max: 10}\n- {<<: *base, key: b}\n- $ref: more.yml\n",
    );
    await writeFile(path.join(root, "catalog/more.yml"), "- {key: c}\n");
    assert.deepEqual(await readCatalog(main), {
      parameters: [
        { key: "a", min: 0, max: 10 },
        { key: "b", min: 0, max: 10 },
        { key: "c" },
      ],
    });
    for (const [text, error] of [
      ["$ref: catalog.yml\n", /Circular/],
      ["cycle: &cycle {self: *cycle}\n", /Circular/],
      ["$ref: absent.yml\n", /ENOENT/],
      ["$ref: https://example.com/catalog.yml\n", /Invalid/],
      ["$ref: catalog/section.yml\nextra: true\n", /Invalid/],
      ["id: first\nid: second\n", /unique/],
    ]) {
      await writeFile(main, text);
      await assert.rejects(readCatalog(main), error);
    }
  } finally {
    await rm(root, { recursive: true, force: true });
  }
});

test("parameter groups inherit names and reject ambiguous structures", async () => {
  const { normalizeGroups } = await import("../tools/catalog-yaml.mjs");
  const catalog = {
    id: "example",
    parameter_groups: [
      {
        group_name: "Shape",
        parameters: [{ key: "height" }, { key: "width" }],
      },
      { group_name: "Cell editor", parameters: [{ key: "nodes" }] },
    ],
  };
  assert.deepEqual(normalizeGroups(catalog), {
    id: "example",
    parameters: [
      { key: "height", group: "Shape" },
      { key: "width", group: "Shape" },
      { key: "nodes", group: "Cell editor" },
    ],
  });
  for (const invalid of [
    { ...catalog, parameters: [] },
    { ...catalog, parameter_groups: [] },
    {
      ...catalog,
      parameter_groups: [
        catalog.parameter_groups[0],
        catalog.parameter_groups[0],
      ],
    },
    {
      ...catalog,
      parameter_groups: [{ group_name: "", parameters: [{ key: "x" }] }],
    },
    { ...catalog, parameter_groups: [{ group_name: "Shape", parameters: [] }] },
    {
      ...catalog,
      parameter_groups: [
        { group_name: "Shape", extra: true, parameters: [{ key: "x" }] },
      ],
    },
    {
      ...catalog,
      parameter_groups: [
        { group_name: "Shape", parameters: [{ key: "x", group: "Other" }] },
      ],
    },
  ])
    assert.throws(() => normalizeGroups(invalid));
});
