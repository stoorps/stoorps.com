import { mkdir, writeFile, readFile } from "node:fs/promises";
import { fileURLToPath } from "node:url";
import { readModels } from "./model-catalog.mjs";
async function writeChanged(file, content) {
  if ((await readFile(file, "utf8").catch(() => null)) !== content)
    await writeFile(file, content);
}
export async function generateModels() {
  const entries = await readModels();
  await mkdir("src/generated", { recursive: true });
  await writeChanged(
    "src/generated/catalog.ts",
    `// Generated from models/*/catalog.toml. Do not edit.\nimport type { ModelDefinition } from '../site/models/types';\nexport const models: readonly ModelDefinition[] = ${JSON.stringify(
      entries.map((e) => e.catalog),
      null,
      2,
    )};\nexport function getModel(id: string): ModelDefinition { const model=models.find(m=>m.id===id); if(!model) throw new Error('Unknown model: '+id); return model; }\n`,
  );
  const loaders = entries
    .map(
      (e) =>
        `  ${JSON.stringify(e.catalog.id)}: () => import('./models/${e.catalog.id}/model.js'),`,
    )
    .join("\n");
  await writeChanged(
    "src/generated/model-loaders.ts",
    `// Generated worker entry points; no WASM is loaded by catalogue pages.\nimport type { ModelModule } from '../site/configurator/protocol';\nconst loaders: Record<string, () => Promise<ModelModule>> = {\n${loaders}\n};\nexport function loadModelModule(id: string) { const load=loaders[id]; if(!load) throw new Error('Unknown model: '+id); return load(); }\n`,
  );
  return entries;
}
if (process.argv[1] === fileURLToPath(import.meta.url)) {
  await generateModels();
  console.log("Generated catalogue and model loaders from TOML.");
}
export function modelCatalogPlugin() {
  return {
    name: "model-catalog",
    async buildStart() {
      await generateModels();
    },
    configureServer(server) {
      server.watcher.add("models/**/catalog.toml");
      server.watcher.on("all", async (event, file) => {
        if (
          !file
            .replaceAll("\\", "/")
            .match(/models\/[^/]+\/(catalog\.toml|Cargo\.toml)$/)
        )
          return;
        try {
          await generateModels();
          server.ws.send({ type: "full-reload" });
        } catch (error) {
          server.config.logger.error(String(error));
          server.ws.send({
            type: "error",
            err: { message: String(error), stack: "" },
          });
        }
      });
    },
  };
}
