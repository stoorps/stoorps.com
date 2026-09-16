/// <reference lib="webworker" />
import { getModel } from "../../generated/catalog";
import { loadModelModule } from "../../generated/model-loaders";
import { validateParameters, geometryContract } from "../models/types";
import type {
  Request,
  Response,
  ModelInstance,
  PartInstance,
  ModelModule,
} from "./protocol";
const scope = self as unknown as DedicatedWorkerGlobalScope;
const modules = new Map<string, Promise<ModelModule>>();
let assembly: ModelInstance | undefined;
let parts: PartInstance[] = [];
function canonical(value: unknown): string {
  if (Array.isArray(value)) return "[" + value.map(canonical).join(",") + "]";
  if (value && typeof value === "object")
    return (
      "{" +
      Object.entries(value)
        .sort(([a], [b]) => a.localeCompare(b))
        .map(([k, v]) => JSON.stringify(k) + ":" + canonical(v))
        .join(",") +
      "}"
    );
  return JSON.stringify(value);
}
function moduleFor(id: string): Promise<ModelModule> {
  let loaded = modules.get(id);
  if (!loaded) {
    loaded = loadModelModule(id).then(async (module) => {
      await module.default();
      return module;
    });
    modules.set(id, loaded);
  }
  return loaded;
}
function dispose() {
  parts.forEach((p) => p.free());
  parts = [];
  assembly?.free();
  assembly = undefined;
}
function respond(message: Response) {
  scope.postMessage(message);
}
scope.onmessage = async ({ data }: MessageEvent<Request>) => {
  try {
    if (data.type === "build") {
      const model = getModel(data.model);
      if (data.revision !== model.revision)
        throw new Error("This model revision is unavailable.");
      validateParameters(model, data.params);
      const module = await moduleFor(model.id);
      if (
        canonical(geometryContract(JSON.parse(module.catalog_json()))) !==
        canonical(geometryContract(model))
      )
        throw new Error(
          "The model catalogue and compiled geometry differ. Rebuild the model before continuing.",
        );
      const started = performance.now();
      const next = new module.Model(JSON.stringify(data.params));
      const nextParts: PartInstance[] = [];
      try {
        for (let i = 0; i < model.parts.length; i++)
          nextParts.push(next.part(i));
        const meshes = nextParts.map((p) => {
          const positions = p.positions();
          // CAD bounding boxes can conservatively expand around fillets. The
          // displayed dimensions use assembled tessellation bounds instead.
          const bounds = [
            Infinity,
            Infinity,
            Infinity,
            -Infinity,
            -Infinity,
            -Infinity,
          ];
          for (let i = 0; i < positions.length; i++) {
            const axis = i % 3;
            bounds[axis] = Math.min(bounds[axis], positions[i]);
            bounds[axis + 3] = Math.max(bounds[axis + 3], positions[i]);
          }
          return {
            positions,
            normals: p.normals(),
            surfaceNormals: p.surface_normals?.(),
            indices: p.indices(),
            volume: p.volume(),
            bounds,
          };
        });
        dispose();
        assembly = next;
        parts = nextParts;
        const message: Response = {
          id: data.id,
          result: {
            parts: meshes,
            measurements: JSON.parse(next.measurements_json()),
            milliseconds: performance.now() - started,
          },
        };
        scope.postMessage(
          message,
          meshes.flatMap((p) => [
            p.positions.buffer,
            p.normals.buffer,
            p.indices.buffer,
            ...(p.surfaceNormals ? [p.surfaceNormals.buffer] : []),
          ]) as ArrayBuffer[],
        );
      } catch (error) {
        nextParts.forEach((p) => p.free());
        next.free();
        throw error;
      }
    } else if (data.type === "export") {
      if (!assembly) throw new Error("Build a configuration before exporting.");
      if (data.format !== "step" && data.format !== "stl")
        throw new Error("Unsupported export format.");
      if (Array.isArray(data.part)) {
        if (
          !data.part.length ||
          new Set(data.part).size !== data.part.length ||
          data.part.some((i) => !Number.isInteger(i) || !parts[i])
        )
          throw new Error("Select valid parts for export.");
        if (data.format !== "step")
          throw new Error("Select a single part for STL.");
        respond({
          id: data.id,
          result: assembly.step_selected(new Uint32Array(data.part)),
        });
      } else if (data.part === -1 && data.format === "step")
        respond({ id: data.id, result: assembly.step() });
      else if (Number.isInteger(data.part) && parts[data.part])
        respond({
          id: data.id,
          result:
            data.format === "step"
              ? parts[data.part].step()
              : parts[data.part].stl(),
        });
      else throw new Error("Select an individual part for STL export.");
    } else if (data.type === "dispose") {
      dispose();
      respond({ id: data.id, result: null });
    }
  } catch (error) {
    respond({
      id: data.id,
      error: error instanceof Error ? error.message : String(error),
    });
  }
};
