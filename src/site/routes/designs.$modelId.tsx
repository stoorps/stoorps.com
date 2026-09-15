import { createFileRoute, Link, notFound } from "@tanstack/react-router";
import { lazy, Suspense, useEffect, useState } from "react";
import { models } from "../../generated/catalog";
const Configurator = lazy(() =>
  import("../configurator/Configurator").then((module) => ({
    default: module.Configurator,
  })),
);
export const Route = createFileRoute("/designs/$modelId")({
  loader: ({ params }) => {
    const model = models.find((model) => model.id === params.modelId);
    if (!model) throw notFound();
    return model;
  },
  head: ({ loaderData }) => ({
    meta: [
      { title: `${loaderData?.title || "Design"} — stoorps` },
      { name: "description", content: loaderData?.description || "" },
    ],
  }),
  component: ModelPage,
});
function PreviewPlaceholder() {
  return (
    <div className="preview-placeholder">
      <p role="status">Loading the configurator…</p>
      <noscript>
        Enable JavaScript to configure this model and download its parts.
      </noscript>
    </div>
  );
}
function ModelPage() {
  const model = Route.useLoaderData();
  const [client, setClient] = useState(false);
  useEffect(() => setClient(true), []);
  return (
    <main id="main" className="model-page">
      <div className="model-title">
        <Link to="/" className="breadcrumb">
          ← All projects
        </Link>
        <div>
          <h1>
            {model.title}
            <span className="title-dot">.</span>
          </h1>
          <span className="pill">
            Revision {model.revision} · {model.parts.length} parts
          </span>
        </div>
        <p>{model.description}</p>
      </div>
      <Suspense fallback={<PreviewPlaceholder />}>
        {client ? (
          <Configurator key={model.id} model={model} />
        ) : (
          <PreviewPlaceholder />
        )}
      </Suspense>
      <section className="model-notes">
        <div>
          <p className="eyebrow">About the design</p>
          <h2>{model.title}</h2>
          <p>{model.overview || model.description}</p>
        </div>
        <div>
          <h3>Before you print</h3>
          <p>
            {model.print_notes ||
              "Check the dimensions and tolerances for your printer before a full print."}
          </p>
          {model.source_url && <a href={model.source_url}>Original design ↗</a>}
        </div>
      </section>
    </main>
  );
}
