import { useEffect, useId, useState } from "react";
import type { ParameterDefinition } from "../models/types";
export function ParameterControl({
  definition: p,
  value,
  onChange,
  disabled,
}: {
  definition: ParameterDefinition;
  value: number;
  onChange: (value: number) => void;
  disabled?: boolean;
}) {
  const id = useId();
  const [draft, setDraft] = useState(String(value));
  useEffect(() => setDraft(Number.isNaN(value) ? "" : String(value)), [value]);
  const precision = Math.max(0, (String(p.step).split(".")[1] || "").length);
  const valid =
    Number.isFinite(value) &&
    value >= p.min &&
    value <= p.max &&
    Math.abs((value - p.min) / p.step - Math.round((value - p.min) / p.step)) <
      1e-7;
  const adjust = (direction: number) =>
    onChange(
      Number(
        Math.min(
          p.max,
          Math.max(
            p.min,
            (Number.isFinite(value) ? value : p.default) + direction * p.step,
          ),
        ).toFixed(precision),
      ),
    );
  if (p.options)
    return (
      <div className="parameter-control">
        <div className="parameter-label">
          <label htmlFor={id}>{p.label}</label>
        </div>
        <select
          id={id}
          value={value}
          disabled={disabled}
          onChange={(e) => onChange(Number(e.target.value))}
        >
          {p.options.map((label, i) => (
            <option key={i} value={p.min + i}>
              {label}
            </option>
          ))}
        </select>
        <p className="parameter-help">{p.help}</p>
      </div>
    );
  return (
    <div className="parameter-control">
      <div className="parameter-label">
        <label htmlFor={id}>{p.label}</label>
        <span>{p.unit}</span>
      </div>
      <div className="parameter-inputs">
        <input
          className="parameter-slider"
          type="range"
          aria-label={`${p.label} slider`}
          min={p.min}
          max={p.max}
          step={p.step}
          value={Number.isFinite(value) ? value : p.default}
          disabled={disabled}
          onChange={(e) => onChange(Number(e.target.value))}
        />
        <div className="number-stepper">
          <button
            type="button"
            aria-label={`Decrease ${p.label}`}
            disabled={disabled || value <= p.min}
            onClick={() => adjust(-1)}
          >
            −
          </button>
          <input
            id={id}
            type="number"
            min={p.min}
            max={p.max}
            step={p.step}
            value={draft}
            disabled={disabled}
            aria-invalid={!valid}
            aria-describedby={`${id}-help`}
            onChange={(e) => {
              setDraft(e.target.value);
              onChange(e.target.valueAsNumber);
            }}
          />
          <button
            type="button"
            aria-label={`Increase ${p.label}`}
            disabled={disabled || value >= p.max}
            onClick={() => adjust(1)}
          >
            +
          </button>
        </div>
      </div>
      <p
        id={`${id}-help`}
        className={!valid ? "parameter-error" : "parameter-help"}
      >
        {!valid
          ? `Use ${p.min}–${p.max}${p.unit ? ` ${p.unit}` : ""}, in steps of ${p.step}.`
          : p.help || `${p.min}–${p.max}${p.unit ? ` ${p.unit}` : ""}`}
      </p>
    </div>
  );
}
