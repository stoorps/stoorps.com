import { createFileRoute } from "@tanstack/react-router";
export const Route = createFileRoute("/licenses")({
  head: () => ({ meta: [{ title: "Open-source notices — stoorps" }] }),
  component: Notices,
});
function Notices() {
  return (
    <main id="main" className="content narrow">
      <p className="eyebrow">Built with open source</p>
      <h1>
        Tools behind
        <br />
        the things.
      </h1>
      <p>
        The configurator uses cadrum and Open CASCADE Technology to generate CAD
        geometry, Manifold for lampshade meshes, and Three.js to display it. The site uses React and TanStack
        Start.
      </p>
      <p>
        Open CASCADE Technology is licensed under LGPL 2.1 with the Open CASCADE
        additional exception. Manifold uses Apache 2.0. The other primary libraries listed here use the
        MIT licence.
      </p>
      <p>
        <a href={`${import.meta.env.BASE_URL}notices/THIRD_PARTY_NOTICES.txt`}>
          Dependency notices and licences
        </a>
      </p>
      <p>
        <a href={`${import.meta.env.BASE_URL}notices/source-and-build.txt`}>
          Geometry source and rebuild information
        </a>
      </p>
    </main>
  );
}
