# stoorps

A personal site with model-scoped Rust CAD packages, a shared WASM engine and a TanStack Start configurator.

## Layout

```text
models/
  bilresa/
    Cargo.toml          # independently buildable model crate
    catalog.yml        # identity, revision, controls, parts and page copy
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

Open the URL printed by Vite, normally http://127.0.0.1:5173. Routes include `/`, `/about` and `/designs/bilresa`. Rebuild WASM after changing Rust or geometry-relevant metadata (defaults, limits, parameter keys or part identities). The development server regenerates its catalogue when YAML changes. Titles, descriptions, labels and colours update without recompiling; geometry-contract changes are rejected until WASM is rebuilt.

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

Each model directory in `models/` contains a Rust Cargo crate and `catalog.yml`. The catalogue selects `backend: cadrum` for CAD solids or `backend: manifold` for triangle-mesh solids. Build tooling discovers these directories, validates their metadata and generates the site catalogue and lazy module loaders. Each crate compiles to its own WASM module; the browser loads only the selected model. Static model pages are generated from the same catalogue. Set `enabled: false` to hide a model from the homepage and disable its design route and static page; omitted flags default to enabled. Disabled models remain available to build and test tooling.

Homepage cards are generated from enabled catalogue entries. They always use the
catalogue `title` and `description`; no per-model homepage code is required.
Optional presentation fields:

- `status`: `work-in-progress` or `ready`; omitted means no status pill.
- `status_note`: explanatory text shown on both the card and model page; requires
  `status`. Work in Progress has a generic fallback note when this is omitted.
- `card_label`: category pill text, such as `Configurable · Woodworking`.
- `card_link_text`: call to action; defaults to `Configure`.
- `artwork_caption` / `artwork_caption_bottom`: optional preview captions.
- `display_order`: integer, ascending; omitted entries follow ordered ones,
  with model ID breaking ties.

Status describes readiness independently of visibility. These presentation fields
do not alter geometry, revision numbers or existing configuration links.

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

## Curve lampshade

`/designs/lampshade` provides profile presets and draggable diameters, a rippled shell or diamond lattice, mounting offset/orientation, a coarse-thread adapter and fit-test collar. Print setup provides guidance without saving profiles. This model uses the pure Rust `manifold-rust` kernel compiled to WASM and exports STL; cadrum models retain STEP. Both backends share the geometry worker and viewer interface. See [the model documentation](models/lampshade/README.md) for dimensions, validation and physical-test limitations.

Set `default_view = "solid"` or `default_view = "outline"` in a model catalogue to choose its initial preview style. If omitted, it defaults to Outline. This is presentation metadata and does not change model revisions or geometry.

### Compact configuration links

New links use Base64url-encoded binary format 2: a version byte, model `share_id`, model revision, then pairs of parameter `share_id` and integer ticks from its minimum. Integers use unsigned base-128 varints. Only changed parameters are included, sorted by sharing ID. Default configurations omit the hash and follow the current catalogue defaults. Existing JSON format-1 links remain readable, including supported revision migrations.

Model sharing IDs must be globally unique; parameter sharing IDs must be unique within a model. These are permanent identities: do not renumber or reuse them when reordering/removing catalogue entries. Changing defaults, minimums, steps or parameter meaning requires a model revision bump. Unsupported compact revisions are rejected rather than silently interpreted with a newer catalogue.

### Catalogue sections and references

Catalogues use YAML (`catalog.yml`). Large catalogues can put parameter groups in
`catalog/shape.yml`, `catalog/pattern.yml`, etc., and include them in order:

```yaml
parameter_groups:
  - $ref: catalog/shape.yml
  - $ref: catalog/pattern.yml
```

Each section has a group root, for example `catalog/shape.yml`:

```yaml
group_name: Shape
parameters:
  - share_id: 1
    key: height
    label: Shade height
    default: 180
    min: 80
    max: 280
    step: 1
    unit: mm
```

Groups can also be written inline under `parameter_groups`. Each group name must
be unique; parameters inherit it, so individual entries should not specify
`group`. The loaders flatten groups into the existing runtime parameter list,
preserving group and parameter order. Do not mix `parameters` and
`parameter_groups` at the catalogue root.

A reference replaces
its mapping; when a referenced list appears inside a list, its entries are
spliced into that list. References resolve relative to the containing file and
must stay inside the model directory. Nested references are supported; cycles,
missing files, remote URLs, fragments and sibling fields on `$ref` mappings are
rejected. Standard YAML anchors, aliases and `<<` merges work within each file;
anchors do not cross file boundaries. Both the JavaScript catalogue loader and
Rust build scripts resolve the same structure. Editing section files triggers a
preview reload and Cargo rebuild. Keep parameter `share_id` values unchanged.
