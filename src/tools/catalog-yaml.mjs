import { readFile, realpath } from "node:fs/promises";
import path from "node:path";
import { parse } from "yaml";

/** Local file references replace their mapping; referenced lists splice into lists. */
export async function readCatalog(file) {
  const root = await realpath(path.dirname(file));
  async function load(file, stack) {
    const actual = await realpath(file);
    const relative = path.relative(root, actual);
    if (
      relative.startsWith(".." + path.sep) ||
      relative === ".." ||
      path.isAbsolute(relative)
    )
      throw new Error(`Catalogue reference must stay inside ${root}: ${file}`);
    if (stack.includes(actual))
      throw new Error(
        `Circular catalogue reference: ${[...stack, actual].join(" -> ")}`,
      );
    const value = parse(await readFile(actual, "utf8"), {
      merge: true,
      maxAliasCount: 100,
    });
    async function resolve(value, parents = []) {
      if (value && typeof value === "object") {
        if (parents.includes(value))
          throw new Error(`Circular YAML alias in ${actual}`);
        parents = [...parents, value];
      }
      if (Array.isArray(value)) {
        const result = [];
        for (const item of value) {
          const resolved = await resolve(item, parents);
          if (
            item &&
            typeof item === "object" &&
            "$ref" in item &&
            Array.isArray(resolved)
          )
            result.push(...resolved);
          else result.push(resolved);
        }
        return result;
      }
      if (value && typeof value === "object") {
        if ("$ref" in value) {
          const ref = value.$ref;
          if (
            Object.keys(value).length !== 1 ||
            typeof ref !== "string" ||
            !ref ||
            path.isAbsolute(ref) ||
            ref.includes(":") ||
            ref.includes("#")
          )
            throw new Error(`Invalid local catalogue reference in ${actual}`);
          return load(path.resolve(path.dirname(actual), ref), [
            ...stack,
            actual,
          ]);
        }
        return Object.fromEntries(
          await Promise.all(
            Object.entries(value).map(async ([k, v]) => [
              k,
              await resolve(v, parents),
            ]),
          ),
        );
      }
      return value;
    }
    return resolve(value);
  }
  return normalizeGroups(await load(file, []));
}

/** Keep the runtime contract flat while authoring each group only once. */
export function normalizeGroups(catalog) {
  if (
    !catalog ||
    typeof catalog !== "object" ||
    !("parameter_groups" in catalog)
  )
    return catalog;
  if (
    "parameters" in catalog ||
    !Array.isArray(catalog.parameter_groups) ||
    !catalog.parameter_groups.length
  )
    throw new Error(
      "Use a non-empty parameter_groups list instead of parameters",
    );
  const names = new Set();
  const parameters = catalog.parameter_groups.flatMap((group) => {
    if (
      !group ||
      typeof group !== "object" ||
      Array.isArray(group) ||
      Object.keys(group).some(
        (key) => !["group_name", "parameters"].includes(key),
      ) ||
      typeof group.group_name !== "string" ||
      !group.group_name.trim() ||
      names.has(group.group_name) ||
      !Array.isArray(group.parameters) ||
      !group.parameters.length
    )
      throw new Error(
        "Each parameter group needs a unique group_name and a non-empty parameters list",
      );
    names.add(group.group_name);
    return group.parameters.map((parameter) => {
      if (
        !parameter ||
        typeof parameter !== "object" ||
        Array.isArray(parameter) ||
        "group" in parameter
      )
        throw new Error(
          "Grouped parameters inherit group_name; do not set group on individual parameters",
        );
      return { ...parameter, group: group.group_name };
    });
  });
  const { parameter_groups, ...metadata } = catalog;
  return { ...metadata, parameters };
}
