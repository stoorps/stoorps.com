fn main() {
    println!("cargo:rerun-if-changed=catalog.toml");
    let source = std::fs::read_to_string("catalog.toml").unwrap();
    let value: toml::Value = toml::from_str(&source).unwrap();
    let json = serde_json::to_string(&value).unwrap();
    std::fs::write(
        std::path::PathBuf::from(std::env::var("OUT_DIR").unwrap()).join("catalog.rs"),
        format!("pub const CATALOG_JSON: &str = {json:?};"),
    )
    .unwrap();
}
