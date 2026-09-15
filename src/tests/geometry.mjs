export function assert(condition, message) {
  if (!condition) throw new Error(message);
}
export function close(actual, expected, tolerance, label) {
  assert(
    Math.abs(actual - expected) <= tolerance,
    `${label}: ${actual} != ${expected}`,
  );
}
export function validateStl(bytes, params) {
  const view = new DataView(bytes.buffer, bytes.byteOffset, bytes.byteLength);
  const count = view.getUint32(80, true);
  assert(bytes.length === 84 + 50 * count, "Binary STL length");
  let volume = 0;
  const min = [Infinity, Infinity, Infinity],
    max = [-Infinity, -Infinity, -Infinity],
    edges = new Map();
  const key = (v) => v.map((x) => x.toFixed(5)).join(",");
  for (let i = 0; i < count; i++) {
    const vertices = [];
    for (let j = 0; j < 3; j++) {
      const v = [0, 1, 2].map((k) =>
        view.getFloat32(84 + 50 * i + 12 + j * 12 + k * 4, true),
      );
      assert(v.every(Number.isFinite), "Finite STL vertices");
      vertices.push(v);
      v.forEach((x, k) => {
        min[k] = Math.min(min[k], x);
        max[k] = Math.max(max[k], x);
      });
    }
    const [a, b, c] = vertices;
    volume +=
      (a[0] * (b[1] * c[2] - b[2] * c[1]) +
        a[1] * (b[2] * c[0] - b[0] * c[2]) +
        a[2] * (b[0] * c[1] - b[1] * c[0])) /
      6;
    for (let j = 0; j < 3; j++) {
      const a = key(vertices[j]),
        b = key(vertices[(j + 1) % 3]);
      assert(a !== b, "Non-degenerate STL edge");
      const k = [a, b].sort().join("|");
      const edge = edges.get(k) || { count: 0, balance: 0 };
      edge.count++;
      edge.balance += a < b ? 1 : -1;
      edges.set(k, edge);
    }
  }
  assert(
    [...edges.values()].every((e) => e.count === 2 && e.balance === 0),
    "STL closed, oriented two-manifold",
  );
  const [w, l, h, d] = params,
    expected = (w * l - (Math.PI * d * d) / 4) * h;
  close(volume, expected, expected * 0.005, "STL volume within 0.5%");
  for (let k = 0; k < 3; k++) {
    close(min[k], 0, 0.0001, "STL minimum");
    close(max[k], params[k], 0.0001, "STL maximum");
  }
  return {
    triangles: count,
    volume,
    bounds: [...min, ...max],
    watertight: true,
    consistentWinding: true,
  };
}
export async function verifyCase(request, params) {
  const build = await request("build", { params });
  const [w, l, h, d] = params,
    expected = (w * l - (Math.PI * d * d) / 4) * h;
  close(build.volume, expected, 1e-6, "Exact BRep volume");
  for (let k = 0; k < 3; k++) {
    close(build.bounds[k], 0, 0.0001, "BRep minimum");
    close(build.bounds[k + 3], params[k], 0.0001, "BRep maximum");
  }
  const step = (await request("export", { format: "step" })).bytes;
  const header = new TextDecoder().decode(step);
  assert(header.startsWith("ISO-10303-21;"), "STEP header");
  assert(header.includes("CYLINDRICAL_SURFACE"), "Analytic cylinder preserved");
  const { result } = await request("inspect", { bytes: step });
  assert(result.solids.length === 1, "STEP contains one solid");
  close(result.solids[0].volume, expected, 1e-5, "STEP reimport volume");
  result.solids[0].bounds.forEach((x, k) =>
    close(x, build.bounds[k], 0.0001, "STEP reimport bounds"),
  );
  const stl = (await request("export", { format: "stl" })).bytes;
  return {
    params,
    milliseconds: build.milliseconds,
    volume: build.volume,
    stepBytes: step.length,
    stepReimport: result,
    stl: validateStl(stl, params),
    step,
    stlBytes: stl,
  };
}
