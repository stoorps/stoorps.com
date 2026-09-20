import { access, readFile, writeFile } from "node:fs/promises";
import { readModels } from "./model-catalog.mjs";
const modelPages = (await readModels())
  .filter(({ catalog }) => catalog.enabled !== false)
  .map(({ catalog }) => `designs/${catalog.id}/index.html`);
for (const route of [
  "index.html",
  "about/index.html",
  ...modelPages,
  "licenses/index.html",
]) {
  const file = `dist/client/${route}`;
  const html = await readFile(file, "utf8");
  if (!html.includes("stoorps") || html.includes("Internal Server Error"))
    throw new Error(`Invalid static page: ${route}`);
}
const home = await readFile("dist/client/index.html", "utf8");
for (const { catalog } of await readModels()) {
  if (catalog.enabled !== false) continue;
  const exists = await access(
    `dist/client/designs/${catalog.id}/index.html`,
  ).then(
    () => true,
    () => false,
  );
  if (exists || home.includes(`/designs/${catalog.id}`))
    throw new Error(
      `Disabled model is still published or linked: ${catalog.id}`,
    );
}
await writeFile("dist/client/.nojekyll", "");
// A plain static 404 does not depend on a server or SPA rewrite.
const base = process.env.SITE_BASE_PATH || "/";
await writeFile(
  "dist/client/404.html",
  `<!doctype html><html lang="en"><meta charset="utf-8"><meta name="viewport" content="width=device-width,initial-scale=1"><title>Not found — stoorps</title><body style="font:18px system-ui;background:#f7f7f1;color:#285b48;padding:10vw"><h1>Nothing here. Yet.</h1><p>This page could not be found.</p><a href="${base}">Back to stoorps</a></body></html>`,
);
console.log("Static pages verified. Publish dist/client only.");
