const TAU = Math.PI * 2;
export function geometryKey(p) {
  const previewOnly = new Set([
    "density_mode",
    "bulb_length",
    "bulb_offset",
    "bulb_gap",
    "bulb_overlay",
  ]);
  return JSON.stringify(
    Object.fromEntries(
      Object.entries(p).filter(([key]) => !previewOnly.has(key)),
    ),
  );
}
export function profileRadius(p, t) {
  const u = t < 0.5 ? t * 2 : t * 2 - 1;
  const b = u * (1 - p.curve) + ((1 - Math.cos(Math.PI * u)) / 2) * p.curve;
  return (
    ((t < 0.5 ? p.bottom_diameter : p.middle_diameter) * (1 - b) +
      (t < 0.5 ? p.middle_diameter : p.top_diameter) * b) /
    2
  );
}
let cacheKey = "",
  cache;
export function surfacePlan(p) {
  const key = JSON.stringify([
    p.height,
    p.bottom_diameter,
    p.middle_diameter,
    p.top_diameter,
    p.curve,
    p.lobes,
    p.ripple_depth,
    p.twist,
    p.density,
    p.cell_pitch,
    p.cell_aspect,
    p.thickness,
  ]);
  if (key === cacheKey) return cache;
  const nz = 128,
    na = 1024,
    arcs = [],
    lengths = [];
  for (let j = 0; j <= nz; j++) {
    const r = profileRadius(p, j / nz),
      arc = [0];
    for (let i = 0; i < na; i++) {
      const a = (TAU * (i + 0.5)) / na,
        phase = p.lobes * a;
      arc.push(
        arc[i] +
          (Math.hypot(
            r + p.ripple_depth * Math.sin(phase),
            p.ripple_depth * p.lobes * Math.cos(phase),
          ) *
            TAU) /
            na,
      );
    }
    lengths.push(arc[na]);
    arcs.push(arc.map((v) => v / arc[na]));
  }
  function angle(u, z) {
    const t = Math.max(0, Math.min(nz, (z / p.height) * nz)),
      j = Math.min(nz - 1, Math.floor(t)),
      f = t - j;
    u = ((u % 1) + 1) % 1;
    const at = (i) => arcs[j][i] * (1 - f) + arcs[j + 1][i] * f;
    let lo = 0,
      hi = na;
    while (hi - lo > 1) {
      const m = (hi + lo) >> 1;
      if (at(m) < u) lo = m;
      else hi = m;
    }
    return (TAU * (lo + (u - at(lo)) / (at(hi) - at(lo)))) / na;
  }
  function point(u, z) {
    const a = angle(u, z) + (((p.twist * Math.PI) / 180) * z) / p.height,
      r =
        profileRadius(p, z / p.height) +
        p.ripple_depth *
          Math.sin(
            p.lobes * (a - (((p.twist * Math.PI) / 180) * z) / p.height),
          );
    return [r * Math.cos(a), r * Math.sin(a), z];
  }
  const vertical = [0];
  for (let j = 1; j <= nz; j++) {
    let d = 0;
    for (let i = 0; i < 64; i++) {
      const a = point(i / 64, (p.height * (j - 1)) / nz),
        b = point(i / 64, (p.height * j) / nz);
      d += Math.hypot(...a.map((v, k) => v - b[k]));
    }
    vertical.push(vertical[j - 1] + d / 64);
  }
  const circumference =
    (lengths.reduce((a, b) => a + b, 0) - (lengths[0] + lengths[nz]) / 2) / nz;
  const height = vertical[nz],
    pitch = Math.max(
      p.cell_pitch || 32 - (26 * p.density) / 100,
      2 * p.thickness + 2,
    );
  const cols = Math.max(8, Math.round(circumference / pitch)),
    rows = Math.max(3, Math.round(height / (pitch * p.cell_aspect)));
  function z(v) {
    const target = Math.max(0, Math.min(1, v)) * height;
    let lo = 0,
      hi = nz;
    while (hi - lo > 1) {
      const m = (hi + lo) >> 1;
      if (vertical[m] < target) lo = m;
      else hi = m;
    }
    return (
      (p.height *
        (lo + (target - vertical[lo]) / (vertical[hi] - vertical[lo]))) /
      nz
    );
  }
  cacheKey = key;
  return (cache = {
    circumference,
    maxCircumference: Math.max(...lengths),
    minCircumference: Math.min(...lengths),
    height,
    pitch,
    cols,
    rows,
    width: circumference / cols,
    cellHeight: height / rows,
    angle,
    z,
  });
}
export function bulbClearance(p) {
  const mount = p.height - p.mount_depth - p.plate_thickness;
  const top = mount - p.bulb_offset,
    bottom = top - p.bulb_length;
  const lo = Math.max(0, bottom),
    hi = Math.min(p.height, top);
  if (
    p.fit_test ||
    lo >= hi ||
    Object.values(p).some((v) => !Number.isFinite(v))
  )
    return { diameter: 0, bottom, top, available: false };
  let r = Infinity;
  for (let i = 0; i <= 256; i++) {
    const z = lo + ((hi - lo) * i) / 256;
    r = Math.min(
      r,
      profileRadius(p, z / p.height) -
        (p.lobes ? p.ripple_depth : 0) -
        p.thickness / 2,
    );
  }
  for (const [a, b] of [
    [0, 3],
    [p.height - 3, p.height],
    [mount - 6, mount - 3],
  ])
    if (hi >= a && lo <= b) {
      for (let i = 0; i <= 32; i++) {
        const z =
          Math.max(lo, a) + ((Math.min(hi, b) - Math.max(lo, a)) * i) / 32;
        r = Math.min(
          r,
          profileRadius(p, z / p.height) -
            (p.lobes ? p.ripple_depth : 0) -
            p.thickness -
            1.3,
        );
      }
    }
  if (hi >= mount - 8 && lo <= mount) r = Math.min(r, p.clamp_diameter / 2);
  if (hi >= mount && lo <= mount + p.plate_thickness)
    r = Math.min(r, (p.fixture_diameter + p.hole_clearance) / 2);
  const diameter = Math.max(0, 2 * (r - p.bulb_gap));
  return {
    diameter,
    bottom,
    top,
    available: true,
    protrudes: bottom < 0 || top > p.height,
  };
}
