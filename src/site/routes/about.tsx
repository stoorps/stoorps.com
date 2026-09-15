import { createFileRoute, Link } from "@tanstack/react-router";
export const Route = createFileRoute("/about")({
  head: () => ({ meta: [{ title: "About — stoorps" }] }),
  component: About,
});
function About() {
  return (
    <main id="main" className="content about-page">
      <Link to="/" className="about-back"><span aria-hidden="true">←</span> Back</Link>
      <h1>
        Software.
        <br />
        And the things
        <br />
        <span className="muted-heading">it makes possible.</span>
      </h1>
      <div className="about-copy">
        <p className="lede">
          I’m stoorps, a software engineer with a soft spot for Rust, C# and
          making things work.
        </p>
        <p>
          My work has taken me through music production hardware, warehouse
          automation, industrial vending, simulator rides and retail. I enjoy
          the places where software meets something you can actually touch.
        </p>
        <p>
          This site collects some of those interests: open-source tools,
          practical designs and experiments. The configurable models let you
          adapt a design to your own setup and download the parts to make it.
        </p>
        <div className="about-links">
          <a href="https://github.com/stoorps">Find me on GitHub ↗</a>
          <a href="https://mastodon.social/@stoorps" rel="me">
            Mastodon ↗
          </a>
          <Link to="/">Explore the projects →</Link>
        </div>
      </div>
    </main>
  );
}
