import { createFileRoute, Link } from "@tanstack/react-router";
import { models } from "../../generated/catalog";
import { visibleModels } from "../models/types";
import { ModelCard } from "../components/ModelCard";
export const Route = createFileRoute("/")({ component: Home });
function Home() {
  const cards = visibleModels(models);
  return (
    <main id="main" className="home content">
      <section className="home-intro">
        <div className="home-greeting">
          <h1>Hey, I’m stoorps.</h1>
          <nav className="home-socials" aria-label="Find me online">
            <a
              href="https://github.com/stoorps"
              aria-label="GitHub"
              title="GitHub"
            >
              <svg viewBox="0 0 24 24" aria-hidden="true">
                <path
                  fill="currentColor"
                  d="M12 .8a11.2 11.2 0 0 0-3.54 21.83c.56.1.76-.24.76-.54v-2.08c-3.12.68-3.78-1.33-3.78-1.33-.51-1.29-1.24-1.64-1.24-1.64-1.02-.7.08-.69.08-.69 1.13.08 1.72 1.16 1.72 1.16 1 1.72 2.62 1.22 3.26.93.1-.72.39-1.22.71-1.5-2.49-.28-5.11-1.24-5.11-5.54 0-1.22.44-2.22 1.15-3-.12-.28-.5-1.42.11-2.96 0 0 .94-.3 3.08 1.15a10.7 10.7 0 0 1 5.6 0c2.14-1.45 3.08-1.15 3.08-1.15.61 1.54.23 2.68.11 2.96.72.78 1.15 1.78 1.15 3 0 4.31-2.63 5.26-5.13 5.54.4.35.76 1.03.76 2.08v3.07c0 .3.2.65.77.54A11.2 11.2 0 0 0 12 .8Z"
                />
              </svg>
            </a>
            <a
              href="https://mastodon.social/@stoorps"
              rel="me"
              aria-label="Mastodon"
              title="Mastodon"
            >
              <svg viewBox="0 0 24 24" aria-hidden="true">
                <path
                  fill="currentColor"
                  d="M21.58 13.91c-.32 1.65-2.87 3.46-5.8 3.81-1.53.18-3.03.35-4.63.28-2.62-.12-4.69-.63-4.69-.63v.77c.34 2.65 2.63 2.81 4.72 2.89 2.12.08 4.02-.52 4.02-.52l.09 1.89s-1.49.8-4.14.95c-1.46.08-3.28-.04-5.4-.6C1.16 21.51.37 16.54.25 11.5.21 10 .23 8.58.23 7.39c0-5.17 3.39-6.69 3.39-6.69C5.33-.08 8.26-.41 11.31-.44h.07c3.05.03 5.98.36 7.69 1.14 0 0 3.39 1.52 3.39 6.69 0 0 .04 3.81-.88 6.52ZM18.4 7.79c0-1.28-.33-2.3-.99-3.05-.68-.75-1.56-1.13-2.65-1.13-1.26 0-2.21.48-2.84 1.44l-.61 1.03-.61-1.03c-.63-.96-1.58-1.44-2.84-1.44-1.09 0-1.97.38-2.65 1.13-.66.75-.99 1.77-.99 3.05v6.26H6.7V7.97c0-1.28.54-1.93 1.62-1.93 1.19 0 1.79.77 1.79 2.29v3.33h2.4V8.33c0-1.52.6-2.29 1.79-2.29 1.08 0 1.62.65 1.62 1.93v6.08h2.48V7.79Z"
                  transform="translate(1 1) scale(.94)"
                />
              </svg>
            </a>
          </nav>
        </div>
        <p className="lede">
          I’m a software engineer who likes making useful things—software,
          physical designs, and the occasional experiment.
        </p>
        <Link to="/about" className="home-about-link">
          More about me <span aria-hidden="true">→</span>
        </Link>
      </section>
      {cards.length > 0 && (
        <section aria-labelledby="featured-heading">
          <div className="section-heading">
            <h2 id="featured-heading">From the workbench</h2>
          </div>
          <div className="model-card-list">
            {cards.map((model) => (
              <ModelCard key={model.id} model={model} />
            ))}
          </div>
        </section>
      )}
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
