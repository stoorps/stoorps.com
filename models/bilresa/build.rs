fn main() {
    println!("cargo:rerun-if-changed=catalog.toml");
    let source = std::fs::read_to_string("catalog.toml").expect("Read BILRESA catalogue");
    let value: toml::Value = toml::from_str(&source).expect("Parse BILRESA catalogue");
    let json = serde_json::to_string(&value).expect("Serialize catalogue");
    let mut generated = format!("pub const CATALOG_JSON: &str = {json:?};\n");
    for parameter in value["parameters"].as_array().expect("Parameter list") {
        let key = parameter["key"].as_str().unwrap();
        assert!(key.chars().all(|c| c.is_ascii_lowercase() || c == '_'));
        for field in ["default", "min", "max", "step"] {
            let number = parameter[field]
                .as_float()
                .or_else(|| parameter[field].as_integer().map(|v| v as f64))
                .expect("Numeric parameter");
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
