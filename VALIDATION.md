# Spike results — 15 September 2026

## Result

The box-with-hole vertical slice works with **Rust → cadrum 0.8.20 → OpenCASCADE 8.0.1 → browser WASM**. No fallback kernel or authoring-language compromise was needed. Rust source is compiled ahead of time; parameter changes run that compiled model inside a browser worker. This does not compile arbitrary new Rust source in the browser.

## Primary-source evaluation

Inspected upstream cadrum commit `8788df70c60b986b5ab387edb75a2f6f341a8c7a`, the published 0.8.20 crate, and the author's browser example. The repository is research input, not a vendored dependency; Cargo pins the published release and checksums.

- [cadrum source and README](https://github.com/lzpel/cadrum/tree/8788df70c60b986b5ab387edb75a2f6f341a8c7a): verified `Solid::cube`, `Solid::cylinder`, translations, `(&block - &cutter).build()`, mesh positions/normals/indices, volume and bounding-box queries.
- [Rust API](https://github.com/lzpel/cadrum/blob/8788df70c60b986b5ab387edb75a2f6f341a8c7a/src/lib.rs): `Solid::write_step` takes a Rust writer; `Mesh::write_stl` does too. `Vec<u8>` works in WASM, with no filesystem. `Solid::read_step` takes a reader. The crate also supports BRep and GLB, but they are outside this spike. No implemented 3MF path was established.
- [Cross-toolchain definition](https://github.com/lzpel/cadrum/blob/8788df70c60b986b5ab387edb75a2f6f341a8c7a/docker/Dockerfile_wasm32-unknown-unknown): WASI SDK 33, clang 22.1, a rebuilt legacy-exception-handling libc++ sysroot, target-specific C/C++ flags and Rust linker flags. OCCT's static target prebuilt is downloaded by cadrum's build script; this spike did not rebuild OCCT from source.
- [Author's browser example](https://github.com/lzpel/opencascade-wasm32-unknown-unknown-example): confirms the cross-image workflow and need to call the WASI stub anchor and C++ global constructors before OCCT operations.

The build script pins the tested Docker image digest `sha256:4dc88df287da141e67588ba80ebf1d14d5cb24f5b05302c57b11b2a1e9bed1df`. It contains Rust 1.97.0. wasm-bindgen library and CLI are both pinned to 0.2.128. Dependency lockfiles are included. The default `color` and `png` features are disabled; this still uses OCCT, not cadrum's experimental pure backend.

## Problems found and resolved

1. wasm-bindgen 0.2.114 compiled the Rust code but failed while generating JS glue: `__instance_terminated global required for catch wrappers`. Matching library/CLI 0.2.128 resolves it with the supplied cross image. No upstream patch was needed.
2. cadrum's bounding-box implementation calls `BRepBndLib::Add`, which expands the bounds by tessellation deflection after meshing. The model captures BRep bounds before tessellation; tests independently check exported mesh dimensions.
3. Default tessellation missed the 0.5% volume threshold on the 12 × 12 × 2 mm / Ø11 thin-wall case. Explicit 0.01 mm absolute linear deflection and 0.15 rad angular deflection pass all five cases. The tolerance was not relaxed.

## Executed checks

### Browser worker suite

Opened `/tests/browser.html` in the Codex Chromium browser, reporting Chrome 152 on Linux. All five cases passed, including exact BRep volume, bounds, STEP reimport, a preserved analytic cylindrical surface, and binary STL edge/winding/volume checks. Three malformed parameter sets were rejected. The harness uses the same worker module as the app.

| Width × length × height, hole Ø (mm) | BRep volume (mm³) | Browser build + mesh + extraction |
|---|---:|---:|
| 40 × 30 × 12, Ø10 | 13457.522204 | 202.4 ms (first measured build) |
| 52 × 30 × 12, Ø10 | 17777.522204 | 29.0 ms |
| 40 × 30 × 12, Ø18 | 11346.371941 | 22.1 ms |
| 60 × 45 × 20, Ø8 | 52994.690351 | 20.9 ms |
| 12 × 12 × 2, Ø11 | 97.933644 | 24.7 ms |

These are observations from this machine, not a benchmark across devices. Timings exclude WASM download/initialisation, 150 ms input debounce, STEP/STL export and rendering. Preview meshes contain 352–396 triangles.

### Visible interface and actual downloads

- Inspected the rendered 3D solid; the hole is visible and camera controls are enabled.
- Changed width 40 → 52 in the input: visible geometry widened, volume changed 13.458 → 17.778 cm³, and the build counter advanced. That rebuild reported 22.4 ms.
- Set diameter to 30 with length 30: Rust rejected the model, retained the previous preview, and disabled both export buttons. Restoring diameter 18 permits a new build.
- Clicked both export buttons. The browser automation download-event observer timed out for STEP, but the actual files were saved successfully in Downloads; their contents were inspected directly. The downloaded STEP is a valid OCCT solid with volume 17777.522204 mm³. The downloaded STL is watertight with bounds [0,0,0]–[52,30,12] mm and mesh volume 17778.400798 mm³. This verifies the actual buttons, not only direct writer calls.

### Additional export validation

`npm test` executes the same compiled WASM in Node 24.18.0 and saves five STEP/STL pairs and a JSON report. All five cases and four invalid inputs pass.

`tests/validate_exports.py` imports those files with independent native `cadquery-ocp` 8.0.1 and trimesh 5.1.0:

- STEP reader succeeds; one solid; `BRepCheck_Analyzer.IsValid()` true.
- STEP volume agrees with `(width × length − π × diameter² / 4) × height` within 1e−5 mm³.
- Hole centre lies outside the solid near both ends and halfway through; material probes lie inside.
- STL has one connected, closed, consistently oriented volume; Euler characteristic 0 (one tunnel).
- STL bounds agree within 0.0001 mm and volume within 0.5%. Worst measured relative mesh volume error is about 0.181% in the thin-wall case.

Reports and reproducible example exports are in `artifacts/`. Python validation dependencies are recorded in `tests/requirements-validation.txt`.

`npm run build` succeeds. The uncompressed WASM is approximately 17.5 MB (5.07 MB gzip); frontend JS is approximately 515 kB (130 kB gzip). Vite reports its standard >500 kB chunk advisory. `npm audit` reports zero known vulnerabilities for the installed frontend dependency tree.

## Remaining limits

- BILRESA is reconstructed from the recovered Onshape source and local STL references. It is not an exact feature-history conversion; only the independent counts are supported as controls. Source STEP comparison and physical fit remain unverified.
- No FreeCAD GUI or QIDI slicer import/print was performed. Independent native CAD and mesh readers verify geometry compatibility, but do not establish printer fit or tolerances.
- Browser tests cover this Chromium environment; Safari, Firefox, mobile performance and long-session memory behaviour are untested. Legacy WASM exception support is a runtime requirement.
- Tests cover five representative box variants, not every allowed parameter combination. There is no arbitrary-feature CAD engine stress test.
- 3MF, deployment and the broader catalogue/platform concept remain outside scope.
- cadrum is MIT; the linked OCCT is LGPL 2.1 with its additional exception. Its bundled notices and source/relink obligations need handling before distributing a product. The rebuildable source/toolchain is documented here; no deployment was made.

## BILRESA three-part result

`bilresa.html` runs the Rust model in the same WASM worker and generates the main body, blanking plate and cover top. Counts are independent integers from 0 to 8; zero keeps the 4 mm margin. STEP can include all three solids or one selected part; STL exports one selected part. Separated preview coordinates never affect exports.

Seven WASM cases passed: 1/1, 2/0, 0/2, 0/0, 3/1, 1/3 and 8/0, plus four invalid inputs. Independent native OCCT and trimesh checks passed for all 21 part pairs and seven combined STEP files: valid CAD solids, closed consistently oriented single-component meshes, mesh/CAD volumes within 0.5%, and zero pairwise solid-intersection volume in assembled coordinates. This samples the supported count range, not every combination.

The browser worker suite passed 1/1, 2/0, 0/2, 0/0 and 3/1, exporting and reimporting three-solid STEP assemblies and exporting each STL; three invalid counts were rejected. Final observed build/mesh/extraction timings were respectively 381, 433, 313, 176 and 475 ms, excluding WASM startup, debounce, export and rendering. The UI also rebuilt 2-left/0-right visibly, and the separated preview showed all three components.

Compared against the local one-each STL references using 2,000 deterministically sampled surface points in each direction:

| Part | STL volume difference | Maximum sampled surface distance | RMS distance |
|---|---:|---:|---:|
| Main body | −0.00120% | 0.00501 mm | 0.00132 mm |
| Blanking plate | +0.00000394% | 0.00000382 mm | 0.00000074 mm |
| Cover top | −0.01030% | 0.00171 mm | 0.00032 mm |

Sampling is not an exhaustive Hausdorff-distance proof. The references are local exports whose exact Onshape version is not established. The source feature code and parameter expressions were captured, but a current source STEP file was not obtained. See `reference/README.md` for physical-vs-derived dimensions, the zero-side corner adaptation and tiny pocket differences.

Validation found and corrected two reconstruction issues: side rail grooves must extend below the central opening by 0.125 mm, and almost-semicircular magnetic recess fillets created collapsed STL triangles at the largest tested coordinates. Leaving a 0.001 mm straight segment avoids those collapsed triangles; no STL repair is needed in the passing checks.

Physical fit, printing and the 0.3 mm adhesive allowance remain unverified. The other source measurements are recorded in Rust but are not exposed as independently supported controls.

Final UI downloads were independently read from Downloads: the assembly STEP contains three valid solids, and the body STL is one closed volume matching the final WASM test export. The production frontend build and native Rust unit test also pass.
