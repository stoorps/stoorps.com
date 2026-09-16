import {
  createRootRoute,
  HeadContent,
  Link,
  Outlet,
  Scripts,
  useLocation,
} from "@tanstack/react-router";
import { PageMotion } from "../components/PageMotion";
import { site } from "../site";
import motionStylesheet from "../motion.css?url";
import stylesheet from "../styles.css?url";
export const Route = createRootRoute({
  head: () => ({
    meta: [
      { charSet: "utf-8" },
      { name: "theme-color", content: "#1e1e20" },
      { name: "viewport", content: "width=device-width, initial-scale=1" },
      { title: "stoorps — Things I make" },
      { name: "description", content: site.description },
    ],
    links: [{ rel: "stylesheet", href: stylesheet }, { rel: "stylesheet", href: motionStylesheet }],
  }),
  component: Root,
  notFoundComponent: () => (
    <main className="content narrow">
      <p className="eyebrow">404 / Lost & found</p>
      <h1>
        Nothing here.
        <br />
        Yet.
      </h1>
      <p>That page or model could not be found.</p>
      <Link to="/" className="button">
        Back to the projects
      </Link>
    </main>
  ),
  errorComponent: ({ reset }) => (
    <main className="content narrow">
      <h1>Something went wrong.</h1>
      <p>The page could not be loaded.</p>
      <button onClick={reset}>Try again</button>
    </main>
  ),
});
function Root() {
  const minimalNavigation = useLocation({
    select: (location) => ["/", "/about", "/cv"].includes(location.pathname.replace(/\/$/, "") || "/"),
  });
  return (
    <html lang="en">
      <head>
        <HeadContent />
      </head>
      <body>
        <a
          className="skip-link"
          href="#main"
          onClick={(e) => {
            e.preventDefault();
            const main = document.getElementById("main");
            main?.setAttribute("tabindex", "-1");
            main?.focus();
            main?.scrollIntoView();
          }}
        >
          Skip to content
        </a>
        {!minimalNavigation && <header className="site-header">
          <Link to="/" className="wordmark" aria-label="stoorps home">
            stoorps<span className="wordmark-dot">.</span>
          </Link>
          <nav aria-label="Main navigation">
            <Link to="/" activeOptions={{ exact: true }}>
              Projects
            </Link>
            <Link to="/about">About</Link>
            <a href="https://github.com/stoorps">
              GitHub <span aria-hidden="true">↗</span>
            </a>
          </nav>
        </header>}
        <Outlet />
        <PageMotion />
        <footer className={`site-footer${minimalNavigation ? " minimal-footer" : ""}`}>
          {!minimalNavigation && <span>
            stoorps <span className="muted">/ Things I make</span>
          </span>}
          <div>
            {!minimalNavigation && <a href="https://mastodon.social/@stoorps" rel="me">
              Mastodon ↗
            </a>}
            <Link to="/licenses">Open-source notices</Link>
          </div>
        </footer>
        <Scripts />
      </body>
    </html>
  );
}
