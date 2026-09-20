import { useMemo } from "react";
import type { Parameters } from "../models/types";
import { openArea } from "./lampshade-metrics.mjs";
import { bulbClearance } from "../../../models/lampshade/surface.mjs";
export function LampshadeReadouts({ params: p }: { params: Parameters }) {
  const area = useMemo(() => openArea(p), [p]);
  const bulb = bulbClearance(p);
  return (
    <section className="shade-readouts" aria-label="Shade estimates">
      <div className="shade-readout">
        <span>Open area</span>
        <strong>{area === null ? "—" : `${area.toFixed(1)}%`}</strong>
      </div>
      <div className="shade-readout">
        <span>Bulb clearance</span>
        <strong>
          {bulb.available ? `Ø ${bulb.diameter.toFixed(1)}` : "—"}
          {bulb.available && <em> mm</em>}
        </strong>
      </div>
    </section>
  );
}
