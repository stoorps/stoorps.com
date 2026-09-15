import init, { Part, Bilresa, inspect_step } from "../pkg/models_spike.js";
let part;
let assembly;
let assemblyParts = [];
const payload = (p) => ({
  positions: p.positions(),
  normals: p.normals(),
  indices: p.indices(),
  volume: p.volume(),
  bounds: p.bounds(),
});
const ready = init();
self.onmessage = async ({ data }) => {
  const { id, type, params } = data;
  try {
    await ready;
    if (type === "build") {
      const start = performance.now();
      const next = new Part(...params);
      const result = {
        positions: next.positions(),
        normals: next.normals(),
        indices: next.indices(),
        volume: next.volume(),
        bounds: next.bounds(),
        milliseconds: performance.now() - start,
      };
      part?.free();
      part = next;
      self.postMessage({ id, type, ...result });
    } else if (type === "build-bilresa") {
      const start = performance.now();
      const next = new Bilresa(...params);
      const nextParts = [];
      try {
        for (let i = 0; i < 3; i++) nextParts.push(next.part(i));
        const parts = nextParts.map(payload);
        assemblyParts.forEach((p) => p.free());
        assembly?.free();
        assembly = next;
        assemblyParts = nextParts;
        self.postMessage({
          id,
          type,
          parts,
          milliseconds: performance.now() - start,
        });
      } catch (error) {
        nextParts.forEach((p) => p.free());
        next.free();
        throw error;
      }
    } else if (type === "export-bilresa") {
      if (!assembly) throw new Error("Build the BILRESA parts first.");
      const selected = data.part;
      if (selected === -1 && data.format !== "step")
        throw new Error("Select one part for STL export.");
      const p = assemblyParts[selected];
      if (selected !== -1 && !p) throw new Error("Unknown part.");
      const bytes =
        selected === -1
          ? assembly.step()
          : data.format === "step"
            ? p.step()
            : p.stl();
      self.postMessage({ id, type, bytes });
    } else if (type === "export") {
      if (!part) throw new Error("Build a part first.");
      self.postMessage({
        id,
        type,
        format: data.format,
        bytes: data.format === "step" ? part.step() : part.stl(),
      });
    } else if (type === "inspect") {
      self.postMessage({
        id,
        type,
        result: JSON.parse(inspect_step(data.bytes)),
      });
    }
  } catch (error) {
    self.postMessage({ id, type, error: String(error) });
  }
};
