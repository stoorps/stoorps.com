# All recipes run from the workspace root. npm scripts also work without just.
default:
    @just --list

# Build every model and the box regression fixture.
wasm:
    npm run wasm:build

# Start the personal site.
dev:
    npm run dev

# Build static HTML/assets, then check TypeScript.
build:
    npm run build
    npm run typecheck

# Run catalogue, link and WASM geometry checks (run wasm first).
test:
    npm run test:all

# Native Rust tests, including model dimensions.
rust-test:
    CARGO_TARGET_DIR=artifacts/native-target cargo test --workspace

# Serve the production files at localhost:4174.
preview:
    npm run preview:static
