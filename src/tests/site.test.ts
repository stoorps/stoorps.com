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
test("stores overrides against an explicit model and revision", () => {
  const value = JSON.parse(
    Buffer.from(
      encodeConfiguration(bilresa, defaults(bilresa)),
      "base64url",
    ).toString(),
  );
  assert.deepEqual(value, {
    schema: 1,
    model: "bilresa",
    modelRevision: 2,
    overrides: {},
  });
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
