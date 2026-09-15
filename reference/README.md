# BILRESA source references

Authoritative Onshape document: https://cad.onshape.com/documents/a8d7f806b021fde567015702/w/f3c1ffb1b3be092d2a2ce889/e/95dc2ef3acda41e95cefbb8f

- `onshape-main.fs`: read-only Show code export captured 2026-09-15 before the user removed the accidental final Delete part 1 operation. It is a source snapshot, not runnable Rust.
- `onshape-parameters.json`: parameter expressions extracted from that source.
- `sketch-dimensions.txt`: sketch constraint expressions and initial guesses from that source. Initial guesses are solver seeds, **not authoritative solved coordinates**.
- `bilresa-left-2-v2.stl`: copied from the user's confirmed `Downloads/Switch Cover - Left 2 v2.stl`. An earlier two-left-switch output, not the current one-each-side configuration.

User clarification, 2026-09-15:

- Left and right BILRESA counts are independent configurable nonnegative integers.
- Keep the 4 mm pitch margin when a side has zero switches, for rigidity.
- Use the current depth/tolerance formulas. The `NEG TOL!!!` description is obsolete.
- The magnetic adhesive allowance remains 0.3 mm and still needs measuring.
- Generate the body and removable cover components. The restored Part Studio has three parts: Part 1, Blanking Plate, Cover Top.
- Delete part 1 was accidental and has been removed by the user in Onshape. Do not reproduce it.

Prefix meanings: `switch_` BILRESA dimensions, `mag_` supplied magnetic mounting plate, `sp_` wall light-switch plate, `sp_cluster_` its protruding switches, `sp_screw_` its screws, `bracket_rail_` sliding cover rails, `asm_` assembly, `cover_` removable cover.

## Reconstruction and comparison

- `one-each-0.stl`, `one-each-1.stl`, `one-each-2.stl` copy the main body, blanking plate and cover top from local `Downloads/Part Studio 1 (2)`. These match the visible one-per-side layout; their export date/version has not been established as the latest Onshape state. The two earlier left-only files were explicitly confirmed by the user.
- Physical reference bounds are 182.10 × 90.37 × 23.10 mm for one switch each side. The supplied derived `asm_width`/`asm_height` evaluate to 198.50/90.75 mm, but do not control those outside reference sketch dimensions. Preserve the working outline; do not silently substitute the derived dimensions.
- A zero-count side retains 4 mm. Its corner radius is reduced to 3.99 mm to fit that margin. This is an explicit adaptation for zero-count robustness, rather than a claim that the source feature tree handles zero unchanged.
- Pocket centres use y=0 rather than the source's tiny ±0.005 mm offsets. Magnetic-pocket corner radii stop 0.001 mm short of half-width to avoid collapsed triangles at large count/coordinate values. The magnetic adhesive allowance remains 0.3 mm pending measurement.
- The side rail grooves end at y=-43 mm while the central opening ends at y=-42.875 mm. Blanking plate and cap are built as separate solids with their reference clearances. Unused source inputs remain recorded but are not represented as supported user controls.
- The source STEP export was attempted through Onshape, but no file arrived. Validation currently compares local STL surfaces and independently checks the generated STEP solids. Exact source STEP and a physical fit test would strengthen this comparison.
