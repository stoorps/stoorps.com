import { useEffect } from "react";
import { useLocation } from "@tanstack/react-router";

/** Reveal content once per route visit, without hiding server-rendered content. */
export function PageMotion() {
  const pathname = useLocation({ select: location => location.pathname });
  useEffect(() => {
    const media = matchMedia("(prefers-reduced-motion: reduce)");
    const nodes = Array.from(document.querySelectorAll<HTMLElement>(
      ".home-greeting, .home-intro > .lede, .home-about-link, .home > section:not(.home-intro), .project-card, .about-back, .about-page > h1, .about-copy > *",
    ));
    if (media.matches) return;
    const observer = new IntersectionObserver(entries => {
      entries.forEach(({ target, isIntersecting }) => {
        if (isIntersecting) {
          (target as HTMLElement).dataset.motion = "visible";
          observer.unobserve(target);
        }
      });
    }, { threshold: 0, rootMargin: "0px 0px -24px 0px" });
    nodes.forEach((node, index) => {
      node.dataset.motion = "pending";
      node.style.setProperty("--reveal-delay", `${Math.min(index, 4) * 55}ms`);
      observer.observe(node);
    });
    const clear = () => nodes.forEach(node => { delete node.dataset.motion; node.style.removeProperty("--reveal-delay"); });
    const reduce = () => { if (media.matches) { observer.disconnect(); clear(); } };
    media.addEventListener("change", reduce);
    return () => { observer.disconnect(); clear(); media.removeEventListener("change", reduce); };
  }, [pathname]);
  return null;
}
