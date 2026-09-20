import type { Parameters } from "../models/types";
export type PartMesh = {
  explode?: [number, number, number];
  pivot?: [number, number];
  hand?: number;
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
  assembly?: Assembly;
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
  part_count?(): number;
  assembly_json?(): string;
  part(index: number): PartInstance;
  measurements_json(): string;
  step_selected(selection: Uint32Array): Uint8Array;
  step(): Uint8Array;
  free(): void;
};

export type ProfileNode = { x: number; y: number; mid?: [number, number] };
export type AssemblyPart = {
  id: string;
  name: string;
  color: number;
  group: string;
  material: string;
  profile: ProfileNode[];
  frame: { o: number[]; u: number[]; v: number[]; n: number[] };
  thickness: number;
  extrusion: number[];
  stock: number[];
  explode: [number, number, number];
  pivot?: [number, number];
  hand: number;
  notes: string[];
  operations: {
    id: string;
    kind: string;
    point: number[];
    direction: number[];
    diameter: number;
    depth: number;
    note: string;
  }[];
};
export type Assembly = {
  parts: AssemblyPart[];
  colliders: {
    group: string;
    name: string;
    polygon: number[][];
    z: number[];
  }[];
  metrics: Record<string, number | number[]>;
  hardware: { name: string; quantity: number; specification: string }[];
  warnings: string[];
  motion?: { angle: number; limit: string; valid: boolean };
};
export type ModelModule = {
  default(): Promise<unknown>;
  catalog_json(): string;
  Model: new (parameters: string) => ModelInstance;
};
