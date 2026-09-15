import { createFileRoute, Link } from "@tanstack/react-router";
import { ModelArtwork } from "../components/ModelArtwork";
export const Route = createFileRoute("/")({ component: Home });
function Home() {
  return (
    <main id="main" className="home content">
      <section className="home-intro">
        <p className="eyebrow">
          <span className="small-dot" /> Software, objects & experiments
        </p>
        <h1>
          Things I make.
          <br />
          <span className="muted-heading">Things you can make yours.</span>
        </h1>
        <p className="lede">
          I’m stoorps, a software engineer who likes making useful things.
          Here’s a growing collection of code, physical designs and experiments.
        </p>
      </section>
      <section aria-labelledby="featured-heading">
        <div className="section-heading">
          <h2 id="featured-heading">On the workbench</h2>
          <span className="eyebrow">01 / Physical design</span>
        </div>
        <Link
          to="/designs/$modelId"
          params={{ modelId: "bilresa" }}
          className="featured-model"
        >
          <div className="model-card-art">
            <span className="art-caption">BILRESA / Three-part assembly</span>
            <ModelArtwork />
            <span className="art-caption bottom">Made to fit your setup</span>
          </div>
          <div className="model-card-copy">
            <span className="pill">Configurable · 3D printable</span>
            <h3>
              A little order.
              <br />
              Right by the light switch.
            </h3>
            <p>
              A cover for your wall switch, with space for BILRESA remotes on
              either side. Choose your layout and download the parts.
            </p>
            <span className="text-action">
              Configure the cover <span aria-hidden="true">↗</span>
            </span>
          </div>
        </Link>
      </section>
      <section className="software-section" aria-labelledby="software-heading">
        <div className="section-heading">
          <h2 id="software-heading">A few things in code</h2>
          <a className="subtle-link" href="https://github.com/stoorps">
            More on GitHub ↗
          </a>
        </div>
        <div className="project-grid">
          <a
            className="project-card"
            href="https://github.com/cosmic-utils/cosmic-ext-storage"
          >
            <span className="eyebrow">Rust / Desktop</span>
            <h3>
              cosmic-ext-storage <span>↗</span>
            </h3>
            <p>A disk utility for the COSMIC desktop.</p>
          </a>
          <a className="project-card" href="https://github.com/stoorps/vib-rs">
            <span className="eyebrow">Rust / Libraries</span>
            <h3>
              vib-rs <span>↗</span>
            </h3>
            <p>Rust libraries for working with Vib.</p>
          </a>
          <a className="project-card" href="https://github.com/stoorps/aim">
            <span className="eyebrow">Rust / Tools</span>
            <h3>
              aim <span>↗</span>
            </h3>
            <p>An AppImage manager.</p>
          </a>
        </div>
      </section>
    </main>
  );
}
