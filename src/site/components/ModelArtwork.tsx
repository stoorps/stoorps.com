/** Lightweight plan illustration; the interactive model loads only on its own page. */
export function ModelArtwork() {
  return (
    <svg
      className="model-artwork"
      viewBox="0 0 560 370"
      role="img"
      aria-label="BILRESA cover with a remote pocket on each side and a central removable door"
    >
      <defs>
        <filter id="model-shadow" x="-30%" y="-30%" width="160%" height="180%">
          <feDropShadow
            dx="0"
            dy="18"
            stdDeviation="14"
            floodColor="#26483c"
            floodOpacity=".17"
          />
        </filter>
        <linearGradient id="model-face" x2="0" y2="1">
          <stop stopColor="#6aa08d" />
          <stop offset="1" stopColor="#4b816f" />
        </linearGradient>
      </defs>
      <g
        transform="translate(280 185) rotate(-12) skewX(9) translate(-230 -114)"
        filter="url(#model-shadow)"
      >
        <rect x="0" y="9" width="460" height="228" rx="48" fill="#386c59" />
        <rect width="460" height="228" rx="48" fill="url(#model-face)" />
        <rect x="13" y="18" width="102" height="184" rx="50" fill="#305e4e" />
        <rect x="18" y="32" width="92" height="169" rx="45" fill="#649784" />
        <rect x="345" y="18" width="102" height="184" rx="50" fill="#305e4e" />
        <rect x="350" y="32" width="92" height="169" rx="45" fill="#649784" />
        <rect x="43" y="62" width="42" height="116" rx="20" fill="#5a8d7a" />
        <rect x="375" y="62" width="42" height="116" rx="20" fill="#5a8d7a" />
        <path d="M122 20H338V207H122Z" fill="#315e4c" />
        <path d="M122 22H338V195H122Z" fill="#ece7cc" />
        <path d="M122 195H338V202H122Z" fill="#cbc6ad" />
        <path d="M122 10H338V23H122Z" fill="#b48d60" />
      </g>
    </svg>
  );
}
