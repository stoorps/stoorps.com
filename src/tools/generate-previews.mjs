import { mkdir, readFile, writeFile } from 'node:fs/promises';
import { pathToFileURL } from 'node:url';
import path from 'node:path';
import { readModels } from './model-catalog.mjs';
import { renderPreview } from './render-preview.mjs';

await mkdir('src/generated/previews', { recursive: true });
const images = {};
for (const { catalog } of await readModels()) {
  const directory = path.resolve('src/generated/models', catalog.id);
  const module = await import(pathToFileURL(path.join(directory, 'model.js')));
  await module.default({ module_or_path: await readFile(path.join(directory, 'model_bg.wasm')) });
  const embedded = JSON.parse(module.catalog_json());
  if (embedded.id !== catalog.id || embedded.revision !== catalog.revision)
    throw new Error(`${catalog.id}: rebuild WASM before generating artwork (catalogue revision mismatch)`);
  const model = new module.Model(JSON.stringify(Object.fromEntries(catalog.parameters.map(p => [p.key, p.default]))));
  try {
    const parts = catalog.parts.map((_, index) => {
      const part = model.part(index);
      try { return { positions: part.positions().slice(), indices: part.indices().slice() }; }
      finally { part.free(); }
    });
    const svg = renderPreview(parts, catalog.title);
    const file = `${catalog.id}.svg`;
    await writeFile(`src/generated/previews/${file}`, svg);
    images[catalog.id] = file;
    console.log(`Rendered ${catalog.id} V${catalog.revision} outline (${Math.round(svg.length / 1024)} KB)`);
  } finally { model.free(); }
}
await writeFile('src/generated/previews.ts', Object.entries(images).map(([id, file], i) => `import image${i} from './previews/${file}?url';`).join('\n') + '\nexport const previewImages: Record<string, string> = {\n' + Object.keys(images).map((id,i)=>`  ${JSON.stringify(id)}: image${i},`).join('\n') + '\n};\n');
