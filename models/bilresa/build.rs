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
            let number = parameter[field].as_integer().expect("Integer count limits");
            assert!(number >= 0 && number <= u32::MAX as i64);
            generated.push_str(&format!(
                "pub const {}_{}: u32 = {number};\n",
                key.to_uppercase(),
                field.to_uppercase()
            ));
        }
    }
    let path = std::path::PathBuf::from(std::env::var("OUT_DIR").unwrap()).join("catalog.rs");
    std::fs::write(path, generated).unwrap();
}
