# Implementation Plan: Modern Podcast Website

**Branch**: `001-modern-podcast-site` | **Date**: 2025-12-06 | **Spec**: `spec.md`
**Input**: Feature specification from `/specs/001-modern-podcast-site/spec.md`

## Summary

Build a small, responsive static podcast website using the .NET ecosystem. Primary user-facing pages: landing page (one featured episode), `/episodes` (20 mocked episodes), `/about`, and `/faq`. Content is embedded in local files (Markdown/JSON). The site will be produced as static HTML/CSS/JS suitable for deployment to a static host.

Primary technical approach: use .NET tooling (Statiq or lightweight build pipeline) to convert content files into pre-rendered static pages. Use minimal client-side JavaScript and responsive CSS (Tailwind or CSS Modules) to achieve a sleek, mobile-first design.

## Technical Context

**Language/Version**: .NET (C#) — target .NET 8 (or latest LTS)  
**Primary Dependencies**: ASP.NET Core / Statiq (static site generator for .NET) — or a simple custom build script using Markdig for Markdown parsing  
**Storage**: Files (Markdown/JSON/YAML) in `content/episodes` — no database  
**Testing**: Basic smoke tests using PowerShell scripts and optional xUnit for any programmatic generators  
**Target Platform**: Static hosting (Vercel, Netlify, GitHub Pages, or static CDN)  
**Project Type**: Web (static site)  
**Performance Goals**: Fast FCP/LCP; main content visible without JavaScript; keep initial payload minimal  
**Constraints**: No runtime servers or databases required for core content; all content must be pre-rendered at build time  
**Scale/Scope**: Small — 20 episodes (mocked), single-site static output, low build-time footprint

## Constitution Check

GATE: Static-first, mobile-first, and performance priorities stated in the constitution are respected by design (static build, responsive CSS, minimal JS). No violations.

## Project Structure

Selected structure (simple static site layout):

```text
site/
├── content/
│   └── episodes/        # {slug}.md or {slug}.json (20 files)
├── templates/           # page templates (Razor or Statiq templates)
├── assets/              # images, icons, styles
├── styles/              # Tailwind config or CSS modules
├── tools/               # small build scripts (optional)
└── output/              # generated static site (build artifact)
```

**Structure Decision**: Use a single `site/` folder to keep the static generation contained. Templates may be Razor or Statiq templates depending on chosen tool.

## Phase 0 (Research) — Summary

- Decision: Use a .NET-native static site generation approach (Statiq) or a minimal custom pipeline that uses Markdig for Markdown parsing and produces static Razor/HTML files. This keeps the project within the .NET ecosystem as requested and simplifies deployment.
- Rationale: Matches user's ASP.NET Core preference while providing an established SSG workflow in .NET (Statiq) and keeps build-time output static.
- Alternatives considered: Next.js/Hugo/Jekyll — rejected because user requested .NET/ASP.NET Core environment.

## Phase 1 (Design) — Deliverables

- `research.md` — consolidated research and decisions (Phase 0 output)
- `data-model.md` — episode entity model and validation rules
- `contracts/episode.schema.json` — JSON Schema for episode content files
- `quickstart.md` — how to run dev/build locally and produce output

## Phase 2 (Implementation) — Next steps (not executed here)

- Create the `site/` skeleton and add `content/episodes` with 20 mocked files
- Implement templates and styles for landing, episodes list, episode page, about, faq
- Add minimal audio player placeholder markup (no streaming required)
- Run `dotnet` or Statiq build and verify output in `site/output`
- Add smoke tests and visual checks (responsive snapshots)

## Complexity Tracking

No constitution violations detected; complexity remains low due to static-only scope. Use of Statiq (or similar) is an incremental addition but justified to keep the stack .NET-native and avoid mixing JS frameworks.
# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

[Extract from feature spec: primary requirement + technical approach from research]

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: [e.g., Python 3.11, Swift 5.9, Rust 1.75 or NEEDS CLARIFICATION]  
**Primary Dependencies**: [e.g., FastAPI, UIKit, LLVM or NEEDS CLARIFICATION]  
**Storage**: [if applicable, e.g., PostgreSQL, CoreData, files or N/A]  
**Testing**: [e.g., pytest, XCTest, cargo test or NEEDS CLARIFICATION]  
**Target Platform**: [e.g., Linux server, iOS 15+, WASM or NEEDS CLARIFICATION]
**Project Type**: [single/web/mobile - determines source structure]  
**Performance Goals**: [domain-specific, e.g., 1000 req/s, 10k lines/sec, 60 fps or NEEDS CLARIFICATION]  
**Constraints**: [domain-specific, e.g., <200ms p95, <100MB memory, offline-capable or NEEDS CLARIFICATION]  
**Scale/Scope**: [domain-specific, e.g., 10k users, 1M LOC, 50 screens or NEEDS CLARIFICATION]

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

[Gates determined based on constitution file]

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)
<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
# [REMOVE IF UNUSED] Option 1: Single project (DEFAULT)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
