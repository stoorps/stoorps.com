import { NumberStepper } from "./NumberStepper";
import { useId, useState } from "react";
import type { Parameters } from "../models/types";
import {
  applyNodes,
  cellNodes,
  presetNodes,
  validateOpening,
  cellContactWarning,
  strandPlan,
} from "../../../models/lampshade/cells.mjs";

type Node = { x: number; y: number; hx: number; hy: number; smooth: number };
export function CellEditor({
  params: p,
  onChange,
}: {
  params: Parameters;
  onChange: (p: Parameters) => void;
}) {
  const [selected, setSelected] = useState(0);
  const [customNodes, setCustomNodes] = useState("");
  const tabsId = useId();
  const nodes: Node[] = cellNodes(p),
    index = Math.min(selected, nodes.length - 1),
    node = nodes[index];
  let error = "";
  try {
    if (p.cell_cut_inside || p.cell_cut_outside) validateOpening(p);
  } catch (e) {
    error = (e as Error).message;
  }
  const contactWarning = cellContactWarning(p);
  let strokeWidth = 0.12;
  try {
    strokeWidth = p.thickness / ((strandPlan(p).width * p.cell_scale) / 2);
  } catch {}
  const presets = [
    { name: "Triangle", count: 3, rotation: Math.PI / 2 },
    { name: "Square", count: 4, rotation: Math.PI / 4 },
    { name: "Diamond", count: 4, rotation: 0 },
    { name: "Pentagon", count: 5, rotation: Math.PI / 2 },
    { name: "Hexagon", count: 6, rotation: 0 },
    { name: "Octagon", count: 8, rotation: 0 },
    { name: "Rounded", count: 6, rotation: 0, smooth: true },
  ];
  const matched = presets.find((preset) => {
    const expected = presetNodes(preset.count, preset.rotation, preset.smooth);
    return (
      expected.length === nodes.length &&
      expected.every((n: Node, i: number) =>
        ["x", "y", "smooth", ...(n.smooth ? ["hx", "hy"] : [])].every(
          (k) =>
            Math.abs(n[k as keyof Node] - nodes[i][k as keyof Node]) < 0.00001,
        ),
      )
    );
  });
  const presetName =
    customNodes === JSON.stringify(nodes)
      ? "Custom"
      : matched?.name || "Custom";
  function commit(next: Node[]) {
    setCustomNodes(JSON.stringify(next));
    onChange(applyNodes(p, next));
  }
  function update(change: Partial<Node>) {
    const next = nodes.map((n, i) => (i === index ? { ...n, ...change } : n));
    commit(next);
  }
  function preset(count: number, rotation = 0, smooth = false) {
    setSelected(0);
    setCustomNodes("");
    onChange(applyNodes(p, presetNodes(count, rotation, smooth)));
  }
  const path =
    nodes
      .map((n, i) => {
        const b = nodes[(i + 1) % nodes.length];
        return `${i === 0 ? `M ${n.x} ${-n.y}` : ""} C ${n.x + (n.smooth ? n.hx : 0)} ${-(n.y + (n.smooth ? n.hy : 0))} ${b.x - (b.smooth ? b.hx : 0)} ${-(b.y - (b.smooth ? b.hy : 0))} ${b.x} ${-b.y}`;
      })
      .join(" ") + " Z";
  function drag(
    e: React.PointerEvent<SVGElement>,
    kind: "node" | "handle",
    i: number,
    sign = 1,
  ) {
    if (!e.currentTarget.hasPointerCapture(e.pointerId)) return;
    const svg = e.currentTarget.ownerSVGElement!,
      pt = svg.createSVGPoint();
    pt.x = e.clientX;
    pt.y = e.clientY;
    const local = pt.matrixTransform(svg.getScreenCTM()!.inverse());
    const x = Math.max(-1, Math.min(1, (local.x - 140) / 105)),
      y = Math.max(-1, Math.min(1, (140 - local.y) / 105));
    const n = nodes[i],
      round = (v: number) => Math.round(v * 1000) / 1000;
    const change =
      kind === "node"
        ? { x: round(x), y: round(y) }
        : {
            hx: round(
              Math.max(
                -(1 - Math.abs(n.x)),
                Math.min(1 - Math.abs(n.x), (x - n.x) * sign),
              ),
            ),
            hy: round(
              Math.max(
                -(1 - Math.abs(n.y)),
                Math.min(1 - Math.abs(n.y), (y - n.y) * sign),
              ),
            ),
          };
    commit(nodes.map((n, j) => (j === i ? { ...n, ...change } : n)));
  }
  return (
    <section className="cell-editor" aria-label="Cell opening editor">
      <h3>Cell shape</h3>
      <div className="cell-cutaways">
        <label>
          <input
            type="checkbox"
            checked={!!p.cell_cut_inside}
            onChange={(e) =>
              onChange({ ...p, cell_cut_inside: e.target.checked ? 1 : 0 })
            }
          />{" "}
          Cut away inside
        </label>
        <label>
          <input
            type="checkbox"
            checked={!!p.cell_cut_outside}
            onChange={(e) =>
              onChange({ ...p, cell_cut_outside: e.target.checked ? 1 : 0 })
            }
          />{" "}
          Cut away outside
        </label>
      </div>
      <p className="parameter-help">
        {p.cell_cut_outside
          ? p.cell_cut_inside
            ? "Strand rings: keep a band around each shape. Wall / strand thickness controls its width and depth."
            : "Filled shapes: keep the shape and its outer band, removing the surrounding material."
          : p.cell_cut_inside
            ? "Cutout cells: remove each shape from a continuous wall."
            : "Solid wall: neither region is removed."}
      </p>
      <label className="editor-preset">
        Cell preset
        <select
          value={presetName}
          onChange={(e) => {
            const item = presets.find((p) => p.name === e.target.value);
            if (item) preset(item.count, item.rotation, item.smooth);
            else setCustomNodes(JSON.stringify(nodes));
          }}
        >
          <option>Custom</option>
          {presets.map((p) => (
            <option key={p.name}>{p.name}</option>
          ))}
        </select>
      </label>
      <svg
        viewBox="0 0 280 280"
        aria-label="Drag cell nodes and curve handles"
        className="cell-canvas"
      >
        <rect
          x="20"
          y="20"
          width="240"
          height="240"
          rx="4"
          fill="currentColor"
          fillOpacity={p.cell_cut_outside ? "0" : ".1"}
        />
        <rect
          x="35"
          y="35"
          width="210"
          height="210"
          fill="none"
          stroke="currentColor"
          strokeDasharray="4 4"
          opacity=".4"
        />
        <path
          d={path}
          transform="translate(140 140) scale(105)"
          fill={
            p.cell_cut_inside
              ? p.cell_cut_outside
                ? "none"
                : "#f6f6ed"
              : "currentColor"
          }
          stroke={error ? "#a54032" : "currentColor"}
          strokeWidth={p.cell_cut_outside ? strokeWidth : ".015"}
        />
        <path
          d={path}
          transform="translate(140 140) scale(105)"
          fill="none"
          stroke="#f6f6ed"
          strokeWidth=".032"
          pointerEvents="none"
        />
        <path
          d={path}
          transform="translate(140 140) scale(105)"
          fill="none"
          stroke="#a74421"
          strokeWidth=".012"
          pointerEvents="none"
        />
        {nodes.map((n, i) => (
          <g key={i}>
            {i === index &&
              !!n.smooth &&
              [-1, 1].map((sign) => (
                <g key={sign}>
                  <line
                    x1={140 + n.x * 105}
                    y1={140 - n.y * 105}
                    x2={140 + (n.x + sign * n.hx) * 105}
                    y2={140 - (n.y + sign * n.hy) * 105}
                    stroke="#b7431c"
                    strokeWidth="2.5"
                  />
                  <circle
                    cx={140 + (n.x + sign * n.hx) * 105}
                    cy={140 - (n.y + sign * n.hy) * 105}
                    r="5"
                    fill="#f6f6ed"
                    stroke="#b7431c"
                    strokeWidth="2"
                    onPointerDown={(e) =>
                      e.currentTarget.setPointerCapture(e.pointerId)
                    }
                    onPointerMove={(e) => drag(e, "handle", i, sign)}
                    onPointerUp={(e) =>
                      e.currentTarget.releasePointerCapture(e.pointerId)
                    }
                  />
                </g>
              ))}
            <circle
              cx={140 + n.x * 105}
              cy={140 - n.y * 105}
              r={i === index ? 7 : 5}
              fill="#b7431c"
              stroke="#fffdf5"
              strokeWidth="2.5"
              onPointerDown={(e) => {
                setSelected(i);
                e.currentTarget.setPointerCapture(e.pointerId);
              }}
              onPointerMove={(e) => drag(e, "node", i)}
              onPointerUp={(e) =>
                e.currentTarget.releasePointerCapture(e.pointerId)
              }
            />
            <text
              x={151 + n.x * 105}
              y={136 - n.y * 105}
              fontSize="12"
              fill="#702a16"
              stroke="#fffdf5"
              strokeWidth="3"
              paintOrder="stroke"
            >
              {i + 1}
            </text>
          </g>
        ))}
      </svg>
      <p className="parameter-help">
        {p.cell_cut_outside
          ? "Neighbouring shapes must overlap with material to join. Cell size scales this shape relative to the spacing; the boundary is a drawing guide, not a separation limit."
          : "The dashed boundary reserves material between neighbouring cutouts."}{" "}
        The finished shape is automatically centred and scaled to fit −1 to 1,
        preserving its proportions. Use cell size and aspect to adjust the
        pattern. A shape cannot cross itself. Rotation is applied to the
        finished pattern.
      </p>
      <div className="node-tab-bar">
        <div role="tablist" aria-label="Cell nodes" className="node-tabs">
          {nodes.map((_, i) => (
            <button
              type="button"
              role="tab"
              key={i}
              id={`${tabsId}-${i}`}
              aria-selected={i === index}
              aria-controls={`${tabsId}-panel`}
              tabIndex={i === index ? 0 : -1}
              onClick={() => setSelected(i)}
              onKeyDown={(e) => {
                const next =
                  e.key === "ArrowRight"
                    ? (i + 1) % nodes.length
                    : e.key === "ArrowLeft"
                      ? (i + nodes.length - 1) % nodes.length
                      : e.key === "Home"
                        ? 0
                        : e.key === "End"
                          ? nodes.length - 1
                          : -1;
                if (next < 0) return;
                e.preventDefault();
                setSelected(next);
                document.getElementById(`${tabsId}-${next}`)?.focus();
              }}
            >
              Node {i + 1}
            </button>
          ))}
        </div>
        <button
          type="button"
          className="icon-button node-add"
          title="Add node after selected"
          aria-label="Add node after selected"
          disabled={nodes.length === 8}
          onClick={() => {
            const b = nodes[(index + 1) % nodes.length],
              next = [...nodes];
            next.splice(index + 1, 0, {
              x: Math.round((node.x + b.x) * 500) / 1000,
              y: Math.round((node.y + b.y) * 500) / 1000,
              hx: 0,
              hy: 0,
              smooth: 0,
            });
            setSelected(index + 1);
            commit(next);
          }}
        >
          +
        </button>
      </div>
      <div
        role="tabpanel"
        id={`${tabsId}-panel`}
        aria-labelledby={`${tabsId}-${index}`}
        className="cell-fields"
      >
        {(["x", "y", "hx", "hy"] as const).map((k) => (
          <label key={k}>
            {k === "hx"
              ? "Handle X"
              : k === "hy"
                ? "Handle Y"
                : k.toUpperCase()}
            <NumberStepper
              label={
                k === "hx"
                  ? "Handle X"
                  : k === "hy"
                    ? "Handle Y"
                    : k.toUpperCase()
              }
              value={node[k]}
              min={-1}
              max={1}
              step={0.001}
              disabled={k.startsWith("h") && !node.smooth}
              onChange={(value) => {
                if (Number.isFinite(value)) update({ [k]: value });
              }}
            />
          </label>
        ))}
        <label>
          Point type
          <select
            value={node.smooth}
            onChange={(e) => {
              const smooth = Number(e.target.value),
                prev = nodes[(index + nodes.length - 1) % nodes.length],
                next = nodes[(index + 1) % nodes.length];
              update({
                smooth,
                ...(smooth
                  ? {
                      hx:
                        Math.round(
                          Math.max(
                            -(1 - Math.abs(node.x)),
                            Math.min(
                              1 - Math.abs(node.x),
                              (next.x - prev.x) / 6,
                            ),
                          ) * 1000,
                        ) / 1000,
                      hy:
                        Math.round(
                          Math.max(
                            -(1 - Math.abs(node.y)),
                            Math.min(
                              1 - Math.abs(node.y),
                              (next.y - prev.y) / 6,
                            ),
                          ) * 1000,
                        ) / 1000,
                    }
                  : {}),
              });
            }}
          >
            <option value={0}>Corner</option>
            <option value={1}>Smooth</option>
          </select>
        </label>
        <button
          type="button"
          className="icon-button node-remove"
          title="Remove selected node"
          aria-label="Remove selected node"
          disabled={nodes.length === 3}
          onClick={() => {
            commit(nodes.filter((_, i) => i !== index));
            setSelected(Math.max(0, index - 1));
          }}
        >
          <svg
            width="20"
            height="20"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="1.7"
            aria-hidden="true"
          >
            <path d="M3 6h18M9 6V3h6v3M5 6l1 15h12l1-15M10 10v7M14 10v7" />
          </svg>
        </button>
      </div>
      {contactWarning && !error && (
        <p role="status" className="parameter-error">
          {contactWarning}
        </p>
      )}
      {error && (
        <p role="alert" className="parameter-error">
          {error}
        </p>
      )}
    </section>
  );
}
