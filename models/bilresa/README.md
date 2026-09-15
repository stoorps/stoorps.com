# BILRESA model package

- `model.rs` builds the main body, blanking plate and cover top. Fixed mechanical measurements live here.
- `catalog.toml` owns the model ID/revision, exposed parameter defaults and limits, part names/colours and model page copy.
- `build.rs` reads that catalogue at compile time. The Rust parameter defaults/limits and exported `catalog_json()` derive from the same TOML used by the website.
- `reference/` contains the original Onshape source and reference meshes. These are validation input, not runtime geometry.

Run from the workspace root:

```sh
CARGO_TARGET_DIR=artifacts/native-target cargo test -p model-bilresa
CARGO_TARGET_DIR=artifacts/native-target cargo run -p model-bilresa --example bilresa_probe -- 2 0
npm run wasm:build -- --models-only
```

The native probe writes its STEP/STL exports into `artifacts/` (create that directory first on a clean checkout). WASM output is generated under `src/generated/models/bilresa/` and is not committed.

## Adding a model

Create another folder under `models/` with its own `Cargo.toml`, `catalog.toml` and `model.rs`. The Cargo workspace includes `models/*`; npm tooling discovers all model catalogues and generates their lazy loaders and static `/designs/<id>` pages. The homepage is editorial and can feature selected models separately.

A model module exports the shared ABI in `src/site/configurator/protocol.ts`:

- Default WASM initialiser.
- `catalog_json()` returning the metadata it was compiled against.
- `Model(parametersJson)` validating parameters and building solids.
- `Model.part(index)` returning a `model_engine::Part`, in catalogue part order.
- `Model.step()` exporting the complete assembly.
- `Model.step_selected(indices)` exporting a validated subset.
- `Model.measurements_json()` returning nominal measurement labels, values and endpoints.

Use `model-engine` for meshing/export and call its `initialize()` from the model's WASM start function. The shared engine has no dependency on BILRESA or any model list. Match metadata to the actual geometry; the worker rejects mismatched compiled/source catalogues.

## Supported configuration (revision 2)

- Counts: 0–4 independently on each side.
- Plate width: 75–120 mm; height: 80–120 mm; depth: 6–10 mm.
- Advanced: remote/plate/rail/cover clearances, magnetic plate clearance and adhesive recess, wall thickness and remote spacing. Exact ranges and steps live in TOML.

Defaults retain the original shape. Outer height follows the plate height with the reference 2.31 mm edge wall at default thickness. Rails and blanking plate follow height independently of width, retaining the reference 0.125 mm stopper relief. At tight rail clearance, the blanking plate stops at least 0.025 mm inside the central opening to avoid intersecting the body.

Compatible count-only revision-1 links migrate to current defaults. Counts above four are explicitly rejected. Revision-1 binaries are not retained; future incompatible revisions require a deliberate versioning strategy.

The boundary suite covers six dimensional/tolerance combinations plus seven count layouts. It is not exhaustive across all combinations. Physical printing and adhesive fit remain unverified.
