import type { Parameters } from "../models/types";
export type PartMesh = {
  positions: Float32Array;
  normals: Float32Array;
  surfaceNormals?: Float32Array;
  rounded?: boolean;
  indices: Uint32Array;
  volume: number;
  bounds: number[];
};
export type Measurement = {
  approximate?: boolean;
  label: string;
  value: number;
  from: [number, number, number];
  to: [number, number, number];
};
export type BuildResult = {
  parts: PartMesh[];
  milliseconds: number;
  measurements: Measurement[];
};
export type ExportFormat = "step" | "stl";
export type Command =
  | { type: "build"; model: string; revision: number; params: Parameters }
  | { type: "export"; part: number | number[]; format: ExportFormat }
  | { type: "dispose" };
export type Request = Command & { id: number };
export type Response =
  | { id: number; error: string }
  | { id: number; result: BuildResult | Uint8Array | null };

/** Every model crate exports this small browser ABI, independently of the site. */
export type PartInstance = {
  positions(): Float32Array;
  normals(): Float32Array;
  surface_normals?(): Float32Array;
  indices(): Uint32Array;
  volume(): number;
  bounds(): Float64Array;
  step(): Uint8Array;
  stl(): Uint8Array;
  free(): void;
};
export type ModelInstance = {
  part(index: number): PartInstance;
  measurements_json(): string;
  step_selected(selection: Uint32Array): Uint8Array;
  step(): Uint8Array;
  free(): void;
};
export type ModelModule = {
  default(): Promise<unknown>;
  catalog_json(): string;
  Model: new (parameters: string) => ModelInstance;
};
