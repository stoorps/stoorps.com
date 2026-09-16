import Module from "manifold-3d";
import { connectedCells, validateOpening, latticeLayout } from "./cells.mjs";

import { strandCells } from "./strand-cells.mjs";

const TAU = Math.PI * 2;
export function profile(p, t) {
  const u = t < 0.5 ? t * 2 : t * 2 - 1;
  const blend = u * (1 - p.curve) + ((1 - Math.cos(Math.PI * u)) / 2) * p.curve;
  const a = t < 0.5 ? p.bottom_diameter : p.middle_diameter;
  const b = t < 0.5 ? p.middle_diameter : p.top_diameter;
  return (a + (b - a) * blend) / 2;
}

export { latticeLayout } from "./cells.mjs";

// Maximum sampled chord deviation in millimetres; used for preview and STL.
export function sampleSections(sectionAt, seeds, tolerance = 0.05) {
  const samples = [{ t: 0, section: sectionAt(0) }];
  function refine(a, b, depth) {
    const probes = [0.25, 0.5, 0.75].map((f) => ({
      t: a.t + (b.t - a.t) * f,
      section: sectionAt(a.t + (b.t - a.t) * f),
      f,
    }));
    const error = Math.max(
      ...probes.flatMap(({ section, f }) =>
        section.map((point, i) =>
          Math.hypot(
            ...point.map(
              (v, k) => v - (a.section[i][k] * (1 - f) + b.section[i][k] * f),
            ),
          ),
        ),
      ),
    );
    if (error > tolerance && depth < 10) {
      refine(a, probes[1], depth + 1);
      refine(probes[1], b, depth + 1);
    } else samples.push(b);
  }
  for (let i = 1; i <= seeds; i++)
    refine(
      samples[samples.length - 1],
      { t: i / seeds, section: sectionAt(i / seeds) },
      0,
    );
  return samples;
}

export function validate(p, catalog) {
  for (const d of catalog.parameters) {
    const v = p[d.key];
    if (
      !Number.isFinite(v) ||
      v < d.min ||
      v > d.max ||
      Math.abs((v - d.min) / d.step - Math.round((v - d.min) / d.step)) > 1e-7
    )
      throw new Error(`Check ${d.label.toLowerCase()}.`);
  }
  if (Object.keys(p).some((k) => !catalog.parameters.some((d) => d.key === k)))
    throw new Error("Unknown setting.");
  if (p.fixture_diameter + p.hole_clearance > p.clamp_diameter - 4)
    throw new Error(
      "Increase the clamping area diameter: leave at least 2 mm of plate around the hole.",
    );
  if (p.mount_depth > p.height - 16)
    throw new Error(
      "Mount depth must leave at least 16 mm below the mounting face.",
    );
  if (
    Math.min(p.bottom_diameter, p.middle_diameter, p.top_diameter) / 2 -
      p.ripple_depth -
      p.thickness / 2 <
    p.clamp_diameter / 2 + 9
  )
    throw new Error(
      "Widen the shade or reduce the clamping area to leave room for the mounting collar.",
    );
}

// Mesh-backed implementation of the existing model ABI. No geometry is retained
// in WASM between builds: all intermediate solids are explicitly deleted.
export function createModule(catalog) {
  let wasm;
  return {
    initialize: async (options) => {
      wasm = await Module(options);
      wasm.setup();
    },
    catalog_json: () => JSON.stringify(catalog),
    Model: class {
      constructor(json) {
        if (!wasm) throw new Error("Geometry engine is not initialized.");
        const p = JSON.parse(json);
        validate(p, catalog);
        this.p = p;
        if (p.pattern === 1 && (p.cell_cut_inside || p.cell_cut_outside))
          validateOpening(p);
        const { Manifold, Mesh } = wasm;
        const allocated = [];
        const own = (x) => {
          allocated.push(x);
          return x;
        };
        const union = (xs) => own(Manifold.union(xs));
        const raw = (verts, faces) =>
          own(
            new Manifold(
              new Mesh({
                numProp: 3,
                vertProperties: new Float32Array(verts.flat()),
                triVerts: new Uint32Array(faces.flat()),
              }),
            ),
          );
        // Sweep a closed cross-section along a path. The sections are ordered
        // counterclockwise when viewed from the end of the path.
        function sweep(sections, closed = false) {
          const verts = sections.flat(),
            faces = [],
            n = sections.length,
            k = sections[0].length;
          for (let j = 0; j < (closed ? n : n - 1); j++)
            for (let i = 0; i < k; i++) {
              const a = j * k + i,
                b = j * k + ((i + 1) % k),
                c = ((j + 1) % n) * k + ((i + 1) % k),
                d = ((j + 1) % n) * k + i;
              faces.push([a, b, c], [a, c, d]);
            }
          if (!closed)
            for (let i = 1; i < k - 1; i++)
              faces.push(
                [0, i + 1, i],
                [(n - 1) * k, (n - 1) * k + i, (n - 1) * k + i + 1],
              );
          return raw(verts, faces);
        }
        // Annular solid with arbitrary radii; angular seams share vertex IDs.
        function annulus(z0, z1, inner, outer, n = 128, nz = 1) {
          const v = [],
            f = [];
          for (let j = 0; j <= nz; j++)
            for (let side = 0; side < 2; side++)
              for (let i = 0; i < n; i++) {
                const a = (TAU * i) / n,
                  z = z0 + ((z1 - z0) * j) / nz,
                  r = (side ? outer : inner)(a, z);
                v.push([r * Math.cos(a), r * Math.sin(a), z]);
              }
          const idx = (j, s, i) => j * 2 * n + s * n + ((i + n) % n);
          for (let j = 0; j < nz; j++)
            for (let i = 0; i < n; i++) {
              for (let s = 0; s < 2; s++) {
                const a = idx(j, s, i),
                  b = idx(j, s, i + 1),
                  c = idx(j + 1, s, i + 1),
                  d = idx(j + 1, s, i);
                f.push(
                  ...(s
                    ? [
                        [a, b, c],
                        [a, c, d],
                      ]
                    : [
                        [a, c, b],
                        [a, d, c],
                      ]),
                );
              }
            }
          for (let i = 0; i < n; i++) {
            let a = idx(0, 0, i),
              b = idx(0, 0, i + 1),
              c = idx(0, 1, i + 1),
              d = idx(0, 1, i);
            f.push([a, b, c], [a, c, d]);
            a = idx(nz, 0, i);
            b = idx(nz, 0, i + 1);
            c = idx(nz, 1, i + 1);
            d = idx(nz, 1, i);
            f.push([a, c, b], [a, d, c]);
          }
          return raw(v, f);
        }
        const fixed = (r) => () => r;
        const radius = (a, z) =>
          profile(p, z / p.height) +
          p.ripple_depth *
            Math.sin(
              p.lobes * (a - (((p.twist * Math.PI) / 180) * z) / p.height),
            );
        const root = p.clamp_diameter / 2 + 2,
          collarR = root + 4;
        // A 4 mm pitch, 1.2 mm deep trapezoidal thread, two turns long.
        // Male and female use exactly the same helix; only radial clearance differs.
        const thread = (a, z) => {
          let phase = (((z / 4 - a / TAU) % 1) + 1) % 1;
          let ridge = Math.max(
            0,
            Math.min(1, (0.38 - Math.abs(phase - 0.5)) / 0.18),
          );
          const entry = Math.min(1, Math.max(0, (z + 8) / 1.2));
          return root + 1.2 * ridge * entry;
        };
        try {
          const female = annulus(
            -8,
            0,
            (a, z) => thread(a, z) + p.thread_clearance,
            fixed(collarR),
            128,
            64,
          );
          const male = annulus(-8, 0.2, fixed(root - 2), thread, 128, 64);
          const plate = annulus(
            0,
            p.plate_thickness,
            fixed((p.fixture_diameter + p.hole_clearance) / 2),
            fixed(collarR),
            128,
          );
          const adapter = union([male, plate]);
          let shade = female;
          if (!p.fit_test) {
            const zMount = p.height - p.mount_depth - p.plate_thickness;
            const pieces = [];
            const w = p.thickness;
            if (p.pattern === 0 || (!p.cell_cut_inside && !p.cell_cut_outside))
              pieces.push(
                annulus(
                  0,
                  p.height,
                  (a, z) => radius(a, z) - w / 2,
                  (a, z) => radius(a, z) + w / 2,
                  Math.max(192, p.lobes * 12),
                  96,
                ),
              );
            else if (p.cell_cut_outside) {
              pieces.push(
                ...strandCells(p, latticeLayout(p), radius, wasm, own),
              );
            } else {
              const mesh = connectedCells(p, latticeLayout(p), radius);
              pieces.push(raw(mesh.vertices, mesh.faces));
            }

            for (const [lo, hi] of [
              [0, 3],
              [p.height - 3, p.height],
              [zMount - 6, zMount - 3],
            ])
              pieces.push(
                annulus(
                  lo,
                  hi,
                  (a, z) => radius(a, z) - w / 2 - 1,
                  (a, z) => radius(a, z) + w / 2 + 0.3,
                  Math.max(192, p.lobes * 12),
                  3,
                ),
              );
            pieces.push(own(female.translate([0, 0, zMount])));
            for (let i = 0; i < 4; i++) {
              const a = (TAU * i) / 4,
                r = radius(a, zMount - 4.5);
              const spoke = own(Manifold.cube([r - collarR + 3, 4, 3]));
              const moved = own(spoke.translate([collarR - 1, -2, zMount - 6]));
              pieces.push(own(moved.rotate([0, 0, (a * 180) / Math.PI])));
            }
            shade = union(pieces);
            this.parts = [
              pack(shade, p.orientation ? p.height : 0, !!p.orientation),
              pack(
                own(adapter.translate([0, 0, zMount])),
                p.orientation ? p.height : 0,
                !!p.orientation,
              ),
            ];
          } else this.parts = [pack(shade), pack(adapter)];
          function pack(s, flipHeight = 0, flip = false) {
            if (s.status() !== "NoError" || s.isEmpty())
              throw new Error(
                "This combination could not form a printable solid. Reduce pattern density or ripple depth.",
              );
            const components = s.decompose().map(own);
            const material = components.filter((c) => c.volume() > 0);
            // Reinforcing rings can seal a cell opening into an internal pocket.
            // This design has no intentional enclosed cavities: retain the outer
            // material shell to fill those pockets, preserving all through-holes.
            // Positive disconnected pieces are never silently discarded.
            if (material.length !== 1)
              throw new Error(
                p.cell_cut_outside
                  ? "With outside cut away, neighbouring shapes must overlap with solid material. Increase cell size or thickness, adjust the nodes or row offset, or keep the outside."
                  : "This configuration leaves disconnected material. Increase strand thickness or reduce twist.",
              );
            s = material[0];
            const mesh = s.getMesh(),
              positions = new Float32Array(mesh.numVert * 3);
            for (let i = 0; i < mesh.numVert; i++) {
              positions[i * 3] = mesh.vertProperties[i * mesh.numProp];
              positions[i * 3 + 1] =
                mesh.vertProperties[i * mesh.numProp + 1] * (flip ? -1 : 1);
              positions[i * 3 + 2] = flip
                ? flipHeight - mesh.vertProperties[i * mesh.numProp + 2]
                : mesh.vertProperties[i * mesh.numProp + 2];
            }
            return new Part(
              positions,
              new Uint32Array(mesh.triVerts),
              s.volume(),
              p,
              flipHeight,
              flip,
            );
          }
        } finally {
          for (const solid of allocated.reverse()) solid.delete();
        }
      }
      part(i) {
        if (!this.parts[i]) throw new Error("Unknown part");
        return this.parts[i];
      }
      measurements_json() {
        const p = this.p;
        return JSON.stringify(
          p.fit_test
            ? []
            : [
                {
                  label: "Height",
                  value: p.height,
                  from: [0, 0, 0],
                  to: [0, 0, p.height],
                },
                {
                  label: "Fixture hole",
                  value: p.fixture_diameter + p.hole_clearance,
                  from: [
                    -(p.fixture_diameter + p.hole_clearance) / 2,
                    0,
                    p.orientation ? p.mount_depth : p.height - p.mount_depth,
                  ],
                  to: [
                    (p.fixture_diameter + p.hole_clearance) / 2,
                    0,
                    p.orientation ? p.mount_depth : p.height - p.mount_depth,
                  ],
                },
              ],
        );
      }
      /** @returns {never} */
      step() {
        throw new Error("This mesh model exports STL.");
      }
      step_selected() {
        return this.step();
      }
      free() {}
    },
  };
}
class Part {
  constructor(positions, indices, volume, parameters, flipHeight, flip) {
    this.parameters = parameters;
    this.flipHeight = flipHeight;
    this.flip = flip;
    this.vertices = positions;
    this.triangles = indices;
    this.v = volume;
  }
  positions() {
    return this.vertices.slice();
  }
  indices() {
    return this.triangles.slice();
  }
  // Analytic directions only on the two curved skins. Zero marks hardware,
  // joins and other surfaces, which retain their ordinary creased normals.
  surface_normals() {
    const p = this.parameters,
      out = new Float32Array(this.vertices.length);
    const twist = (p.twist * Math.PI) / 180 / p.height;
    for (let i = 0; i < out.length; i += 3) {
      const x = this.vertices[i],
        y = this.vertices[i + 1] * (this.flip ? -1 : 1),
        z = this.flip
          ? this.flipHeight - this.vertices[i + 2]
          : this.vertices[i + 2];
      if (z < 0 || z > p.height) continue;
      const a = Math.atan2(y, x),
        r = Math.hypot(x, y),
        phase = p.lobes * (a - twist * z);
      const offset =
        r - profile(p, z / p.height) - p.ripple_depth * Math.sin(phase);
      if (Math.abs(Math.abs(offset) - p.thickness / 2) > 0.0001) continue;
      const t = z / p.height,
        u = t < 0.5 ? t * 2 : t * 2 - 1;
      const delta =
        t < 0.5
          ? p.middle_diameter - p.bottom_diameter
          : p.top_diameter - p.middle_diameter;
      const profileSlope =
        (delta / p.height) *
        (1 - p.curve + ((p.curve * Math.PI) / 2) * Math.sin(Math.PI * u));
      const da = p.ripple_depth * p.lobes * Math.cos(phase);
      const dz = profileSlope - da * twist,
        side = offset > 0 ? 1 : -1;
      const normal = [
        Math.cos(a) + (da / r) * Math.sin(a),
        Math.sin(a) - (da / r) * Math.cos(a),
        -dz,
      ];
      const length = Math.hypot(...normal);
      for (let k = 0; k < 3; k++)
        out[i + k] =
          ((side * normal[k]) / length) * (this.flip && k > 0 ? -1 : 1);
    }
    return out;
  }
  normals() {
    const n = new Float32Array(this.vertices.length),
      p = this.vertices;
    for (let t = 0; t < this.triangles.length; t += 3) {
      const [a, b, c] = this.triangles.slice(t, t + 3).map((i) => i * 3);
      const u = [p[b] - p[a], p[b + 1] - p[a + 1], p[b + 2] - p[a + 2]],
        v = [p[c] - p[a], p[c + 1] - p[a + 1], p[c + 2] - p[a + 2]];
      const cross = [
        u[1] * v[2] - u[2] * v[1],
        u[2] * v[0] - u[0] * v[2],
        u[0] * v[1] - u[1] * v[0],
      ];
      for (const i of [a, b, c])
        for (let k = 0; k < 3; k++) n[i + k] += cross[k];
    }
    for (let i = 0; i < n.length; i += 3) {
      const l = Math.hypot(n[i], n[i + 1], n[i + 2]) || 1;
      for (let k = 0; k < 3; k++) n[i + k] /= l;
    }
    return n;
  }
  bounds() {
    const b = [Infinity, Infinity, Infinity, -Infinity, -Infinity, -Infinity];
    this.vertices.forEach((v, i) => {
      b[i % 3] = Math.min(b[i % 3], v);
      b[(i % 3) + 3] = Math.max(b[(i % 3) + 3], v);
    });
    return new Float64Array(b);
  }
  volume() {
    return this.v;
  }
  stl() {
    const count = this.triangles.length / 3,
      bytes = new Uint8Array(84 + count * 50),
      dv = new DataView(bytes.buffer);
    dv.setUint32(80, count, true);
    for (let i = 0; i < count; i++)
      for (let j = 0; j < 3; j++)
        for (let k = 0; k < 3; k++)
          dv.setFloat32(
            84 + i * 50 + 12 + j * 12 + k * 4,
            this.vertices[this.triangles[i * 3 + j] * 3 + k],
            true,
          );
    return bytes;
  }
  /** @returns {never} */
  step() {
    throw new Error("This mesh model exports STL.");
  }
  free() {}
}
