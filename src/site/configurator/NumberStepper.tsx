import { useEffect, useState } from "react";

export function NumberStepper({
  id,
  label,
  value,
  min,
  max,
  step,
  onChange,
  disabled,
  describedBy,
}: {
  id?: string;
  label: string;
  value: number;
  min: number;
  max: number;
  step: number;
  onChange: (value: number) => void;
  disabled?: boolean;
  describedBy?: string;
}) {
  const [draft, setDraft] = useState(String(value));
  useEffect(
    () => setDraft(Number.isFinite(value) ? String(value) : ""),
    [value],
  );
  const precision = (String(step).split(".")[1] || "").length;
  const valid =
    Number.isFinite(value) &&
    value >= min &&
    value <= max &&
    Math.abs((value - min) / step - Math.round((value - min) / step)) < 1e-7;
  const adjust = (direction: number) =>
    onChange(
      Number(
        Math.min(
          max,
          Math.max(
            min,
            (Number.isFinite(value) ? value : min) + direction * step,
          ),
        ).toFixed(precision),
      ),
    );
  return (
    <div className="number-stepper">
      <button
        type="button"
        aria-label={`Decrease ${label}`}
        disabled={disabled || value <= min}
        onClick={() => adjust(-1)}
      >
        −
      </button>
      <input
        id={id}
        aria-label={label}
        type="number"
        min={min}
        max={max}
        step={step}
        value={draft}
        disabled={disabled}
        aria-invalid={!valid}
        aria-describedby={describedBy}
        onChange={(e) => {
          setDraft(e.target.value);
          onChange(e.target.valueAsNumber);
        }}
      />
      <button
        type="button"
        aria-label={`Increase ${label}`}
        disabled={disabled || value >= max}
        onClick={() => adjust(1)}
      >
        +
      </button>
    </div>
  );
}
