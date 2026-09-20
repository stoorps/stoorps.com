# stoorps: personal site with a reusable configurator

## Decision

Use TanStack Start + React + TypeScript. The immediate release is a personal site; the intended next direction is a creator platform. Static prerendering targets GitHub Pages now, without committing the configurator to a server runtime. The existing stoorps.com/Vercel deployment has not been altered.

## Boundaries

- `models/<id>/`: independently buildable model crate, `model.rs`, `catalog.yml`, optional reference files and examples. BILRESA is the first model.
- `src/engine/`: reusable Part, mesh/STEP/STL exports, STEP reimport and WASM runtime initialisation. It has no model-specific dependencies.
- `src/site/`: personal pages, generic model page, React configurator, typed worker client and URL codec.
- `src/tools/`: discovers model folders, validates YAML, generates catalogue/loaders and compiles each crate independently. Static model routes are derived from discovery.
- `src/generated/`: ignored build outputs, including one WASM module per model. The worker loads only the requested model, compares its compiled geometry contract against the site's metadata, then constructs it through a common ABI.
- `src/tests/`: all regression tests; the box is a separate fixture crate and is not shipped by the site.

The shared engine is shared Rust source, not a separately downloaded kernel: each model WASM currently statically links OCCT. This gives independent model builds but repeats the kernel across different model downloads. Only the selected model is fetched.

Source metadata is owned by each model. BILRESA's build script derives exposed numeric defaults/limits and its exported catalogue from YAML. Units, groups, help text and Advanced flags are presentation metadata; fixed mechanical details remain Rust code. The generated TypeScript catalogue is an index, not a second hand-maintained source of truth.

The old root HTML demos, `web/` UI and central hand-written catalogue were deleted. Standard package/configuration files remain at root for their tools. Public assets live under `src/site/public`; reference meshes are scoped to BILRESA.

## Links and revisions

A model route identifies the page. `#config=` contains base64url-encoded UTF-8 JSON:

```json
{"schema":1,"model":"bilresa","modelRevision":2,"overrides":{"num_switches_left":2,"num_switches_right":0}}
```

Defaults are tied to the model revision. Only deviations are stored. Valid completed builds replace the current history entry without adding one per edit. Copy Link uses the same codec; on clipboard failure a selectable URL is shown. Bad data, duplicate fields in the URL fragment, unknown parameters, fractional/out-of-range counts and unsupported schema/model revisions are rejected. Unsupported revisions never silently use today's geometry. The reset action explicitly lets the visitor start with the current defaults.

Revision 2 adds dimensional and tolerance controls and reduces counts to 0–4. Revision 1 links containing only supported counts migrate explicitly to revision 2 defaults, which preserve the original count-only shapes. Older counts above four are rejected with an explanation, never clamped. Historical revision-1 WASM is not hosted. Future incompatible geometry changes need retained versioned modules or an explicit user-facing migration. Schema migration and geometry revision migration are separate concerns. Base64 is neither compression nor encryption.

## Worker and lifecycle

The configurator loads after hydration; personal pages don't initialise the CAD engine. Mesh buffers are transferred from the worker to the viewer. Parameter edits are debounced; queued builds are coalesced to the newest edit, and stale responses are discarded. Validation/pending builds disable exports and sharing. Export batches disable parameter changes to preserve one configuration across files. Leaving the page terminates the worker and disposes renderer, controls, materials and geometry. A 60-second timeout or worker failure rejects pending requests, with explicit restart in the UI.

Visibility and export selection are independent arrays in catalogue part order. STEP supports any nonempty subset of parts. STL ZIPs contain selected parts, configuration metadata (including part IDs) and units/fit notes. Export filenames include model revision and part identity; full settings are in the ZIP metadata and shared link. Separated preview transforms never alter exported coordinates.

## Static hosting

`npm run build` generates prerendered routes and verifies their HTML. Publish only `dist/client`, never the temporary SSR build in `dist/server`. `SITE_BASE_PATH=/` targets a custom domain or user Pages site; `/repo-name/` targets a repository Pages site. No catch-all server rewrite is needed. A minimal 404 and `.nojekyll` are generated.

The Pages workflow validates pull requests and deploys only when manually dispatched. Set repository variable `SITE_BASE_PATH` before using a repository subpath. There is no Git remote configured in this workspace yet; the existing personal-site repository has not been selected or modified.

## Next creator-platform step

Add creator identity/ownership and a publishable model manifest first. Then choose authentication, storage and publication states. Creator-uploaded source requires a dedicated isolated build pipeline, quotas and revision storage; it must not run inside the web server process. The current registry is intentionally a repository-maintained catalogue, not a claim that arbitrary models can already be uploaded.

## Viewer and parameters

The generic parameter controls support decimal inputs, steppers and sliders, grouped by model YAML. Basic controls are always visible; Advanced settings are collapsible. Numeric limits and increments are validated in both TypeScript and Rust; model-specific relational checks run in Rust.

The viewer owns Solid, Outline and Blueprint presentation. It preserves camera position during regeneration and fades in the new mesh over 200 ms (disabled for reduced motion). Outline uses thresholded tessellation edges and an expanded back-face silhouette, with opaque background-coloured faces for occlusion; this is not exact CAD hidden-line removal or a technical drawing export. Blueprint uses orthographic front/top/side cameras. Switching back restores the previous 3D camera.

Measurements use assembled positions even during exploded previews. Nominal plate measurements come from `Model.measurements_json()`; overall and individual-part sizes come from mesh extrema, displayed to two decimals. These overall values are approximate to tessellation accuracy, not CAD metrology. The native CAD library's conservative bounding boxes overestimate some filleted surfaces and are unsuitable for these labels.

The desktop workspace grows with viewport height, with a scrollable control panel. Mobile uses ordinary page flow and a preview above the controls. Header, model title band and footer share the deep green theme.

Outline and Blueprint share a dark palette and model-centred grid. The grid rotates into XY, XZ or YZ for the selected blueprint direction; its radial fade is scaled to the model bounds in that plane. A 0–100% transparency slider fades surfaces and blends an additional depth-independent edge pass, making hidden geometry readable. These are preview-only settings. Measurements default on in both drawing modes; an explicit toggle overrides that default for the lifetime of the mounted viewer.

### Generated catalogue artwork

`npm run build` (and `predev`) runs `src/tools/generate-previews.mjs` after catalogue generation. It loads each built model's WASM in Node, constructs the default parameter configuration, and renders all parts into `src/generated/previews/<id>.svg`. Missing WASM or a catalogue revision mismatch fails the build; rebuild geometry with `npm run wasm:build` after model changes. `npm run previews:generate` can refresh artwork independently.

The renderer projects actual mesh geometry with a CPU depth buffer, extracting sharp edges and view-dependent silhouettes. Hidden edges are drawn at the shared outline transparency. Camera direction, colours, edge threshold and transparency defaults are shared with the live viewer through `outline-style.mjs`. This is a deterministic vector interpretation of the outline style, not a WebGL screenshot: shader antialiasing and measurement annotations are omitted; a projected, radially fading grid is included, and thicker strokes keep thumbnails legible. It needs no browser, GPU, external rendering service or new runtime dependency.

Generated images are ignored build outputs. Vite imports them through the generated preview manifest, giving each image a content-hashed URL with base-path support. Catalogue pages load static images only; they do not initialise the CAD engine. The homepage's featured model selection remains editorial; image generation discovers every model automatically.
