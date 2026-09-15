import { previewImages } from "../../generated/previews";
import type { ModelDefinition } from "../models/types";

/** Build-generated artwork: no geometry engine is loaded by catalogue pages. */
export function ModelArtwork({ model }: { model: ModelDefinition }) {
  return <img className="model-artwork" src={previewImages[model.id]} width="1000" height="660" alt={`${model.title} — outline preview of the default configuration`} decoding="async" />;
}
