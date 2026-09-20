//! Lampshade model: pure Rust construction and Manifold mesh booleans.
use manifold_rust::{
    linalg::{cross, dot, Vec2, Vec3},
    manifold::Manifold,
    types::{Error, MeshGL, OpType},
};
use serde_json::{json, Value};
use std::{
    collections::HashMap,
    f64::consts::{PI, TAU},
};
use wasm_bindgen::prelude::*;
mod cells;
mod iso;
mod strands;
mod surface;
include!(concat!(env!("OUT_DIR"), "/catalog.rs"));
pub type Params = HashMap<String, f64>;
pub type Result<T> = std::result::Result<T, String>;
#[wasm_bindgen]
pub fn catalog_json() -> String {
    CATALOG_JSON.into()
}
pub fn defaults() -> Params {
    serde_json::from_str::<Value>(CATALOG_JSON).unwrap()["parameters"]
        .as_array()
        .unwrap()
        .iter()
        .map(|v| {
            (
                v["key"].as_str().unwrap().into(),
                v["default"].as_f64().unwrap(),
            )
        })
        .collect()
}
pub fn validate(p: &Params) -> Result<()> {
    let cat: Value = serde_json::from_str(CATALOG_JSON).unwrap();
    let defs = cat["parameters"].as_array().unwrap();
    if p.len() != defs.len() {
        return Err("Unknown or missing setting.".into());
    }
    for d in defs {
        let k = d["key"].as_str().unwrap();
        let v = *p.get(k).ok_or("Missing setting")?;
        let min = d["min"].as_f64().unwrap();
        let max = d["max"].as_f64().unwrap();
        let step = d["step"].as_f64().unwrap();
        if !v.is_finite()
            || v < min
            || v > max
            || ((v - min) / step - ((v - min) / step).round()).abs() > 1e-7
        {
            return Err(format!(
                "Check {}.",
                d["label"].as_str().unwrap().to_lowercase()
            ));
        }
    }
    let minimum_radius = p["bottom_diameter"]
        .min(p["middle_diameter"])
        .min(p["top_diameter"])
        / 2.;
    let reserve =
        (2. * p["thickness"] + 1.3).max(p["clamp_diameter"] / 2. + 9. + p["thickness"] / 2.);
    let max_ripple = (((minimum_radius - reserve + 1e-9) * 10.).floor() / 10.).max(0.);
    if p["ripple_depth"] > max_ripple + 1e-9 {
        return Err(format!("Ripple depth must be at most {max_ripple:.1} mm for this profile, wall and mounting collar."));
    }
    if p["fixture_diameter"] + p["hole_clearance"] > p["clamp_diameter"] - 4. {
        return Err(
            "Increase the clamping area diameter: leave at least 2 mm of plate around the hole."
                .into(),
        );
    }
    if p["mount_depth"] > p["height"] - 16. {
        return Err("Mount depth must leave at least 16 mm below the mounting face.".into());
    }
    if p["bottom_diameter"]
        .min(p["middle_diameter"])
        .min(p["top_diameter"])
        / 2.
        - p["ripple_depth"]
        - p["thickness"] / 2.
        < p["clamp_diameter"] / 2. + 9.
    {
        return Err(
            "Widen the shade or reduce the clamping area to leave room for the mounting collar."
                .into(),
        );
    }
    Ok(())
}
pub fn profile(p: &Params, t: f64) -> f64 {
    let u = if t < 0.5 { t * 2. } else { t * 2. - 1. };
    let blend = u * (1. - p["curve"]) + (1. - (PI * u).cos()) / 2. * p["curve"];
    let (a, b) = if t < 0.5 {
        (p["bottom_diameter"], p["middle_diameter"])
    } else {
        (p["middle_diameter"], p["top_diameter"])
    };
    (a + (b - a) * blend) / 2.
}
pub fn radius(p: &Params, a: f64, z: f64) -> f64 {
    profile(p, z / p["height"])
        + p["ripple_depth"] * (p["lobes"] * (a - p["twist"] * PI / 180. * z / p["height"])).sin()
}
pub fn raw(vertices: &[[f64; 3]], faces: &[[usize; 3]]) -> Manifold {
    Manifold::from_mesh_gl(&MeshGL {
        num_prop: 3,
        vert_properties: vertices.iter().flatten().map(|x| *x as f32).collect(),
        tri_verts: faces.iter().flatten().map(|x| *x as u32).collect(),
        ..Default::default()
    })
}
fn union(xs: &[Manifold]) -> Manifold {
    Manifold::batch_boolean(xs, OpType::Add)
}
fn annulus(
    z0: f64,
    z1: f64,
    inner: impl Fn(f64, f64) -> f64,
    outer: impl Fn(f64, f64) -> f64,
    n: usize,
    nz: usize,
) -> Manifold {
    let mut v = vec![];
    let mut f = vec![];
    for j in 0..=nz {
        for side in 0..2 {
            for i in 0..n {
                let a = TAU * i as f64 / n as f64;
                let z = z0 + (z1 - z0) * j as f64 / nz as f64;
                let r = if side == 1 { outer(a, z) } else { inner(a, z) };
                v.push([r * a.cos(), r * a.sin(), z]);
            }
        }
    }
    let idx = |j: usize, s: usize, i: usize| j * 2 * n + s * n + i % n;
    for j in 0..nz {
        for i in 0..n {
            for s in 0..2 {
                let (a, b, c, d) = (
                    idx(j, s, i),
                    idx(j, s, i + 1),
                    idx(j + 1, s, i + 1),
                    idx(j + 1, s, i),
                );
                if s == 1 {
                    f.extend([[a, b, c], [a, c, d]])
                } else {
                    f.extend([[a, c, b], [a, d, c]])
                }
            }
        }
    }
    for i in 0..n {
        let (a, b, c, d) = (
            idx(0, 0, i),
            idx(0, 0, i + 1),
            idx(0, 1, i + 1),
            idx(0, 1, i),
        );
        f.extend([[a, b, c], [a, c, d]]);
        let (a, b, c, d) = (
            idx(nz, 0, i),
            idx(nz, 0, i + 1),
            idx(nz, 1, i + 1),
            idx(nz, 1, i),
        );
        f.extend([[a, c, b], [a, d, c]]);
    }
    raw(&v, &f)
}
pub fn build(p: &Params) -> Result<Vec<Part>> {
    validate(p)?;
    if p["pattern"] == 1. && (p["cell_cut_inside"] != 0. || p["cell_cut_outside"] != 0.) {
        cells::opening(p)?;
    }
    let root = p["clamp_diameter"] / 2. + 2.;
    let collar = root + 4.;
    let thread = |a: f64, z: f64| {
        let phase = (z / 4. - a / TAU).rem_euclid(1.);
        let ridge = ((0.38 - (phase - 0.5).abs()) / 0.18).clamp(0., 1.);
        root + 1.2 * ridge * ((z + 8.) / 1.2).clamp(0., 1.)
    };
    let female = annulus(
        -8.,
        0.,
        |a, z| thread(a, z) + p["thread_clearance"],
        |_, _| collar,
        128,
        64,
    );
    let male = annulus(-8., 0.2, |_, _| root - 2., thread, 128, 64);
    let plate = annulus(
        0.,
        p["plate_thickness"],
        |_, _| (p["fixture_diameter"] + p["hole_clearance"]) / 2.,
        |_, _| collar,
        128,
        1,
    );
    let adapter = union(&[male, plate]);
    if p["fit_test"] != 0. {
        return Ok(vec![
            Part::pack(&female, p, false)?,
            Part::pack(&adapter, p, false)?,
        ]);
    }
    let h = p["height"];
    let zm = h - p["mount_depth"] - p["plate_thickness"];
    let w = p["thickness"];
    let n = 192usize.max(p["lobes"] as usize * 12);
    let mut pieces = vec![];
    if p["pattern"] == 2. {
        pieces.push(strands::build(p)?);
    } else if p["pattern"] == 0. || (p["cell_cut_inside"] == 0. && p["cell_cut_outside"] == 0.) {
        pieces.push(annulus(
            0.,
            h,
            |a, z| radius(p, a, z) - w / 2.,
            |a, z| radius(p, a, z) + w / 2.,
            n,
            96,
        ));
    } else if p["cell_cut_outside"] != 0. {
        pieces.extend(cells::strands(p)?)
    } else {
        let (v, f) = cells::connected(p)?;
        pieces.push(raw(&v, &f));
    }
    let wall = pieces.pop().expect("shade wall");
    // Reinforcement grows inward from the wall centreline, leaving the visible
    // skin to the shade mesh. Keep the former band thickness for the joint.
    let mount_segments = n.max(p["lobes"] as usize * 48);
    for (lo, hi) in [(0., 3.), (h - 3., h), (zm - 6., zm - 3.)] {
        pieces.push(annulus(
            lo,
            hi,
            |a, z| radius(p, a, z) - w - 1.3,
            |a, z| radius(p, a, z),
            mount_segments,
            12,
        ));
    }
    pieces.push(female.translate(Vec3::new(0., 0., zm)));
    // Clip the whole width and height of each spoke to the curved wall, rather
    // than choosing a rectangular endpoint from a single radius sample.
    let envelope = annulus(
        zm - 6.,
        zm - 3.,
        |_, _| collar - 2.,
        |a, z| radius(p, a, z),
        mount_segments,
        12,
    );
    let reach = p["bottom_diameter"]
        .max(p["middle_diameter"])
        .max(p["top_diameter"])
        / 2.
        + p["ripple_depth"]
        + w;
    for i in 0..4 {
        let a = TAU * i as f64 / 4.;
        let spoke = Manifold::cube(Vec3::new(reach - collar + 1., 4., 3.), false)
            .translate(Vec3::new(collar - 1., -2., zm - 6.))
            .rotate(0., 0., a * 180. / PI);
        pieces.push(spoke.intersection(&envelope));
    }
    Ok(vec![
        Part::pack(&wall.union(&union(&pieces)), p, p["orientation"] != 0.)?,
        Part::pack(
            &adapter.translate(Vec3::new(0., 0., zm)),
            p,
            p["orientation"] != 0.,
        )?,
    ])
}
#[wasm_bindgen]
pub struct Model {
    parts: Vec<Part>,
    p: Params,
}
#[wasm_bindgen]
impl Model {
    #[wasm_bindgen(constructor)]
    pub fn new(json: &str) -> std::result::Result<Model, JsValue> {
        let p: Params =
            serde_json::from_str(json).map_err(|e| JsValue::from_str(&e.to_string()))?;
        let parts = build(&p).map_err(|e| JsValue::from_str(&e))?;
        Ok(Self { parts, p })
    }
    pub fn part(&self, i: usize) -> std::result::Result<Part, JsValue> {
        self.parts
            .get(i)
            .cloned()
            .ok_or_else(|| JsValue::from_str("Unknown part"))
    }
    pub fn measurements_json(&self) -> String {
        let p = &self.p;
        if p["fit_test"] != 0. {
            return "[]".into();
        }
        let hole = p["fixture_diameter"] + p["hole_clearance"];
        let z = if p["orientation"] != 0. {
            p["mount_depth"]
        } else {
            p["height"] - p["mount_depth"]
        };
        json!([{"label":"Height","value":p["height"],"from":[0.,0.,0.],"to":[0.,0.,p["height"]]},{"label":"Fixture hole","value":hole,"from":[-hole/2.,0.,z],"to":[hole/2.,0.,z]}]).to_string()
    }
    pub fn step(&self) -> std::result::Result<Vec<u8>, JsValue> {
        Err(JsValue::from_str("This mesh model exports STL."))
    }
    pub fn step_selected(&self, _selection: Vec<u32>) -> std::result::Result<Vec<u8>, JsValue> {
        self.step()
    }
}
#[wasm_bindgen]
#[derive(Clone)]
pub struct Part {
    vertices: Vec<f32>,
    triangles: Vec<u32>,
    v: f64,
    p: Params,
    flip: bool,
}
impl Part {
    fn pack(s: &Manifold, p: &Params, flip: bool) -> Result<Self> {
        if s.status() != Error::NoError || s.is_empty() {
            return Err("This combination could not form a printable solid. Reduce pattern density or ripple depth.".into());
        }
        // Check connected components directly in the exported mesh. Kernel
        // decomposition copies every component and can exhaust WASM memory on
        // numerical fragments left by many tube intersections.
        let m = s.get_mesh_gl(-1);
        let mut parent: Vec<usize> = (0..m.vert_properties.len() / m.num_prop as usize).collect();
        fn root(parent: &mut [usize], mut i: usize) -> usize {
            while parent[i] != i {
                parent[i] = parent[parent[i]];
                i = parent[i];
            }
            i
        }
        for t in m.tri_verts.chunks_exact(3) {
            let a = root(&mut parent, t[0] as usize);
            for &b in &t[1..] {
                let b = root(&mut parent, b as usize);
                parent[b] = a;
            }
        }
        let mut volumes = HashMap::<usize, f64>::new();
        let point = |i: u32| {
            let j = i as usize * m.num_prop as usize;
            Vec3::new(
                m.vert_properties[j] as f64,
                m.vert_properties[j + 1] as f64,
                m.vert_properties[j + 2] as f64,
            )
        };
        for t in m.tri_verts.chunks_exact(3) {
            let component = root(&mut parent, t[0] as usize);
            *volumes.entry(component).or_default() +=
                dot(point(t[0]), cross(point(t[1]), point(t[2]))) / 6.;
        }
        let material: Vec<_> = volumes.iter().filter(|(_, v)| **v > 1e-6).collect();
        if material.len() != 1 {
            return Err(if p["pattern"]==2. {"The strand layers do not form a connected shade. Increase layer merge or strand diameter, or adjust the wave shape."} else if p["cell_cut_outside"]!=0. {"With outside cut away, neighbouring shapes must overlap with solid material. Increase cell size or thickness, adjust the nodes or row offset, or keep the outside."} else {"This configuration leaves disconnected material. Increase strand thickness or reduce twist."}.into());
        }
        let component = *material[0].0;
        let volume = *material[0].1;
        let faces: Vec<u32> = m
            .tri_verts
            .chunks_exact(3)
            .filter(|t| root(&mut parent, t[0] as usize) == component)
            .flatten()
            .copied()
            .collect();
        let mut vertices = vec![];
        let mut remap = vec![0u32; parent.len()];
        for (i, q) in m
            .vert_properties
            .chunks_exact(m.num_prop as usize)
            .enumerate()
        {
            if root(&mut parent, i) != component {
                continue;
            }
            remap[i] = (vertices.len() / 3) as u32;
            vertices.extend([
                q[0],
                q[1] * if flip { -1. } else { 1. },
                if flip {
                    p["height"] as f32 - q[2]
                } else {
                    q[2]
                },
            ]);
        }
        Ok(Self {
            vertices,
            triangles: faces.iter().map(|i| remap[*i as usize]).collect(),
            v: volume,
            p: p.clone(),
            flip,
        })
    }
}
#[wasm_bindgen]
impl Part {
    pub fn positions(&self) -> Vec<f32> {
        self.vertices.clone()
    }
    pub fn indices(&self) -> Vec<u32> {
        self.triangles.clone()
    }
    pub fn volume(&self) -> f64 {
        self.v
    }
    pub fn bounds(&self) -> Vec<f64> {
        let mut b = vec![f64::INFINITY; 6];
        b[3..].fill(f64::NEG_INFINITY);
        for (i, v) in self.vertices.iter().enumerate() {
            b[i % 3] = b[i % 3].min(*v as f64);
            b[i % 3 + 3] = b[i % 3 + 3].max(*v as f64)
        }
        b
    }
    pub fn normals(&self) -> Vec<f32> {
        let mut n = vec![0f32; self.vertices.len()];
        let v = |i: usize| {
            Vec3::new(
                self.vertices[i] as f64,
                self.vertices[i + 1] as f64,
                self.vertices[i + 2] as f64,
            )
        };
        for t in self.triangles.chunks_exact(3) {
            let (a, b, c) = (t[0] as usize * 3, t[1] as usize * 3, t[2] as usize * 3);
            let x = cross(v(b) - v(a), v(c) - v(a));
            for i in [a, b, c] {
                n[i] += x.x as f32;
                n[i + 1] += x.y as f32;
                n[i + 2] += x.z as f32
            }
        }
        for q in n.chunks_exact_mut(3) {
            let l = (q[0] * q[0] + q[1] * q[1] + q[2] * q[2]).sqrt();
            if l > 0. {
                for x in q {
                    *x /= l
                }
            }
        }
        n
    }
    pub fn surface_normals(&self) -> Vec<f32> {
        let p = &self.p;
        let mut out = vec![0.; self.vertices.len()];
        if p["pattern"] == 2. {
            return out;
        }
        let tw = p["twist"] * PI / 180. / p["height"];
        for (i, q) in self.vertices.chunks_exact(3).enumerate() {
            let x = q[0] as f64;
            let y = q[1] as f64 * if self.flip { -1. } else { 1. };
            let z = if self.flip {
                p["height"] - q[2] as f64
            } else {
                q[2] as f64
            };
            if z < 0. || z > p["height"] {
                continue;
            }
            let a = y.atan2(x);
            let r = x.hypot(y);
            let phase = p["lobes"] * (a - tw * z);
            let offset = r - radius(p, a, z);
            if (offset.abs() - p["thickness"] / 2.).abs() > 0.0001 {
                continue;
            }
            let t = z / p["height"];
            let (u, delta) = if t < 0.5 {
                (t * 2., p["middle_diameter"] - p["bottom_diameter"])
            } else {
                (t * 2. - 1., p["top_diameter"] - p["middle_diameter"])
            };
            let slope =
                delta / p["height"] * ((1. - p["curve"]) + p["curve"] * PI / 2. * (PI * u).sin());
            let da = p["ripple_depth"] * p["lobes"] * phase.cos();
            let dz = slope - da * tw;
            let normal = [a.cos() + da / r * a.sin(), a.sin() - da / r * a.cos(), -dz];
            let l = normal.iter().map(|v| v * v).sum::<f64>().sqrt();
            for k in 0..3 {
                out[i * 3 + k] = (offset.signum() * normal[k] / l
                    * if self.flip && k > 0 { -1. } else { 1. })
                    as f32
            }
        }
        out
    }
    pub fn stl(&self) -> Vec<u8> {
        let count = self.triangles.len() / 3;
        let mut out = vec![0; 84 + count * 50];
        out[80..84].copy_from_slice(&(count as u32).to_le_bytes());
        for (i, t) in self.triangles.chunks_exact(3).enumerate() {
            for (j, idx) in t.iter().enumerate() {
                for k in 0..3 {
                    let start = 84 + i * 50 + 12 + j * 12 + k * 4;
                    out[start..start + 4]
                        .copy_from_slice(&self.vertices[*idx as usize * 3 + k].to_le_bytes());
                }
            }
        }
        out
    }
    pub fn step(&self) -> std::result::Result<Vec<u8>, JsValue> {
        Err(JsValue::from_str("This mesh model exports STL."))
    }
}
