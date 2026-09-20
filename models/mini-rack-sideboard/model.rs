//! Cabinet pieces, machining and fabrication profiles share one model in millimetres.
use cadrum::{DVec3 as V, Edge, Solid};
use serde::{Deserialize, Serialize};
use serde_json::{json, Value};
use std::collections::BTreeMap;
use std::f64::consts::{FRAC_PI_2, PI};
use wasm_bindgen::prelude::*;
include!(concat!(env!("OUT_DIR"), "/catalog.rs"));
type R<T> = Result<T, String>;
type Params = BTreeMap<String, f64>;
fn err(e: impl ToString) -> String {
    e.to_string()
}
fn v(a: [f64; 3]) -> V {
    V::from_array(a)
}
#[derive(Clone, Serialize, Deserialize)]
pub struct Node {
    pub x: f64,
    pub y: f64,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub mid: Option<[f64; 2]>,
}
fn pt(x: f64, y: f64) -> Node {
    Node { x, y, mid: None }
}
fn arc(x: f64, y: f64, mx: f64, my: f64) -> Node {
    Node {
        x,
        y,
        mid: Some([mx, my]),
    }
}
fn rect(x: f64, y: f64, w: f64, h: f64) -> Vec<Node> {
    vec![pt(x, y), pt(x + w, y), pt(x + w, y + h), pt(x, y + h)]
}
fn outline(w: f64, d: f64, rf: f64, rr: f64) -> Vec<Node> {
    let q = 1.0 - 0.5_f64.sqrt();
    vec![
        pt(-w / 2.0 + rf, 0.0),
        pt(w / 2.0 - rf, 0.0),
        arc(w / 2.0, rf, w / 2.0 - rf * q, rf * q),
        pt(w / 2.0, d - rr),
        arc(w / 2.0 - rr, d, w / 2.0 - rr * q, d - rr * q),
        pt(-w / 2.0 + rr, d),
        arc(-w / 2.0, d - rr, -w / 2.0 + rr * q, d - rr * q),
        pt(-w / 2.0, rf),
        arc(-w / 2.0 + rf, 0.0, -w / 2.0 + rf * q, rf * q),
    ]
}
#[derive(Clone, Serialize)]
pub struct Frame {
    o: [f64; 3],
    u: [f64; 3],
    v: [f64; 3],
    n: [f64; 3],
}
impl Frame {
    fn xy(z: f64) -> Self {
        Self {
            o: [0.0, 0.0, z],
            u: [1.0, 0.0, 0.0],
            v: [0.0, 1.0, 0.0],
            n: [0.0, 0.0, 1.0],
        }
    }
    fn map(&self, x: f64, y: f64) -> V {
        v(self.o) + v(self.u) * x + v(self.v) * y
    }
    fn local(&self, p: V) -> [f64; 3] {
        let d = p - v(self.o);
        [d.dot(v(self.u)), d.dot(v(self.v)), d.dot(v(self.n))]
    }
}
fn edges(profile: &[Node], f: &Frame) -> R<Vec<Edge>> {
    let mut result = vec![];
    for i in 0..profile.len() {
        let a = &profile[i];
        let b = &profile[(i + 1) % profile.len()];
        let start = f.map(a.x, a.y);
        let end = f.map(b.x, b.y);
        if start.distance(end) < 1e-7 {
            continue;
        }
        result.push(
            if let Some(m) = b.mid {
                Edge::arc_3pts(start, f.map(m[0], m[1]), end)
            } else {
                Edge::line(start, end)
            }
            .map_err(err)?,
        );
    }
    Ok(result)
}
#[derive(Clone, Serialize)]
pub struct Operation {
    id: String,
    kind: String,
    point: [f64; 3],
    direction: [f64; 3],
    diameter: f64,
    depth: f64,
    note: String,
}
#[derive(Clone, Serialize)]
pub struct Piece {
    pub id: String,
    pub name: String,
    pub color: u32,
    group: String,
    material: String,
    profile: Vec<Node>,
    frame: Frame,
    thickness: f64,
    extrusion: [f64; 3],
    stock: [f64; 3],
    explode: [f64; 3],
    #[serde(skip_serializing_if = "Option::is_none")]
    pivot: Option<[f64; 2]>,
    hand: f64,
    operations: Vec<Operation>,
    notes: Vec<String>,
}
#[derive(Clone, Serialize)]
pub struct Collider {
    group: String,
    name: String,
    polygon: Vec<[f64; 2]>,
    z: [f64; 2],
}
pub struct Cabinet {
    pub solids: Vec<Solid>,
    pub pieces: Vec<Piece>,
    colliders: Vec<Collider>,
    hardware: Vec<Value>,
    metrics: Value,
}
impl Cabinet {
    fn add(
        &mut self,
        id: String,
        name: String,
        group: &str,
        profile: Vec<Node>,
        frame: Frame,
        thickness: f64,
        explode: [f64; 3],
        wood: bool,
    ) -> R<usize> {
        let solid =
            Solid::extrude(&edges(&profile, &frame)?, v(frame.n) * thickness).map_err(err)?;
        let xs: Vec<_> = profile.iter().map(|p| p.x).collect();
        let ys: Vec<_> = profile.iter().map(|p| p.y).collect();
        let extent = |a: &Vec<f64>| {
            a.iter().cloned().fold(f64::NEG_INFINITY, f64::max)
                - a.iter().cloned().fold(f64::INFINITY, f64::min)
        };
        let index = self.pieces.len();
        self.solids.push(solid);
        self.pieces.push(Piece {
            id,
            name,
            color: if wood { 0xb9976b } else { 0xdcc69d },
            group: group.into(),
            material: if wood { "Solid birch" } else { "Birch plywood" }.into(),
            profile,
            extrusion: (v(frame.n) * thickness).to_array(),
            frame,
            thickness,
            stock: [extent(&xs), extent(&ys), thickness],
            explode,
            pivot: None,
            hand: 0.0,
            operations: vec![],
            notes: vec![],
        });
        Ok(index)
    }
    fn collider(&mut self, group: &str, name: &str, poly: Vec<[f64; 2]>, z: [f64; 2]) {
        self.colliders.push(Collider {
            group: group.into(),
            name: name.into(),
            polygon: poly,
            z,
        });
    }
    fn drill(
        &mut self,
        index: usize,
        id: &str,
        kind: &str,
        point: V,
        dir: V,
        diam: f64,
        depth: f64,
        note: &str,
    ) -> R<()> {
        let profile = vec![Edge::circle(diam / 2.0, dir)
            .map_err(err)?
            .translate(point - dir * 0.02)];
        let cutter = Solid::extrude(&profile, dir * (depth + 0.02)).map_err(err)?;
        self.solids[index] = (&self.solids[index] - &cutter).build().map_err(err)?;
        let p = &mut self.pieces[index];
        let local = p.frame.local(point);
        p.operations.push(Operation {
            id: id.into(),
            kind: kind.into(),
            point: local,
            direction: [
                dir.dot(v(p.frame.u)),
                dir.dot(v(p.frame.v)),
                dir.dot(v(p.frame.n)),
            ],
            diameter: diam,
            depth,
            note: note.into(),
        });
        Ok(())
    }
    fn metadata(&self) -> String {
        json!({"parts":self.pieces,"colliders":self.colliders,"hardware":self.hardware,"metrics":self.metrics,"warnings":["Custom hinges and pocket-hole settings are provisional: verify against your hardware before machining.","Rack fitment, ventilation and PC keep-outs are deferred to the second pass.","Fit and load capacity have not been physically tested."]}).to_string()
    }
}
fn parse(input: &str) -> R<Params> {
    let mut p: Params = serde_json::from_str(input).map_err(err)?;
    let catalog: Value = serde_json::from_str(CATALOG_JSON).unwrap();
    let defs = catalog["parameters"].as_array().unwrap();
    if p.keys()
        .any(|k| !defs.iter().any(|d| d["key"].as_str() == Some(k)))
    {
        return Err("Unknown cabinet parameter".into());
    }
    for d in defs {
        let key = d["key"].as_str().unwrap();
        let val = *p
            .entry(key.into())
            .or_insert(d["default"].as_f64().unwrap());
        let min = d["min"].as_f64().unwrap();
        let max = d["max"].as_f64().unwrap();
        let step = d["step"].as_f64().unwrap();
        if !val.is_finite()
            || val < min
            || val > max
            || ((val - min) / step - ((val - min) / step).round()).abs() > 1e-7
        {
            return Err(format!("{} is outside its allowed range.", d["label"]));
        }
    }
    Ok(p)
}
// Path runs from the centre meeting edge, around the front corner, to the rear end.
fn path_point(s: f64, w: f64, r: f64, bd: f64, gap: f64) -> ([f64; 2], [f64; 2]) {
    let straight = w / 2.0 - gap / 2.0 - r;
    let radius = r - bd / 2.0;
    if s <= straight {
        return ([-gap / 2.0 - s, bd / 2.0], [-1.0, 0.0]);
    }
    let a = (s - straight) / radius;
    if a <= FRAC_PI_2 {
        return (
            [-w / 2.0 + r - radius * a.sin(), r - radius * a.cos()],
            [-a.cos(), a.sin()],
        );
    }
    (
        [-w / 2.0 + bd / 2.0, r + (s - straight - radius * FRAC_PI_2)],
        [0.0, 1.0],
    )
}
fn rib(w: f64, r: f64, bd: f64, gap: f64, depth: f64, hand: f64) -> Vec<Node> {
    let cx = -w / 2.0 + r;
    let q = 0.5_f64.sqrt();
    let ri = r - bd;
    let mut p = vec![
        pt(-gap / 2.0, 0.0),
        pt(cx, 0.0),
        arc(-w / 2.0, r, cx - r * q, r - r * q),
        pt(-w / 2.0, depth),
        pt(-w / 2.0 + bd, depth),
        pt(-w / 2.0 + bd, r),
        arc(cx, bd, cx - ri * q, r - ri * q),
        pt(-gap / 2.0, bd),
    ];
    for n in &mut p {
        n.x *= hand;
        if let Some(m) = &mut n.mid {
            m[0] *= hand
        }
    }
    p
}
pub fn build(input: &str) -> R<Cabinet> {
    let p = parse(input)?;
    let g = |k: &str| p[k];
    let (w, d, h, t, r, rr, bw, bd) = (
        g("o_width"),
        g("o_depth"),
        g("o_height"),
        g("board_stock_thickness"),
        g("of_radius"),
        g("or_radius"),
        g("batten_stock_width"),
        g("batten_stock_depth"),
    );
    let (nb, nt, nr, ns, nd) = (
        g("num_bottom_layers") as usize,
        g("num_top_layers") as usize,
        g("num_door_rib_layers") as usize,
        g("num_shelf_layers") as usize,
        g("num_divider_layers") as usize,
    );
    let (base, top, shelf) = (
        nb as f64 * t,
        h - nt as f64 * t,
        nb as f64 * t + g("i_shelf_height"),
    );
    let gap = g("centre_gap");
    let dd = g("sd_depth");
    let clear = g("i_shelf_clearance");
    let bottom = base + g("door_bottom_gap");
    let door_top = top - g("door_top_gap");
    let bl = door_top - bottom - 2.0 * nr as f64 * t;
    let radius = r - bd / 2.0;
    let path = w / 2.0 - gap / 2.0 - r + radius * FRAC_PI_2 + dd - r;
    let margin = g("door_end_margin");
    let count = ((path - 2.0 * margin + g("sd_batten_gap")) / (bw + g("sd_batten_gap")))
        .round()
        .max(2.0) as usize;
    let pitch = (path - 2.0 * margin - bw) / (count - 1) as f64;
    if r >= w / 2.0 || r + rr >= d || r <= bd + clear + 5.0 || dd < r + bw || dd >= d - t - 30.0 {
        return Err("The front radius or door side extent does not fit this cabinet.".into());
    }
    if shelf <= base + 100.0 || shelf + ns as f64 * t >= top - 80.0 || bl < 100.0 {
        return Err("Shelf or laminated layers leave too little clear height.".into());
    }
    if pitch <= bw || count > 100 {
        return Err("Batten layout has no usable gap or too many pieces.".into());
    }
    if g("dowel_diameter") + 2.0 > bw.min(bd)
        || g("dowel_depth") >= nr as f64 * t - g("hinge_door_recess")
        || g("dowel_depth") * 2.0 >= bl
    {
        return Err("Dowel diameter or depth does not fit the batten and rib stock.".into());
    }
    if g("hinge_width") > bd - 2.0
        || g("hinge_corner") * 2.0 > g("hinge_width").min(g("hinge_length"))
        || g("pivot_x") > g("hinge_width")
        || g("pivot_y") > g("hinge_length")
    {
        return Err("Hinge footprint or pivot offsets do not fit the door rib.".into());
    }
    let mut c = Cabinet {
        solids: vec![],
        pieces: vec![],
        colliders: vec![],
        hardware: vec![],
        metrics: json!({}),
    };
    let full = outline(w, d, r, rr);
    let mut base_ids = vec![];
    let mut top_ids = vec![];
    for (name, n, z, sign, ids) in [
        ("base", nb, 0.0, -1.0, &mut base_ids),
        ("top", nt, top, 1.0, &mut top_ids),
    ] {
        for i in 0..n {
            ids.push(c.add(
                format!("{name}-{}", i + 1),
                format!(
                    "{} layer {}",
                    if name == "base" { "Base" } else { "Top" },
                    i + 1
                ),
                "carcass",
                full.clone(),
                Frame::xy(z + i as f64 * t),
                t,
                [
                    0.0,
                    0.0,
                    sign * (70.0 + (if sign < 0.0 { n - i } else { i + 1 }) as f64 * 30.0),
                ],
                false,
            )?);
        }
    }
    let sy = dd + g("door_side_gap");
    let side_h = top - base;
    let mut sides = vec![];
    for hand in [1.0, -1.0] {
        let x = hand * (-w / 2.0 + t / 2.0);
        let f = Frame {
            o: [x - t / 2.0, sy, base],
            u: [0.0, 1.0, 0.0],
            v: [0.0, 0.0, 1.0],
            n: [1.0, 0.0, 0.0],
        };
        let label = if hand > 0.0 { "Left" } else { "Right" };
        sides.push(c.add(
            format!("{}-side", label.to_lowercase()),
            format!("{label} side"),
            "carcass",
            rect(0.0, 0.0, d - t - sy, side_h),
            f,
            t,
            [-hand * 140.0, 0.0, 0.0],
            false,
        )?);
        c.collider(
            "carcass",
            &format!("{label} fixed side"),
            vec![
                [x - t / 2.0, sy],
                [x + t / 2.0, sy],
                [x + t / 2.0, d - t],
                [x - t / 2.0, d - t],
            ],
            [base, top],
        );
    }
    let rear = c.add(
        "rear".into(),
        "Rear panel".into(),
        "carcass",
        rect(-w / 2.0 + t, 0.0, w - 2.0 * t, side_h),
        Frame {
            o: [0.0, d - t, base],
            u: [1.0, 0.0, 0.0],
            v: [0.0, 0.0, 1.0],
            n: [0.0, 1.0, 0.0],
        },
        t,
        [0.0, 160.0, 0.0],
        false,
    )?;
    c.collider(
        "carcass",
        "Rear panel",
        vec![
            [-w / 2.0 + t, d - t],
            [w / 2.0 - t, d - t],
            [w / 2.0 - t, d],
            [-w / 2.0 + t, d],
        ],
        [base, top],
    );
    // Shelf is inset from the curved door by bd + clearance, with rear contact.
    let inset = bd + clear;
    let mut shelf_profile = outline(w - 2.0 * inset, d - t - inset, r - inset, 1.0);
    for n in &mut shelf_profile {
        n.y += inset;
        if let Some(m) = &mut n.mid {
            m[1] += inset
        }
    }
    let mut shelves = vec![];
    for i in 0..ns {
        shelves.push(c.add(
            format!("shelf-{}", i + 1),
            format!("Shelf layer {}", i + 1),
            "carcass",
            shelf_profile.clone(),
            Frame::xy(shelf + i as f64 * t),
            t,
            [0.0, 0.0, 45.0 + i as f64 * 30.0],
            false,
        )?);
    }
    let poly = sample(&shelf_profile, 0.03);
    c.collider("carcass", "Shelf", poly, [shelf, shelf + ns as f64 * t]);
    let mut lower = vec![];
    let mut upper = vec![];
    for (label, z, height, ids) in [
        ("lower", base, shelf - base, &mut lower),
        (
            "upper",
            shelf + ns as f64 * t,
            top - shelf - ns as f64 * t,
            &mut upper,
        ),
    ] {
        for i in 0..nd {
            ids.push(c.add(
                format!("{label}-divider-{}", i + 1),
                format!("{label} divider layer {}", i + 1),
                "carcass",
                rect(inset, 0.0, d - t - inset, height),
                Frame {
                    o: [-(nd as f64) * t / 2.0 + i as f64 * t, 0.0, z],
                    u: [0.0, 1.0, 0.0],
                    v: [0.0, 0.0, 1.0],
                    n: [1.0, 0.0, 0.0],
                },
                t,
                [
                    (i as f64 - (nd as f64 - 1.0) / 2.0) * 45.0,
                    45.0,
                    if label == "lower" { -20.0 } else { 80.0 },
                ],
                false,
            )?);
        }
        c.collider(
            "carcass",
            &format!("{label} divider"),
            vec![
                [-(nd as f64) * t / 2.0, inset],
                [nd as f64 * t / 2.0, inset],
                [nd as f64 * t / 2.0, d - t],
                [-(nd as f64) * t / 2.0, d - t],
            ],
            [z, z + height],
        );
    }
    let mut dowels = 0;
    for hand in [1.0, -1.0] {
        let group = if hand > 0.0 {
            "left-door"
        } else {
            "right-door"
        };
        let label = if hand > 0.0 { "Left" } else { "Right" };
        let pivot = [
            hand * (-w / 2.0 + g("pivot_inset")),
            dd - g("pivot_from_end"),
        ];
        let pocket_x = -w / 2.0 + g("pivot_inset") - g("pivot_x");
        let pocket_y = dd - g("pivot_from_end") - g("pivot_y");
        if pocket_x < -w / 2.0 + 0.5
            || pocket_x + g("hinge_width") > -w / 2.0 + bd - 0.5
            || pocket_y < r + 1.0
            || pocket_y + g("hinge_length") > dd - 1.0
        {
            return Err(
                "Move the hinge footprint inside the straight rear end of the door rib.".into(),
            );
        }
        let mut ribs = vec![];
        for (which, z, sign) in [
            ("bottom", bottom, -1.0),
            ("top", door_top - nr as f64 * t, 1.0),
        ] {
            let mut ids = vec![];
            for i in 0..nr {
                let idx = c.add(
                    format!("{group}-{which}-rib-{}", i + 1),
                    format!("{label} {which} rib layer {}", i + 1),
                    group,
                    rib(w, r, bd, gap, dd, hand),
                    Frame::xy(z + i as f64 * t),
                    t,
                    [
                        -hand * 250.0,
                        -160.0,
                        sign * (70.0 + (if sign < 0.0 { nr - i } else { i + 1 }) as f64 * 28.0),
                    ],
                    false,
                )?;
                c.pieces[idx].pivot = Some(pivot);
                c.pieces[idx].hand = hand;
                ids.push(idx);
            }
            ribs.push(ids);
        }
        for i in 0..count {
            let s = margin + bw / 2.0 + i as f64 * pitch;
            let (pos, tangent) = path_point(s, w, r, bd, gap);
            let u = V::new(tangent[0] * hand, tangent[1], 0.0);
            let normal = V::new(tangent[1] * hand, -tangent[0], 0.0);
            let f = Frame {
                o: [pos[0] * hand, pos[1], bottom + nr as f64 * t],
                u: u.to_array(),
                v: normal.to_array(),
                n: [0.0, 0.0, 1.0],
            };
            let poly = rect(-bw / 2.0, -bd / 2.0, bw, bd);
            let idx = c.add(
                format!("{group}-batten-{}", i + 1),
                format!("{label} batten {}", i + 1),
                group,
                poly.clone(),
                f.clone(),
                bl,
                [
                    -hand * 250.0 - normal.x * 110.0 + (pos[0] * hand + hand * w / 4.0) * 0.25,
                    -160.0 - normal.y * 110.0 + (pos[1] - dd / 2.0) * 0.25,
                    0.0,
                ],
                true,
            )?;
            c.pieces[idx].pivot = Some(pivot);
            c.pieces[idx].hand = hand;
            c.collider(
                group,
                &format!("{label} batten {}", i + 1),
                poly.iter()
                    .map(|n| {
                        let v = f.map(n.x, n.y);
                        [v.x, v.y]
                    })
                    .collect(),
                [bottom + nr as f64 * t, door_top - nr as f64 * t],
            );
            for end in 0..2 {
                let z = if end == 0 {
                    bottom + nr as f64 * t
                } else {
                    door_top - nr as f64 * t
                };
                let dir = if end == 0 { V::Z } else { -V::Z };
                let location = V::new(pos[0] * hand, pos[1], z);
                let id = format!("{group}-dowel-{}-{}", i + 1, end + 1);
                let dia = g("dowel_diameter") + g("dowel_allowance");
                c.drill(
                    idx,
                    &id,
                    "dowel",
                    location,
                    dir,
                    dia,
                    g("dowel_depth"),
                    "Blind hole; glue dowel and batten end to rib",
                )?;
                let rib_ids = &ribs[end];
                for &ri in rib_ids {
                    let rb = c.pieces[ri].frame.o[2];
                    let lo = (z - g("dowel_depth")).max(rb);
                    let hi = (z + g("dowel_depth")).min(rb + t);
                    if hi > lo + 1e-6 {
                        let start = if end == 0 { hi } else { lo };
                        c.drill(ri,&id,"dowel",V::new(location.x,location.y,start),-dir,dia,hi-lo,"Drill assembled rib to engagement depth; guide shows this layer's intersection")?;
                    }
                }
                dowels += 1;
            }
        }
        // Conservative curved rib colliders use short convex quads. Error is recorded.
        for k in 0..100 {
            let s0 = path * k as f64 / 100.0;
            let s1 = path * (k + 1) as f64 / 100.0;
            let (a, ta) = path_point(s0, w, r, bd, gap);
            let (b, tb) = path_point(s1, w, r, bd, gap);
            let point = |p: [f64; 2], t: [f64; 2], sg: f64| {
                [
                    (p[0] + t[1] * bd / 2.0 * sg) * hand,
                    p[1] - t[0] * bd / 2.0 * sg,
                ]
            };
            let poly = vec![
                point(a, ta, -1.0),
                point(a, ta, 1.0),
                point(b, tb, 1.0),
                point(b, tb, -1.0),
            ];
            for z in [
                [bottom, bottom + nr as f64 * t],
                [door_top - nr as f64 * t, door_top],
            ] {
                c.collider(group, &format!("{label} door rib"), poly.clone(), z);
            }
        }
        // Hinge pockets in the cabinet faces and door faces, crossing layer boundaries.
        let mut pocket = outline(
            g("hinge_width"),
            g("hinge_length"),
            g("hinge_corner").max(0.001),
            g("hinge_corner").max(0.001),
        );
        for n in &mut pocket {
            n.x = (n.x + g("hinge_width") / 2.0 + pocket_x) * hand;
            n.y += pocket_y;
            if let Some(m) = &mut n.mid {
                m[0] = (m[0] + g("hinge_width") / 2.0 + pocket_x) * hand;
                m[1] += pocket_y
            }
        }
        for (ids, face, dir, depth) in [
            (&base_ids, base, -1.0, g("hinge_recess")),
            (&top_ids, top, 1.0, g("hinge_recess")),
            (&ribs[0], bottom, 1.0, g("hinge_door_recess")),
            (&ribs[1], door_top, -1.0, g("hinge_door_recess")),
        ] {
            if depth <= 0.0 {
                continue;
            }
            let f = Frame::xy(face);
            let cutter = Solid::extrude(&edges(&pocket, &f)?, V::Z * (dir * depth)).map_err(err)?;
            for &i in ids {
                let z = c.pieces[i].frame.o[2];
                if z < face.max(face + dir * depth) - 1e-7
                    && z + t > face.min(face + dir * depth) + 1e-7
                {
                    c.solids[i] = (&c.solids[i] - &cutter).build().map_err(err)?;
                    c.pieces[i].notes.push(format!("Hinge pocket: {} × {} mm, corner R{}, depth {} mm from {} face; pivot X{} Y{} from footprint datum. Verify hardware.",g("hinge_width"),g("hinge_length"),g("hinge_corner"),depth,if dir>0.0{"bottom"}else{"top"},g("pivot_x"),g("pivot_y")));
                    let pocket_point = c.pieces[i].frame.local(V::new(
                        hand * (pocket_x + g("hinge_width") / 2.0),
                        pocket_y + g("hinge_length") / 2.0,
                        face,
                    ));
                    c.pieces[i].operations.push(Operation{id:format!("{group}-hinge-pocket"),kind:"hinge-pocket".into(),point:pocket_point,direction:[g("hinge_width"),g("hinge_length"),g("hinge_corner")],diameter:0.0,depth,note:"Rectangle dimensions in direction field; machine from indicated Z face after lamination".into()});
                }
            }
        }
        c.hardware.push(json!({"name":format!("{label} custom pivot hinge set"),"quantity":1,"specification":"Top and bottom pivot, custom footprint; product and screw pattern unconfirmed"}));
    }
    c.hardware.push(json!({"name":"Birch dowels","quantity":dowels,"specification":format!("Ø{} × {} mm; {} mm engagement each side",g("dowel_diameter"),2.0*g("dowel_depth"),g("dowel_depth"))}));
    // Compound splay preserves a rectangular cross-section perpendicular to the leg.
    let lh = g("leg_height");
    let mut foot_ranges = vec![];
    let nx = g("num_leg_battens_x") as usize;
    let ny = g("num_leg_battens_y") as usize;
    let mut foot_points = vec![];
    for sx in [-1.0, 1.0] {
        for sygn in [-1.0, 1.0] {
            let cx = sx * (w / 2.0 - g("leg_edge_inset_x"));
            let cy = if sygn < 0.0 {
                g("leg_edge_inset_y")
            } else {
                d - g("leg_edge_inset_y")
            };
            let foot_start = c.pieces.len();
            let axis = V::new(
                -sx * g("leg_angle_lr").to_radians().tan(),
                -sygn * g("leg_angle_fb").to_radians().tan(),
                1.0,
            )
            .normalize();
            let u = (V::X - axis * axis.x).normalize();
            let vv = axis.cross(u);
            let mut foot_poly = vec![];
            for ix in 0..nx {
                for iy in 0..ny {
                    let corners = [
                        (ix as f64 - nx as f64 / 2.0, iy as f64 - ny as f64 / 2.0),
                        (
                            ix as f64 + 1.0 - nx as f64 / 2.0,
                            iy as f64 - ny as f64 / 2.0,
                        ),
                        (
                            ix as f64 + 1.0 - nx as f64 / 2.0,
                            iy as f64 + 1.0 - ny as f64 / 2.0,
                        ),
                        (
                            ix as f64 - nx as f64 / 2.0,
                            iy as f64 + 1.0 - ny as f64 / 2.0,
                        ),
                    ];
                    let coords: Vec<V> = corners
                        .iter()
                        .map(|(x, y)| {
                            let q = u * (*x * bw) + vv * (*y * bd);
                            V::new(cx, cy, 0.0) + q - axis * q.z / axis.z
                        })
                        .collect();
                    if coords
                        .iter()
                        .any(|q| q.x.abs() > w / 2.0 - rr || q.y < rr || q.y > d - rr)
                    {
                        return Err("Foot mounting footprint extends outside the base.".into());
                    }
                    let label = format!(
                        "{}-{}",
                        if sx < 0.0 { "left" } else { "right" },
                        if sygn < 0.0 { "front" } else { "rear" }
                    );
                    let poly: Vec<Node> = coords
                        .iter()
                        .map(|q| pt(q.x - axis.x * lh / axis.z, q.y - axis.y * lh / axis.z))
                        .collect();
                    let idx = c.add(
                        format!("foot-{label}-{}-{}", ix + 1, iy + 1),
                        format!("{label} foot batten {} / {}", ix + 1, iy + 1),
                        "feet",
                        poly.clone(),
                        Frame::xy(-lh),
                        lh,
                        [
                            // Stock indices increase in world X/Y in every foot.
                            // Mirror the bundle offset, not the spacing within it.
                            sx * 80.0 + (ix as f64 - (nx - 1) as f64 / 2.0) * 35.0,
                            sygn * 60.0 + (iy as f64 - (ny - 1) as f64 / 2.0) * 35.0,
                            -120.0,
                        ],
                        true,
                    )?;
                    c.solids[idx] =
                        Solid::extrude(&edges(&poly, &Frame::xy(-lh))?, axis * (lh / axis.z))
                            .map_err(err)?;
                    let blank = lh / axis.z + (u.z.abs() * bw + vv.z.abs() * bd) / axis.z;
                    c.pieces[idx].extrusion = (axis * (lh / axis.z)).to_array();
                    c.pieces[idx].stock = [bw, bd, blank];
                    c.pieces[idx].notes.push(format!("Compound splay LR {}°, FB {}°. Blank length {:.2} mm includes slanted end extent. Cut both ends parallel to floor; plan outline is not the stock cross-section.",g("leg_angle_lr"),g("leg_angle_fb"),blank));
                    foot_poly.extend(coords.iter().map(|q| [q.x, q.y]));
                }
            }
            let q = u * (-(nx as f64 - 1.0) * bw / 2.0) + vv * (-(ny as f64 - 1.0) * bd / 2.0);
            let mount = V::new(cx, cy, 0.0) + q - axis * q.z / axis.z;
            foot_points.push(mount);
            foot_ranges.push((foot_start, c.pieces.len()));
        }
    }
    // Through screws: holes in all base laminations and pilot holes into supported pieces.
    let mut screws = 0;
    let fix_points = |length: f64| {
        let count = (length / g("screw_spacing")).ceil().max(1.0) as usize;
        (0..=count)
            .map(move |i| 20.0 + (length - 40.0) * i as f64 / count as f64)
            .collect::<Vec<_>>()
    };
    for &side in &sides {
        let x = c.pieces[side].frame.o[0] + t / 2.0;
        for y in fix_points(d - t - sy) {
            let loc = V::new(x, sy + y, 0.0);
            for &bi in &base_ids {
                let z = c.pieces[bi].frame.o[2];
                c.drill(
                    bi,
                    "side-base",
                    "through-screw",
                    V::new(loc.x, loc.y, z),
                    V::Z,
                    g("screw_diameter") + 0.5,
                    t,
                    "Screw upwards from underside; countersink to suit selected head",
                )?;
            }
            c.drill(
                side,
                "side-base",
                "pilot",
                V::new(loc.x, loc.y, base),
                V::Z,
                g("screw_diameter") * 0.7,
                g("screw_engagement"),
                "Provisional pilot diameter; verify timber and screw",
            )?;
            screws += 1;
        }
    }
    for y in fix_points(d - t - inset) {
        let x = -(nd as f64) * t / 2.0 + t / 2.0;
        for &bi in &base_ids {
            let z = c.pieces[bi].frame.o[2];
            c.drill(
                bi,
                "divider-base",
                "through-screw",
                V::new(x, inset + y, z),
                V::Z,
                g("screw_diameter") + 0.5,
                t,
                "Screw upwards through base",
            )?;
        }
        c.drill(
            lower[0],
            "divider-base",
            "pilot",
            V::new(x, inset + y, base),
            V::Z,
            g("screw_diameter") * 0.7,
            g("screw_engagement"),
            "Lower divider bears directly on base",
        )?;
        screws += 1;
    }
    for (loc, (first, last)) in foot_points.into_iter().zip(foot_ranges) {
        let cutter = Solid::extrude(
            &[Edge::circle(g("screw_diameter") * 0.35, V::Z)
                .map_err(err)?
                .translate(loc)],
            -V::Z * g("screw_engagement"),
        )
        .map_err(err)?;
        for idx in first..last {
            let cut = (&c.solids[idx] - &cutter).build().map_err(err)?;
            if c.solids[idx].volume() - cut.volume() > 0.01 {
                c.drill(idx,"foot-base","pilot",loc,-V::Z,g("screw_diameter")*0.7,g("screw_engagement"),"Provisional vertical pilot into glued foot bundle; fit before shelf if access requires")?;
            }
        }
        for &bi in &base_ids {
            let z = c.pieces[bi].frame.o[2] + t;
            c.drill(
                bi,
                "foot-base",
                "through-screw",
                V::new(loc.x, loc.y, z),
                -V::Z,
                g("screw_diameter") + 0.5,
                t,
                "Provisional foot fixing: screw down from top of base; final attachment deferred",
            )?;
        }
        screws += 1;
    }
    for x in fix_points(w - 2.0 * t) {
        let x = -w / 2.0 + t + x;
        for &bi in &base_ids {
            let z = c.pieces[bi].frame.o[2];
            c.drill(
                bi,
                "rear-base",
                "through-screw",
                V::new(x, d - t / 2.0, z),
                V::Z,
                g("screw_diameter") + 0.5,
                t,
                "Screw upwards through base into rear panel; same construction as fixed sides",
            )?;
        }
        c.drill(
            rear,
            "rear-base",
            "pilot",
            V::new(x, d - t / 2.0, base),
            V::Z,
            g("screw_diameter") * 0.7,
            g("screw_engagement"),
            "Rear panel to base",
        )?;
        screws += 1;
    }
    c.hardware.push(json!({"name":"Through-screws","quantity":screws,"specification":format!("Ø{} × at least {} mm (base {} + engagement {}); head and exact length provisional",g("screw_diameter"),base+g("screw_engagement"),base,g("screw_engagement"))}));
    // Pocket bores are parameterised generic stepped drill geometry, not a branded jig preset.
    let mut pockets = 0;
    for (ids, endz, up) in [
        (&lower, shelf, true),
        (&upper, shelf + ns as f64 * t, false),
        (&upper, top, true),
    ] {
        let idx = ids[0];
        let x = c.pieces[idx].frame.o[0];
        for y in fix_points(d - t - inset) {
            pocket(
                &mut c,
                idx,
                V::new(x, inset + y, endz),
                V::X,
                if up { V::Z } else { -V::Z },
                &p,
            )?;
            pockets += 1;
        }
    }
    for &idx in &sides {
        let left = c.pieces[idx].frame.o[0] < 0.0;
        let face = if left {
            c.pieces[idx].frame.o[0] + t
        } else {
            c.pieces[idx].frame.o[0]
        };
        for y in fix_points(d - t - sy) {
            pocket(
                &mut c,
                idx,
                V::new(face, sy + y, top),
                if left { -V::X } else { V::X },
                V::Z,
                &p,
            )?;
            pockets += 1;
        }
    }
    for x in fix_points(w - 2.0 * t) {
        pocket(
            &mut c,
            rear,
            V::new(-w / 2.0 + t + x, d - t, top),
            V::Y,
            V::Z,
            &p,
        )?;
        pockets += 1;
    }
    if g("shelf_rear_fixing") == 0.0 {
        for x in fix_points(w - 2.0 * inset) {
            pocket(
                &mut c,
                shelves[0],
                V::new(-w / 2.0 + inset + x, d - t, shelf),
                V::Z,
                V::Y,
                &p,
            )?;
            pockets += 1;
        }
    } else {
        let mut n = 0;
        for x in fix_points(w - 2.0 * inset) {
            c.drill(
                rear,
                "shelf-rear",
                "through-screw",
                V::new(-w / 2.0 + inset + x, d, shelf + t / 2.0),
                -V::Y,
                g("screw_diameter") + 0.5,
                t,
                "Through screw from outside rear into shelf edge",
            )?;
            n += 1;
        }
        c.hardware.push(json!({"name":"Shelf rear through-screws","quantity":n,"specification":format!("Ø{}; length to suit {} mm rear plus engagement",g("screw_diameter"),t)}));
    }
    c.hardware.push(json!({"name":"Pocket screws","quantity":pockets,"specification":format!("Jig and screw length unconfirmed; generic bore Ø{}, pilot Ø{}, angle {}°",g("pocket_diameter"),g("pocket_pilot"),g("pocket_angle"))}));
    c.hardware.push(json!({"name":"Wood glue","quantity":1,"specification":"For plywood laminations, foot bundles and batten/dowel joints; quantity to be determined from chosen adhesive coverage"}));
    c.metrics = json!({"battenCountPerDoor":count,"battenLength":bl,"pathLength":path,"actualGap":pitch-bw,"pitch":pitch,"cabinetHeight":h,"overallHeight":h+lh,"shelfClearHeight":shelf-base,"hardwareLimit":g("hinge_max_angle"),"swingClearance":g("swing_clearance"),"collisionChordError":r*(1.0-(path/100.0/radius/2.0).cos())+0.04,"pivotLeft":[-w/2.0+g("pivot_inset"),dd-g("pivot_from_end")],"pivotRight":[w/2.0-g("pivot_inset"),dd-g("pivot_from_end")],"footBlankLength":lh*(1.0+g("leg_angle_lr").to_radians().tan().powi(2)+g("leg_angle_fb").to_radians().tan().powi(2)).sqrt()});
    Ok(c)
}
fn pocket(c: &mut Cabinet, idx: usize, edge: V, inward: V, toward: V, p: &Params) -> R<()> {
    let angle = p["pocket_angle"].to_radians();
    let thick = c.pieces[idx].thickness;
    let offset = (thick / 2.0) / angle.tan();
    let entry = edge - toward * offset;
    let direction = (toward * angle.cos() + inward * angle.sin()).normalize();
    let total = offset / angle.cos();
    let shoulder = total - 8.0;
    if shoulder <= 3.0 {
        return Err(
            "Pocket dimensions leave no shoulder; adjust stock thickness or pocket angle.".into(),
        );
    }
    c.drill(
        idx,
        "pocket-joint",
        "pocket",
        entry,
        direction,
        p["pocket_diameter"],
        shoulder,
        "Generic jig geometry. Verify drill stop, screw and driver access before machining",
    )?;
    c.drill(
        idx,
        "pocket-pilot",
        "pilot",
        entry + direction * shoulder,
        direction,
        p["pocket_pilot"],
        total - shoulder + 1.0,
        "Pilot exits mating edge; select screw length for receiving stock",
    )?;
    Ok(())
}
// Arc sampling for collision metadata only; CAD and template curves remain analytic.
fn sample(profile: &[Node], tolerance: f64) -> Vec<[f64; 2]> {
    let mut out = vec![];
    for i in 0..profile.len() {
        let a = &profile[i];
        let b = &profile[(i + 1) % profile.len()];
        out.push([a.x, a.y]);
        if let Some(m) = b.mid {
            let (ax, ay, bx, by, cx, cy) = (a.x, a.y, m[0], m[1], b.x, b.y);
            let det = 2.0 * (ax * (by - cy) + bx * (cy - ay) + cx * (ay - by));
            let ux = ((ax * ax + ay * ay) * (by - cy)
                + (bx * bx + by * by) * (cy - ay)
                + (cx * cx + cy * cy) * (ay - by))
                / det;
            let uy = ((ax * ax + ay * ay) * (cx - bx)
                + (bx * bx + by * by) * (ax - cx)
                + (cx * cx + cy * cy) * (bx - ax))
                / det;
            let r = (ax - ux).hypot(ay - uy);
            let start = (ay - uy).atan2(ax - ux);
            let mut sweep = ((cy - uy).atan2(cx - ux) - start).rem_euclid(2.0 * PI);
            let mid = ((by - uy).atan2(bx - ux) - start).rem_euclid(2.0 * PI);
            if mid > sweep + 1e-7 {
                sweep -= 2.0 * PI
            }
            let n = (sweep.abs() / (2.0 * (1.0 - tolerance / r).acos())).ceil() as usize;
            for k in 1..n {
                let angle = start + sweep * k as f64 / n as f64;
                out.push([ux + r * angle.cos(), uy + r * angle.sin()]);
            }
        }
    }
    out
}
#[cfg(target_arch = "wasm32")]
#[wasm_bindgen(start)]
pub fn start() {
    model_engine::initialize();
}
#[wasm_bindgen]
pub fn catalog_json() -> String {
    CATALOG_JSON.into()
}
#[wasm_bindgen]
pub struct Model {
    cabinet: Cabinet,
}
#[wasm_bindgen]
impl Model {
    #[wasm_bindgen(constructor)]
    pub fn new(input: &str) -> Result<Model, JsValue> {
        Ok(Self {
            cabinet: build(input).map_err(|e| JsValue::from_str(&e))?,
        })
    }
    pub fn assembly_json(&self) -> String {
        self.cabinet.metadata()
    }
    pub fn part_count(&self) -> usize {
        self.cabinet.solids.len()
    }
    pub fn measurements_json(&self) -> String {
        "[]".into()
    }
    pub fn part(&self, index: usize) -> Result<model_engine::Part, JsValue> {
        model_engine::Part::from_solid(
            self.cabinet
                .solids
                .get(index)
                .ok_or_else(|| JsValue::from_str("Unknown part"))?
                .clone(),
        )
    }
    pub fn step_selected(&self, selection: &[u32]) -> Result<Vec<u8>, JsValue> {
        let mut selected = vec![];
        for i in selection {
            selected.push(
                self.cabinet
                    .solids
                    .get(*i as usize)
                    .ok_or_else(|| JsValue::from_str("Unknown part"))?,
            )
        }
        let mut bytes = vec![];
        Solid::write_step(selected, &mut bytes).map_err(|e| JsValue::from_str(&e.to_string()))?;
        Ok(bytes)
    }
    pub fn step(&self) -> Result<Vec<u8>, JsValue> {
        self.step_selected(&(0..self.part_count() as u32).collect::<Vec<_>>())
    }
}
