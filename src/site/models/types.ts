export type Parameters = Record<string, number>;
export type ParameterDefinition = {
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
