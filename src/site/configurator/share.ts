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
  const overrides = Object.fromEntries(
    model.parameters
      .filter((p) => params[p.key] !== p.default)
      .map((p) => [p.key, params[p.key]]),
  );
  const json = JSON.stringify({
    schema: 1,
    model: model.id,
    modelRevision: model.revision,
    overrides,
  });
  const bytes = new TextEncoder().encode(json);
  return btoa(Array.from(bytes, (byte) => String.fromCharCode(byte)).join(""))
    .replace(/\+/g, "-")
    .replace(/\//g, "_")
    .replace(/=+$/, "");
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
  try {
    const binary = atob(encoded.replace(/-/g, "+").replace(/_/g, "/"));
    value = JSON.parse(
      new TextDecoder("utf-8", { fatal: true }).decode(
        Uint8Array.from(binary, (c) => c.charCodeAt(0)),
      ),
    );
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
  if (value.modelRevision !== model.revision && !legacy)
    throw new Error(
      "This model revision is unavailable. The design has not been substituted.",
    );
  if (!record(value.overrides))
    throw new Error("This configuration has invalid settings.");
  const params = defaults(model);
  for (const [key, valueOverride] of Object.entries(value.overrides)) {
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
  url.hash = `config=${encodeConfiguration(model, params)}`;
  return url.href;
}
