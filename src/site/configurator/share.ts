import {
  defaults,
  validateParameters,
  type ModelDefinition,
  type Parameters,
} from "../models/types";
const MAX_LENGTH = 4096;
function record(value: unknown): value is Record<string, unknown> {
  return value !== null && typeof value === "object" && !Array.isArray(value);
}
export function encodeConfiguration(
  model: ModelDefinition,
  params: Parameters,
): string {
  validateParameters(model, params);
  // Format 2: version, model ID, revision, then parameter ID / tick pairs.
  const bytes: number[] = [2];
  const write = (value: number) => {
    if (!Number.isSafeInteger(value) || value < 0)
      throw new Error("This configuration cannot be encoded.");
    do {
      const byte = value % 128;
      value = Math.floor(value / 128);
      bytes.push(byte + (value ? 128 : 0));
    } while (value);
  };
  write(model.share_id);
  write(model.revision);
  for (const p of [...model.parameters].sort(
    (a, b) => a.share_id - b.share_id,
  )) {
    const ticks = Math.round((params[p.key] - p.min) / p.step);
    if (ticks === Math.round((p.default - p.min) / p.step)) continue;
    write(p.share_id);
    write(ticks);
  }
  return base64url(Uint8Array.from(bytes));
}
function base64url(bytes: Uint8Array): string {
  return btoa(Array.from(bytes, (byte) => String.fromCharCode(byte)).join(""))
    .replace(/\+/g, "-")
    .replace(/\//g, "_")
    .replace(/=+$/, "");
}
function decodeCompact(model: ModelDefinition, bytes: Uint8Array): Parameters {
  let offset = 1;
  const read = () => {
    let value = 0,
      factor = 1;
    for (let count = 0; count < 8; count++) {
      const byte = bytes[offset++];
      if (byte === undefined) break;
      value += (byte & 127) * factor;
      if (!Number.isSafeInteger(value)) break;
      if (byte < 128) {
        if (count && byte === 0) break;
        return value;
      }
      factor *= 128;
    }
    throw new Error("This configuration link is malformed.");
  };
  if (read() !== model.share_id)
    throw new Error("This link belongs to a different model.");
  if (read() !== model.revision)
    throw new Error(
      "This model revision is unavailable. The design has not been substituted.",
    );
  const params = defaults(model);
  const seen = new Set<number>();
  while (offset < bytes.length) {
    const id = read();
    const p = model.parameters.find((p) => p.share_id === id);
    if (!p || seen.has(id))
      throw new Error("This configuration has unknown or duplicate settings.");
    seen.add(id);
    const ticks = read();
    if (ticks > Math.round((p.max - p.min) / p.step))
      throw new Error("This configuration has invalid settings.");
    // Remove floating point arithmetic noise while preserving catalogue precision.
    params[p.key] = Number((p.min + ticks * p.step).toPrecision(15));
  }
  validateParameters(model, params);
  return params;
}

export function decodeConfiguration(
  model: ModelDefinition,
  encoded: string,
): Parameters {
  if (
    !encoded ||
    encoded.length > MAX_LENGTH ||
    !/^[A-Za-z0-9_-]+$/.test(encoded)
  )
    throw new Error("This configuration link is malformed.");
  let value: unknown;
  let bytes: Uint8Array;
  try {
    const binary = atob(encoded.replace(/-/g, "+").replace(/_/g, "/"));
    bytes = Uint8Array.from(binary, (c) => c.charCodeAt(0));
    if (base64url(bytes) !== encoded) throw new Error("Noncanonical encoding");
  } catch {
    throw new Error("This configuration link could not be read.");
  }
  if (bytes[0] === 2) return decodeCompact(model, bytes);
  try {
    value = JSON.parse(new TextDecoder("utf-8", { fatal: true }).decode(bytes));
  } catch {
    throw new Error("This configuration link could not be read.");
  }
  if (
    !record(value) ||
    Object.keys(value).some(
      (k) => !["schema", "model", "modelRevision", "overrides"].includes(k),
    )
  )
    throw new Error("This configuration link has an invalid format.");
  if (value.schema !== 1)
    throw new Error("This link uses an unsupported configuration format.");
  if (value.model !== model.id)
    throw new Error("This link belongs to a different model.");
  const legacy =
    model.id === "bilresa" && model.revision === 2 && value.modelRevision === 1;
  const legacyShade =
    model.id === "lampshade" &&
    model.revision >= 2 &&
    value.modelRevision === 1;
  const previousCells =
    model.id === "lampshade" &&
    model.revision >= 3 &&
    value.modelRevision === 2;
  const previousCutaways =
    model.id === "lampshade" &&
    model.revision === 4 &&
    value.modelRevision === 3;
  if (
    value.modelRevision !== model.revision &&
    !legacy &&
    !legacyShade &&
    !previousCells &&
    !previousCutaways
  )
    throw new Error(
      "This model revision is unavailable. The design has not been substituted.",
    );
  if (!record(value.overrides))
    throw new Error("This configuration has invalid settings.");
  const params = defaults(model);
  for (const [key, valueOverride] of Object.entries(value.overrides)) {
    if (
      (legacyShade || previousCells || previousCutaways) &&
      ["cell_cut_inside", "cell_cut_outside", "cell_scale"].includes(key)
    )
      throw new Error("This older link contains unsupported cutaway settings.");
    if ((legacyShade || previousCells) && key.startsWith("cell_"))
      throw new Error("This older link contains unsupported cell settings.");
    if (legacyShade && ["cells", "rows"].includes(key)) {
      const [min, max] = key === "cells" ? [8, 32] : [3, 14];
      if (
        typeof valueOverride !== "number" ||
        !Number.isInteger(valueOverride) ||
        valueOverride < min ||
        valueOverride > max
      )
        throw new Error("This older link contains invalid mesh counts.");
      continue;
    }
    if (legacyShade && key === "density")
      throw new Error("This older link contains unsupported settings.");
    if (
      !model.parameters.some((p) => p.key === key) ||
      typeof valueOverride !== "number"
    )
      throw new Error("This configuration has unknown or invalid settings.");
    if (legacy && !["num_switches_left", "num_switches_right"].includes(key))
      throw new Error("This older link contains unsupported settings.");
    if (legacy && valueOverride > 4)
      throw new Error(
        "This older layout uses more than four BILRESAs on a side. The current design supports 0–4; choose a new layout to continue.",
      );
    params[key] = valueOverride;
  }
  if (legacyShade) {
    // Preserve shape and fixture settings. Approximate the former cell area;
    // a single density cannot preserve independent horizontal/vertical counts.
    const cells = Number(value.overrides.cells ?? 20),
      rows = Number(value.overrides.rows ?? 8);
    const diameter =
      (params.bottom_diameter +
        2 * params.middle_diameter +
        params.top_diameter) /
      4;
    const spacing = Math.sqrt(
      (((Math.PI * diameter) / cells) * params.height) / rows,
    );
    params.density = Math.max(
      0,
      Math.min(100, Math.round(((32 - spacing) / 26) * 100)),
    );
  }
  validateParameters(model, params);
  return params;
}
export function parametersFromHash(
  model: ModelDefinition,
  hash: string,
): Parameters {
  if (!hash || hash === "#") return defaults(model);
  const search = new URLSearchParams(hash.replace(/^#/, ""));
  if (
    [...search.keys()].some((key) => key !== "config") ||
    search.getAll("config").length !== 1
  )
    throw new Error("This configuration link is malformed.");
  return decodeConfiguration(model, search.get("config")!);
}
export function configurationUrl(
  model: ModelDefinition,
  params: Parameters,
  currentUrl: string,
): string {
  const url = new URL(currentUrl);
  const encoded = encodeConfiguration(model, params);
  url.hash =
    encoded === encodeConfiguration(model, defaults(model))
      ? ""
      : `config=${encoded}`;
  return url.href;
}
