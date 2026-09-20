import type { Parameters } from "../models/types";
import { NumberStepper } from "./NumberStepper";
export function CellDensityControl({
  params: p,
  onChange,
}: {
  params: Parameters;
  onChange: (p: Parameters) => void;
}) {
  const minPitch = 2 * p.thickness + 2;
  const pitch = Math.max(minPitch, p.cell_pitch || 32 - (26 * p.density) / 100);
  const mode = p.density_mode || 0;
  const factor = mode === 2 ? p.cell_aspect : 1;
  const value = mode === 0 ? ((32 - pitch) / 26) * 100 : pitch * factor;
  const min = mode === 0 ? 0 : minPitch * factor,
    max = mode === 0 ? ((32 - minPitch) / 26) * 100 : 32 * factor;
  const label = ["Pattern density", "Target cell width", "Target cell height"][
    mode
  ];
  function change(v: number) {
    if (!Number.isFinite(v)) return;
    const next = mode === 0 ? 32 - (26 * v) / 100 : v / factor;
    onChange({
      ...p,
      cell_pitch:
        Math.round(Math.max(minPitch, Math.min(32, next)) * 100) / 100,
    });
  }
  return (
    <div className="parameter-control">
      <label className="editor-preset">
        Control by
        <select
          aria-label="Cell layout control"
          value={mode}
          onChange={(e) =>
            onChange({ ...p, density_mode: Number(e.target.value) })
          }
        >
          <option value={0}>Pattern density</option>
          <option value={1}>Cell width</option>
          <option value={2}>Cell height</option>
        </select>
      </label>
      <div className="parameter-label">
        <label htmlFor="cell-density-value">{label}</label>
        <span>{mode === 0 ? "Coarse → fine" : "mm"}</span>
      </div>
      <div className="parameter-inputs">
        <input
          className="parameter-slider"
          aria-label={`${label} slider`}
          type="range"
          min={min}
          max={max}
          step="any"
          value={value}
          onChange={(e) => change(Number(e.target.value))}
        />
        <NumberStepper
          id="cell-density-value"
          label={label}
          value={Number(value.toFixed(2))}
          min={min}
          max={max}
          step={0.01}
          onChange={change}
        />
      </div>
      <p className="parameter-help">
        Targets spacing between repeat centres along the curved wall. Whole-cell
        counts are rounded; proportions link width and height. Changing mode
        keeps the pattern unchanged.
      </p>
    </div>
  );
}
