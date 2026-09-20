#[path = "../../src/tools/catalog-build.rs"]
mod catalog_build;
fn main() {
    println!("cargo:rerun-if-changed=../../src/tools/catalog-build.rs");
    let value = catalog_build::read_catalog("catalog.yml");
    let json = serde_json::to_string(&value).unwrap();
    std::fs::write(
        std::path::PathBuf::from(std::env::var("OUT_DIR").unwrap()).join("catalog.rs"),
        format!("pub const CATALOG_JSON: &str = {json:?};"),
    )
    .unwrap();
}
