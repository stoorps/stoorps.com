import { createFileRoute, Link, notFound } from "@tanstack/react-router";
import { Configurator } from "../configurator/Configurator";
import { models } from "../../generated/catalog";
export const Route = createFileRoute("/designs/$modelId")({
  loader: ({ params }) => {
    const model = models.find((model) => model.id === params.modelId);
    if (!model || model.enabled === false) throw notFound();
    return model;
  },
  head: ({ loaderData }) => ({
    meta: [
      { name: "theme-color", content: "#173d35" },
      { title: `${loaderData?.title || "Design"} — stoorps` },
      { name: "description", content: loaderData?.description || "" },
    ],
  }),
  component: ModelPage,
});
function ModelPage() {
  const model = Route.useLoaderData();
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
            <span className="pill title-version">V{model.revision}</span>
          </div>
          <p>{model.description}</p>
        </div>
        <div id="model-downloads" />
      </div>
      <Configurator key={model.id} model={model} />
    </main>
  );
}
