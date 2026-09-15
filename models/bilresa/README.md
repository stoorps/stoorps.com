# BILRESA model package

- `model.rs` builds the main body, blanking plate and cover top. Fixed mechanical measurements live here.
- `catalog.toml` owns the model ID/revision, exposed parameter defaults and limits, part names/colours and model page copy.
- `build.rs` reads that catalogue at compile time. The Rust count defaults/limits and exported `catalog_json()` derive from the same TOML used by the website.
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

Use `model-engine` for meshing/export and call its `initialize()` from the model's WASM start function. The shared engine has no dependency on BILRESA or any model list. Match metadata to the actual geometry; the worker rejects mismatched compiled/source catalogues.

Model revision 1 preserves the current working geometry and defaults. A change to geometry/defaults needs a deliberate revision strategy that keeps old shared links reproducible. Adding a revision field alone does not implement historical version storage.
