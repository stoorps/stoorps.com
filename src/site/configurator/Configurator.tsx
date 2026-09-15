import { useEffect, useRef, useState } from "react";
import type { ModelDefinition, Parameters } from "../models/types";
import { defaults, validateParameters } from "../models/types";
import { configurationUrl, parametersFromHash } from "./share";
import { GeometryEngine } from "./engine";
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
  const [selected, setSelected] = useState(-1),
    [exploded, setExploded] = useState(false),
    [exporting, setExporting] = useState(false);
  const [copyStatus, setCopyStatus] = useState(""),
    [fallbackLink, setFallbackLink] = useState(""),
    [restart, setRestart] = useState(0);
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
    const stem = `${model.id}-r${model.revision}-${model.parameters.map((p) => `${p.key}-${params[p.key]}`).join("-")}`;
    try {
      if (format === "zip") {
        const { zipSync, strToU8 } = await import("fflate");
        const files: Record<string, Uint8Array> = {};
        for (let i = 0; i < model.parts.length; i++)
          files[`${stem}-${model.parts[i].id}.stl`] =
            await engine.current!.export(i, "stl");
        files["configuration.json"] = strToU8(
          JSON.stringify(
            {
              schema: 1,
              model: model.id,
              modelRevision: model.revision,
              parameters: params,
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
        const bytes = await engine.current!.export(selected, format);
        if (active.current)
          download(
            bytes,
            `${stem}-${selected === -1 ? "assembly" : model.parts[selected].id}.${format}`,
          );
      }
    } catch (e) {
      if (active.current) setError((e as Error).message);
    } finally {
      if (active.current) setExporting(false);
    }
  }
  const displayed = params || defaults(model);
  return (
    <div
      className="configurator"
      aria-busy={!ready && !error && !linkError && !validation}
    >
      <aside className="controls-panel">
        <div className="controls-heading">
          <p className="eyebrow">Make it yours</p>
          <h2>Your setup.</h2>
          <p>
            {model.configuration_hint ||
              "Adjust the parameters to suit your setup."}
          </p>
        </div>
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
          {model.parameters.map((p) => (
            <label className="parameter" key={p.key}>
              <span>{p.label}</span>
              <input
                name={p.key}
                type="number"
                min={p.min}
                max={p.max}
                step={p.step}
                required
                value={Number.isNaN(displayed[p.key]) ? "" : displayed[p.key]}
                aria-invalid={!!validation}
                onChange={(e) => {
                  setParams({ ...displayed, [p.key]: e.target.valueAsNumber });
                  setCopyStatus("");
                }}
              />
            </label>
          ))}
        </fieldset>
        <p className="field-note">{model.parameter_note}</p>
        <div className="control-divider" />
        <label className="select-label">
          View / export
          <select
            value={selected}
            disabled={exporting}
            onChange={(e) => setSelected(Number(e.target.value))}
          >
            <option value={-1}>All three parts</option>
            {model.parts.map((p, i) => (
              <option key={p.id} value={i}>
                {p.name}
              </option>
            ))}
          </select>
        </label>
        <label className="checkbox-label">
          <input
            type="checkbox"
            checked={exploded}
            disabled={selected !== -1}
            onChange={(e) => setExploded(e.target.checked)}
          />
          Separate parts in preview
        </label>
        <p className={`build-status ${ready ? "is-ready" : ""}`} role="status">
          {validation ||
            (linkError ? "Configuration needs attention." : message)}
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
        <div className="export-buttons">
          <button
            disabled={!ready || exporting}
            onClick={() => exportFiles("step")}
          >
            {exporting ? "Preparing download…" : "Download STEP"}{" "}
            <span aria-hidden="true">↓</span>
          </button>
          <button
            className="secondary"
            disabled={!ready || exporting}
            onClick={() => exportFiles(selected === -1 ? "zip" : "stl")}
          >
            {selected === -1 ? "Download all STLs" : "Download STL"}{" "}
            <span aria-hidden="true">↓</span>
          </button>
        </div>
        <p className="field-note">
          {selected === -1
            ? "STEP includes the full assembly. The ZIP contains three individual STL files."
            : "Download this part as editable STEP or printable STL."}{" "}
          Dimensions are in millimetres.
        </p>
        <div className="share-row">
          <button
            className="quiet-button"
            disabled={!ready || exporting}
            onClick={copyLink}
          >
            Copy link ↗
          </button>
          <button
            className="quiet-button muted"
            disabled={exporting}
            onClick={reset}
          >
            Reset layout
          </button>
        </div>
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
      </aside>
      <Viewer
        model={model}
        parts={result?.parts || []}
        selected={selected}
        exploded={exploded}
      />
    </div>
  );
}
