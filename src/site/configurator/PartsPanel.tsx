import { useEffect, useRef } from "react";
import type { ModelDefinition } from "../models/types";
function Selection({
  checked,
  mixed,
  label,
  onChange,
  disabled,
}: {
  checked: boolean;
  mixed?: boolean;
  label: string;
  onChange: () => void;
  disabled?: boolean;
}) {
  const ref = useRef<HTMLInputElement>(null);
  useEffect(() => {
    if (ref.current) ref.current.indeterminate = !!mixed;
  }, [mixed]);
  return (
    <input
      ref={ref}
      type="checkbox"
      aria-label={label}
      checked={checked}
      onChange={onChange}
      disabled={disabled}
    />
  );
}
export function Eye({ open }: { open: boolean }) {
  return (
    <svg
      width="18"
      height="18"
      viewBox="0 0 24 24"
      fill="none"
      stroke="currentColor"
      strokeWidth="1.7"
      aria-hidden="true"
    >
      <path d="M2 12s3.5-7 10-7 10 7 10 7-3.5 7-10 7S2 12 2 12Z" />
      <circle cx="12" cy="12" r="3" />
      {!open && <path d="m3 3 18 18" />}
    </svg>
  );
}
export function PartsPanel({
  model,
  visible,
  included,
  setVisible,
  setIncluded,
  disabled,
}: {
  model: ModelDefinition;
  visible: boolean[];
  included: boolean[];
  setVisible: (v: boolean[]) => void;
  setIncluded: (v: boolean[]) => void;
  disabled: boolean;
}) {
  const allVisible = visible.every(Boolean),
    allIncluded = included.every(Boolean);
  return (
    <section className="parts-panel" aria-label="Parts">
      <div className="parts-row parts-header">
        <h3>Parts</h3>
        <span title="Visible in preview">View</span>
        <span>Export</span>
      </div>
      <div className="parts-row all-parts">
        <strong>All parts</strong>
        <button
          className="eye-button"
          aria-label="Show all parts"
          aria-pressed={
            allVisible ? "true" : visible.some(Boolean) ? "mixed" : "false"
          }
          onClick={() => setVisible(visible.map(() => !allVisible))}
        >
          <Eye open={allVisible} />
        </button>
        <Selection
          label="Export all parts"
          checked={allIncluded}
          mixed={!allIncluded && included.some(Boolean)}
          disabled={disabled}
          onChange={() => setIncluded(included.map(() => !allIncluded))}
        />
      </div>
      {model.parts.map((p, i) => (
        <div className="parts-row" key={p.id}>
          <span className="part-name">
            <i
              style={{
                background: `#${p.color.toString(16).padStart(6, "0")}`,
              }}
            />
            {p.name}
          </span>
          <button
            className="eye-button"
            aria-label={`Show ${p.name}`}
            aria-pressed={visible[i]}
            onClick={() =>
              setVisible(visible.map((v, j) => (j === i ? !v : v)))
            }
          >
            <Eye open={visible[i]} />
          </button>
          <Selection
            label={`Export ${p.name}`}
            checked={included[i]}
            disabled={disabled}
            onChange={() =>
              setIncluded(included.map((v, j) => (j === i ? !v : v)))
            }
          />
        </div>
      ))}
    </section>
  );
}
