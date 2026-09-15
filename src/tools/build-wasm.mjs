import { spawnSync } from "node:child_process";
import { mkdir, readFile } from "node:fs/promises";
import { parse } from "smol-toml";
import { generateModels } from "./generate-models.mjs";
const image =
  process.env.CADRUM_BUILD_IMAGE ||
  "ghcr.io/lzpel/cross-wasm32-unknown-unknown@sha256:4dc88df287da141e67588ba80ebf1d14d5cb24f5b05302c57b11b2a1e9bed1df";
const models = await generateModels();
const jobs = models.map((m) => ({
  crate: m.crate,
  out: `src/generated/models/${m.catalog.id}`,
}));
const fixture = parse(
  await readFile("src/tests/fixtures/box/Cargo.toml", "utf8"),
);
if (!process.argv.includes("--models-only"))
  jobs.push({ crate: fixture.package.name, out: "src/generated/fixtures/box" });
const script = `set -euo pipefail
cargo build --release --locked -p "$1"
if [ "$(/work/target/tools/bin/wasm-bindgen --version 2>/dev/null || true)" != "wasm-bindgen 0.2.128" ]; then
 env -u CARGO_BUILD_TARGET cargo install wasm-bindgen-cli --version 0.2.128 --locked --root /work/target/tools
fi
/work/target/tools/bin/wasm-bindgen "target/wasm32-unknown-unknown/release/$2.wasm" --target web --out-name model --out-dir "$3"
`;
for (const job of jobs) {
  await mkdir(job.out, { recursive: true });
  const result = spawnSync(
    "docker",
    [
      "run",
      "--rm",
      "-v",
      `${process.cwd()}:/work`,
      "-v",
      "models-cargo-cache:/root/.cargo/registry",
      "-w",
      "/work",
      image,
      "bash",
      "-c",
      script,
      "build-model",
      job.crate,
      job.crate.replaceAll("-", "_"),
      job.out,
    ],
    { stdio: "inherit" },
  );
  if (result.error) throw result.error;
  if (result.status !== 0) process.exit(result.status || 1);
}
