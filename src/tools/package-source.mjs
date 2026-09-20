import { readFile, writeFile, readdir, mkdir } from "node:fs/promises";
import { zipSync } from "fflate";
async function sourceFiles(root) {
  const files = [];
  for (const entry of await readdir(root, { withFileTypes: true })) {
    const name = `${root}/${entry.name}`;
    if (entry.isDirectory() && entry.name !== "reference")
      files.push(...(await sourceFiles(name)));
    else if (entry.isFile() && /\.(rs|toml|ya?ml|mjs|md)$/.test(name)) files.push(name);
  }
  return files;
}
const files = [
  "Cargo.toml",
  "Cargo.lock",
  "package.json",
  "package-lock.json",
  "src/tools/build-wasm.mjs",
  "src/tools/generate-models.mjs",
  "src/tools/model-catalog.mjs",
  "src/tools/catalog-yaml.mjs",
  "src/tools/catalog-build.rs",
  ...(await sourceFiles("models")),
  ...(await sourceFiles("src/engine")),
  ...(await sourceFiles("src/tests/fixtures")),
];
const archive = Object.fromEntries(
  await Promise.all(
    files.map(async (file) => [file, new Uint8Array(await readFile(file))]),
  ),
);
await mkdir("src/site/public/notices", { recursive: true });
await writeFile(
  "src/site/public/notices/geometry-source.zip",
  zipSync(archive),
);
