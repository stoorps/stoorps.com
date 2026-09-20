# Mini Rack Sideboard

First-pass configurable woodworking reconstruction of the linked Onshape design.
Dimensions and machining are generated in Rust/OCCT; the worker uses that same
assembly metadata for dynamic parts, motion, the cut list and fabrication guides.

## Default configuration

- Cabinet: 1300 × 600 × 800 mm, plus 60 mm vertical foot height.
- 18 mm birch plywood; two layers for base/top/dividers, one shelf layer,
  three layers for each door rib. Solid birch battens: 20 × 30 mm.
- Shelf underside 420 mm above the base's top face.
- 31 individual battens per door. With 2 mm top/bottom working gaps, their
  finished length is 616 mm. Resolved centre-path spacing gives a 10.205 mm gap.
- 102 separately selectable/explodable wood pieces. Screws, dowels, glue and
  hinge sets are in the hardware schedule; purchased hardware is not rendered.
- Compound feet preserve rectangular stock sections, with horizontal end cuts.
  Foot face templates unfold all four faces of each batten at full size.
- Provisional hinge settings allow 110° simultaneous opening in this geometry.
  Move the pivot and the allowed angle is recomputed against the fixed panels,
  shelf, dividers and opposite door, including the configured safety gap.

## Controls and outputs

The Cabinet, Stock, Doors, Clearances, Feet, Hinges and Joinery groups configure
stock and placement. The Open control moves both doors together. Explosion
separates wood pieces and layers; it does not change cutting dimensions.

Hinge footprint X points inward from the outside side edge, Y points towards
the rear end of the door. `pivot_inset` and `pivot_from_end` locate the axis;
`pivot_x` and `pivot_y` locate it within the rectangular footprint. Both leaves
share width/length/corner radius, with separate cabinet and door recess depths.
Hardware-specific hinge mounting holes await the actual product drawing.

The fabrication ZIP contains grouped cut quantities, a full-assembly hardware
schedule, numbered machining operations, full-size PDF/SVG templates, analytic
DXF outlines, four unfolded cutting faces per foot batten, an exploded diagram
and an assembly sequence. Partial selections restrict cutting templates and
operations; the hardware schedule and assembly guide remain whole-assembly.
PDF pages are actual size; use a reader's poster mode to tile them and check the
100 mm calibration line. DXFs are finished contours without tool compensation.

Operations use each piece's local frame from assembly.json. For drilled holes,
`direction` is the drilling vector. For hinge pockets it is width/length/corner
radius, not a vector. Entry Z distinguishes opposite faces. Pocket marks locate
entries, not the outline of an angled cutter. Glued layers retain individual
identities; machine a recess to the assembled-stack depth, not that depth into
every lamination.

## Source interpretation and changes

The source's variable definitions, feature code and door curve pattern were
inspected in the shared Onshape browser. This is a parametric reconstruction,
not an imported frozen STEP. No exact solid-to-solid STEP comparison has been
completed. The source URL and observed defaults are in reference/source.json.

The source counts battens using the outer-radius path, then patterns at a fixed
30 mm pitch along a path inset by half the batten depth. Here count and resolved
pitch use the actual centre path, with equal ends and an explicit end margin.
This preserves the default count while making spacing consistent after edits.
Profiles are symmetrical analytic curves; small source sketch offsets are not
retained. Added clearances shorten the source's 620 mm battens to 616 mm.
Feet, hinge pockets, dowel holes and screw/pocket machining are new.
Rear-to-base attachment assumes the same upward through-screws as the sides.

## Validation boundaries

Automated checks exercise solid validity/meshes, dynamic piece and dowel counts,
motion limits and closed conflicts, foot cut geometry, true-size export pages,
selection scope and invalid height combinations. Type checking and site tests
cover integration. The motion solver conservatively advances through a swept
path using convex plan polygons and height overlap; it includes curve sampling
error. It applies to the one simultaneous Open path, not arbitrary independent
door positions. Top/base gaps are validated separately in the CAD model.

Hardware machining remains provisional: generic pocket bores, dowel fit,
through-screw pilots and hinge leaf recesses require the selected hardware and
jig. There is no strength/load certification or automatic driver-access proof.
The first pass does not certify every allowed parameter combination as buildable;
geometry checks reject obvious conflicts, but joint engagement and machining
interactions still need a physical dry-fit before production. In particular,
through-screw head/countersink and hinge screw patterns are not guessed.

[PLAN.md](PLAN.md) records the agreed scope and second-pass rack, PC, airflow,
cable-entry, hardware and final foot-attachment decisions.
