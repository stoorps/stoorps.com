//! Verified BILRESA inputs. Dimensions are millimetres.
use serde::{Deserialize, Serialize};
use wasm_bindgen::prelude::*;
include!(concat!(env!("OUT_DIR"), "/catalog.rs"));

#[derive(Clone, Debug, Deserialize, Serialize)]
#[serde(default)]
pub struct BilresaParameters {
    pub num_switches_left: u32,
    pub num_switches_right: u32,
    pub switch_pitch: f64,
    pub switch_width: f64,
    pub switch_height: f64,
    pub switch_depth: f64,
    pub switch_tolerance: f64,
    pub mag_width: f64,
    pub mag_height: f64,
    pub mag_thickness: f64,
    pub mag_xy_tolerance: f64,
    pub mag_adhesive_tolerance: f64,
    pub sp_width: f64,
    pub sp_height: f64,
    pub sp_depth: f64,
    pub sp_tolerance: f64,
    pub sp_cluster_height: f64,
    pub sp_cluster_width: f64,
    pub sp_cluster_depth: f64,
    pub sp_cluster_tolerance: f64,
    pub sp_screw_offset_x: f64,
    pub sp_screw_offset_y: f64,
    pub sp_screw_head_d: f64,
    pub sp_screw_shaft_d: f64,
    pub sp_screw_shaft_tol: f64,
    pub bracket_rail_z_offset: f64,
    pub bracket_rail_z_thickness: f64,
    pub bracket_rail_stopper_relief: f64,
    pub bracket_rail_width: f64,
    pub bracket_rail_tolerance: f64,
    pub asm_wall_thickness: f64,
    pub cover_tolerance: f64,
    pub cover_if_depth: f64,
    pub asm_corner_fillets: f64,
    pub asm_face_fillets: f64,
}
impl Default for BilresaParameters {
    fn default() -> Self {
        Self {
            num_switches_left: NUM_SWITCHES_LEFT_DEFAULT,
            num_switches_right: NUM_SWITCHES_RIGHT_DEFAULT,
            switch_pitch: 4.0,
            switch_width: 39.85,
            switch_height: 70.0,
            switch_depth: 18.0,
            switch_tolerance: 0.1,
            mag_width: 16.93,
            mag_height: 46.88,
            mag_thickness: 0.56,
            mag_xy_tolerance: 0.01,
            mag_adhesive_tolerance: 0.3,
            sp_width: 85.6,
            sp_height: 85.35,
            sp_depth: 8.19,
            sp_tolerance: 0.2,
            sp_cluster_height: 27.45,
            sp_cluster_width: 31.9,
            sp_cluster_depth: 6.6,
            sp_cluster_tolerance: 0.01,
            sp_screw_offset_x: 11.69,
            sp_screw_offset_y: 0.0,
            sp_screw_head_d: 6.3,
            sp_screw_shaft_d: 3.1,
            sp_screw_shaft_tol: 0.2,
            bracket_rail_z_offset: 3.5,
            bracket_rail_z_thickness: 2.5,
            bracket_rail_stopper_relief: 3.0,
            bracket_rail_width: 3.0,
            bracket_rail_tolerance: 0.2,
            asm_wall_thickness: 2.5,
            cover_tolerance: 0.05,
            cover_if_depth: 1.5,
            asm_corner_fillets: 20.0,
            asm_face_fillets: 2.5,
        }
    }
}

#[derive(Clone, Debug, Serialize)]
pub struct BilresaDimensions {
    pub asm_left_switch_w_total: f64,
    pub asm_right_switch_w_total: f64,
    pub asm_width: f64,
    pub asm_height: f64,
    pub asm_sp_clearance: f64,
    pub asm_cluster_clearance: f64,
    pub asm_total_depth: f64,
    pub asm_cover_section_depth: f64,
    pub cover_if_extension: f64,
}
impl BilresaParameters {
    pub fn dimensions(&self) -> BilresaDimensions {
        let pitch = self.switch_width + 2.0 * self.switch_tolerance + self.switch_pitch;
        let left = pitch * self.num_switches_left as f64 + self.switch_pitch;
        let right = pitch * self.num_switches_right as f64 + self.switch_pitch;
        let plate = self.sp_depth + self.sp_tolerance;
        let cluster = plate + self.sp_cluster_depth + self.sp_cluster_tolerance;
        BilresaDimensions {
            asm_left_switch_w_total: left,
            asm_right_switch_w_total: right,
            asm_width: self.sp_width
                + 2.0
                    * (self.sp_tolerance
                        + self.asm_wall_thickness
                        + self.bracket_rail_width
                        + self.bracket_rail_tolerance
                        + self.asm_wall_thickness)
                + left
                + right,
            asm_height: self.sp_height + 2.0 * (self.sp_tolerance + self.asm_wall_thickness),
            asm_sp_clearance: plate,
            asm_cluster_clearance: cluster,
            asm_total_depth: self.switch_depth
                + self.switch_tolerance
                + 2.0 * self.asm_wall_thickness,
            asm_cover_section_depth: cluster + self.sp_cluster_tolerance,
            cover_if_extension: self.cover_if_depth + 0.125,
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn verified_onshape_defaults_and_zero_side_margin() {
        let mut p = BilresaParameters::default();
        let d = p.dimensions();
        assert!((d.asm_width - 198.5).abs() < 1e-10);
        assert!((d.asm_total_depth - 23.1).abs() < 1e-10);
        assert!((d.asm_cover_section_depth - 15.01).abs() < 1e-10);
        p.num_switches_left = 0;
        assert_eq!(p.dimensions().asm_left_switch_w_total, 4.0);
        p.num_switches_right = 3;
        assert!((p.dimensions().asm_right_switch_w_total - 136.15).abs() < 1e-10);
    }
}

use cadrum::{DVec3, Solid};
type ModelResult<T> = Result<T, String>;
fn cube(x0: f64, x1: f64, y0: f64, y1: f64, z0: f64, z1: f64) -> Solid {
    Solid::cube(DVec3::new(x0, y0, z0), DVec3::new(x1, y1, z1))
}
fn cut(a: &Solid, b: &Solid) -> ModelResult<Solid> {
    (a - b).build().map_err(|e| e.to_string())
}
fn rounded_box(x0: f64, x1: f64, y0: f64, y1: f64, z0: f64, z1: f64, r: f64) -> ModelResult<Solid> {
    let b = cube(x0, x1, y0, y1, z0, z1);
    if r <= 0.0 {
        return Ok(b);
    }
    let edges: Vec<_> = b
        .iter_edge()
        .filter(|e| {
            let a = e.start_point();
            let b = e.end_point();
            (a.x - b.x).abs() < 1e-7 && (a.y - b.y).abs() < 1e-7
        })
        .collect();
    b.fillet_edges(r, edges).map_err(|e| e.to_string())
}

/// Construct each part independently; touching pieces must not be fused together.
/// Formula provenance and reference comparisons are documented in reference/README.md.
pub fn build(p: &BilresaParameters) -> ModelResult<Vec<Solid>> {
    if p.num_switches_left > NUM_SWITCHES_LEFT_MAX || p.num_switches_right > NUM_SWITCHES_RIGHT_MAX
    {
        return Err("Use 0–8 switches per side for this spike.".into());
    }
    let d = p.dimensions();
    let half_plate = (p.sp_width + 2.0 * p.sp_tolerance) / 2.0;
    let half_height = (p.sp_height + 2.0 * p.sp_tolerance) / 2.0;
    // The source outer horizontal sketch lines are not tied to asm_height.
    // Keep their measured separation; only counts are exposed in this spike.
    let outer_y = 45.185;
    let x0 = -half_plate - d.asm_left_switch_w_total;
    let x1 = half_plate + d.asm_right_switch_w_total;
    let depth = d.asm_total_depth;
    let mut body = cube(x0, x1, -outer_y, outer_y, 0.0, depth);
    for (x, span) in [
        (x0, d.asm_left_switch_w_total),
        (x1, d.asm_right_switch_w_total),
    ] {
        // Empty sides retain their margin and use a radius that fits that wall.
        let radius = p.asm_corner_fillets.min(span - 0.01).min(outer_y - 0.01);
        let edges: Vec<_> = body
            .iter_edge()
            .filter(|e| {
                let a = e.start_point();
                let b = e.end_point();
                (a.x - x).abs() < 1e-6
                    && (b.x - x).abs() < 1e-6
                    && (a.y - b.y).abs() < 1e-6
                    && (a.z - b.z).abs() > 1.0
            })
            .collect();
        body = body
            .fillet_edges(radius, edges)
            .map_err(|e| e.to_string())?;
    }
    let top_edges: Vec<_> = body
        .iter_edge()
        .filter(|e| {
            (e.start_point().z - depth).abs() < 1e-6 && (e.end_point().z - depth).abs() < 1e-6
        })
        .collect();
    body = body
        .fillet_edges(p.asm_face_fillets, top_edges)
        .map_err(|e| e.to_string())?;
    body = cut(
        &body,
        &cube(
            -half_plate,
            half_plate,
            -half_height,
            half_height,
            -1.0,
            depth + 1.0,
        ),
    )?;
    let rail_x = half_plate + p.bracket_rail_width + p.bracket_rail_tolerance;
    // The source's upper opening starts at sp_depth - sp_tolerance (Extrude 1).
    body = cut(
        &body,
        &cube(
            -rail_x,
            rail_x,
            half_height,
            outer_y + 1.0,
            p.sp_depth - p.sp_tolerance,
            depth + 1.0,
        ),
    )?;
    let pocket_w = p.switch_width + 2.0 * p.switch_tolerance;
    let pocket_h = p.switch_height + 2.0 * p.switch_tolerance;
    for (sign, count) in [(-1.0, p.num_switches_left), (1.0, p.num_switches_right)] {
        for n in 0..count {
            let cx = sign
                * (half_plate
                    + p.switch_pitch
                    + pocket_w / 2.0
                    + n as f64 * (pocket_w + p.switch_pitch));
            let pocket = rounded_box(
                cx - pocket_w / 2.0,
                cx + pocket_w / 2.0,
                -pocket_h / 2.0,
                pocket_h / 2.0,
                p.asm_wall_thickness,
                depth + 1.0,
                p.switch_width / 2.0,
            )?;
            body = cut(&body, &pocket)?;
            let mw = p.mag_width + 2.0 * p.mag_xy_tolerance;
            let mh = p.mag_height + 2.0 * p.mag_xy_tolerance;
            let magnet = rounded_box(
                cx - mw / 2.0,
                cx + mw / 2.0,
                -mh / 2.0,
                mh / 2.0,
                p.asm_wall_thickness - p.mag_adhesive_tolerance,
                p.asm_wall_thickness + 0.1,
                mw / 2.0 - 0.001,
            )?;
            body = cut(&body, &magnet)?;
        }
    }
    // Side grooves extend to y=-43; the central opening ends at y=-42.875.
    let rail_z = d.asm_cluster_clearance;
    let rail_top = rail_z + p.bracket_rail_z_thickness + p.bracket_rail_tolerance;
    for (a, b) in [(-rail_x, -half_plate), (half_plate, rail_x)] {
        body = cut(
            &body,
            &cube(a, b, -half_plate, outer_y + 1.0, rail_z, rail_top),
        )?;
    }
    let blank_half = (p.sp_width + 2.0 * (p.sp_tolerance + p.bracket_rail_width)
        - p.bracket_rail_tolerance)
        / 2.0;
    let blank_z = rail_z + p.bracket_rail_tolerance / 2.0;
    let blank = cube(
        -blank_half,
        blank_half,
        -half_plate + p.bracket_rail_tolerance,
        half_plate + p.cover_if_depth,
        blank_z,
        blank_z + p.bracket_rail_z_thickness,
    );
    let cap_top = p.sp_depth - p.sp_tolerance + depth - d.asm_sp_clearance;
    let radius = p.asm_face_fillets;
    // Build a wider filleted strip, then trim it to the cap thickness. The
    // source's 2.5 mm radius exceeds the 2.31 mm wall, leaving a partial arc.
    let mut top = cube(
        -rail_x,
        rail_x,
        half_height - radius - 1.0,
        outer_y,
        p.sp_depth - p.sp_tolerance,
        cap_top,
    );
    let edge: Vec<_> = top
        .iter_edge()
        .filter(|e| {
            let a = e.start_point();
            let b = e.end_point();
            (a.y - outer_y).abs() < 1e-6
                && (b.y - outer_y).abs() < 1e-6
                && (a.z - cap_top).abs() < 1e-6
                && (b.z - cap_top).abs() < 1e-6
        })
        .collect();
    top = top.fillet_edges(radius, edge).map_err(|e| e.to_string())?;
    top = cut(
        &top,
        &cube(
            -rail_x - 1.0,
            rail_x + 1.0,
            half_height - radius - 2.0,
            half_height,
            p.sp_depth - p.sp_tolerance - 1.0,
            depth + 1.0,
        ),
    )?;
    top = cut(
        &top,
        &cube(
            -rail_x - 1.0,
            rail_x + 1.0,
            half_height - 1.0,
            half_plate + p.cover_if_depth + p.cover_tolerance,
            blank_z - p.cover_tolerance,
            blank_z + p.bracket_rail_z_thickness + p.cover_tolerance,
        ),
    )?;
    Ok(vec![body, blank, top])
}

#[cfg(target_arch = "wasm32")]
#[wasm_bindgen(start)]
pub fn start() {
    model_engine::initialize();
}

/// Expose exactly the metadata used when this model was compiled.
#[wasm_bindgen]
pub fn catalog_json() -> String {
    CATALOG_JSON.into()
}

#[derive(Deserialize)]
#[serde(deny_unknown_fields)]
struct Counts {
    num_switches_left: u32,
    num_switches_right: u32,
}

#[wasm_bindgen]
pub struct Model {
    solids: Vec<Solid>,
}
#[wasm_bindgen]
impl Model {
    #[wasm_bindgen(constructor)]
    pub fn new(parameters: &str) -> Result<Model, JsValue> {
        let counts: Counts =
            serde_json::from_str(parameters).map_err(|e| JsValue::from_str(&e.to_string()))?;
        if counts.num_switches_left < NUM_SWITCHES_LEFT_MIN
            || counts.num_switches_left > NUM_SWITCHES_LEFT_MAX
            || counts.num_switches_right < NUM_SWITCHES_RIGHT_MIN
            || counts.num_switches_right > NUM_SWITCHES_RIGHT_MAX
        {
            return Err(JsValue::from_str(
                "Switch counts must be whole numbers from 0 to 8.",
            ));
        }
        let p = BilresaParameters {
            num_switches_left: counts.num_switches_left,
            num_switches_right: counts.num_switches_right,
            ..Default::default()
        };
        Ok(Self {
            solids: build(&p).map_err(|e| JsValue::from_str(&e))?,
        })
    }
    pub fn part(&self, index: usize) -> Result<model_engine::Part, JsValue> {
        let solid = self
            .solids
            .get(index)
            .ok_or_else(|| JsValue::from_str("Unknown part"))?;
        model_engine::Part::from_solid(solid.clone())
    }
    pub fn step(&self) -> Result<Vec<u8>, JsValue> {
        let mut bytes = Vec::new();
        Solid::write_step(&self.solids, &mut bytes)
            .map_err(|e| JsValue::from_str(&e.to_string()))?;
        Ok(bytes)
    }
}
