import { NumberStepper } from "./NumberStepper";
import { useId } from "react";
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
  const valid =
    Number.isFinite(value) &&
    value >= p.min &&
    value <= p.max &&
    Math.abs((value - p.min) / p.step - Math.round((value - p.min) / p.step)) <
      1e-7;
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
        <NumberStepper
          id={id}
          label={p.label}
          value={value}
          min={p.min}
          max={p.max}
          step={p.step}
          onChange={onChange}
          disabled={disabled}
          describedBy={`${id}-help`}
        />
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
