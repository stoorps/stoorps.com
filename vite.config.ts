import { defineConfig } from "vite";
import { tanstackStart } from "@tanstack/react-start/plugin/vite";
import react from "@vitejs/plugin-react";
import { modelCatalogPlugin } from "./src/tools/generate-models.mjs";
import { readModels } from "./src/tools/model-catalog.mjs";
const modelPages = (await readModels()).map(({ catalog }) => ({
  path: `/designs/${catalog.id}`,
}));
const base = process.env.SITE_BASE_PATH || "/";
if (!base.startsWith("/") || !base.endsWith("/") || base.includes(".."))
  throw new Error("SITE_BASE_PATH must start and end with /");
export default defineConfig({
  base,
  worker: { format: "es" },
  publicDir: "src/site/public",
  server: { host: "127.0.0.1", port: 5173 },
  plugins: [
    modelCatalogPlugin(),
    tanstackStart({
      srcDirectory: "src/site",
      router: { basepath: base === "/" ? "/" : base.slice(0, -1) },
      pages: modelPages,
      prerender: {
        enabled: true,
        autoSubfolderIndex: true,
        crawlLinks: false,
        failOnError: true,
      },
    }),
    react(),
  ],
});
