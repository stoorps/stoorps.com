import type { Parameters } from "../models/types";
import type { BuildResult } from "./protocol";

export function LampshadeProfile({
  params: p,
  onChange,
}: {
  params: Parameters;
  onChange: (p: Parameters) => void;
}) {
  const keys = ["top_diameter", "middle_diameter", "bottom_diameter"];
  const xs = keys.map((k) => 40 + p[k] / 2),
    ys = [20, 100, 180];
  const curvePoints = Array.from({ length: 81 }, (_, i) => {
    const u = (i <= 40 ? i : i - 40) / 40;
    const blend =
      u * (1 - p.curve) + ((1 - Math.cos(Math.PI * u)) / 2) * p.curve;
    const a = i <= 40 ? p.top_diameter : p.middle_diameter,
      b = i <= 40 ? p.middle_diameter : p.bottom_diameter;
    return `${40 + (a + (b - a) * blend) / 2} ${20 + i * 2}`;
  });
  const presets = [
    {
      name: "Bell",
      top_diameter: 120,
      middle_diameter: 170,
      bottom_diameter: 220,
    },
    {
      name: "Drum",
      top_diameter: 180,
      middle_diameter: 180,
      bottom_diameter: 180,
    },
    {
      name: "Hourglass",
      top_diameter: 200,
      middle_diameter: 120,
      bottom_diameter: 200,
    },
  ];
  return (
    <section className="shade-profile" aria-label="Shade profile">
      <div className="shade-presets">
        {presets.map(({ name, ...values }) => (
          <button
            type="button"
            key={name}
            onClick={() => onChange({ ...p, ...values })}
          >
            {name}
          </button>
        ))}
      </div>
      <svg
        viewBox="0 0 210 200"
        role="img"
        aria-label="Side profile: drag the three points to change diameters"
      >
        <path
          d="M40 20V180"
          stroke="currentColor"
          strokeDasharray="3 4"
          fill="none"
          opacity=".3"
        />
        <path
          d={`M40 20 L${curvePoints.join(" L")} H40`}
          fill="currentColor"
          fillOpacity=".08"
          stroke="currentColor"
          strokeWidth="2"
        />
        {keys.map((k, i) => (
          <circle
            key={k}
            cx={xs[i]}
            cy={ys[i]}
            r="7"
            fill="currentColor"
            style={{ cursor: "ew-resize", touchAction: "none" }}
            onPointerDown={(e) =>
              e.currentTarget.setPointerCapture(e.pointerId)
            }
            onPointerMove={(e) => {
              if (!e.currentTarget.hasPointerCapture(e.pointerId)) return;
              const svg = e.currentTarget.ownerSVGElement!;
              const pt = svg.createSVGPoint();
              pt.x = e.clientX;
              pt.y = e.clientY;
              const local = pt.matrixTransform(svg.getScreenCTM()!.inverse());
              onChange({
                ...p,
                [k]: Math.max(
                  100,
                  Math.min(290, Math.round((local.x - 40) * 2)),
                ),
              });
            }}
            onPointerUp={(e) =>
              e.currentTarget.releasePointerCapture(e.pointerId)
            }
          />
        ))}
      </svg>
      <p className="parameter-help">
        Drag the points, or use the diameter fields below. Presets change only
        the profile.
      </p>
    </section>
  );
}

export function LampshadeGuidance({
  params: p,
  result,
}: {
  params: Parameters;
  result: BuildResult | null;
}) {
  const notes: string[] = [];
  if (result)
    for (const [i, part] of result.parts.entries()) {
      const b = part.bounds,
        dims = [b[3] - b[0], b[4] - b[1], b[5] - b[2]];
      if (
        dims[0] + 10 > p.build_x ||
        dims[1] + 10 > p.build_y ||
        dims[2] > p.build_z
      )
        notes.push(
          `${i === 0 ? "Shade / collar" : "Adapter"} exceeds the upright build envelope with 5 mm brim space on each side. Change dimensions or check another orientation in your slicer.`,
        );
    }
  if (p.thickness < p.extrusion_width * 3)
    notes.push(
      "Wall / strand thickness is below three extrusion widths. Increase it for a more robust first print.",
    );
  if (p.layer_height > p.nozzle * 0.75)
    notes.push(
      "Layer height is above 75% of nozzle diameter. Check your slicer settings.",
    );
  if (p.extrusion_width < p.nozzle * 0.9 || p.extrusion_width > p.nozzle * 1.5)
    notes.push(
      "Extrusion width differs substantially from nozzle diameter. Check your slicer line width.",
    );
  if ((p.material === 1 || p.material === 3) && p.nozzle < 0.4)
    notes.push(
      "A small nozzle may be unsuitable for fibre-filled filament. Check its manufacturer guidance.",
    );
  return (
    <section className="shade-guidance" aria-label="Print recommendations">
      <strong>Print checks</strong>
      <p>
        Suggested starting strand thickness:{" "}
        {Math.max(1.2, p.extrusion_width * 3).toFixed(2)} mm or more. This is
        guidance, not a printable limit.
      </p>
      <p>
        Fixture hole: {(p.fixture_diameter + p.hole_clearance).toFixed(2)} mm.
        Thread clearance: {p.thread_clearance.toFixed(2)} mm per side.
      </p>
      {notes.map((note) => (
        <p className="parameter-error" key={note}>
          {note}
        </p>
      ))}
      <p>
        Print the fit-test pair first. Inspect overhangs and supports in the
        slicer. Material families do not establish a temperature or load rating.
      </p>
    </section>
  );
}
