import { defineConfig } from "vite";
import { modelCatalogPlugin } from "./generate-models.mjs";
export default defineConfig({
  plugins: [modelCatalogPlugin()],
  server: { host: "127.0.0.1", port: 5175 },
});
