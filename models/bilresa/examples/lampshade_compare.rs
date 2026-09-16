//! Isolated CAD-kernel comparison, not a production model or replacement ABI.
use cadrum::{Boolean, DVec3, Edge, Solid};
use serde_json::{json, Value};
use std::{
    f64::consts::{PI, TAU},
    fs::File,
    time::Instant,
};
type Result<T> = std::result::Result<T, Box<dyn std::error::Error>>;
fn main() -> Result<()> {
    let p: Value =
        serde_json::from_reader(File::open("artifacts/lampshade-comparison/input.json")?)?;
    let get = |k: &str| p[k].as_f64().unwrap();
    let h = get("height");
    let w = get("thickness");
    let lobes = get("lobes");
    let radius = |a: f64, z: f64| {
        let t = z / h;
        let u = if t < 0.5 { t * 2.0 } else { t * 2.0 - 1.0 };
        let blend = u * (1.0 - get("curve")) + (1.0 - (PI * u).cos()) / 2.0 * get("curve");
        let (a0, b) = if t < 0.5 {
            (get("bottom_diameter"), get("middle_diameter"))
        } else {
            (get("middle_diameter"), get("top_diameter"))
        };
        (a0 + (b - a0) * blend) / 2.0
            + get("ripple_depth") * (lobes * (a - get("twist") * PI / 180.0 * z / h)).sin()
    };
    let started = Instant::now();
    let body = |offset: f64, z0: f64, z1: f64| {
        Solid::bspline(37, (lobes as usize * 12).max(192), false, |i, j| {
            let z = z0 + (z1 - z0) * i as f64 / 36.0;
            let a = -TAU * j as f64 / (lobes as usize * 12).max(192) as f64;
            let r = radius(a, z) + offset;
            DVec3::new(r * a.cos(), r * a.sin(), z)
        })
        .and_then(|solid| Solid::sew(solid.iter_face(), 1e-6))
    };
    eprintln!("building curved shell");
    let outer = body(w / 2.0, 0.0, h)?;
    let inner = body(-w / 2.0, -1.0, h + 1.0)?;
    eprintln!("outer {} inner {}", outer.volume(), inner.volume());
    let mut shade = (&outer - &inner).build()?;
    eprintln!(
        "shell: {:.3}s, volume {}",
        started.elapsed().as_secs_f64(),
        shade.volume()
    );
    let cols = p["cols"].as_u64().unwrap() as usize;
    let rows = p["rows"].as_u64().unwrap() as usize;
    let hole = p["hole"].as_array().unwrap();
    let row_limit = std::env::var("CAD_ROWS")
        .ok()
        .and_then(|s| s.parse::<usize>().ok())
        .unwrap_or(rows);
    let mut cutters = Vec::new();
    for row in 0..row_limit.min(rows) {
        for col in 0..cols {
            let center =
                TAU * (col as f64 + 0.5 + get("cell_offset") * (row % 2) as f64) / cols as f64;
            let zc = h * (row as f64 + 0.5) / rows as f64;
            let radial = DVec3::new(center.cos(), center.sin(), 0.0);
            let tangent = DVec3::new(-center.sin(), center.cos(), 0.0);
            // Planar radial cutters approximate the existing wrapped openings.
            // The shell itself is a smooth interpolated B-rep, not a triangle mesh.
            let pts: Vec<_> = hole
                .iter()
                .map(|q| {
                    let dx = q[0].as_f64().unwrap();
                    let dy = q[1].as_f64().unwrap();
                    radial * (radius(center, zc) - 15.0)
                        + tangent * (dx * TAU / cols as f64 * radius(center, zc))
                        + DVec3::Z * (zc + dy * h / rows as f64)
                })
                .collect();
            let edges: Vec<_> = pts
                .iter()
                .enumerate()
                .map(|(i, a)| Edge::line(*a, pts[(i + 1) % pts.len()]))
                .collect::<std::result::Result<_, _>>()?;
            cutters.push(Solid::extrude(&edges, radial * 30.0)?);
        }
    }
    eprintln!("cutting {} openings in one operation", cutters.len());
    let tools = cutters
        .iter()
        .map(Boolean::from)
        .reduce(|a, b| a + b)
        .unwrap();
    shade = (&shade - tools).build()?;
    let generation = started.elapsed().as_secs_f64();
    let mesh_start = Instant::now();
    let mesh = Solid::mesh(
        [&shade],
        cadrum::Tessellation {
            deflection_linear: 0.01,
            deflection_angular: 0.15,
            relative_linear: false,
        },
    )?;
    let meshing = mesh_start.elapsed().as_secs_f64();
    mesh.write_stl(&mut File::create(
        "artifacts/lampshade-comparison/cadrum.stl",
    )?)?;
    Solid::write_step(
        [&shade],
        &mut File::create("artifacts/lampshade-comparison/cadrum.step")?,
    )?;
    let result = json!({"generationSeconds":generation,"meshingSeconds":meshing,"rows":row_limit.min(rows),"totalRows":rows,"volume":shade.volume(),"cadFaces":shade.iter_face().count(),"positions":mesh.vertices.iter().flat_map(|v|[v.x,v.y,v.z]).collect::<Vec<_>>(),"normals":mesh.normals.iter().flat_map(|v|[v.x,v.y,v.z]).collect::<Vec<_>>(),"indices":mesh.indices});
    serde_json::to_writer(
        File::create("artifacts/lampshade-comparison/cadrum.json")?,
        &result,
    )?;
    eprintln!("complete: geometry {generation:.3}s, meshing {meshing:.3}s");
    Ok(())
}
