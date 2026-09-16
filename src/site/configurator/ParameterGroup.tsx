import { flushSync } from "react-dom";
import { useEffect, useRef, useState, type ReactNode } from "react";

export function ParameterGroup({
  name,
  children,
  defaultOpen = name.toLowerCase() === "layout",
}: {
  name: string;
  children: ReactNode;
  defaultOpen?: boolean;
}) {
  const [open, setOpen] = useState(defaultOpen);
  const details = useRef<HTMLDetailsElement>(null);
  const motion = useRef<Animation | null>(null);
  const desired = useRef(defaultOpen);
  useEffect(
    () => () => {
      motion.current?.cancel();
    },
    [],
  );
  function toggle() {
    const element = details.current!;
    const summary = element.querySelector("summary")!;
    const content = element.querySelector<HTMLElement>(
      ".parameter-group-content",
    )!;
    const from = element.getBoundingClientRect().height;
    motion.current?.cancel();
    desired.current = !desired.current;
    if (matchMedia("(prefers-reduced-motion: reduce)").matches) {
      setOpen(desired.current);
      return;
    }
    flushSync(() => setOpen(true));
    const to = desired.current
      ? element.getBoundingClientRect().height
      : summary.getBoundingClientRect().height + 1;
    const animation = element.animate(
      [{ height: `${from}px` }, { height: `${to}px` }],
      {
        duration: 240,
        easing: "cubic-bezier(.22,1,.36,1)",
      },
    );
    content.getAnimations().forEach((a) => a.cancel());
    content.animate(
      desired.current
        ? [{ opacity: 0 }, { opacity: 1 }]
        : [{ opacity: 1 }, { opacity: 0 }],
      {
        duration: desired.current ? 180 : 100,
        delay: desired.current ? 80 : 0,
        fill: "both",
      },
    );
    motion.current = animation;
    animation.onfinish = () => {
      setOpen(desired.current);
      content.getAnimations().forEach((a) => a.cancel());
      motion.current = null;
    };
  }
  return (
    <details
      ref={details}
      className="parameter-group parameter-expander"
      open={open}
    >
      <summary
        onClick={(event) => {
          event.preventDefault();
          toggle();
        }}
      >
        {name}
      </summary>
      <div className="parameter-group-content">{children}</div>
    </details>
  );
}
