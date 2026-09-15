import { defineConfig } from "vite";
export default defineConfig({
  build: {
    rollupOptions: { input: { box: "index.html", bilresa: "bilresa.html" } },
  },
});
