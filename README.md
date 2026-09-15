# stoorps

A personal site with model-scoped Rust CAD packages, a shared WASM engine and a TanStack Start configurator.

## Layout

```text
models/
  bilresa/
    Cargo.toml          # independently buildable model crate
    catalog.toml        # identity, revision, controls, parts and page copy
    model.rs            # geometry and the model's WASM entry point
    build.rs            # generates Rust constants from the catalogue
    reference/          # original Onshape source and comparison meshes
    examples/           # native export probe
src/
  engine/               # shared meshing, Part exports, STEP inspection
  site/                 # TanStack pages, React configurator, public assets
  tools/                # discovery, compilation, packaging and build checks
  tests/                # link/catalogue/CAD validation and box fixture
  generated/            # ignored: catalogue, model loaders, per-model WASM
Cargo.toml              # Cargo workspace and shared dependencies
justfile                # optional shortcuts
```

Standard npm, TypeScript and Vite configuration stays at the root. Research and validation notes live in `docs/`; generated builds/results live in ignored `dist/`, `target/` and `artifacts/` directories.

## Run

Node 24, npm and Docker are required for the WASM build.

```sh
npm ci
npm run wasm:build
npm run dev
```

Open the URL printed by Vite, normally http://127.0.0.1:5173. Routes include `/`, `/about` and `/designs/bilresa`. Rebuild WASM after changing Rust or geometry-relevant metadata (defaults, limits, parameter keys or part identities). The development server regenerates its catalogue when TOML changes. Titles, descriptions, labels and colours update without recompiling; geometry-contract changes are rejected until WASM is rebuilt.

## Check and build

```sh
npm run test:all
npm run build
npm run typecheck
npm run preview:static
```

The preview serves `dist/client` on port 4174. For a repository Pages site use `SITE_BASE_PATH=/repository-name/ npm run build`. Only publish `dist/client`; no application server is required.

`just wasm`, `just dev`, `just test`, `just build` and `just preview` wrap these commands if just is installed. `just rust-test` runs native Rust tests in a separate cache, avoiding the Docker-owned WASM build directory. Independent CAD readers can be run using `src/tests/validate_exports.py` and `src/tests/validate_bilresa.py` after generating test exports.

## Models

Each directory in `models/` contains a Cargo crate and `catalog.toml`. Build tooling discovers these directories, validates their metadata and generates the site catalogue and lazy module loaders. Each crate compiles to its own WASM module; the browser loads only the selected model. Static model pages are generated from the same catalogue.

See [the BILRESA package](models/bilresa/README.md) for the model contract and authoring guidance. The catalogue is the source of truth for the exposed count defaults/limits and UI metadata. BILRESA's fixed mechanical measurements remain in `model.rs`.

BILRESA revision 2 generates three parts, supports independent counts of 0–4, and exposes plate dimensions and advanced fit/structure settings. The preview offers Solid, Outline and orthographic Blueprint modes with optional measurements. Physical fit and the 0.3 mm adhesive allowance are not yet verified. Shared links record the model ID, schema version, model revision and parameter overrides.

## Cleanup and references

The obsolete root demo pages, `web/` UI/worker, central handwritten `catalog.ts`, old build scripts and old browser harnesses have been removed. The box remains only as a regression fixture under `src/tests/fixtures/box`, excluded from the public model catalogue and production assets.

- `docs/ARCHITECTURE.md`: boundaries, schema and platform direction.
- `docs/VALIDATION.md`: executed checks and limitations.
- `docs/SPIKE.md`: historical investigation notes.
- `models/bilresa/reference/README.md`: design provenance.
- `npm run dev:tests`: serves `/src/tests/product-browser.html` on port 5175.

The deployment workflow remains manual. The existing stoorps.com repository, DNS and Vercel deployment have not been changed.

Catalogue artwork is generated automatically during `npm run build` from each model's default WASM geometry. It uses the same outline palette, angle and transparency as the configurator, and is served as a static SVG. To refresh images alone, run `npm run previews:generate` after `npm run wasm:build`. No manually maintained model illustrations are needed.
