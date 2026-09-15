pub mod bilresa;
use cadrum::{DVec3, Solid};
use wasm_bindgen::prelude::*;

#[cfg(target_arch = "wasm32")]
#[wasm_bindgen(start)]
pub fn start() {
    cadrum::__anchor_wasi_stub();
    unsafe extern "C" {
        fn __wasm_call_ctors();
    }
    unsafe {
        __wasm_call_ctors();
    }
}

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
fn js_error(e: impl ToString) -> JsValue {
    JsValue::from_str(&e.to_string())
}

#[wasm_bindgen]
pub struct Part {
    solid: Solid,
    mesh: cadrum::Mesh,
    bounds: [DVec3; 2],
}

impl Part {
    fn from_solid(solid: Solid) -> Result<Self, JsValue> {
        // OCCT's BRepBndLib::Add inflates boxes by mesh deflection after meshing.
        // Capture the geometric bounds before adding triangulation to the BRep.
        let bounds = solid.bounding_box();
        let mesh = Solid::mesh(
            [&solid],
            cadrum::Tessellation {
                deflection_linear: 0.01,
                deflection_angular: 0.15,
                relative_linear: false,
            },
        )
        .map_err(js_error)?;
        Ok(Self {
            solid,
            mesh,
            bounds,
        })
    }
}

#[wasm_bindgen]
impl Part {
    #[wasm_bindgen(constructor)]
    pub fn new(width: f64, length: f64, height: f64, diameter: f64) -> Result<Part, JsValue> {
        let solid = box_with_hole(width, length, height, diameter).map_err(js_error)?;
        Self::from_solid(solid)
    }
    pub fn positions(&self) -> Vec<f32> {
        self.mesh
            .vertices
            .iter()
            .flat_map(|v| [v.x as f32, v.y as f32, v.z as f32])
            .collect()
    }
    pub fn normals(&self) -> Vec<f32> {
        self.mesh
            .normals
            .iter()
            .flat_map(|v| [v.x as f32, v.y as f32, v.z as f32])
            .collect()
    }
    pub fn indices(&self) -> Vec<u32> {
        self.mesh.indices.iter().map(|v| *v as u32).collect()
    }
    pub fn volume(&self) -> f64 {
        self.solid.volume()
    }
    pub fn bounds(&self) -> Vec<f64> {
        self.bounds.iter().flat_map(|v| [v.x, v.y, v.z]).collect()
    }
    pub fn step(&self) -> Result<Vec<u8>, JsValue> {
        let mut bytes = Vec::new();
        Solid::write_step([&self.solid], &mut bytes).map_err(js_error)?;
        Ok(bytes)
    }
    pub fn stl(&self) -> Result<Vec<u8>, JsValue> {
        let mut bytes = Vec::new();
        self.mesh.write_stl(&mut bytes).map_err(js_error)?;
        Ok(bytes)
    }
}

/// Reimport exported STEP in the browser kernel for an actual geometry round trip.
#[wasm_bindgen]
pub fn inspect_step(bytes: &[u8]) -> Result<String, JsValue> {
    let solids = Solid::read_step(&mut std::io::Cursor::new(bytes)).map_err(js_error)?;
    let details: Vec<_> = solids.iter().map(|s| {
        let b = s.bounding_box();
        serde_json::json!({"volume":s.volume(),"bounds":[b[0].x,b[0].y,b[0].z,b[1].x,b[1].y,b[1].z],"faces":s.iter_face().count()})
    }).collect();
    Ok(serde_json::json!({"solids":details}).to_string())
}

#[wasm_bindgen]
pub struct Bilresa {
    solids: Vec<Solid>,
}
#[wasm_bindgen]
impl Bilresa {
    #[wasm_bindgen(constructor)]
    pub fn new(left: f64, right: f64) -> Result<Bilresa, JsValue> {
        if [left, right]
            .iter()
            .any(|v| !v.is_finite() || *v < 0.0 || *v > 8.0 || v.fract() != 0.0)
        {
            return Err(js_error("Switch counts must be whole numbers from 0 to 8."));
        }
        let mut p = bilresa::BilresaParameters::default();
        p.num_switches_left = left as u32;
        p.num_switches_right = right as u32;
        Ok(Self {
            solids: bilresa::build(&p).map_err(js_error)?,
        })
    }
    pub fn part(&self, index: usize) -> Result<Part, JsValue> {
        let solid = self
            .solids
            .get(index)
            .ok_or_else(|| js_error("Unknown part"))?;
        Part::from_solid(solid.clone())
    }
    pub fn step(&self) -> Result<Vec<u8>, JsValue> {
        let mut bytes = Vec::new();
        Solid::write_step(&self.solids, &mut bytes).map_err(js_error)?;
        Ok(bytes)
    }
}
