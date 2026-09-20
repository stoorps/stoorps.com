import { zipSync, strToU8 } from "fflate";
import { PDFDocument, StandardFonts, rgb, PrintScaling } from "pdf-lib";
const mm = 72 / 25.4;
const fmt = (n) => Number(n.toFixed(3));
const esc = (s) =>
  String(s).replace(
    /[&<>"']/g,
    (c) =>
      ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" })[
        c
      ],
  );
const ascii = (s) =>
  String(s)
    .replaceAll("Ø", "Dia ")
    .replaceAll("×", "x")
    .replaceAll("°", " deg")
    .replace(/[^\x20-\x7e\n]/g, "-");
function arcInfo(a, b) {
  const [mx, my] = b.mid,
    ax = a.x,
    ay = a.y,
    bx = b.x,
    by = b.y;
  const det = 2 * (ax * (my - by) + mx * (by - ay) + bx * (ay - my));
  const x =
    ((ax * ax + ay * ay) * (my - by) +
      (mx * mx + my * my) * (by - ay) +
      (bx * bx + by * by) * (ay - my)) /
    det;
  const y =
    ((ax * ax + ay * ay) * (bx - mx) +
      (mx * mx + my * my) * (ax - bx) +
      (bx * bx + by * by) * (mx - ax)) /
    det;
  const r = Math.hypot(ax - x, ay - y),
    start = Math.atan2(ay - y, ax - x),
    end = Math.atan2(by - y, bx - x);
  let sweep = (end - start + 2 * Math.PI) % (2 * Math.PI);
  const middle =
    (Math.atan2(my - y, mx - x) - start + 2 * Math.PI) % (2 * Math.PI);
  if (middle > sweep + 1e-8) sweep -= 2 * Math.PI;
  return { x, y, r, start, sweep };
}
export function profilePath(profile) {
  let path = `M ${fmt(profile[0].x)} ${fmt(profile[0].y)}`;
  for (let i = 1; i <= profile.length; i++) {
    const a = profile[i - 1],
      b = profile[i % profile.length];
    if (b.mid) {
      const arc = arcInfo(a, b);
      path += ` A ${fmt(arc.r)} ${fmt(arc.r)} 0 ${Math.abs(arc.sweep) > Math.PI ? 1 : 0} ${arc.sweep > 0 ? 1 : 0} ${fmt(b.x)} ${fmt(b.y)}`;
    } else path += ` L ${fmt(b.x)} ${fmt(b.y)}`;
  }
  return path + " Z";
}
function box(profile) {
  const xs = profile.map((n) => n.x),
    ys = profile.map((n) => n.y);
  return {
    x: Math.min(...xs),
    y: Math.min(...ys),
    w: Math.max(...xs) - Math.min(...xs),
    h: Math.max(...ys) - Math.min(...ys),
  };
}
function csv(rows) {
  return (
    rows
      .map((r) =>
        r.map((v) => '"' + String(v).replaceAll('"', '""') + '"').join(","),
      )
      .join("\r\n") + "\r\n"
  );
}
export function definitionGroups(parts) {
  const groups = new Map();
  for (const part of parts) {
    const key = JSON.stringify([
      part.material,
      part.profile,
      part.stock,
      part.thickness,
      part.operations.map((o) => [
        o.kind,
        o.point,
        o.direction,
        o.diameter,
        o.depth,
      ]),
      part.notes,
    ]);
    const group = groups.get(key);
    if (group) group.ids.push(part.id);
    else groups.set(key, { part, ids: [part.id] });
  }
  return [...groups.values()];
}
// Unfold each stock face using the same tilted extrusion as the CAD solid.
export function footCutFaces(part) {
  if (part.group !== "feet") return [];
  const length = Math.hypot(...part.extrusion);
  const axis = part.extrusion.map((x) => x / length);
  const points = part.profile.map((p) =>
    part.frame.o.map(
      (x, i) => x + part.frame.u[i] * p.x + part.frame.v[i] * p.y,
    ),
  );
  const dot = (a, b) => a.reduce((s, x, i) => s + x * b[i], 0);
  const origin = points[0];
  const heights = points.map((p) =>
    dot(
      p.map((x, i) => x - origin[i]),
      axis,
    ),
  );
  const datum = Math.min(...heights);
  return points.map((p, i) => {
    const j = (i + 1) % points.length;
    const delta = points[j].map((x, k) => x - p[k]);
    const axial = dot(delta, axis);
    const width = Math.sqrt(Math.max(0, dot(delta, delta) - axial * axial));
    const a = heights[i] - datum,
      b = heights[j] - datum;
    return {
      ...part,
      id: `${part.id}-face-${i + 1}`,
      name: `${part.name} / stock face ${i + 1}`,
      group: "foot-cut",
      profile: [
        { x: 0, y: a },
        { x: width, y: b },
        { x: width, y: b + length },
        { x: 0, y: a + length },
      ],
      operations: [],
      notes: [
        "Unfolded stock face; match adjacent numbered edges. Parallel floor and mounting cuts.",
      ],
    };
  });
}
function operationLabels(operations) {
  const groups = new Map();
  operations.forEach((o, i) => {
    const key = o.point
      .slice(0, 2)
      .map((x) => x.toFixed(3))
      .join(",");
    if (!groups.has(key)) groups.set(key, []);
    groups.get(key).push(i + 1);
  });
  return operations.map((o) =>
    groups
      .get(
        o.point
          .slice(0, 2)
          .map((x) => x.toFixed(3))
          .join(","),
      )
      .join("/"),
  );
}
function operationSvg(op, index, label = String(index + 1)) {
  const [x, y] = op.point;
  const color = op.kind === "hinge-pocket" ? "#a35416" : "#326d78";
  let shape = "";
  if (op.kind === "hinge-pocket") {
    const [w, h, r] = op.direction;
    shape = `<rect x="${x - w / 2}" y="${y - h / 2}" width="${w}" height="${h}" rx="${r}" fill="none" stroke="${color}"/>`;
  } else if (Math.abs(op.direction[2]) > 0.999)
    shape = `<circle cx="${x}" cy="${y}" r="${op.diameter / 2}" fill="none" stroke="${color}"/>`;
  return `${shape}<path d="M${x - 2},${y}h4 M${x},${y - 2}v4" stroke="${color}"/><text x="${x + 3}" y="${y - 3}" font-size="3" fill="${color}">${label}</text>`;
}
export function templateSvg(part, ids = [part.id]) {
  const b = box(part.profile),
    w = Math.max(220, b.w + 30),
    h = b.h + 75;
  return `<svg xmlns="http://www.w3.org/2000/svg" width="${w}mm" height="${h}mm" viewBox="0 0 ${w} ${h}">
 <rect width="100%" height="100%" fill="white"/>
 <g font-family="sans-serif" fill="#173d35"><text x="10" y="10" font-size="5">${esc(part.name)}</text>
 <text x="10" y="17" font-size="3.5">${esc(part.id)} | Qty ${ids.length} | ${esc(part.material)} | ${part.stock.map(fmt).join(" x ")} mm</text>
 <text x="10" y="24" font-size="3">${part.group === "feet" ? "FOOT FLOOR OUTLINE; top fixing marks use the same XY datum." : "FINISHED OUTLINE; no cutter/bush/kerf compensation."}</text>
 <g transform="translate(${15 - b.x},${35 - b.y})" stroke-width="0.25"><path d="${profilePath(part.profile)}" stroke="#173d35" fill="none"/>${part.operations.map((o, i) => operationSvg(o, i, operationLabels(part.operations)[i])).join("")}</g>
 <text x="10" y="${h - 25}" font-size="3">Operation numbers refer to operations.csv. Check entry face / Z and drilling direction.</text>
 <text x="10" y="${h - 19}" font-size="3">Print at 100%. Check the 100 mm line below. Hardware settings are provisional.</text>
 <path d="M10 ${h - 10}h100 M10 ${h - 12}v4 M110 ${h - 12}v4" stroke="#173d35" stroke-width="0.3"/>
 <text x="115" y="${h - 9}" font-size="3">100 mm</text></g></svg>`;
}
export function profileDxf(part) {
  const rows = [
    "0",
    "SECTION",
    "2",
    "HEADER",
    "9",
    "$INSUNITS",
    "70",
    "4",
    "0",
    "ENDSEC",
    "0",
    "SECTION",
    "2",
    "ENTITIES",
  ];
  const push = (...v) => rows.push(...v.map(String));
  for (let i = 0; i < part.profile.length; i++) {
    const a = part.profile[i],
      b = part.profile[(i + 1) % part.profile.length];
    if (b.mid) {
      const q = arcInfo(a, b);
      let from = q.start,
        to = q.start + q.sweep;
      if (q.sweep < 0) [from, to] = [to, from];
      push(
        0,
        "ARC",
        8,
        "FINISHED_OUTLINE",
        10,
        q.x,
        20,
        q.y,
        30,
        0,
        40,
        q.r,
        50,
        ((from * 180) / Math.PI + 360) % 360,
        51,
        ((to * 180) / Math.PI + 360) % 360,
      );
    } else
      push(
        0,
        "LINE",
        8,
        "FINISHED_OUTLINE",
        10,
        a.x,
        20,
        a.y,
        30,
        0,
        11,
        b.x,
        21,
        b.y,
        31,
        0,
      );
  }
  push(0, "ENDSEC", 0, "EOF");
  return rows.join("\n") + "\n";
}
function sampled(profile) {
  const points = [];
  for (let i = 0; i < profile.length; i++) {
    const a = profile[i],
      b = profile[(i + 1) % profile.length];
    points.push([a.x, a.y]);
    if (b.mid) {
      const q = arcInfo(a, b);
      const steps = Math.ceil(Math.abs(q.sweep) * 30);
      for (let k = 1; k < steps; k++) {
        const a = q.start + (q.sweep * k) / steps;
        points.push([q.x + q.r * Math.cos(a), q.y + q.r * Math.sin(a)]);
      }
    }
  }
  return points;
}
function diagram(parts) {
  const project = (q) => [(q[0] - q[1]) * 0.75, (q[0] + q[1]) * 0.25 - q[2]];
  const lines = [];
  for (const part of parts) {
    const p = sampled(part.profile);
    const point = (q, z) =>
      project(
        part.frame.o.map(
          (v, i) =>
            v +
            part.frame.u[i] * q[0] +
            part.frame.v[i] * q[1] +
            ((part.extrusion?.[i] ?? part.frame.n[i] * part.thickness) * z) /
              part.thickness +
            part.explode[i],
        ),
      );
    for (let i = 0; i < p.length; i++) {
      const a = p[i],
        b = p[(i + 1) % p.length];
      lines.push(
        [point(a, 0), point(b, 0)],
        [point(a, part.thickness), point(b, part.thickness)],
      );
      if (i % 8 === 0 || p.length === 4)
        lines.push([point(a, 0), point(a, part.thickness)]);
    }
  }
  const flat = lines.flat(),
    xs = flat.map((p) => p[0]),
    ys = flat.map((p) => p[1]);
  const min = [Math.min(...xs), Math.min(...ys)],
    max = [Math.max(...xs), Math.max(...ys)];
  const scale = Math.min(730 / (max[0] - min[0]), 440 / (max[1] - min[1]));
  const segments = lines.map((l) =>
    l.map((p) => [35 + (p[0] - min[0]) * scale, 35 + (p[1] - min[1]) * scale]),
  );
  return {
    segments,
    svg: `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 800 520"><rect width="800" height="520" fill="white"/><path d="${segments.map(([a, b]) => `M${a.map(fmt).join(",")}L${b.map(fmt).join(",")}`).join(" ")}" stroke="#173d35" fill="none" stroke-width="0.45"/><text x="35" y="510" font-family="sans-serif" font-size="13">Exploded assembly - piece identities are listed in the cut list.</text></svg>`,
  };
}
const steps = [
  "Verify measured plywood and batten sizes, hinge drawing, pocket jig, screw lengths and dowel fit. This pack is a configurable prototype, not a hardware-certified drawing.",
  "Cut and label plywood layers. Glue the base, top and divider laminations, and the four foot bundles. Keep registration edges aligned and clamp flat. Machine assembled-stack pockets to their specified depth, not separately to full depth in every layer.",
  "Drill dowel holes in the door ribs and batten ends using the matching numbered templates. Use a stop and dry-fit before gluing. Assemble each curved door flat in a locating jig; glue dowels and end joints and check diagonals while clamping.",
  "Cut the foot bundles to the two configured splay angles, with parallel horizontal top and floor cuts. Confirm the long-point blank dimensions and dry-fit against the base. The foot plan is a mounting layout, not a saw-angle setting.",
  "With underside access, screw the fixed side panels and lower divider onto the base. Keep the lower divider in direct bearing with the shelf. Fix the rear panel upwards through the base using the same through-screw method as the sides.",
  "Fit the shelf and attach it to the rear using the selected pocket or rear through-screw arrangement. Pocket-screw the lower divider to the shelf. Verify jig and driver access before closing the assembly.",
  "Pocket-screw the upper divider to the shelf. Fit the top using the upper divider, sides and rear pockets; access these from inside while the doors are absent. Check pocket screw length against the receiving board thickness.",
  "Provisionally screw the feet down from the top of the base while access remains available. Depending on driver access, do this before fitting the shelf. Final removable foot attachment is deferred.",
  "Machine verified hinge recesses and mounting holes, align top and bottom axes, and fit the doors. Product-specific hinge screw holes are intentionally not guessed. Dry-cycle the doors and confirm the computed opening clearance physically.",
  "Add rack fitment, PC mounting, fans, ventilation and cable entry in the second pass. Do not cut the rear panel or place rack fixings from this first-pass pack.",
];
export async function fabricationFiles(
  assembly,
  params,
  revision = 1,
  selection = assembly.parts.map((_, i) => i),
) {
  if (
    !selection.length ||
    new Set(selection).size !== selection.length ||
    selection.some((i) => !Number.isInteger(i) || !assembly.parts[i])
  )
    throw new Error("Select valid pieces.");
  if (assembly.motion && !assembly.motion.valid)
    throw new Error(
      "Resolve the closed-door clearance conflict before exporting fabrication guides.",
    );
  const parts = selection.map((i) => assembly.parts[i]),
    groups = definitionGroups(parts),
    files = {};
  const put = (name, text) => (files[name] = strToU8(text));
  put(
    "configuration.json",
    JSON.stringify(
      {
        model: "mini-rack-sideboard",
        revision,
        units: "mm",
        parameters: params,
        selectedParts: parts.map((p) => p.id),
        metrics: assembly.metrics,
        motion: assembly.motion,
      },
      null,
      2,
    ),
  );
  put("assembly.json", JSON.stringify(assembly, null, 2));
  put(
    "cut-list.csv",
    csv([
      [
        "Template / part ID",
        "Other identical pieces",
        "Quantity",
        "Material",
        "Blank X mm",
        "Blank Y mm",
        "Blank Z mm",
        "Notes",
      ],
      ...groups.map(({ part: p, ids }) => [
        p.id,
        ids.join("; "),
        ids.length,
        p.material,
        ...p.stock.map(fmt),
        p.notes.join(" "),
      ]),
    ]),
  );
  put(
    "hardware-full-assembly.csv",
    csv([
      ["Item", "Quantity (whole assembly)", "Specification"],
      ...assembly.hardware.map((h) => [h.name, h.quantity, h.specification]),
    ]),
  );
  put(
    "operations.csv",
    csv([
      [
        "Part",
        "Operation number",
        "Joint",
        "Type",
        "Local X mm",
        "Local Y mm",
        "Local Z mm",
        "Direction X",
        "Direction Y",
        "Direction Z",
        "Diameter mm",
        "Depth mm",
        "Notes",
      ],
      ...parts.flatMap((p) =>
        p.operations.map((o, i) => [
          p.id,
          i + 1,
          o.id,
          o.kind,
          ...o.point.map(fmt),
          ...o.direction.map(fmt),
          o.diameter,
          o.depth,
          o.note,
        ]),
      ),
    ]),
  );
  for (const { part, ids } of groups) {
    put(`templates/${part.id}.svg`, templateSvg(part, ids));
    put(`profiles/${part.id}.dxf`, profileDxf(part));
  }
  const footGuides = parts
    .flatMap(footCutFaces)
    .map((part) => ({ part, ids: [part.id] }));
  for (const { part, ids } of footGuides)
    put(`foot-cut-guides/${part.id}.svg`, templateSvg(part, ids));
  const drawing = diagram(assembly.parts);
  put("assembly-exploded.svg", drawing.svg);
  put(
    "assembly-guide.html",
    `<!doctype html><html lang="en"><meta charset="utf-8"><title>Mini Rack Sideboard - assembly guide</title><style>body{font:16px/1.6 system-ui;max-width:960px;margin:40px auto;padding:24px;color:#173d35}li{margin-bottom:16px}table{border-collapse:collapse;width:100%}td,th{text-align:left;padding:8px;border-bottom:1px solid #ccc}svg{width:100%}</style><h1>Mini Rack Sideboard</h1><p>Revision ${revision} · ${assembly.parts.length} wooden pieces · all dimensions in mm.</p><p><strong>Provisional hardware machining.</strong> Confirm hardware and physical fit before cutting.</p>${drawing.svg}<h2>Assembly sequence</h2><ol>${steps.map((s) => `<li>${esc(s)}</li>`).join("")}</ol><h2>Hardware for the full assembly</h2><table>${assembly.hardware.map((h) => `<tr><td>${esc(h.name)}</td><td>${h.quantity}</td><td>${esc(h.specification)}</td></tr>`).join("")}</table><h2>How to use templates</h2><p>Print templates.pdf at actual size (100%) or use a PDF reader's poster/tiling mode. Never fit to page. Check the 100 mm calibration line. SVGs declare physical millimetres. DXFs contain finished outlines only, in millimetres. No kerf or router-bush compensation is applied.</p><p>Operation coordinates are local to the part frame in assembly.json. Z=0 is the profile face; Z=thickness is the opposite face. The direction columns in operations.csv describe drilling vectors; hinge-pocket direction columns instead contain width, length and corner radius. Pocket and edge operations are locating marks, not cutter contours. Foot templates show floor outlines; top mounting positions are offset by the extrusion vector in assembly.json. Separate numbered foot-cut-guides unfold all four stock faces at full size, aligned to a common long-point datum. These are cutting lines, not saw dial settings. Hardware counts cover the full cabinet even when only some pieces are selected.</p><h2>Configuration</h2><table>${Object.entries(
      params,
    )
      .map(([k, v]) => `<tr><td>${esc(k)}</td><td>${v}</td></tr>`)
      .join("")}</table></html>`,
  );
  const pdf = await PDFDocument.create();
  pdf.setTitle(`Mini Rack Sideboard V${revision} - full-size templates`);
  pdf.catalog.getOrCreateViewerPreferences().setPrintScaling(PrintScaling.None);
  const font = await pdf.embedFont(StandardFonts.Helvetica);
  const color = rgb(0.09, 0.24, 0.21);
  for (const { part, ids } of [...groups, ...footGuides]) {
    const b = box(part.profile),
      w = Math.max(220, b.w + 30),
      h = b.h + 75;
    const page = pdf.addPage([w * mm, h * mm]);
    const text = (s, x, y, size = 9) =>
      page.drawText(ascii(s), {
        x: x * mm,
        y: (h - y) * mm,
        size,
        font,
        color,
      });
    text(part.name, 10, 10, 14);
    text(
      `${part.id} | Qty ${ids.length} | ${part.material} | ${part.stock.map(fmt).join(" x ")} mm`,
      10,
      17,
    );
    text(
      part.group === "feet"
        ? "FOOT FLOOR OUTLINE - top fixing marks use the same XY datum."
        : "FINISHED OUTLINE - no kerf or tool compensation.",
      10,
      24,
      8,
    );
    const ox = 15 - b.x,
      oy = 35 - b.y;
    page.drawSvgPath(profilePath(part.profile), {
      x: ox * mm,
      y: (h - oy) * mm,
      scale: mm,
      borderWidth: 0.25,
      borderColor: color,
    });
    for (const [index, o] of part.operations.entries()) {
      const x = (o.point[0] + ox) * mm,
        y = (h - oy - o.point[1]) * mm;
      if (o.kind === "hinge-pocket") {
        const [a, b, r] = o.direction;
        const x0 = -a / 2,
          y0 = -b / 2;
        const path =
          r === 0
            ? `M ${x0} ${y0} h ${a} v ${b} h ${-a} Z`
            : `M ${x0 + r} ${y0} H ${x0 + a - r} A ${r} ${r} 0 0 1 ${x0 + a} ${y0 + r} V ${y0 + b - r} A ${r} ${r} 0 0 1 ${x0 + a - r} ${y0 + b} H ${x0 + r} A ${r} ${r} 0 0 1 ${x0} ${y0 + b - r} V ${y0 + r} A ${r} ${r} 0 0 1 ${x0 + r} ${y0} Z`;
        page.drawSvgPath(path, {
          x,
          y,
          scale: mm,
          borderWidth: 0.25,
          borderColor: rgb(0.65, 0.32, 0.08),
        });
        text(`R${r}`, o.point[0] + ox, o.point[1] + oy + 3, 5);
      } else if (Math.abs(o.direction[2]) > 0.999)
        page.drawCircle({
          x,
          y,
          size: (o.diameter * mm) / 2,
          borderWidth: 0.4,
          borderColor: rgb(0.15, 0.42, 0.47),
        });
      page.drawLine({
        start: { x: x - 2 * mm, y },
        end: { x: x + 2 * mm, y },
        thickness: 0.3,
        color,
      });
      page.drawLine({
        start: { x, y: y - 2 * mm },
        end: { x, y: y + 2 * mm },
        thickness: 0.3,
        color,
      });
      page.drawText(operationLabels(part.operations)[index], {
        x: x + 3 * mm,
        y: y + 3 * mm,
        size: 7,
        font,
        color,
      });
    }
    text(
      "Operation numbers: operations.csv. Verify entry face / Z and drilling direction.",
      10,
      h - 25,
      8,
    );
    text(
      "Print 100%. Hardware details provisional. Verify this line measures 100 mm:",
      10,
      h - 19,
      8,
    );
    page.drawLine({
      start: { x: 10 * mm, y: 10 * mm },
      end: { x: 110 * mm, y: 10 * mm },
      thickness: 0.5,
      color,
    });
    for (const x of [10, 110])
      page.drawLine({
        start: { x: x * mm, y: 8 * mm },
        end: { x: x * mm, y: 12 * mm },
        thickness: 0.5,
        color,
      });
    text("100 mm", 115, h - 9, 8);
  }
  files["templates.pdf"] = await pdf.save();
  put(
    "README.txt",
    `Mini Rack Sideboard V${revision}\nUnits: millimetres. ${parts.length} selected pieces; ${groups.length} unique templates.\nRead assembly-guide.html first. Hardware schedule is for the FULL assembly.\nFull-size PDF: print Actual size / 100%, or tile using Poster mode; check calibration.\nDXFs are finished outlines; SVGs include operation locating marks.\nPockets and hinge details are provisional until hardware is selected. No rack module yet.\n${assembly.warnings.join("\n")}\n`,
  );
  return files;
}
export async function fabricationPack(...args) {
  return zipSync(await fabricationFiles(...args));
}
