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
        <div className="model-identity">
        <Link to="/" className="breadcrumb">
          ← All projects
        </Link>
        <div className="model-heading-row">
          <h1>
            {model.title}
            <span className="title-dot">.</span>
          </h1>
          <span className="pill title-version">
            V{model.revision}
          </span>
        </div>
        <p>{model.description}</p>
        </div>
        <div id="model-downloads" />
      </div>
      <Suspense fallback={<PreviewPlaceholder />}>
        {client ? (
          <Configurator key={model.id} model={model} />
        ) : (
          <PreviewPlaceholder />
        )}
      </Suspense>

    </main>
  );
}
