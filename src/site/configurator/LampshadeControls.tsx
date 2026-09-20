import { useState } from "react";
import { NumberStepper } from "./NumberStepper";
import type { Parameters } from "../models/types";

export function LampshadeProfile({
  params: p,
  onChange,
}: {
  params: Parameters;
  onChange: (p: Parameters) => void;
}) {
  const [selected, setSelected] = useState<number | null>(null);
  const [customShape, setCustomShape] = useState("");
  const keys = ["top_diameter", "middle_diameter", "bottom_diameter"];
  const labels = ["Top diameter", "Middle diameter", "Bottom diameter"];
  const presets = [
    { name: "Bell", values: [120, 170, 220] },
    { name: "Drum", values: [180, 180, 180] },
    { name: "Hourglass", values: [200, 120, 200] },
  ];
  const signature = JSON.stringify(keys.map((k) => p[k]));
  const preset =
    customShape === signature
      ? "Custom"
      : presets.find((item) => item.values.every((v, i) => v === p[keys[i]]))
          ?.name || "Custom";
  const cx = 255,
    scale = 0.75,
    top = 30,
    bottom = top + p.height * scale;
  const ys = [top, (top + bottom) / 2, bottom];
  const xs = keys.map((k) => cx + (p[k] * scale) / 2);
  const points = Array.from({ length: 81 }, (_, i) => {
    const u = (i <= 40 ? i : i - 40) / 40;
    const blend =
      u * (1 - p.curve) + ((1 - Math.cos(Math.PI * u)) / 2) * p.curve;
    const a = i <= 40 ? p.top_diameter : p.middle_diameter,
      b = i <= 40 ? p.middle_diameter : p.bottom_diameter;
    return [
      cx + ((a + (b - a) * blend) * scale) / 2,
      top + (p.height * scale * i) / 80,
    ];
  });
  const path = points.map(([x, y], i) => `${i ? "L" : "M"}${x} ${y}`).join(" ");
  function diameter(i: number, value: number) {
    if (!Number.isFinite(value)) return;
    const next = { ...p, [keys[i]]: value };
    setCustomShape(JSON.stringify(keys.map((k) => next[k])));
    onChange(next);
  }
  return (
    <section className="shade-profile" aria-label="Shade profile">
      <label className="editor-preset">
        Profile preset
        <select
          value={preset}
          onChange={(e) => {
            const item = presets.find((item) => item.name === e.target.value);
            if (item) {
              setCustomShape("");
              onChange({
                ...p,
                ...Object.fromEntries(
                  keys.map((key, i) => [key, item.values[i]]),
                ),
              });
            } else setCustomShape(signature);
          }}
        >
          <option>Custom</option>
          {presets.map((item) => (
            <option key={item.name}>{item.name}</option>
          ))}
        </select>
      </label>
      <div className="profile-graph-scroll">
        <svg
          viewBox={`0 0 510 ${Math.max(190, bottom + 45)}`}
          aria-label="Side profile with editable diameters and height"
        >
          <path
            d={`M${cx} ${top}V${bottom}`}
            stroke="#a4ada8"
            strokeDasharray="3 4"
            fill="none"
          />
          <path
            d={`${path} H${cx} V${top} Z`}
            transform={`translate(${2 * cx} 0) scale(-1 1)`}
            fill="#a4ada8"
            fillOpacity=".14"
            stroke="#9aa49e"
            strokeWidth="2"
          />
          <path
            d={`${path} H${cx} V${top} Z`}
            fill="currentColor"
            fillOpacity=".08"
            stroke="currentColor"
            strokeWidth="2"
          />
          <path
            d={`M385 ${top}H397 M391 ${top}V${bottom} M385 ${bottom}H397`}
            stroke="currentColor"
            fill="none"
          />
          <path
            d={`M${xs[0] + 8} ${top}H383 M${xs[2] + 8} ${bottom}H383`}
            stroke="#a4ada8"
            strokeDasharray="3 3"
          />
          <foreignObject
            x="398"
            y={(top + bottom) / 2 - 32}
            width="110"
            height="85"
          >
            <div className="profile-dimension">
              <label>
                Height · mm
                <NumberStepper
                  label="Shade height"
                  value={p.height}
                  min={80}
                  max={280}
                  step={1}
                  onChange={(value) => {
                    if (Number.isFinite(value))
                      onChange({ ...p, height: value });
                  }}
                />
              </label>
            </div>
          </foreignObject>
          {selected !== null && (
            <>
              <path
                d={`M138 ${ys[selected]}H${xs[selected] - 9}`}
                stroke="#8c9690"
                strokeDasharray="3 3"
              />
              <foreignObject
                x="0"
                y={ys[selected] - 25}
                width="136"
                height="80"
              >
                <div className="profile-dimension">
                  <label>
                    {labels[selected]} · mm
                    <NumberStepper
                      label={labels[selected]}
                      value={p[keys[selected]]}
                      min={100}
                      max={290}
                      step={1}
                      onChange={(value) => diameter(selected, value)}
                    />
                  </label>
                </div>
              </foreignObject>
            </>
          )}
          {keys.map((key, i) => (
            <circle
              key={key}
              cx={xs[i]}
              cy={ys[i]}
              r="10"
              fill={selected === i ? "#b7431c" : "currentColor"}
              stroke="#fffdf5"
              strokeWidth="2"
              role="button"
              tabIndex={0}
              aria-label={`Edit ${labels[i].toLowerCase()}`}
              aria-pressed={selected === i}
              onFocus={() => setSelected(i)}
              onKeyDown={(e) => {
                if (e.key === "Enter" || e.key === " ") {
                  e.preventDefault();
                  setSelected(i);
                }
                if (e.key === "ArrowLeft" || e.key === "ArrowRight") {
                  e.preventDefault();
                  diameter(
                    i,
                    Math.max(
                      100,
                      Math.min(290, p[key] + (e.key === "ArrowRight" ? 1 : -1)),
                    ),
                  );
                }
              }}
              onPointerDown={(e) => {
                setSelected(i);
                e.currentTarget.setPointerCapture(e.pointerId);
              }}
              onPointerMove={(e) => {
                if (!e.currentTarget.hasPointerCapture(e.pointerId)) return;
                const svg = e.currentTarget.ownerSVGElement!;
                const pt = svg.createSVGPoint();
                pt.x = e.clientX;
                pt.y = e.clientY;
                const local = pt.matrixTransform(svg.getScreenCTM()!.inverse());
                diameter(
                  i,
                  Math.max(
                    100,
                    Math.min(290, Math.round(((local.x - cx) * 2) / scale)),
                  ),
                );
              }}
              onPointerUp={(e) =>
                e.currentTarget.releasePointerCapture(e.pointerId)
              }
            />
          ))}
        </svg>
      </div>
      <p className="parameter-help">
        Drag a point on the right, or select it to edit its diameter on the
        left. The grey side mirrors the profile.
      </p>
    </section>
  );
}
