# Models: Rust/browser CAD spike

A box with a centred through-hole, authored in Rust, rebuilt client-side using cadrum and statically linked OpenCASCADE. Four millimetre controls, an orbitable Three.js preview, and STEP/STL downloads. No geometry backend.

## Run

Prerequisites: Docker, Rust/Cargo (to update the lockfile only), Node 22.12+ and npm.

```sh
./scripts/build-wasm.sh
npm ci
npm run dev
```

Open http://127.0.0.1:5173. First build downloads the cross toolchain, OCCT prebuilt, Rust dependencies and the matching wasm-bindgen CLI. Subsequent builds reuse the `target` directory and named Cargo registry cache. `npm run build` makes static files in `dist`; rebuilding Rust requires rerunning the WASM build script before the frontend build.

## Design and execution

- `src/lib.rs`: the actual model, Rust-side dimension validation, tessellation, in-memory STEP/STL writers and STEP reimport.
- `web/worker.js`: loads one WASM instance; each change constructs a fresh OCCT box, subtracts a cylindrical cutter, and meshes the resulting BRep. It frees the previous Rust part after successful replacement.
- `web/main.js`: debounces changes, discards stale results, updates the preview with the kernel's mesh, and downloads the current solid. Rust stays the modelling surface; JS only coordinates the worker, controls and rendering.
- Valid dimensions: 0.5–200 mm; hole diameter at least 1 mm smaller than the narrower side. The cutter extends 1 mm beyond both end faces. The box defaults (40 × 30 × 12 mm, Ø10 mm) are demonstration values, unrelated to BILRESA.
- STEP contains analytic CAD surfaces; STL has no unit declaration and must be imported in millimetres.

## Checks

```sh
npm test
# Browser worker suite: open http://127.0.0.1:5173/tests/browser.html
# Independent import checks, after npm test:
python3 -m venv .venv
.venv/bin/pip install cadquery-ocp trimesh numpy networkx
.venv/bin/python tests/validate_exports.py
```

The tests cover five parameter sets, exact volume, bounding dimensions, STEP round trips preserving a cylindrical surface, and STL closed edges/orientation/volume. The independent reader additionally checks OCCT solid validity, three through-hole probes, connectedness and STL tunnel topology. Generated exports and machine-readable reports are in `artifacts/` (ignored by Git).

See `VALIDATION.md` for measured outcomes and limitations.

## BILRESA configurator

Open http://127.0.0.1:5173/bilresa.html. Independent left/right counts (0–8 per side) regenerate three Rust-authored solids: main body, blanking plate and cover top. Select a part for individual STEP/STL export, or export all three as one STEP. The separated preview does not move exported coordinates.

`src/bilresa.rs` captures the supplied measurements and formulas, then constructs the parts from CAD primitives, booleans and fillets. Reference STL files are used for comparison only, never as runtime geometry. Only counts are exposed; the other dimensions are held at the supplied values.

```sh
npm run test:bilresa
# Browser: http://127.0.0.1:5173/tests/bilresa-browser.html
.venv/bin/pip install scipy rtree
.venv/bin/python tests/validate_bilresa.py
```

The supplied [Onshape document](https://cad.onshape.com/documents/a8d7f806b021fde567015702/w/f3c1ffb1b3be092d2a2ce889/e/95dc2ef3acda41e95cefbb8f) and local exports informed the reconstruction. See `reference/README.md` for provenance and intentional adaptations. Physical fit is not yet tested; this is a close reconstruction, not a verified exact conversion of the complete feature history.
