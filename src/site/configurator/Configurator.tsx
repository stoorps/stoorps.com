import { useEffect, useRef, useState } from "react";
import { createPortal, flushSync } from "react-dom";
import type { ModelDefinition, Parameters } from "../models/types";
import { defaults, validateParameters } from "../models/types";
import { configurationUrl, parametersFromHash } from "./share";
import { GeometryEngine } from "./engine";
import { ParameterControl } from "./ParameterControl";
import { Viewer } from "./Viewer";
import type { BuildResult } from "./protocol";
function download(bytes: Uint8Array, name: string) {
  const url = URL.createObjectURL(
    new Blob([new Uint8Array(bytes)], { type: "application/octet-stream" }),
  );
  const link = document.createElement("a");
  link.href = url;
  link.download = name;
  link.click();
  setTimeout(() => URL.revokeObjectURL(url), 10000);
}
export function Configurator({ model }: { model: ModelDefinition }) {
  const [params, setParams] = useState<Parameters | null>(null),
    [linkError, setLinkError] = useState("");
  const [result, setResult] = useState<BuildResult | null>(null),
    [builtKey, setBuiltKey] = useState("");
  const [message, setMessage] = useState("Loading your configuration…"),
    [error, setError] = useState("");
  const [visible, setVisible] = useState(model.parts.map(() => true)),
    [included, setIncluded] = useState(model.parts.map(() => true)),
    [exploded, setExploded] = useState(false),
    [exporting, setExporting] = useState(false);
  const [copyStatus, setCopyStatus] = useState(""),
    [fallbackLink, setFallbackLink] = useState(""),
    [restart, setRestart] = useState(0);
  const [openCard, setOpenCard] = useState<"about" | "print" | "setup" | null>("setup");
  const cardStack = useRef<HTMLElement | null>(null);
  const cardMotion = useRef(false);
  const queuedCard = useRef<"about" | "print" | "setup" | null>(null);
  const currentCard = useRef(openCard);
  async function toggleCard(card: "about" | "print" | "setup") {
    if (cardMotion.current) { queuedCard.current = card; return; }
    const next = currentCard.current === card ? null : card;
    const stack = cardStack.current;
    if (!stack || matchMedia("(prefers-reduced-motion: reduce)").matches) {
      currentCard.current = next;
      setOpenCard(next);
      return;
    }
    cardMotion.current = true;
    const cards = Array.from(stack.children) as HTMLElement[];
    const contents = () => Array.from(stack.querySelectorAll<HTMLElement>(".info-card-content, .setup-scroll, .setup-actions, .setup-header-content")).filter(node => node.getClientRects().length);
    const play = async (animations: Animation[]) => {
      await Promise.all(animations.map(animation => animation.finished.catch(() => {})));
      animations.forEach(animation => animation.cancel());
    };
    const heights = cards.map(node => node.getBoundingClientRect().height);
    stack.classList.add("cards-transitioning");
    await play(contents().map(node => node.animate([{ opacity: 1 }, { opacity: 0 }], { duration: 100, fill: "forwards" })));
    stack.classList.add("cards-content-hidden");
    currentCard.current = next;
    flushSync(() => setOpenCard(next));
    const targets = cards.map(node => node.getBoundingClientRect().height);
    await play(cards.map((node, index) => node.animate([
      { height: `${heights[index]}px`, flex: "0 0 auto" },
      { height: `${targets[index]}px`, flex: "0 0 auto" },
    ], { duration: 220, easing: "cubic-bezier(.22,1,.36,1)", fill: "both" })));
    stack.classList.remove("cards-content-hidden");
    await play(contents().map(node => node.animate([{ opacity: 0 }, { opacity: 1 }], { duration: 140, fill: "both" })));
    stack.classList.remove("cards-transitioning");
    cardMotion.current = false;
    const queued = queuedCard.current;
    queuedCard.current = null;
    if (queued) void toggleCard(queued);
  }
  const [downloadHost, setDownloadHost] = useState<HTMLElement | null>(null);
  const helpButton = useRef<HTMLButtonElement>(null);
  const helpPanel = useRef<HTMLDivElement>(null);
  const [helpPosition, setHelpPosition] = useState({ top: 0, left: 0 });
  const [helpOpen, setHelpOpen] = useState(false);
  function positionHelp() {
    const rect = helpButton.current?.getBoundingClientRect();
    if (rect) setHelpPosition({ top: Math.max(16, Math.min(rect.bottom + 8, window.innerHeight - (helpPanel.current?.getBoundingClientRect().height || 260) - 16)), left: Math.max(16, Math.min(rect.right - 330, window.innerWidth - 346)) });
  }
  useEffect(() => {
    const media = matchMedia("(min-width: 761px)");
    const update = () => setDownloadHost(media.matches ? document.getElementById("model-downloads") : null);
    update(); media.addEventListener("change", update);
    return () => media.removeEventListener("change", update);
  }, []);
  useEffect(() => {
    if (!helpOpen) return;
    window.addEventListener("resize", positionHelp);
    window.addEventListener("scroll", positionHelp, true);
    return () => { window.removeEventListener("resize", positionHelp); window.removeEventListener("scroll", positionHelp, true); };
  }, [helpOpen]);
  useEffect(() => {
    if (copyStatus !== "Link copied") return;
    const timer = setTimeout(() => setCopyStatus(""), 2000);
    return () => clearTimeout(timer);
  }, [copyStatus]);
  const engine = useRef<GeometryEngine | null>(null);
  const active = useRef(true);
  const key = params ? JSON.stringify(params) : "";
  let validation = "";
  if (params) {
    try {
      validateParameters(model, params);
    } catch (e) {
      validation = (e as Error).message;
    }
  }
  const ready =
    !!result && key === builtKey && !validation && !linkError && !error;
  useEffect(() => {
    function readLink() {
      try {
        setParams(parametersFromHash(model, window.location.hash));
        setLinkError("");
        setCopyStatus("");
      } catch (e) {
        setLinkError((e as Error).message);
        setParams(null);
      }
    }
    readLink();
    window.addEventListener("hashchange", readLink);
    window.addEventListener("popstate", readLink);
    return () => {
      window.removeEventListener("hashchange", readLink);
      window.removeEventListener("popstate", readLink);
    };
  }, [model]);
  useEffect(() => {
    active.current = true;
    engine.current = new GeometryEngine();
    return () => {
      active.current = false;
      engine.current?.dispose();
      engine.current = null;
    };
  }, [restart]);
  useEffect(() => {
    if (!params || validation || linkError) return;
    let cancelled = false;
    setError("");
    setCopyStatus("");
    setFallbackLink("");
    setBuiltKey("");
    setMessage(
      result
        ? "Updating your parts…"
        : "Loading geometry engine & building your parts…",
    );
    const timer = setTimeout(async () => {
      try {
        const next = await engine.current!.build(model, params);
        if (cancelled) return;
        setResult(next);
        setBuiltKey(key);
        setMessage("Your parts are ready.");
        const url = configurationUrl(model, params, window.location.href);
        window.history.replaceState(window.history.state, "", url);
      } catch (e) {
        if (!cancelled) {
          setError((e as Error).message);
          setMessage("The preview could not be rebuilt.");
        }
      }
    }, 150);
    return () => {
      cancelled = true;
      clearTimeout(timer);
    };
  }, [key, validation, linkError, restart, model]);
  function reset() {
    setLinkError("");
    setError("");
    setParams(defaults(model));
    setCopyStatus("");
    setFallbackLink("");
  }
  async function copyLink() {
    if (!ready || !params) return;
    const url = configurationUrl(model, params, window.location.href);
    try {
      await navigator.clipboard.writeText(url);
      setCopyStatus("Link copied");
      setFallbackLink("");
    } catch {
      setFallbackLink(url);
      setCopyStatus("Copy the link below.");
    }
  }
  async function exportFiles(format: "step" | "stl" | "zip") {
    if (!ready || exporting || !params) return;
    setExporting(true);
    setError("");
    const stem = `${model.id}-r${model.revision}`;
    const selection = included.flatMap((v, i) => (v ? [i] : []));
    if (!selection.length) {
      setExporting(false);
      return;
    }
    try {
      if (format === "zip") {
        const { zipSync, strToU8 } = await import("fflate");
        const files: Record<string, Uint8Array> = {};
        for (const i of selection)
          files[`${stem}-${model.parts[i].id}.stl`] =
            await engine.current!.export(i, "stl");
        files["configuration.json"] = strToU8(
          JSON.stringify(
            {
              schema: 1,
              model: model.id,
              modelRevision: model.revision,
              parameters: params,
              parts: selection.map((i) => model.parts[i].id),
            },
            null,
            2,
          ),
        );
        files["README.txt"] = strToU8(
          "Import STL files in millimetres. All parts use assembled coordinates.\n" +
            (model.print_notes || "") +
            "\n" +
            configurationUrl(model, params, window.location.href),
        );
        if (active.current) download(zipSync(files), `${stem}-parts.zip`);
      } else {
        const bytes = await engine.current!.export(
          format === "step" ? selection : selection[0],
          format,
        );
        if (active.current)
          download(
            bytes,
            `${stem}-${selection.length > 1 ? "assembly" : model.parts[selection[0]].id}.${format}`,
          );
      }
    } catch (e) {
      if (active.current) setError((e as Error).message);
    } finally {
      if (active.current) setExporting(false);
    }
  }
  const selectedParts = included.flatMap((v, i) => (v ? [i] : []));
  const displayed = params || defaults(model);
  const downloadActions = (
          <div className="download-actions">
          <div className="export-buttons">
            <span className="download-version" aria-label={`Version ${model.revision}`}>V{model.revision}</span>
            <button disabled={!ready || exporting || !selectedParts.length}
              aria-label={`Download STLs (${selectedParts.length})`}
              onClick={() => exportFiles(selectedParts.length > 1 ? "zip" : "stl")}>
              <span aria-hidden="true">↓</span> STLs ({selectedParts.length})
            </button>
            <button className="secondary" aria-label="Download STEP"
              disabled={!ready || exporting || !selectedParts.length} onClick={() => exportFiles("step")}>
              <span aria-hidden="true">↓</span> STEP
            </button>
            <button ref={helpButton} className="icon-button download-info" aria-label="About downloads" popoverTarget="export-help" onClick={positionHelp}>
              <svg className="tool-icon" viewBox="0 0 24 24" aria-hidden="true"><circle cx="12" cy="12" r="9"/><path d="M12 11v6m0-10v1"/></svg>
            </button>
          </div>
          <div ref={helpPanel} id="export-help" popover="auto" className="export-help" style={helpPosition} onToggle={event => { setHelpOpen(event.newState === "open"); if (event.newState === "open") positionHelp(); }}>
            <strong>About downloads</strong>
            <p>{selectedParts.length ? `${selectedParts.length} parts selected for export.` : "Select at least one part to download."}</p>
            <p>STEP keeps assembled positions. A single STL downloads directly; multiple STLs download as a ZIP. Dimensions are in millimetres.</p>
            <button className="quiet-button" popoverTarget="export-help" popoverTargetAction="hide">Close</button>
          </div>
        <p className="sr-only" role="status">
          {validation ||
            (linkError ? "Configuration needs attention." : exporting ? "Preparing download…" : message)}
        </p>
        {error && (
          <div className="error-box" role="alert">
            <p>{error}</p>
            <button
              className="quiet-button"
              onClick={() => {
                setError("");
                setBuiltKey("");
                setRestart((v) => v + 1);
              }}
            >
              Restart preview
            </button>
          </div>
        )}
          </div>

  );
  return (
    <div
      className="configurator"
      aria-busy={!ready && !error && !linkError && !validation}
    >
      {downloadHost ? createPortal(downloadActions, downloadHost) : downloadActions}
      <aside ref={cardStack} className="controls-stack" aria-label="Design information and setup">
        {(["about", "print"] as const).map(card => (
          <section className={`info-card ${openCard === card ? "is-open" : ""}`} key={card}>
            <h2><button className="card-toggle" aria-expanded={openCard === card} aria-controls={`${card}-content`} onClick={() => toggleCard(card)}>
              {card === "about" ? "About the design" : "Before you print"}<span aria-hidden="true">{openCard === card ? "⌃" : "⌄"}</span>
            </button></h2>
            <div id={`${card}-content`} className="info-card-content" hidden={openCard !== card}>
              <p>{card === "about" ? model.overview || model.description : model.print_notes || "Check the dimensions and tolerances for your printer before a full print."}</p>
              {card === "about" && model.source_url && <a href={model.source_url}>Original design ↗</a>}
            </div>
          </section>
        ))}
        <section className={`controls-panel ${openCard === "setup" ? "is-open" : ""}`}>

        <div className="controls-heading">
          <div className="setup-row">
            <h2><button className="setup-toggle" aria-expanded={openCard === "setup"} aria-controls="setup-content" onClick={() => toggleCard("setup")}>Customise<span aria-hidden="true">{openCard === "setup" ? "⌃" : "⌄"}</span></button></h2>
            <div className="setup-actions" hidden={openCard !== "setup"}>
              <button
                className="icon-button"
                aria-label={copyStatus === "Link copied" ? "Link copied" : "Copy link"}
                title={copyStatus === "Link copied" ? "Link copied" : "Copy link"}
                disabled={!ready || exporting}
                onClick={copyLink}
              >
                {copyStatus === "Link copied" ? "✓" : <svg className="tool-icon" viewBox="0 0 24 24" aria-hidden="true"><path d="m10 13 4-4m-6 6-2 2a4 4 0 0 1-6-6l4-4a4 4 0 0 1 6 0m4 2 2-2a4 4 0 0 0-6-6l-2 2" transform="translate(3 3) scale(.85)"/></svg>}
              </button>
              <button
                className="icon-button"
                title="Reset all parameters"
                aria-label="Reset all parameters"
                disabled={
                  exporting ||
                  (!!params &&
                    model.parameters.every((p) => params[p.key] === p.default))
                }
                onClick={reset}
              >
                ↺
              </button>
            </div>
          </div>
        </div>
        <div id="setup-content" className="setup-scroll" hidden={openCard !== "setup"}>
          <p className="setup-hint">{model.configuration_hint || "Adjust the parameters to suit your setup."}</p>
        {linkError && (
          <div className="error-box" role="alert">
            <p>{linkError}</p>
            <button className="quiet-button" onClick={reset}>
              Start with the default layout
            </button>
          </div>
        )}
        <fieldset disabled={exporting || !!linkError || !params}>
          <legend className="sr-only">Switch layout</legend>
          {[...new Set(model.parameters.map(p => p.group || "Parameters"))].map(group => (
            <details className="parameter-group parameter-expander" key={group} open={group.toLowerCase() === "layout"}>
              <summary>{group}</summary>
              {model.parameters.filter(p => (p.group || "Parameters") === group).map(p => (
                <ParameterControl key={p.key} definition={p} value={displayed[p.key]}
                  onChange={value => { setParams({ ...displayed, [p.key]: value }); setCopyStatus(""); }} />
              ))}
            </details>
          ))}
        </fieldset>
        <span className="sr-only" role="status">
          {copyStatus}
        </span>
        {copyStatus && <p className="field-note">{copyStatus}</p>}
        {fallbackLink && (
          <label className="fallback-link">
            Configuration link
            <input
              readOnly
              value={fallbackLink}
              onFocus={(e) => e.target.select()}
            />
          </label>
        )}
        </div>
        </section>
      </aside>
      <Viewer
        model={model}
        parts={result?.parts || []}
        visible={visible}
        measurements={result?.measurements || []}
        exploded={exploded}
        setExploded={setExploded}
        included={included}
        setVisible={setVisible}
        setIncluded={setIncluded}
        exporting={exporting}
      />
    </div>
  );
}
