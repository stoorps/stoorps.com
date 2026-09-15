use cadrum::{DVec3, Solid};
use wasm_bindgen::prelude::*;

#[cfg(target_arch = "wasm32")]
pub fn initialize() {
    cadrum::__anchor_wasi_stub();
    unsafe extern "C" {
        fn __wasm_call_ctors();
    }
    unsafe {
        __wasm_call_ctors();
    }
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
    pub fn from_solid(solid: Solid) -> Result<Self, JsValue> {
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
