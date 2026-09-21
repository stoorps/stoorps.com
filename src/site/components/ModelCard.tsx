import { Link } from "@tanstack/react-router";
import type { ModelDefinition } from "../models/types";
import { ModelArtwork } from "./ModelArtwork";
import { ModelStatusPill, ModelStatusNote } from "./ModelStatus";
export function ModelCard({ model }: { model: ModelDefinition }) {
  return (
    <Link
      to="/designs/$modelId"
      params={{ modelId: model.id }}
      className="featured-model"
    >
      <div
        className="model-card-art"
        style={{ viewTransitionName: `model-${model.id}` }}
      >
        {model.artwork_caption && (
          <span className="art-caption">{model.artwork_caption}</span>
        )}
        <ModelArtwork model={model} />
        {model.artwork_caption_bottom && (
          <span className="art-caption bottom">
            {model.artwork_caption_bottom}
          </span>
        )}
      </div>
      <div className="model-card-copy">
        {model.card_label && <span className="pill">{model.card_label}</span>}
        <div className="model-card-heading">
          <h3>{model.title}</h3>
          <ModelStatusPill model={model} />
        </div>
        <p>{model.description}</p>
        <ModelStatusNote model={model} />
        <span className="text-action">
          {model.card_link_text || "Configure"}{" "}
          <span aria-hidden="true">↗</span>
        </span>
      </div>
    </Link>
  );
}
