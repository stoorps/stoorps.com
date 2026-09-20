// Continuous conservative advancement: distance / maximum point speed bounds
// the next collision-free angular interval. All polygons are convex.
const radians = Math.PI / 180;
function rotated(poly, pivot, angle) {
  const c = Math.cos(angle),
    s = Math.sin(angle);
  return poly.map(([x, y]) => [
    pivot[0] + c * (x - pivot[0]) - s * (y - pivot[1]),
    pivot[1] + s * (x - pivot[0]) + c * (y - pivot[1]),
  ]);
}
function bounds(poly) {
  return [
    Math.min(...poly.map((p) => p[0])),
    Math.min(...poly.map((p) => p[1])),
    Math.max(...poly.map((p) => p[0])),
    Math.max(...poly.map((p) => p[1])),
  ];
}
function boxDistance(a, b) {
  return Math.hypot(
    Math.max(0, a[0] - b[2], b[0] - a[2]),
    Math.max(0, a[1] - b[3], b[1] - a[3]),
  );
}
function pointSegment(p, a, b) {
  const dx = b[0] - a[0],
    dy = b[1] - a[1],
    d = dx * dx + dy * dy;
  const t = d
    ? Math.max(0, Math.min(1, ((p[0] - a[0]) * dx + (p[1] - a[1]) * dy) / d))
    : 0;
  return Math.hypot(p[0] - a[0] - dx * t, p[1] - a[1] - dy * t);
}
export function polygonDistance(a, b) {
  let separated = false;
  for (const poly of [a, b])
    for (let i = 0; i < poly.length; i++) {
      const p = poly[i],
        q = poly[(i + 1) % poly.length],
        nx = q[1] - p[1],
        ny = p[0] - q[0];
      const aa = a.map((v) => v[0] * nx + v[1] * ny),
        bb = b.map((v) => v[0] * nx + v[1] * ny);
      if (
        Math.max(...aa) < Math.min(...bb) - 1e-8 ||
        Math.max(...bb) < Math.min(...aa) - 1e-8
      ) {
        separated = true;
        break;
      }
    }
  if (!separated) return 0;
  let d = Infinity;
  for (const [p, q] of [
    [a, b],
    [b, a],
  ])
    for (const v of p)
      for (let i = 0; i < q.length; i++)
        d = Math.min(d, pointSegment(v, q[i], q[(i + 1) % q.length]));
  return d;
}
export function solveMotion(assembly) {
  const m = assembly.metrics,
    clearance = m.swingClearance + (m.collisionChordError || 0);
  const max = m.hardwareLimit * radians;
  const fixed = assembly.colliders
    .filter((c) => c.group === "carcass")
    .map((c) => ({ ...c, box: bounds(c.polygon) }));
  const doors = assembly.colliders
    .filter((c) => c.group.endsWith("-door"))
    .map((c) => {
      const hand = c.group === "left-door" ? 1 : -1,
        pivot = hand === 1 ? m.pivotLeft : m.pivotRight;
      return {
        ...c,
        hand,
        pivot,
        radius: Math.max(
          ...c.polygon.map((p) => Math.hypot(p[0] - pivot[0], p[1] - pivot[1])),
        ),
      };
    });
  // Only compare pieces with overlapping heights; top/base working gaps are
  // validated in CAD. One Open control moves both doors along this exact path.
  const pairs = [];
  const overlap = (a, b) => a.z[0] < b.z[1] - 1e-6 && b.z[0] < a.z[1] - 1e-6;
  for (let i = 0; i < doors.length; i++) {
    for (const f of fixed) if (overlap(doors[i], f)) pairs.push([i, f, -1]);
    for (let j = i + 1; j < doors.length; j++)
      if (doors[i].group !== doors[j].group && overlap(doors[i], doors[j]))
        pairs.push([i, null, j]);
  }
  let angle = 0,
    lastLimit = "Hardware opening limit";
  for (let iteration = 0; iteration < 3000; iteration++) {
    const transformed = doors.map((c) => {
      const polygon = rotated(c.polygon, c.pivot, -c.hand * angle);
      return { polygon, box: bounds(polygon) };
    });
    let advance = max - angle,
      limiting = "Hardware opening limit";
    for (const [i, f, j] of pairs) {
      const a = transformed[i],
        b = j < 0 ? f : transformed[j],
        speed = doors[i].radius + (j < 0 ? 0 : doors[j].radius);
      const lower = boxDistance(a.box, b.box);
      if (lower > clearance + speed * advance) continue;
      const distance = polygonDistance(a.polygon, b.polygon);
      const next = (distance - clearance) / Math.max(speed, 1);
      if (next < advance) {
        advance = next;
        limiting = j < 0 ? f.name : "Opposite door";
      }
    }
    lastLimit = limiting;
    if (advance < 0.00001) {
      return {
        angle: Math.max(0, Math.floor((angle / radians) * 10) / 10),
        limit: limiting,
        valid: angle > 0 || advance >= -1e-7,
      };
    }
    if (angle + advance >= max - 1e-10)
      return {
        angle: m.hardwareLimit,
        limit: "Hardware opening limit",
        valid: true,
      };
    angle += advance * 0.85;
  }
  return {
    angle: Math.floor((angle / radians) * 10) / 10,
    limit: `Conservative motion bound: ${lastLimit}`,
    valid: true,
  };
}
export function validateBattenSpacing(assembly) {
  const battens = assembly.colliders.filter(
    (c) => c.name.includes("batten") && c.group === "left-door",
  );
  for (let i = 1; i < battens.length; i++)
    if (polygonDistance(battens[i - 1].polygon, battens[i].polygon) < 0.1)
      throw new Error(
        "Adjacent battens touch around the radius. Increase the gap or front radius.",
      );
}
