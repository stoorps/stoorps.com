import type { Parameters } from "../models/types";
export type PartMesh = {
  positions: Float32Array;
  normals: Float32Array;
  indices: Uint32Array;
  volume: number;
};
export type BuildResult = { parts: PartMesh[]; milliseconds: number };
export type ExportFormat = "step" | "stl";
export type Command =
  | { type: "build"; model: string; revision: number; params: Parameters }
  | { type: "export"; part: number; format: ExportFormat }
  | { type: "dispose" };
export type Request = Command & { id: number };
export type Response =
  | { id: number; error: string }
  | { id: number; result: BuildResult | Uint8Array | null };

/** Every model crate exports this small browser ABI, independently of the site. */
export type PartInstance = {
  positions(): Float32Array;
  normals(): Float32Array;
  indices(): Uint32Array;
  volume(): number;
  step(): Uint8Array;
  stl(): Uint8Array;
  free(): void;
};
export type ModelInstance = {
  part(index: number): PartInstance;
  step(): Uint8Array;
  free(): void;
};
export type ModelModule = {
  default(): Promise<unknown>;
  catalog_json(): string;
  Model: new (parameters: string) => ModelInstance;
};
