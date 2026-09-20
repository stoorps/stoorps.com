import { useId, useState, type ReactNode } from "react";

export function SettingsTabs({
  groups,
  children,
}: {
  groups: string[];
  children: (group: string) => ReactNode;
}) {
  const id = useId();
  const [selected, setSelected] = useState(groups[0]);
  const active = groups.includes(selected) ? selected : groups[0];
  return (
    <div className="settings-tabs">
      <div
        className="settings-tab-list"
        role="tablist"
        aria-label="Settings sections"
      >
        {groups.map((group, index) => (
          <button
            key={group}
            id={`${id}-tab-${index}`}
            type="button"
            role="tab"
            aria-selected={active === group}
            aria-controls={`${id}-panel-${index}`}
            tabIndex={active === group ? 0 : -1}
            onClick={() => setSelected(group)}
            onKeyDown={(event) => {
              let next = index;
              if (event.key === "ArrowRight")
                next = (index + 1) % groups.length;
              else if (event.key === "ArrowLeft")
                next = (index - 1 + groups.length) % groups.length;
              else if (event.key === "Home") next = 0;
              else if (event.key === "End") next = groups.length - 1;
              else return;
              event.preventDefault();
              setSelected(groups[next]);
              document.getElementById(`${id}-tab-${next}`)?.focus();
            }}
          >
            {group}
          </button>
        ))}
      </div>
      {groups.map((group, index) => (
        <div
          key={group}
          role="tabpanel"
          id={`${id}-panel-${index}`}
          aria-labelledby={`${id}-tab-${index}`}
          hidden={active !== group}
          tabIndex={0}
        >
          {children(group)}
        </div>
      ))}
    </div>
  );
}
