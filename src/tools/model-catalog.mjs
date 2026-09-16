import { readFile, readdir } from "node:fs/promises";
import path from "node:path";
import { parse } from "smol-toml";
export function validateCatalog(value, directory) {
  if (!value || typeof value !== "object" || Array.isArray(value))
    throw new Error("Catalogue must be a TOML table");
  const allowed = [
    "id",
    "revision",
    "title",
    "description",
    "camera",
    "default_view",
    "parameters",
    "parts",
    "configuration_hint",
    "parameter_note",
    "overview",
    "print_notes",
    "source_url",
    "runtime",
    "formats",
  ];
  if (Object.keys(value).some((k) => !allowed.includes(k)))
    throw new Error(`${directory}: unknown catalogue field`);
  if (!/^[a-z][a-z0-9-]*$/.test(value.id) || value.id !== directory)
    throw new Error(`${directory}: id must match its directory`);
  if (!Number.isSafeInteger(value.revision) || value.revision < 1)
    throw new Error(`${directory}: invalid revision`);
  for (const key of ["title", "description"])
    if (typeof value[key] !== "string" || !value[key].trim())
      throw new Error(`${directory}: missing ${key}`);
  for (const key of [
    "configuration_hint",
    "parameter_note",
    "overview",
    "print_notes",
    "source_url",
  ])
    if (value[key] !== undefined && typeof value[key] !== "string")
      throw new Error(`${directory}: invalid ${key}`);
  if (
    value.source_url &&
    !["https:", "http:"].includes(new URL(value.source_url).protocol)
  )
    throw new Error(`${directory}: invalid source URL`);
  if (value.runtime !== undefined && value.runtime !== "mesh")
    throw new Error(`${directory}: invalid runtime`);
  if (
    value.formats !== undefined &&
    (!Array.isArray(value.formats) ||
      !value.formats.length ||
      value.formats.some((f) => !["stl", "step"].includes(f)))
  )
    throw new Error(`${directory}: invalid formats`);
  if (value.default_view !== undefined && !["solid", "outline"].includes(value.default_view))
    throw new Error(`${directory}: invalid default view`);
  if (value.camera !== undefined) {
    const table = value.camera;
    if (
      !table ||
      typeof table !== "object" ||
      Array.isArray(table) ||
      Object.keys(table).some((key) => !["desktop", "mobile"].includes(key))
    )
      throw new Error(`${directory}: invalid camera table`);
    for (const preset of Object.values(table)) {
      if (
        !preset ||
        typeof preset !== "object" ||
        Object.keys(preset).some(
          (key) => !["direction", "zoom", "pan"].includes(key),
        ) ||
        !Array.isArray(preset.direction) ||
        preset.direction.length !== 3 ||
        !preset.direction.every(Number.isFinite) ||
        Math.hypot(...preset.direction) < 0.001 ||
        Math.hypot(...preset.direction.slice(0, 2)) < 0.001 ||
        !Number.isFinite(preset.zoom) ||
        preset.zoom < 0.2 ||
        preset.zoom > 10 ||
        !Array.isArray(preset.pan) ||
        preset.pan.length !== 2 ||
        !preset.pan.every((n) => Number.isFinite(n) && Math.abs(n) <= 1)
      )
        throw new Error(`${directory}: invalid camera preset`);
    }
  }
  if (!Array.isArray(value.parameters) || !value.parameters.length)
    throw new Error(`${directory}: parameters are required`);
  const keys = new Set();
  for (const p of value.parameters) {
    if (
      typeof p.key !== "string" ||
      !/^[a-z][a-z0-9_]*$/.test(p.key) ||
      keys.has(p.key) ||
      typeof p.label !== "string" ||
      !p.label.trim()
    )
      throw new Error(`${directory}: invalid or duplicate parameter`);
    keys.add(p.key);
    for (const key of ["unit", "group", "help"])
      if (p[key] !== undefined && typeof p[key] !== "string")
        throw new Error(`${directory}: invalid parameter ${key}`);
    if (
      p.options !== undefined &&
      (!Array.isArray(p.options) ||
        p.options.length !== p.max - p.min + 1 ||
        p.step !== 1 ||
        p.options.some((o) => typeof o !== "string" || !o))
    )
      throw new Error(`${directory}: invalid options`);
    if (p.advanced !== undefined && typeof p.advanced !== "boolean")
      throw new Error(`${directory}: invalid advanced flag`);
    if (
      Object.keys(p).some(
        (k) =>
          ![
            "key",
            "label",
            "default",
            "min",
            "max",
            "step",
            "unit",
            "group",
            "advanced",
            "help",
            "options",
          ].includes(k),
      )
    )
      throw new Error(`${directory}: unknown parameter field`);
    if (
      ![p.default, p.min, p.max, p.step].every(Number.isFinite) ||
      p.step <= 0 ||
      p.min > p.max ||
      p.default < p.min ||
      p.default > p.max ||
      Math.abs(
        (p.default - p.min) / p.step - Math.round((p.default - p.min) / p.step),
      ) > 1e-8
    )
      throw new Error(`${directory}: invalid parameter range/default`);
  }
  if (!Array.isArray(value.parts) || !value.parts.length)
    throw new Error(`${directory}: parts are required`);
  const parts = new Set();
  for (const p of value.parts) {
    if (
      typeof p.id !== "string" ||
      !/^[a-z][a-z0-9-]*$/.test(p.id) ||
      parts.has(p.id) ||
      typeof p.name !== "string" ||
      !p.name.trim() ||
      !Number.isInteger(p.color) ||
      p.color < 0 ||
      p.color > 0xffffff ||
      Object.keys(p).some((k) => !["id", "name", "color"].includes(k))
    )
      throw new Error(`${directory}: invalid or duplicate part`);
    parts.add(p.id);
  }
  return value;
}
export async function readModels(root = process.cwd()) {
  const directory = path.join(root, "models");
  const entries = (await readdir(directory, { withFileTypes: true }))
    .filter((e) => e.isDirectory())
    .sort((a, b) => a.name.localeCompare(b.name));
  return Promise.all(
    entries.map(async (entry) => {
      const folder = path.join(directory, entry.name);
      const catalog = validateCatalog(
        parse(await readFile(path.join(folder, "catalog.toml"), "utf8")),
        entry.name,
      );
      if (catalog.runtime === "mesh") {
        await readFile(path.join(folder, "model.mjs"), "utf8");
        return { catalog, crate: null, folder };
      }
      const manifest = parse(
        await readFile(path.join(folder, "Cargo.toml"), "utf8"),
      );
      if (!/^[a-z][a-z0-9-]*$/.test(manifest.package?.name))
        throw new Error(`${entry.name}: invalid crate name`);
      return { catalog, crate: manifest.package.name, folder };
    }),
  );
}
