# Curve lampshade

Original parametric geometry; no reference meshes or proprietary texture algorithms are used.

The catalogue selects `backend = "manifold"`. Rust sources (`model.rs` and `cells.rs`) use `manifold-rust` 0.13.1, compiled to WASM and loaded through the same geometry worker interface as cadrum models. `reference.mjs` preserves the previous JavaScript implementation for comparison; it is not loaded by the production generator. The catalogue owns controls, defaults and part identities. It exports STL; STEP is not available for either lampshade part in this version. BILRESA retains its CAD and STEP path.

## Shape and mounting

Three diameter controls define a cosine-interpolated profile, with adjustable blending towards straight segments. A sinusoidal angular displacement adds flutes, with twist varying over height. Surface modes are a radial-thickness shell and a connected perforated cell surface. The cell generator triangulates one opening template, repeats it around and up the profile, and welds shared vertices before joining three reinforcing rings, four spokes and the mounting collar. This avoids Boolean unions at every lattice crossing. Enclosed pockets where reinforcing rings seal cell openings are filled; through-holes are preserved and disconnected material is rejected.

Mount depth is measured from the top edge to the adapter's upper clamping face in pendant orientation. Lamp orientation rotates the complete assembly by 180 degrees; the same inset is then measured from the bottom. The standard adapter has a 26.62 mm nominal aperture plus 0.40 mm diametral clearance, a 40 mm clear clamping area and a 3 mm plate. Its shoulder extends beyond the clamping area to seat against the shade collar. The external connector root diameter is clamping diameter + 4 mm; the female collar outside diameter is clamping diameter + 12 mm.

The connector has a 4 mm pitch, two turns, a trapezoidal 1.2 mm ridge and a tapered entry. Thread clearance is radial, default 0.25 mm. It is our own connector, **not a lampholder thread standard**. The fixture's existing retaining ring clamps the adapter. Aperture and clamping dimensions are configurable for other fixtures; compatibility is not inferred from country or bulb designation.

Fit-test mode replaces the shade with its mating collar. Export both parts, separate them in the slicer and place on the bed. STL coordinates are assembled coordinates, so the parts are not automatically arranged for printing.

## Print setup

Build dimensions default to 305 × 305 × 280 mm, with a 0.4 mm nozzle, 0.45 mm extrusion width, 0.2 mm layer height and PLA-CF material family. No profiles are saved. Print setup is included in the existing configuration URL and ZIP metadata. Settings produce recommendations without silently changing geometry. Build-envelope guidance assumes upright placement and reserves 5 mm per side for a brim; it does not certify other orientations or add supports.

Material categories do not predict thermal resistance, shrinkage or sustained-load performance. Physical fit, heating under the intended LED bulb, creep and printability still need testing. There is no bulb-envelope collision model or automatic support generator in this version. Shell wall thickness is radial, not an exact normal offset; steep profiles need slicer inspection.

## Validation

`npm run test:lampshade` checks connected positive-volume solids, STL encoding, both surface modes, mount offsets/orientation, malformed parameters and mating interference at three clearances. Thread fit must still be checked on a real print.

`npm run wasm:build -- --model=model-lampshade` builds the Rust model with the plain Rust/WASM toolchain, without Open CASCADE. `npm run models:generate` creates the shared catalogue and lazy module loaders. `npm run build` produces the static page and artwork. `npm run test:all` also retains the CAD regression suite.

Revision 2 replaces row/column counts with mesh density (0–100%). Target diamond spacing goes from 32 to 6 mm, limited to twice strand thickness plus 2 mm to retain openings. Counts are derived from mean circumference and height. The default density is 80%. Older revision-1 links preserve shape and fitting settings and approximate their former cell area with the new density; their diamond proportions may change.

Revision 3 adds editable cell openings: triangle, square, diamond, pentagon, hexagon and rounded presets, 3–8 draggable nodes, numeric X/Y positioning, and symmetric cubic curve handles per smooth node. Openings must be simple closed contours inside the editor boundary. Rotation, aspect ratio and alternating row offset control the repeated layout. These are holes in connected panels, so arbitrary polygons do not need to tile edge-to-edge. Density is capped at narrow profiles to retain material between openings; extreme thickness/twist combinations may still be rejected. Thickness is radial and requires slicer inspection on steep profiles.

Revision-1/2 links preserve their dimensional and fitting settings and receive the new default diamond opening. Lattice appearance and material volume change with this new construction. Custom cell nodes travel in configuration URLs; no profile storage is added.

Before the Rust migration, a local development benchmark of the 200/256/100 mm diameter, 180 mm high, density-100 configuration took about 3.7 seconds versus 18.6 seconds for the previous strand-union generator. This is a single-run measurement, not a browser performance guarantee. Straight contour samples are restored after triangulating the corners to avoid degenerate ears. Interior diagonals are balanced before and after edge refinement so thin triangle fans cannot crease across the rippled profile; shared boundary samples remain fixed to keep the seams welded. Perforated panels refine against a conservative 0.02 mm chord-error target using profile, ripple and twist curvature, independently of cell density. The preview uses analytic normals on the inner and outer skins while preserving sharp opening walls and hardware edges. The finer panel geometry is also used by STL exports. Preview and STL use the same geometry. Automated checks cover presets, curve validation, stagger/rotation/aspect layouts and configuration-link migration; physical print testing is still required.

Revision 4 adds independent **Cut away inside** and **Cut away outside** controls. Inside only preserves the perforated-panel construction. Both switches produce a band around the contour; outside only retains the filled contour plus its outer half-band. Neither switch produces the same continuous wall as shell mode. Wall / strand thickness sets the band width and radial depth. Cell size scales contours relative to repeat spacing in the outside-cut modes, allowing positive overlap between neighbours.

Outside-cut geometry fuses repeating cells into one column in 2D, offsets contours for real strand thickness, tessellates that column once and wraps copies onto the shade. Final 3D unions join columns and the mounting hardware. The editor's neighbour warning asks for overlap in these modes; self-intersecting contours remain invalid in every mode. The warning is guidance; final connectivity is checked on the generated solid and disconnected material blocks exports. Point contact alone is not a reliable joint. Revision-3 links retain their edited nodes and start with inside-only cutaway.

Reinforcing bands extend inward from the wall centreline. Support spokes are intersected with a curved envelope across their full width and height, keeping mounting hardware inside the visible shade surface while retaining overlap with the bands.

Cell contours are centred and uniformly normalised to fit −1…1 after curve sampling and rotation, preserving their proportions. The editor and links retain the original node/handle coordinates; moving or uniformly shrinking a drawing does not shrink its generated cells. Aspect and cell size control the repeated pattern. This also applies to existing links, so drawings that previously occupied only part of the editor produce larger openings now. Link updates and copying depend on valid parameter values, not on successful mesh generation, so unfinished cell designs can be shared.
