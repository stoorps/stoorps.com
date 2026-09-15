use cadrum::{DVec3, Solid};
use wasm_bindgen::prelude::*;
/// All dimensions are millimetres. A centred cylindrical cutter extends beyond both faces.
pub fn box_with_hole(width: f64, length: f64, height: f64, diameter: f64) -> Result<Solid, String> {
    if [width, length, height, diameter]
        .iter()
        .any(|v| !v.is_finite() || *v < 0.5 || *v > 200.0)
    {
        return Err("Dimensions must be finite numbers between 0.5 and 200 mm.".into());
    }
    if diameter > width.min(length) - 1.0 {
        return Err("Leave at least 0.5 mm of material around the hole.".into());
    }
    let block = Solid::cube(DVec3::ZERO, DVec3::new(width, length, height));
    let cutter = Solid::cylinder(diameter / 2.0, DVec3::Z * (height + 2.0)).translate(DVec3::new(
        width / 2.0,
        length / 2.0,
        -1.0,
    ));
    (&block - &cutter).build().map_err(|e| e.to_string())
}

#[cfg(target_arch = "wasm32")]
#[wasm_bindgen(start)]
pub fn start() {
    model_engine::initialize();
}
#[wasm_bindgen]
pub fn make_box(
    width: f64,
    length: f64,
    height: f64,
    diameter: f64,
) -> Result<model_engine::Part, JsValue> {
    let solid =
        box_with_hole(width, length, height, diameter).map_err(|e| JsValue::from_str(&e))?;
    model_engine::Part::from_solid(solid)
}
