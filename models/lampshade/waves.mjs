import { surfacePlan } from "./surface.mjs";
export function waveNodes(p) {
  return Array.from({ length: p.wave_nodes }, (_, i) => ({
    x: p[`wave_${i}_x`],
    y:
      p.wave_arch === 1
        ? Math.round(Math.sin(Math.PI * p[`wave_${i}_x`]) * 100) / 100
        : p[`wave_${i}_y`],
    slope:
      p.wave_arch === 1
        ? Math.round(Math.PI * Math.cos(Math.PI * p[`wave_${i}_x`]) * 10) / 10
        : p[`wave_${i}_slope`],
    ...(i === 0 ? { arch: p.wave_arch || 0 } : {}),
  }));
}
export function waveValue(nodes, t) {
  t = ((t % 1) + 1) % 1;
  if (nodes[0].arch === 1) return Math.sin(Math.PI * t);
  for (let i = 0; i < nodes.length; i++) {
    const a = nodes[i],
      b = nodes[(i + 1) % nodes.length],
      end = i === nodes.length - 1 ? 1 : b.x;
    if (t < a.x || t > end) continue;
    const u = (t - a.x) / (end - a.x);
    return (
      (2 * u ** 3 - 3 * u * u + 1) * a.y +
      (u ** 3 - 2 * u * u + u) * a.slope * (end - a.x) +
      (-2 * u ** 3 + 3 * u * u) * b.y +
      (u ** 3 - u * u) *
        (i === nodes.length - 1 && nodes[0].arch === 2 ? -b.slope : b.slope) *
        (end - a.x)
    );
  }
  return 0;
}
export function applyWave(p, nodes) {
  const arch = p.wave_arch ? 2 : 0;
  nodes = nodes.map((n, i) => ({ ...n, ...(i === 0 ? { arch } : {}) }));
  const samples = Array.from({ length: 1024 }, (_, i) =>
    waveValue(nodes, i / 1024),
  );
  const lo = Math.min(...samples),
    span = Math.max(...samples) - lo;
  if (span > 0.01)
    nodes = nodes.map((n) => ({
      ...n,
      y: Math.round(((n.y - lo) / span) * 100) / 100,
      slope: Math.max(
        -12,
        Math.min(12, Math.round((n.slope / span) * 10) / 10),
      ),
    }));
  const next = { ...p, wave_nodes: nodes.length, wave_arch: arch };
  nodes.forEach((n, i) =>
    Object.entries(n)
      .filter(([k]) => ["x", "y", "slope"].includes(k))
      .forEach(([k, v]) => (next[`wave_${i}_${k}`] = v)),
  );
  return next;
}
export function wavePreset(name) {
  return [0, 0.5, 1, 0.5].map((y, i) => ({
    x: i / 4,
    y,
    slope: name === "Smooth" ? [0, 3, 0, -3][i] : 0,
  }));
}

// Bounds prevent centre-line crossings and contact with non-adjacent layers.
export function strandPlan(p) {
  const nodes = waveNodes(p);
  const values = Array.from({ length: 1024 }, (_, i) =>
    waveValue(nodes, i / 1024),
  );
  const low = Math.min(...values),
    span = Math.max(...values) - low;
  if (!(span > 0.01)) return { error: "Give the wave some height." };
  const wave = (t) => ((waveValue(nodes, t) - low) / span) * p.wave_height;
  const delta = Math.max(
    ...values.map((_, i) => wave(i / 1024) - wave(i / 1024 + 0.5)),
  );
  const d = p.thickness;
  const joint = Math.min(0.95, Math.max(0.3, p.extrusion_width / d));
  const minMerge = Math.ceil(100 * (1 - Math.sqrt(1 - joint * joint)));
  const minSpacing = Math.max(delta, (p.wave_height + d + 0.1) / 2);
  const maxMerge = Math.min(
    100,
    Math.floor(100 * (1 - (minSpacing - delta) / d)),
  );
  const maxSpacing = delta + d * (1 - minMerge / 100);
  const available = p.height - p.wave_height - d;
  const minIntervals = Math.max(1, Math.ceil(available / maxSpacing));
  const maxIntervals = Math.floor(available / minSpacing);
  if (maxMerge < minMerge || maxIntervals < minIntervals)
    return {
      error:
        "This wave cannot join safely at this shade height. Adjust wave height or shape.",
      minMerge,
      maxMerge,
    };
  const merge = Math.max(minMerge, Math.min(maxMerge, p.wave_merge));
  const target = delta + d * (1 - merge / 100);
  const intervals = Math.max(
    minIntervals,
    Math.min(maxIntervals, Math.round(available / target)),
  );
  const spacing = available / intervals;
  const perimeter = surfacePlan(p).maxCircumference;
  const repeats = Math.max(3, Math.round(perimeter / p.wave_width));
  return {
    spacing,
    layers: intervals + 1,
    repeats,
    width: perimeter / repeats,
    minMerge,
    maxMerge,
    actualMerge: 100 * (1 - (spacing - delta) / d),
  };
}

export function legacyWaveDefaults(p) {
  const next = { ...p };
  [0, 1, 0, -1].forEach((y, i) => {
    next[`wave_${i}_y`] = y;
    next[`wave_${i}_slope`] = [6, 0, -6, 0][i];
  });
  return next;
}
export function migrateWave(p) {
  const next = { ...p };
  for (let i = 0; i < 8; i++) {
    next[`wave_${i}_y`] = Math.round((p[`wave_${i}_y`] + 1) * 50) / 100;
    next[`wave_${i}_slope`] = Math.round(p[`wave_${i}_slope`] * 5) / 10;
  }
  next.wave_width = Math.max(
    4,
    Math.min(
      200,
      Math.round((surfacePlan(p).maxCircumference / p.wave_peaks) * 10) / 10,
    ),
  );
  next.wave_merge = Math.max(
    1,
    Math.min(
      100,
      Math.round(100 * (1 - (p.wave_spacing - p.wave_height) / p.thickness)),
    ),
  );
  const plan = strandPlan(next);
  if (!plan.error)
    next.wave_merge = Math.max(
      plan.minMerge,
      Math.min(plan.maxMerge, next.wave_merge),
    );
  return next;
}

// Projected open area of one alternating-layer repeat; excludes hardware.
export function strandOpenArea(p, circumference) {
  const nodes = waveNodes(p),
    values = Array.from({ length: 512 }, (_, i) => waveValue(nodes, i / 512));
  const low = Math.min(...values),
    span = Math.max(...values) - low;
  const available = p.height - p.wave_height - p.thickness;
  if (span < 0.01 || available <= 0) return null;
  const plan = strandPlan(p);
  if (plan.error) return null;
  const spacing = plan.spacing,
    width = circumference / plan.repeats,
    height = 2 * spacing;
  const nx = 128,
    ny = 128,
    r = p.thickness / 2,
    mask = new Uint8Array(nx * ny);
  const wave = (t) =>
    ((waveValue(nodes, t) - low) / span) * p.wave_height - p.wave_height / 2;
  const reach = Math.ceil((p.wave_height / 2 + r) / spacing) + 2;
  for (let row = -reach; row <= reach + 2; row++)
    for (let col = -1; col <= 1; col++)
      for (let j = 0; j < 96; j++) {
        const ax = (col + j / 96) * width,
          bx = (col + (j + 1) / 96) * width;
        const ay = row * spacing + wave(j / 96 + (((row % 2) + 2) % 2) * 0.5),
          by = row * spacing + wave((j + 1) / 96 + (((row % 2) + 2) % 2) * 0.5);
        const dx = bx - ax,
          dy = by - ay;
        const x0 = Math.max(0, Math.floor(((ax - r) / width) * nx)),
          x1 = Math.min(nx - 1, Math.ceil(((bx + r) / width) * nx));
        const y0 = Math.max(
            0,
            Math.floor(((Math.min(ay, by) - r) / height) * ny),
          ),
          y1 = Math.min(
            ny - 1,
            Math.ceil(((Math.max(ay, by) + r) / height) * ny),
          );
        for (let y = y0; y <= y1; y++)
          for (let x = x0; x <= x1; x++) {
            const px = ((x + 0.5) / nx) * width,
              py = ((y + 0.5) / ny) * height,
              t = Math.max(
                0,
                Math.min(
                  1,
                  ((px - ax) * dx + (py - ay) * dy) / (dx * dx + dy * dy),
                ),
              );
            if (Math.hypot(px - ax - t * dx, py - ay - t * dy) <= r)
              mask[y * nx + x] = 1;
          }
      }
  return 100 * (1 - mask.reduce((a, b) => a + b, 0) / mask.length);
}

export function revisionFiveWaveDefaults(p) {
  return { ...p, wave_width: 40, wave_height: 8, wave_arch: 0 };
}
