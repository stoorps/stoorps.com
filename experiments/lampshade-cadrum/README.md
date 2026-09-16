# Lampshade CAD comparison spike

This is an isolated **body-only prototype**, not a replacement configurator. It
uses the same cadrum 0.8.20 / OpenCascade dependency as BILRESA, builds smooth
B-spline inner and outer solids, subtracts the inner volume, and cuts all 528
openings in one Boolean operation. It does **not** import our mesh into CAD.

The fixed comparison configuration is the revision-4 default with ripple depth
1 mm and twist 0. Profile diameters are 190/170/120 mm, height 180 mm, density 80,
wall thickness 2 mm, 33 columns and 16 staggered rows.

Scope and limitations:

- Mounting ring, spokes, collar, threads, adapter and rim reinforcements are
  excluded from **both** baseline and CAD geometry. The outstanding attachment
  protrusion is untouched.
- CAD openings use planar radial cutters. The existing implementation maps its
  contours onto the curved surface. Cell edges are therefore not identical.
- CAD shell surfaces interpolate 37 height sections and 288 angular samples;
  the spline is an approximation of the analytic formula, not an exact sinusoid.
- CAD tessellation uses BILRESA's 0.01 mm linear / 0.15 rad angular settings.
- Timings exclude startup and compilation. CAD runs native Rust with OpenCascade;
  the baseline runs Node with Manifold WASM. This is **not** a browser/WASM speed
  comparison or a benchmark of optimized production implementations.
- The viewer uses each pipeline's normals, identical lighting/materials and
  synchronized cameras. Its wireframe button exposes their different meshes.

Run from the repository root:

```sh
mkdir -p artifacts/lampshade-comparison
node experiments/lampshade-cadrum/prepare.mjs
node experiments/lampshade-cadrum/mesh-baseline.mjs
CARGO_TARGET_DIR=artifacts/native-target cargo run -p model-bilresa --example lampshade_compare
python3 -m http.server 4180 --bind 127.0.0.1
```

Open <http://127.0.0.1:4180/experiments/lampshade-cadrum/index.html>.
STEP, STL, JSON and timing results are generated in the ignored
`artifacts/lampshade-comparison` folder. `CAD_ROWS` can limit rows for debugging;
the viewer labels how many rows were actually generated. Do not compare such a
partial run's timing or volume with the full baseline.
