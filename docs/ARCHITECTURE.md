# stoorps: personal site with a reusable configurator

## Decision

Use TanStack Start + React + TypeScript. The immediate release is a personal site; the intended next direction is a creator platform. Static prerendering targets GitHub Pages now, without committing the configurator to a server runtime. The existing stoorps.com/Vercel deployment has not been altered.

## Boundaries

- `models/<id>/`: independently buildable model crate, `model.rs`, `catalog.toml`, optional reference files and examples. BILRESA is the first model.
- `src/engine/`: reusable Part, mesh/STEP/STL exports, STEP reimport and WASM runtime initialisation. It has no model-specific dependencies.
- `src/site/`: personal pages, generic model page, React configurator, typed worker client and URL codec.
- `src/tools/`: discovers model folders, validates TOML, generates catalogue/loaders and compiles each crate independently. Static model routes are derived from discovery.
- `src/generated/`: ignored build outputs, including one WASM module per model. The worker loads only the requested model, compares its compiled geometry contract against the site's metadata, then constructs it through a common ABI.
- `src/tests/`: all regression tests; the box is a separate fixture crate and is not shipped by the site.

The shared engine is shared Rust source, not a separately downloaded kernel: each model WASM currently statically links OCCT. This gives independent model builds but repeats the kernel across different model downloads. Only the selected model is fetched.

Source metadata is owned by each model. BILRESA's build script derives Rust count defaults/limits and its exported catalogue from TOML; fixed mechanical dimensions remain Rust code. The generated TypeScript catalogue is an index, not a second hand-maintained source of truth.

The old root HTML demos, `web/` UI and central hand-written catalogue were deleted. Standard package/configuration files remain at root for their tools. Public assets live under `src/site/public`; reference meshes are scoped to BILRESA.

## Links and revisions

A model route identifies the page. `#config=` contains base64url-encoded UTF-8 JSON:

```json
{"schema":1,"model":"bilresa","modelRevision":1,"overrides":{"num_switches_left":2,"num_switches_right":0}}
```

Defaults are tied to the model revision. Only deviations are stored. Valid completed builds replace the current history entry without adding one per edit. Copy Link uses the same codec; on clipboard failure a selectable URL is shown. Bad data, duplicate fields in the URL fragment, unknown parameters, fractional/out-of-range counts and unsupported schema/model revisions are rejected. Unsupported revisions never silently use today's geometry. The reset action explicitly lets the visitor start with the current defaults.

Revision 1 is the only implementation so far. Before changing geometry or defaults, add a new immutable revision and retain a dispatcher for revision 1. Merely incrementing a number while replacing the only implementation would break existing links. Schema migration and geometry revision migration are separate concerns. Base64 is neither compression nor encryption.

## Worker and lifecycle

The configurator loads after hydration; personal pages don't initialise the CAD engine. Mesh buffers are transferred from the worker to the viewer. Parameter edits are debounced and stale build responses are discarded. Validation/pending builds disable exports and sharing. Export batches disable parameter changes to preserve one configuration across files. Leaving the page terminates the worker and disposes renderer, controls, materials and geometry. A 60-second timeout or worker failure rejects pending requests, with explicit restart in the UI.

STL ZIPs contain one file per part, configuration metadata and units/fit notes. Export filenames include model revision, switch counts and part identity. Separated preview transforms never alter exported coordinates.

## Static hosting

`npm run build` generates prerendered routes and verifies their HTML. Publish only `dist/client`, never the temporary SSR build in `dist/server`. `SITE_BASE_PATH=/` targets a custom domain or user Pages site; `/repo-name/` targets a repository Pages site. No catch-all server rewrite is needed. A minimal 404 and `.nojekyll` are generated.

The Pages workflow validates pull requests and deploys only when manually dispatched. Set repository variable `SITE_BASE_PATH` before using a repository subpath. There is no Git remote configured in this workspace yet; the existing personal-site repository has not been selected or modified.

## Next creator-platform step

Add creator identity/ownership and a publishable model manifest first. Then choose authentication, storage and publication states. Creator-uploaded source requires a dedicated isolated build pipeline, quotas and revision storage; it must not run inside the web server process. The current registry is intentionally a repository-maintained catalogue, not a claim that arbitrary models can already be uploaded.
