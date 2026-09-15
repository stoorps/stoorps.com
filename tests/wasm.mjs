import { readFile, writeFile, mkdir } from "node:fs/promises";
import init, { Part, inspect_step } from "../pkg/models_spike.js";
import { verifyCase, assert } from "./geometry.mjs";
await init({
  module_or_path: await readFile(
    new URL("../pkg/models_spike_bg.wasm", import.meta.url),
  ),
});
let part;
async function request(type, data) {
  if (type === "build") {
    const start = performance.now();
    const next = new Part(...data.params);
    part?.free();
    part = next;
    return {
      volume: part.volume(),
      bounds: part.bounds(),
      milliseconds: performance.now() - start,
    };
  }
  if (type === "export")
    return { bytes: data.format === "step" ? part.step() : part.stl() };
  if (type === "inspect")
    return { result: JSON.parse(inspect_step(data.bytes)) };
}
await mkdir("artifacts", { recursive: true });
const reports = [];
for (const params of [
  [40, 30, 12, 10],
  [52, 30, 12, 10],
  [40, 30, 12, 18],
  [60, 45, 20, 8],
  [12, 12, 2, 11],
]) {
  const result = await verifyCase(request, params),
    name = params.join("x");
  await writeFile(`artifacts/box-${name}.step`, result.step);
  await writeFile(`artifacts/box-${name}.stl`, result.stlBytes);
  delete result.step;
  delete result.stlBytes;
  reports.push(result);
}
for (const params of [
  [NaN, 30, 12, 10],
  [40, 30, 12, 30],
  [40, 30, 0, 10],
  [201, 30, 12, 10],
]) {
  let rejected = false;
  try {
    new Part(...params);
  } catch {
    rejected = true;
  }
  assert(rejected, "Invalid input rejected");
}
part.free();
const report = {
  status: "PASS",
  runtime: process.version,
  cases: reports,
  invalidInputsRejected: 4,
};
await writeFile(
  "artifacts/wasm-validation.json",
  JSON.stringify(report, null, 2),
);
console.log(JSON.stringify(report, null, 2));
