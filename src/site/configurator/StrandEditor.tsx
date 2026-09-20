import { useState, useId } from "react";
import type { Parameters } from "../models/types";
import { NumberStepper } from "./NumberStepper";
import {
  waveNodes,
  waveValue,
  applyWave,
  wavePreset,
  strandPlan,
} from "../../../models/lampshade/waves.mjs";
type Node = { x: number; y: number; slope: number };
export function StrandEditor({
  params: p,
  onChange,
}: {
  params: Parameters;
  onChange: (p: Parameters) => void;
}) {
  const [selected, setSelected] = useState(0);
  const id = useId();
  const plan = strandPlan(p);
  const scale = Math.min(280 / p.wave_width, 140 / p.wave_height);
  const graphWidth = p.wave_width * scale,
    graphHeight = p.wave_height * scale;
  const baseline = graphHeight + 30;
  const nodes: Node[] = waveNodes(p),
    index = Math.min(selected, nodes.length - 1),
    node = nodes[index];
  const preset =
    p.wave_arch === 1
      ? "Smooth"
      : ["Soft peaks"].find(
          (name) =>
            JSON.stringify(
              nodes.map(({ x, y, slope }) => ({ x, y, slope })),
            ) === JSON.stringify(wavePreset(name)),
        ) || "Custom";
  const commit = (next: Node[]) => onChange(applyWave(p, next));
  function update(change: Partial<Node>) {
    commit(nodes.map((n, i) => (i === index ? { ...n, ...change } : n)));
  }
  function drag(
    e: React.PointerEvent<SVGCircleElement>,
    i: number,
    handle = false,
  ) {
    e.preventDefault();
    setSelected(i);
    const svg = e.currentTarget.ownerSVGElement!;
    e.currentTarget.setPointerCapture(e.pointerId);
    const move = (event: PointerEvent) => {
      const point = new DOMPoint(event.clientX, event.clientY).matrixTransform(
        svg.getScreenCTM()!.inverse(),
      );
      if (handle) {
        const dx = (point.x - 20) / graphWidth - nodes[i].x;
        if (Math.abs(dx) > 0.01)
          commit(
            nodes.map((n, j) =>
              j === i
                ? {
                    ...n,
                    slope:
                      Math.round(
                        Math.max(
                          -12,
                          Math.min(
                            12,
                            ((baseline - point.y) / graphHeight - n.y) / dx,
                          ),
                        ) * 10,
                      ) / 10,
                  }
                : n,
            ),
          );
        return;
      }
      const x =
        i === 0
          ? 0
          : Math.max(
              nodes[i - 1].x + 0.02,
              Math.min(
                i === nodes.length - 1 ? 0.98 : nodes[i + 1].x - 0.02,
                (point.x - 20) / graphWidth,
              ),
            );
      const y = Math.max(0, Math.min(1, (baseline - point.y) / graphHeight));
      commit(
        nodes.map((n, j) =>
          j === i
            ? {
                ...n,
                x: Math.round(x * 100) / 100,
                y: Math.round(y * 100) / 100,
              }
            : n,
        ),
      );
    };
    const target = e.currentTarget;
    target.addEventListener("pointermove", move);
    target.addEventListener(
      "pointerup",
      () => target.removeEventListener("pointermove", move),
      { once: true },
    );
  }
  const ordered = nodes.every((n, i) => !i || n.x - nodes[i - 1].x >= 0.0199);
  return (
    <section className="cell-editor" aria-label="Strand wave editor">
      <div className="editor-preset">
        <label htmlFor={`${id}-preset`}>Wave preset</label>
        <select
          id={`${id}-preset`}
          value={preset}
          onChange={(e) => {
            setSelected(0);
            const smooth = e.target.value === "Smooth";
            onChange({
              ...applyWave({ ...p, wave_arch: 0 }, wavePreset(e.target.value)),
              wave_arch: smooth ? 1 : 0,
              ...(smooth
                ? {
                    wave_width: Math.max(4, p.wave_height),
                    wave_height: Math.max(4, p.wave_height),
                  }
                : {}),
            });
          }}
        >
          <option disabled>Custom</option>
          <option>Smooth</option>
          <option>Soft peaks</option>
        </select>
      </div>
      <svg
        viewBox="0 0 320 200"
        style={{ width: "100%", touchAction: "none" }}
        aria-label="Editable repeating strand wave"
      >
        <path
          d={`M20 ${baseline}H${20 + graphWidth} M20 30V${baseline} M${20 + graphWidth} 30V${baseline}`}
          stroke="#a7b4aa"
          fill="none"
        />
        <path
          d={Array.from(
            { length: 161 },
            (_, i) =>
              `${i ? "L" : "M"} ${20 + (i / 160) * graphWidth} ${baseline - waveValue(nodes, i / 160) * graphHeight}`,
          ).join(" ")}
          fill="none"
          stroke="#2f604e"
          strokeWidth="4"
        />
        {nodes.map((n, i) => (
          <circle
            key={i}
            cx={20 + n.x * graphWidth}
            cy={baseline - n.y * graphHeight}
            r={i === index ? 7 : 5}
            fill="#f8f7ef"
            stroke="#234f40"
            strokeWidth="2"
            onPointerDown={(e) => drag(e, i)}
            onClick={() => setSelected(i)}
            role="button"
            aria-label={`Select wave point ${i + 1}`}
            tabIndex={0}
            onKeyDown={(e) => {
              if (e.key === "Enter" || e.key === " ") setSelected(i);
            }}
          />
        ))}
        <line
          x1={20 + node.x * graphWidth}
          y1={baseline - node.y * graphHeight}
          x2={20 + (node.x + 0.08) * graphWidth}
          y2={baseline - (node.y + node.slope * 0.08) * graphHeight}
          stroke="#b25b34"
          strokeWidth="2"
        />
        <circle
          cx={20 + (node.x + 0.08) * graphWidth}
          cy={baseline - (node.y + node.slope * 0.08) * graphHeight}
          r="4"
          fill="#b25b34"
          onPointerDown={(e) => drag(e, index, true)}
          aria-label="Drag wave tangent"
        />
        <text x="20" y={baseline + 22} fontSize="11" fill="currentColor">
          {p.wave_width} mm wide × {p.wave_height} mm high
        </text>
      </svg>
      <p className="parameter-help">
        {plan.error ||
          `${plan.repeats} repeats · fitted width ${plan.width?.toFixed(1)} mm · ${plan.layers} layers · fitted merge ${plan.actualMerge?.toFixed(0)}%`}
      </p>
      <div className="node-tabs" role="tablist" aria-label="Wave points">
        {nodes.map((_, i) => (
          <button
            type="button"
            key={i}
            role="tab"
            aria-selected={index === i}
            aria-controls={`${id}-point`}
            onClick={() => setSelected(i)}
          >
            {i + 1}
          </button>
        ))}
        <button
          type="button"
          aria-label="Add wave point"
          disabled={nodes.length === 8}
          onClick={() => {
            const i = nodes.reduce(
              (best, n, i) =>
                (i === nodes.length - 1 ? 1 : nodes[i + 1].x) - n.x >
                (best === nodes.length - 1 ? 1 : nodes[best + 1].x) -
                  nodes[best].x
                  ? i
                  : best,
              0,
            );
            const x =
              Math.round(
                ((nodes[i].x + (i === nodes.length - 1 ? 1 : nodes[i + 1].x)) /
                  2) *
                  100,
              ) / 100;
            const next = [...nodes];
            next.splice(i + 1, 0, {
              x,
              y: Math.max(
                0,
                Math.min(1, Math.round(waveValue(nodes, x) * 100) / 100),
              ),
              slope: 0,
            });
            commit(next);
            setSelected(i + 1);
          }}
        >
          +
        </button>
      </div>
      <div id={`${id}-point`} role="tabpanel" className="cell-fields">
        <label>
          Position X
          <NumberStepper
            label="Wave point X"
            value={node.x}
            min={0}
            max={0.98}
            step={0.01}
            disabled={index === 0}
            onChange={(x) => update({ x })}
          />
        </label>
        <label>
          Height Y
          <NumberStepper
            label="Wave point Y"
            value={node.y}
            min={0}
            max={1}
            step={0.01}
            onChange={(y) => update({ y })}
          />
        </label>
        <label>
          Tangent slope
          <NumberStepper
            label="Wave tangent slope"
            value={node.slope}
            min={-12}
            max={12}
            step={0.1}
            onChange={(slope) => update({ slope })}
          />
        </label>
        <button
          type="button"
          aria-label="Remove wave point"
          disabled={nodes.length <= 3 || index === 0}
          onClick={() => {
            commit(nodes.filter((_, i) => i !== index));
            setSelected(Math.max(0, index - 1));
          }}
        >
          ×
        </button>
      </div>
      {!ordered && (
        <p role="alert">
          Keep points ordered from left to right, at least 0.02 apart.
        </p>
      )}
    </section>
  );
}
