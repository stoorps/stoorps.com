# Curve lampshade

Original parametric geometry; no reference meshes or proprietary texture algorithms are used.

The model is a mesh runtime (`model.mjs`) using Manifold in the existing geometry worker. The catalogue owns controls, defaults and part identities. It exports STL; STEP is not available for either lampshade part in this version. BILRESA retains its CAD and STEP path.

## Shape and mounting

Three diameter controls define a cosine-interpolated profile, with adjustable blending towards straight segments. A sinusoidal angular displacement adds flutes, with twist varying over height. Surface modes are a radial-thickness shell and two opposing families of rectangular helical strands. Boolean unions join crossings, three reinforcing rings, four spokes and the mounting collar into one connected solid.

Mount depth is measured from the top edge to the adapter's upper clamping face in pendant orientation. Lamp orientation rotates the complete assembly by 180 degrees; the same inset is then measured from the bottom. The standard adapter has a 26.62 mm nominal aperture plus 0.40 mm diametral clearance, a 40 mm clear clamping area and a 3 mm plate. Its shoulder extends beyond the clamping area to seat against the shade collar. The external connector root diameter is clamping diameter + 4 mm; the female collar outside diameter is clamping diameter + 12 mm.

The connector has a 4 mm pitch, two turns, a trapezoidal 1.2 mm ridge and a tapered entry. Thread clearance is radial, default 0.25 mm. It is our own connector, **not a lampholder thread standard**. The fixture's existing retaining ring clamps the adapter. Aperture and clamping dimensions are configurable for other fixtures; compatibility is not inferred from country or bulb designation.

Fit-test mode replaces the shade with its mating collar. Export both parts, separate them in the slicer and place on the bed. STL coordinates are assembled coordinates, so the parts are not automatically arranged for printing.

## Print setup

Build dimensions default to 305 × 305 × 280 mm, with a 0.4 mm nozzle, 0.45 mm extrusion width, 0.2 mm layer height and PLA-CF material family. No profiles are saved. Print setup is included in the existing configuration URL and ZIP metadata. Settings produce recommendations without silently changing geometry. Build-envelope guidance assumes upright placement and reserves 5 mm per side for a brim; it does not certify other orientations or add supports.

Material categories do not predict thermal resistance, shrinkage or sustained-load performance. Physical fit, heating under the intended LED bulb, creep and printability still need testing. There is no bulb-envelope collision model or automatic support generator in this version. Shell wall thickness is radial, not an exact normal offset; steep profiles need slicer inspection.

## Validation

`npm run test:lampshade` checks connected positive-volume solids, STL encoding, both surface modes, mount offsets/orientation, malformed parameters and mating interference at three clearances. Thread fit must still be checked on a real print. The defaults build in approximately two seconds on the development machine; complex settings take longer.

`npm run models:generate` creates the browser wrapper; no Rust compilation is needed for this model. `npm run build` produces the static page and artwork. `npm run test:all` also retains the CAD regression suite.

Revision 2 replaces row/column counts with mesh density (0–100%). Target diamond spacing goes from 32 to 6 mm, limited to twice strand thickness plus 2 mm to retain openings. Counts are derived from mean circumference and height. The default density is 80%. Older revision-1 links preserve shape and fitting settings and approximate their former cell area with the new density; their diamond proportions may change.

Lattice strands now use adaptive longitudinal sampling (0.05 mm sampled chord-deviation target), with at least four seed intervals per ripple cycle to avoid aliasing. Preview and STL still use the same connected Boolean-unioned geometry. A local before/after benchmark of the 200/256/100 mm diameter, 180 mm high, density-100 configuration reduced generation from 27.96 s to 18.63 s and triangles from 882,402 to 617,928. Volume changed by about 0.11% from the tessellation adjustment. These are single-run development measurements, not a browser performance guarantee; dense unions remain expensive.
