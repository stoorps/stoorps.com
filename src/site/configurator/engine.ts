import type { ModelDefinition, Parameters } from "../models/types";
import type { BuildResult, Command, ExportFormat, Response } from "./protocol";
export class GeometryEngine {
  private worker = new Worker(
    new URL("./geometry.worker.ts", import.meta.url),
    { type: "module" },
  );
  private sequence = 0;
  private dead = false;
  private pending = new Map<
    number,
    {
      resolve: (result: unknown) => void;
      reject: (error: Error) => void;
      timer: ReturnType<typeof setTimeout>;
    }
  >();
  constructor() {
    this.worker.onmessage = ({ data }: MessageEvent<Response>) => {
      const pending = this.pending.get(data.id);
      if (!pending) return;
      clearTimeout(pending.timer);
      this.pending.delete(data.id);
      if ("error" in data) pending.reject(new Error(data.error));
      else pending.resolve(data.result);
    };
    this.worker.onerror = () =>
      this.shutdown(
        new Error("The geometry engine stopped. Try restarting the preview."),
      );
    this.worker.onmessageerror = () =>
      this.shutdown(
        new Error("The geometry engine returned an unreadable result."),
      );
  }
  private request<T>(command: Command): Promise<T> {
    if (this.dead)
      return Promise.reject(
        new Error(
          "The geometry engine is unavailable. Restart the preview to continue.",
        ),
      );
    return new Promise((resolve, reject) => {
      const id = ++this.sequence;
      const timer = setTimeout(
        () =>
          this.shutdown(
            new Error(
              "The geometry engine took too long. Restart the preview to try again.",
            ),
          ),
        60000,
      );
      this.pending.set(id, {
        resolve: (result) => resolve(result as T),
        reject,
        timer,
      });
      this.worker.postMessage({ ...command, id });
    });
  }
  build(model: ModelDefinition, params: Parameters) {
    return this.request<BuildResult>({
      type: "build",
      model: model.id,
      revision: model.revision,
      params,
    });
  }
  export(part: number, format: ExportFormat) {
    return this.request<Uint8Array>({ type: "export", part, format });
  }
  private shutdown(reason: Error) {
    this.dead = true;
    this.worker.terminate();
    this.pending.forEach((p) => {
      clearTimeout(p.timer);
      p.reject(reason);
    });
    this.pending.clear();
  }
  dispose() {
    this.shutdown(new Error("Preview closed."));
  }
}
