export type Parameters = Record<string, number>;
export type ParameterDefinition = {
  share_id: number;
  options?: string[];
  key: string;
  unit?: string;
  group?: string;
  help?: string;
  advanced?: boolean;
  label: string;
  default: number;
  min: number;
  max: number;
  step: number;
};
export type CameraPreset = {
  direction: readonly [number, number, number];
  zoom: number;
  pan: readonly [number, number];
};
export type ModelDefinition = {
  /** Omitted flags preserve existing live models. */
  enabled?: boolean;
  status?: "work-in-progress" | "ready";
  status_note?: string;
  card_label?: string;
  card_link_text?: string;
  artwork_caption?: string;
  artwork_caption_bottom?: string;
  display_order?: number;
  share_id: number;
  default_view?: "solid" | "outline";
  backend: "cadrum" | "manifold";
  formats?: ("stl" | "step")[];
  camera?: { desktop?: CameraPreset; mobile?: CameraPreset };
  id: string;
  revision: number;
  title: string;
  description: string;
  configuration_hint?: string;
  parameter_note?: string;
  overview?: string;
  print_notes?: string;
  source_url?: string;
  parameters: readonly ParameterDefinition[];
  parts: readonly { id: string; name: string; color: number }[];
};
export function defaults(model: ModelDefinition): Parameters {
  return Object.fromEntries(model.parameters.map((p) => [p.key, p.default]));
}
export function validateParameters(
  model: ModelDefinition,
  params: Parameters,
): void {
  const keys = model.parameters.map((p) => p.key);
  if (Object.keys(params).some((key) => !keys.includes(key)))
    throw new Error("This configuration contains an unknown setting.");
  for (const p of model.parameters) {
    const value = params[p.key];
    if (
      !Number.isFinite(value) ||
      value < p.min ||
      value > p.max ||
      Math.abs(
        (value - p.min) / p.step - Math.round((value - p.min) / p.step),
      ) > 1e-8
    )
      throw new Error(
        `${p.label}: use ${p.min}–${p.max}${p.unit ? ` ${p.unit}` : ""}, in steps of ${p.step}.`,
      );
  }
}

/** Presentation can change without invalidating the compiled geometry. */
export function geometryContract(model: ModelDefinition) {
  return {
    id: model.id,
    revision: model.revision,
    backend: model.backend,
    parameters: model.parameters.map(
      ({ key, default: defaultValue, min, max, step }) => ({
        key,
        default: defaultValue,
        min,
        max,
        step,
      }),
    ),
    parts: model.parts.map((part) => part.id),
  };
}

/** Catalogue order is editorial; ID breaks ties without depending on filesystem order. */
export function visibleModels(
  models: readonly ModelDefinition[],
): ModelDefinition[] {
  return models
    .filter((model) => model.enabled !== false)
    .sort(
      (a, b) =>
        (a.display_order ?? Number.MAX_SAFE_INTEGER) -
          (b.display_order ?? Number.MAX_SAFE_INTEGER) ||
        a.id.localeCompare(b.id),
    );
}
