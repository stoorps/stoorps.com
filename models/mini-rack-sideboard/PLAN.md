# Mini Rack Sideboard — scope and deferred decisions

Design record from the user discussion, 2026-09-20. The first-pass model is now
registered in the website. See README.md for implementation details and current
validation boundaries.

Source: [Onshape Mini Rack Sideboard](https://cad.onshape.com/documents/fc5774ab553e18e1aa304c41/w/49cc550f28a881aef7c99042/e/318336473a11cc59144005c4).

## First-pass scope

Reconstruct the Onshape design as a configurable woodworking assembly in the
existing model application. Preserve the design intent and parameter expressions;
use exported reference geometry to check the reconstruction. STEP alone does not
preserve the source's parameter logic.

- Default material: 18 mm birch plywood and solid birch battens. Stock dimensions
  remain configurable. The `num_*` values normally describe glued laminations of
  plywood sheets or bundles of battens, not monolithic thick stock.
- Keep each physical board layer and batten individually identifiable, including
  pieces inside door and foot assemblies. Reuse geometry for identical pieces.
- Generate door battens from the full path: front straight, corner radius, and
  side/rear return. Batten dimensions and desired gap drive count and placement.
  Derive count, pitch, and length; resolve end margins and report actual spacing.
  Check rectangular batten interference around the curve.
- Exploded view must separate every physical piece, including individual battens
  and laminated layers. Use assembly-level and piece-level explosion transforms.
- Open/Close rotates each complete door about its pivot axis. Support configurable
  cabinet-side and door-side hinge footprints, recess depths, pivot offsets from a
  defined datum, mounting positions, and maximum opening angle. Top and bottom
  axes must align. Account for physical interference throughout the sweep, required
  clearance, and the other door's position; show the limiting component.
- Model explicit working clearances and joinery allowances. The source's 5 mm
  shelf-to-closed-door clearance is not proof of clearance during opening. Keep
  finished dimensions distinct from cutting kerf/tool compensation.
- Add feet with independent left/right and front/back splay. Define height as
  vertical rise, insets at the top attachment, and derive compound cuts with flat
  mounting and floor contact. Preserve individual battens in each glued bundle.
- Keep shelf height as a direct parameter. Do not introduce rack-unit-driven
  cabinet or shelf sizing in this pass.
- Primary outputs: BOM/cut list, labelled full-size templates, drilling and recess
  guides, and assembly instructions. PDF/SVG and useful DXF profiles are planned;
  STL is not the target output. Include part identities and configuration revision.
- Share one source of geometry and joint data between the preview, manufacturing
  operations, BOM, and guides. Preview poses must not alter fabrication dimensions.

### Agreed joint schedule

| Connection                                  | First-pass construction                                                              |
| ------------------------------------------- | ------------------------------------------------------------------------------------ |
| Side panels to base                         | Through-screws upwards through the base                                              |
| Lower divider to base                       | Through-screws upwards through the base                                              |
| Lower divider to shelf                      | Pocket screws; direct divider bearing supports shelf compression loads               |
| Shelf to rear panel                         | Prefer pocket screws where feasible; through-screws from the rear are an alternative |
| Upper centre divider to shelf and top       | Pocket screws                                                                        |
| Side panels and rear panel to top           | Pocket screws                                                                        |
| Battens to upper/lower door ribs            | Locating dowels plus glue                                                            |
| Plywood laminations and foot batten bundles | Glue                                                                                 |
| Feet to base                                | Provisionally through-screw downwards from the top of the base                       |

Joints should record participating pieces, entry face, fixing count/spacing, hole
geometry, and installation access. Check breakthrough, edge distance, collisions
with other machining, and assembly order. Dowel placement must follow the generated
batten positions. Pocket screws require material, jig, and driver access checks.

## Second pass — explicitly deferred

These are recorded for later work, not blockers to reconstructing the basic model.

### Rack fitment module

- Rack width is confirmed as **10 inch**. Design rack fitment as a separate module
  after the basic cabinet is established.
- Website intent: twin-stack setup, **two adjacent bays of n rack units**, with the
  shelf capping the lower rack keep-out spaces. Each bay should eventually support
  rack fitment or an open equipment use.
- Personal use: one lower bay for a 9U minirack and the other for a sideways Corsair
  4000D, or possibly custom PC mounting. These are design intentions, not yet
  independently verified fit claims.
- Defer rail selection/drawings, mounting spacing, front/rear rail arrangement,
  usable depth, fixing arrangement, load path, and detailed equipment keep-outs.
- Defer rack-unit-driven shelf positioning and cabinet-height adjustment. The user
  agrees with that future direction; use the existing shelf-height parameter now.
- Existing cabinet parameters can be adjusted later for clearance/keep-out needs.
  Do not require hardware specifics before the first pass.

### PC and rear-panel features

- Defer PC orientation details, exact envelope, retained case feet, access,
  custom mounting, and service clearances.
- Add ventilation cutouts, fan mounting, and cable entry later.
- Coordinate future openings with structural joints, screw locations, rack
  hardware, airflow requirements, and cable routing.

### Final feet attachment

- Removable feet are likely desirable, but not decided.
- Use the agreed provisional through-screws from the top of the base for now.
- Revisit the final joint, fixing specification, removability, access, and strength
  once the basic foot geometry and equipment arrangement are established.

## Hardware and fabrication details still to resolve

These can remain adjustable/provisional while the model is developed. Final
machining guides must clearly distinguish confirmed hardware from placeholders.

- Exact pivot hinge product/drawing: leaf shapes, pockets, bores, screw pattern,
  pivot datum, separation, travel limit, and suitability for the door.
- Pocket-hole system and screw sizes, pilot/countersink details for through-screws,
  fixing spacing, and validated engagement for the actual laminated stock.
- Dowel diameter, fit, depth in each member, and whether the available batten
  section supports one or two dowels per end.
- Glue specification, lamination alignment/clamping, grain orientation, and
  practical assembly-jig design.
- Measured stock thickness, working gaps, finish and joinery allowances.
- Router cutter/bush details before generating compensated routing guides.
- Physical fit and assembly validation before treating the fabrication pack as
  ready to build.

## Implementation sequence

1. Capture the source expressions/profiles and reference solids; reproduce the
   cabinet and dynamically generated batten layout.
2. Add physical-piece identities and assembly hierarchy; support dynamic instance
   counts and full explosion in the shared viewer/worker.
3. Model joints and their operations, clearances, hinges/door motion, and feet.
4. Generate and verify matching BOM, templates, and assembly guides.
5. Add rack fitment, equipment-specific keep-outs, rear-panel features, and refined
   foot attachment in the second pass.

No further design questions are required to begin the first pass.
