import type { ModelDefinition } from "../models/types";
const labels = { "work-in-progress": "Work in Progress", ready: "Ready" };
export function ModelStatusPill({ model }: { model: ModelDefinition }) {
  return model.status ? (
    <span className="pill model-status-pill" data-status={model.status}>
      {labels[model.status]}
    </span>
  ) : null;
}
export function ModelStatusNote({ model }: { model: ModelDefinition }) {
  const note =
    model.status_note ??
    (model.status === "work-in-progress"
      ? "This design is still in progress and may change."
      : undefined);
  return note ? <p className="model-status-note">{note}</p> : null;
}
