import { test } from "node:test";
import assert from "node:assert/strict";
import { getModel } from "../generated/catalog.ts";
import { defaults, geometryContract } from "../site/models/types.ts";
const bilresa = getModel("bilresa");
import {
  encodeConfiguration,
  decodeConfiguration,
  configurationUrl,
  parametersFromHash,
} from "../site/configurator/share.ts";
const encodeRaw = (value: unknown) =>
  Buffer.from(JSON.stringify(value)).toString("base64url");
test("all supported count pairs round-trip, including zero and asymmetric counts", () => {
  for (let left = 0; left <= 4; left++)
    for (let right = 0; right <= 4; right++) {
      const params = {
        ...defaults(bilresa),
        num_switches_left: left,
        num_switches_right: right,
      };
      const encoded = encodeConfiguration(bilresa, params);
      assert.match(encoded, /^[A-Za-z0-9_-]+$/);
      assert.deepEqual(decodeConfiguration(bilresa, encoded), params);
    }
});
test("compact IDs retain identity and omit default hashes", () => {
  const params = {
    ...defaults(bilresa),
    num_switches_left: 2,
    num_switches_right: 0,
  };
  assert.equal(encodeConfiguration(bilresa, params).length, 10);
  assert.equal(
    configurationUrl(
      bilresa,
      defaults(bilresa),
      "https://example.com/designs/bilresa?ref=x#config=old",
    ),
    "https://example.com/designs/bilresa?ref=x",
  );
  assert.throws(
    () =>
      decodeConfiguration(
        { ...bilresa, share_id: 99 },
        encodeConfiguration(bilresa, params),
      ),
    /different model/,
  );
  assert.throws(
    () =>
      decodeConfiguration(
        { ...bilresa, revision: 99 },
        encodeConfiguration(bilresa, params),
      ),
    /revision/,
  );
  assert.deepEqual(
    decodeConfiguration(
      { ...bilresa, parameters: [...bilresa.parameters].reverse() },
      encodeConfiguration(bilresa, params),
    ),
    params,
  );
});
test("compact decoder rejects damaged and ambiguous byte streams", () => {
  for (const bytes of [
    [2],
    [2, 1],
    [2, 1, 2, 1],
    [2, 1, 2, 1, 5],
    [2, 1, 2, 99, 0],
    [2, 1, 2, 1, 2, 1, 3],
    [2, 129, 0, 2],
    [2, 1, 2, 1, 128],
    [3, 1, 2],
    [2, 1, 2, 1, ...Array(9).fill(255)],
  ])
    assert.throws(() =>
      decodeConfiguration(bilresa, Buffer.from(bytes).toString("base64url")),
    );
});
test("every catalogue parameter round-trips its minimum, maximum and an interior step", () => {
  for (const model of [bilresa, getModel("lampshade")]) {
    for (const p of model.parameters) {
      for (const tick of [0, 1, Math.floor((p.max - p.min) / p.step + 1e-8)]) {
        const value = Number((p.min + tick * p.step).toPrecision(15));
        const params = { ...defaults(model), [p.key]: value };
        assert.deepEqual(
          decodeConfiguration(model, encodeConfiguration(model, params)),
          params,
        );
      }
    }
  }
});
test("links preserve origin, repository base, pathname and unrelated query", () => {
  const url = new URL(
    configurationUrl(
      bilresa,
      { ...defaults(bilresa), num_switches_left: 2, num_switches_right: 0 },
      "https://example.com/models/designs/bilresa/?ref=test#old",
    ),
  );
  assert.equal(url.origin, "https://example.com");
  assert.equal(url.pathname, "/models/designs/bilresa/");
  assert.equal(url.search, "?ref=test");
  assert.deepEqual(parametersFromHash(bilresa, url.hash), {
    ...defaults(bilresa),
    num_switches_left: 2,
    num_switches_right: 0,
  });
});
test("rejects malformed, oversized, duplicate, unknown and unsupported configurations", () => {
  const valid = {
    schema: 1,
    model: "bilresa",
    modelRevision: 2,
    overrides: {},
  };
  for (const value of [
    null,
    [],
    {},
    { ...valid, schema: 2 },
    { ...valid, model: "other" },
    { ...valid, modelRevision: 99 },
    { ...valid, extra: 1 },
    { ...valid, overrides: [] },
    { ...valid, overrides: { unknown: 1 } },
    { ...valid, overrides: { num_switches_left: "2" } },
    { ...valid, overrides: { num_switches_left: -1 } },
    { ...valid, overrides: { num_switches_left: 9 } },
    { ...valid, overrides: { num_switches_right: 1.5 } },
    JSON.parse(
      '{"schema":1,"model":"bilresa","modelRevision":1,"overrides":{"__proto__":2}}',
    ),
  ])
    assert.throws(() => decodeConfiguration(bilresa, encodeRaw(value)));
  for (const value of [
    "",
    "!",
    "a".repeat(4097),
    Buffer.from("not json").toString("base64url"),
  ])
    assert.throws(() => decodeConfiguration(bilresa, value));
  const encoded = encodeRaw(valid);
  assert.throws(() =>
    parametersFromHash(bilresa, `#config=${encoded}&config=${encoded}`),
  );
  assert.throws(() =>
    parametersFromHash(bilresa, `#config=${encoded}&extra=1`),
  );
  assert.deepEqual(parametersFromHash(bilresa, ""), defaults(bilresa));
});
test("invalid edits never become shareable configurations", () => {
  for (const v of [NaN, Infinity, -1, 1.5, 9])
    assert.throws(() =>
      encodeConfiguration(bilresa, {
        num_switches_left: v,
        num_switches_right: 1,
      }),
    );
});

test("copy edits preserve the geometry contract; limits and part identities do not", () => {
  const copy = {
    ...bilresa,
    title: "Renamed",
    description: "New description",
    parameters: bilresa.parameters.map((p) => ({ ...p, label: "New label" })),
    parts: bilresa.parts.map((p) => ({ ...p, name: "New name", color: 0 })),
  };
  assert.deepEqual(geometryContract(copy), geometryContract(bilresa));
  assert.notDeepEqual(
    geometryContract({
      ...copy,
      parameters: copy.parameters.map((p) => ({ ...p, max: p.max + 1 })),
    }),
    geometryContract(bilresa),
  );
  assert.notDeepEqual(
    geometryContract({
      ...copy,
      parts: copy.parts.map((p) => ({ ...p, id: p.id + "-changed" })),
    }),
    geometryContract(bilresa),
  );
});

test("decimal dimensions and advanced overrides round-trip; legacy counts migrate", () => {
  const params = {
    ...defaults(bilresa),
    sp_width: 90.12,
    sp_height: 103.45,
    cover_tolerance: 0.12,
  };
  assert.deepEqual(
    decodeConfiguration(bilresa, encodeConfiguration(bilresa, params)),
    params,
  );
  const legacy = {
    schema: 1,
    model: "bilresa",
    modelRevision: 1,
    overrides: { num_switches_left: 3 },
  };
  assert.deepEqual(decodeConfiguration(bilresa, encodeRaw(legacy)), {
    ...defaults(bilresa),
    num_switches_left: 3,
  });
  assert.throws(
    () =>
      decodeConfiguration(
        bilresa,
        encodeRaw({ ...legacy, overrides: { num_switches_right: 5 } }),
      ),
    /four/,
  );
  assert.throws(() =>
    encodeConfiguration(bilresa, { ...params, sp_width: 90.123 }),
  );
});

test("older lampshade links retain shape and migrate counts to density", () => {
  const model = getModel("lampshade");
  const p = decodeConfiguration(
    model,
    encodeRaw({
      schema: 1,
      model: "lampshade",
      modelRevision: 1,
      overrides: {
        bottom_diameter: 200,
        middle_diameter: 120,
        top_diameter: 200,
        cells: 32,
        rows: 14,
      },
    }),
  );
  assert.equal(p.middle_diameter, 120);
  assert.ok(p.density >= 0 && p.density <= 100);
  assert.ok(!("cells" in p));
  assert.ok(!("rows" in p));
  assert.deepEqual(
    decodeConfiguration(model, encodeConfiguration(model, p)),
    p,
  );
  assert.throws(() =>
    decodeConfiguration(
      model,
      encodeRaw({
        schema: 1,
        model: "lampshade",
        modelRevision: 1,
        overrides: { cells: 999 },
      }),
    ),
  );
});

test("revision-2 shade links migrate and edited cells round trip", () => {
  const model = getModel("lampshade");
  const p = decodeConfiguration(
    model,
    encodeRaw({
      schema: 1,
      model: "lampshade",
      modelRevision: 2,
      overrides: {
        bottom_diameter: 200,
        middle_diameter: 256,
        top_diameter: 100,
        density: 100,
      },
    }),
  );
  assert.equal(p.middle_diameter, 256);
  assert.equal(p.density, 100);
  assert.equal(p.cell_nodes, 4);
  const edited = {
    ...p,
    cell_nodes: 6,
    cell_0_x: 0.5,
    cell_0_hy: 0.2,
    cell_0_smooth: 1,
  };
  assert.deepEqual(
    decodeConfiguration(model, encodeConfiguration(model, edited)),
    edited,
  );
});

test("revision-3 cell links keep cutout mode and revision-4 cutaways round trip", () => {
  const model = getModel("lampshade");
  const p = decodeConfiguration(
    model,
    encodeRaw({
      schema: 1,
      model: "lampshade",
      modelRevision: 3,
      overrides: {
        cell_nodes: 6,
        cell_0_smooth: 1,
        cell_0_hy: 0.28,
        density: 78,
      },
    }),
  );
  assert.equal(p.cell_cut_inside, 1);
  assert.equal(p.cell_cut_outside, 0);
  assert.equal(p.cell_nodes, 6);
  const next = {
    ...p,
    cell_cut_outside: 1,
    cell_cut_inside: 0,
    cell_scale: 1.4,
  };
  assert.deepEqual(
    decodeConfiguration(model, encodeConfiguration(model, next)),
    next,
  );
  assert.throws(
    () =>
      decodeConfiguration(
        model,
        encodeRaw({
          schema: 1,
          model: "lampshade",
          modelRevision: 3,
          overrides: { cell_cut_outside: 1 },
        }),
      ),
    /unsupported cutaway/,
  );
});

test("delta links retain every setting together, including inactive cell nodes and handles", () => {
  for (const model of [bilresa, getModel("lampshade")]) {
    const params = Object.fromEntries(
      model.parameters.map((p) => [
        p.key,
        Number((p.default === p.max ? p.min : p.max).toPrecision(15)),
      ]),
    );
    const url = configurationUrl(
      model,
      params,
      `https://example.com/designs/${model.id}`,
    );
    assert.deepEqual(parametersFromHash(model, new URL(url).hash), params);
  }
});
