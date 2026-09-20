#[path = "../../src/tools/catalog-build.rs"]
mod catalog_build;
fn main() {
    println!("cargo:rerun-if-changed=../../src/tools/catalog-build.rs");
    let value = catalog_build::read_catalog("catalog.yml");
    let json = serde_json::to_string(&value).expect("Serialize catalogue");
    let mut generated = format!("pub const CATALOG_JSON: &str = {json:?};\n");
    for parameter in value["parameters"].as_array().expect("Parameter list") {
        let key = parameter["key"].as_str().unwrap();
        assert!(key.chars().all(|c| c.is_ascii_lowercase() || c == '_'));
        for field in ["default", "min", "max", "step"] {
            let number = parameter[field].as_f64().expect("Numeric parameter");
            let kind = if key.starts_with("num_") {
                "u32"
            } else {
                "f64"
            };
            let literal = if kind == "u32" {
                format!("{}", number as u32)
            } else {
                format!("{number:?}")
            };
            generated.push_str(&format!(
                "pub const {}_{}: {kind} = {literal};\n",
                key.to_uppercase(),
                field.to_uppercase()
            ));
        }
    }
    let path = std::path::PathBuf::from(std::env::var("OUT_DIR").unwrap()).join("catalog.rs");
    std::fs::write(path, generated).unwrap();
}
