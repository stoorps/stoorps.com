#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
IMAGE=${CADRUM_BUILD_IMAGE:-ghcr.io/lzpel/cross-wasm32-unknown-unknown@sha256:4dc88df287da141e67588ba80ebf1d14d5cb24f5b05302c57b11b2a1e9bed1df}
docker run --rm -v "$PWD":/work -v models-cargo-cache:/root/.cargo/registry -w /work "$IMAGE" bash -c '
set -euo pipefail
cargo build --release --locked
if [ "$(/work/target/tools/bin/wasm-bindgen --version 2>/dev/null || true)" != "wasm-bindgen 0.2.128" ]; then
  env -u CARGO_BUILD_TARGET cargo install wasm-bindgen-cli --version 0.2.128 --locked --root /work/target/tools
fi
/work/target/tools/bin/wasm-bindgen target/wasm32-unknown-unknown/release/models_spike.wasm --target web --out-dir pkg
'
