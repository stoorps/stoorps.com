// Shared by model build scripts so native Cargo and site tooling resolve the same files.
use serde_json::Value;
use std::path::{Path, PathBuf};

pub fn read_catalog(file: &str) -> Value {
    fn load(file: &Path, root: &Path, stack: &mut Vec<PathBuf>) -> Value {
        let file = file.canonicalize().expect("Read catalogue reference");
        assert!(
            file.starts_with(root),
            "Catalogue reference escapes model directory"
        );
        assert!(
            !stack.contains(&file),
            "Circular catalogue reference: {}",
            file.display()
        );
        println!("cargo:rerun-if-changed={}", file.display());
        stack.push(file.clone());
        let source = std::fs::read_to_string(&file).expect("Read YAML catalogue");
        let mut yaml: serde_yaml_ng::Value =
            serde_yaml_ng::from_str(&source).expect("Parse YAML catalogue");
        yaml.apply_merge().expect("Resolve YAML merge keys");
        let value =
            serde_json::to_value(yaml).expect("Catalogue must contain JSON-compatible values");
        let result = resolve(value, &file, root, stack);
        stack.pop();
        result
    }
    fn resolve(value: Value, file: &Path, root: &Path, stack: &mut Vec<PathBuf>) -> Value {
        match value {
            Value::Array(items) => {
                let mut result = vec![];
                for item in items {
                    let reference = item.as_object().is_some_and(|m| m.contains_key("$ref"));
                    let resolved = resolve(item, file, root, stack);
                    if reference && resolved.is_array() {
                        result.extend(resolved.as_array().unwrap().iter().cloned());
                    } else {
                        result.push(resolved);
                    }
                }
                Value::Array(result)
            }
            Value::Object(mut map) => {
                if let Some(reference) = map.remove("$ref") {
                    let reference = reference.as_str().expect("Reference must be a string");
                    assert!(
                        map.is_empty()
                            && !reference.is_empty()
                            && !Path::new(reference).is_absolute()
                            && !reference.contains(':')
                            && !reference.contains('#'),
                        "Invalid local catalogue reference"
                    );
                    load(&file.parent().unwrap().join(reference), root, stack)
                } else {
                    Value::Object(
                        map.into_iter()
                            .map(|(key, value)| (key, resolve(value, file, root, stack)))
                            .collect(),
                    )
                }
            }
            other => other,
        }
    }
    let file = Path::new(file).canonicalize().expect("Locate catalog.yml");
    normalize_groups(load(&file, file.parent().unwrap(), &mut vec![]))
}

fn normalize_groups(mut catalog: Value) -> Value {
    let Some(root) = catalog.as_object_mut() else {
        return catalog;
    };
    let Some(groups) = root.remove("parameter_groups") else {
        return catalog;
    };
    assert!(
        !root.contains_key("parameters"),
        "Use parameter_groups instead of parameters"
    );
    let groups = groups.as_array().expect("parameter_groups must be a list");
    assert!(!groups.is_empty(), "parameter_groups cannot be empty");
    let mut names = std::collections::HashSet::new();
    let mut parameters = vec![];
    for group in groups {
        let group = group
            .as_object()
            .expect("Parameter group must be a mapping");
        assert!(
            group.keys().all(|k| k == "group_name" || k == "parameters"),
            "Unknown parameter group field"
        );
        let name = group
            .get("group_name")
            .and_then(Value::as_str)
            .expect("Missing group_name");
        assert!(
            !name.trim().is_empty() && names.insert(name),
            "Group names must be non-empty and unique"
        );
        let items = group
            .get("parameters")
            .and_then(Value::as_array)
            .expect("Group parameters must be a list");
        assert!(!items.is_empty(), "Group parameters cannot be empty");
        for item in items {
            let mut item = item
                .as_object()
                .expect("Parameter must be a mapping")
                .clone();
            assert!(
                !item.contains_key("group"),
                "Grouped parameters inherit group_name"
            );
            item.insert("group".into(), Value::String(name.into()));
            parameters.push(Value::Object(item));
        }
    }
    root.insert("parameters".into(), Value::Array(parameters));
    catalog
}
