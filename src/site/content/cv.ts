/** Transcribed from Sam Storer's supplied CV. Skill levels approximate its unlabelled bars. */
export const roles = [
  { company: "Sainsbury’s", role: "Software Engineer (C4)", dates: "March 2025 — present", theme: "Making store software work harder.", tags: ["Next.js", ".NET", "MongoDB", "AWS", "Docker"], points: [
    "Led the end-to-end rewrite of Stock App, used by thousands of in-store colleagues for stock information and operational workflows.",
    "Architected and solely delivered the infrastructure for rapid experimentation and targeted feature rollouts, with delivery through GitHub Actions.",
    "Led RFID discovery for clothing stock management across stores and storage environments.",
    "Contributed to an AI innovation initiative evaluating specification-driven development while delivering a production stock-management application, and helped define internal guidance for responsible AI-assisted development.",
  ] },
  { company: "Simworx", role: "Senior Software Engineer", dates: "March 2024 — 2025", theme: "From industrial control to the ride experience.", tags: ["Rust", "C#", "gRPC", "egui", "Avalonia"], points: [
    "Re-engineered the Beckhoff ADS client in Rust for a safer, more maintainable foundation for industrial control integrations.",
    "Designed and implemented a Beckhoff ADS gRPC service in C#, with C# and Rust clients.",
    "Designed the UI and UX for a Rust ride-motion visualiser and editor using egui.",
    "Refactored a motion-control studio from WPF to Avalonia.",
  ] },
  { company: "Hayley SmartVend", role: "Senior Software Engineer", dates: "January 2023 — February 2024", theme: "A complete platform, down to the machine.", tags: ["Avalonia", "ASP.NET Core", "Linux", "STM32", "C++"], points: [
    "Led a new cross-platform vending-machine application in Avalonia and a gRPC POS configuration and management platform for Windows and Linux.",
    "Created a custom Fedora operating-system image with an integrated management client and automatic device registration.",
    "Maintained the legacy UWP kiosk application and built Azure DevOps pipelines for staging and development releases.",
    "Added PID motor control to STM32 C++ firmware and contributed to the ASP.NET MVC management portal and API.",
  ] },
  { company: "Breathe Technologies", role: "Senior Software Engineer", dates: "January 2022 — January 2023", theme: "Joining up the moving parts of a warehouse.", tags: ["ASP.NET", "Xamarin.Forms", "WPF", "PLCs", "IBM MQ"], points: [
    "Developed a containerised, YAML-configurable ETL engine and an mDNS-inspired discovery system integrated with Ocelot, including WAN and remote extensions.",
    "Built bespoke warehouse-automation solutions for customers including Technicolor, DHL and UPGS.",
    "Integrated PLCs, Raspberry Pis and Arduinos running custom firmware with IBM MQ and third-party APIs.",
    "Mentored apprentice developers.",
  ] },
  { company: "CATS³", role: "Software Lead", dates: "August 2012 — December 2022", theme: "From apprentice to software lead.", tags: ["C#", "WPF / MVVM", ".NET Core", "gRPC", "C++"], points: [
    "Joined as an apprentice in 2012 and became Software Lead in 2019, managing a small engineering team, its workload, documentation and product releases.",
    "Architected a test-creation suite for materials, automotive and aerospace testing. Modernised a WinForms platform spanning more than 170 projects with WPF, MVVM, automated testing and dependency injection.",
    "Migrated from .NET Framework 4 to .NET Core and researched cross-platform UI frameworks. Owned CI, release pipelines, TFS and Git repositories.",
    "Contributed to Cubic Script for custom Control Cube hardware and re-engineered C++ networking libraries in C# using gRPC.",
    "Delivered bespoke testing software for customers including McLaren F1, Williams F1, Airbus, AgustaWestland, Toyota and Ford Australia; supported and commissioned systems with customers in the UK and Germany.",
  ] },
];
export const skillGroups = {
  Languages: [["C#",90],["Rust",85],["C / C++",60],["Python",52],["TypeScript / JavaScript / React",75],["Flutter / Dart",68]],
  ".NET": [[".NET 8",90],[".NET Framework",90],["Avalonia",90],["WPF",90],["UWP",76],["WinForms",90],["Xamarin",66],["MAUI",59],["ASP.NET Core",67],["Blazor",67],["WCF",66],["gRPC",81]],
  "Tools & systems": [["Docker",91],["CI/CD",90],["Git",90],["Azure & AWS",67],["Jira",90],["MongoDB",60],["STM32 / C",52],["Arduino",67],["Linux & Bash",90],["Figma / XD",90],["T-SQL",39],["AI specification-driven development",90],["Infrastructure as code / Terraform",57]],
} satisfies Record<string, [string, number][]>;
export const projects = [
  { name: "COSMIC Storage", category: "Open source / Linux", text: "A free, open-source disk utility for the COSMIC desktop.", href: "https://github.com/cosmic-utils/cosmic-ext-storage" },
  { name: "AI Orchestrator", category: "Open source / Tools", text: "An open-source tool in development for running and managing multiple AI agents and workspaces concurrently." },
  { name: "Beer Board", category: "Web / In the real world", text: "A website and live beer-board system designed and delivered for a busy Birmingham city-centre pub." },
  { name: "AR Art Gallery", category: "Mobile / Augmented reality", text: "A Flutter application hosting a Unity AR experience for art galleries and museums." },
  { name: "MIDI Controller", category: "Hardware / Music", text: "An Arduino-powered MIDI control surface for music production, with a custom-designed and printed enclosure." },
  { name: "3D modelling & printing", category: "Design / Making", text: "Functional models and components designed in Onshape and Fusion 360, including the configurable designs on this site.", href: "/" },
];
