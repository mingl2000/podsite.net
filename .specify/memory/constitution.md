# PodSite Constitution

## Core Principles

### I. Static-First Architecture
All content must be statically generated or pre-rendered. Content pages and routes are produced at build time (SSG). No runtime database connections or server-side rendering for content pages.

### II. Mobile-First Responsive Design
All UI is mobile-first and responsive. Minimum supported viewport width: 320px. Core layouts must be verified at common breakpoints (mobile, tablet, desktop).

### III. Performance Priority
Prefer minimal client JavaScript; prioritize FCP and LCP. Avoid heavy runtime frameworks or unnecessary third-party scripts. Assets should be optimized and cached.

### IV. Content-Embedded Data
All site data (episodes, metadata, navigation) is stored in content files (Markdown, JSON, or YAML) co-located with the site source. No runtime data stores required.

### V. Build Verification
Every change that affects content or routes must produce a successful static build (`npm run build`) and generate the expected static output.

## Technology Stack (Minimum)

- **Framework**: Next.js configured for Static Site Generation (SSG) or equivalent static-site framework
- **Styling**: CSS Modules, Tailwind, or minimal CSS-in-JS (responsive-first)
- **Content**: Markdown (.md/.mdx) or JSON/YAML files in a content directory
- **Build Output**: Static HTML, CSS, JS artifacts only
- **Hosting**: Must be deployable to static hosts (Vercel, Netlify, GitHub Pages, CDN)

## Development Workflow (Minimum)

- Local development: `npm run dev` (hot-reload for content)
- Pre-merge checks: `npm run build` must succeed and produce expected routes
- Accessibility: basic checks for headings, alt text on images, and keyboard navigation for key pages
- Testing: smoke tests for critical pages (home, episodes list, episode page)

## Governance

This constitution is the source of truth for project constraints. Changes to these requirements require a documented amendment and agreement from maintainers.

**Version**: 1.0.0 | **Ratified**: 2025-12-06 | **Last Amended**: 2025-12-06
# PodSite Constitution

## Core Principles

### I. Static-First Architecture
All content must be statically generated or pre-rendered. Content pages and routes are produced at build time (SSG). No runtime database connections or server-side rendering for content pages.

### II. Mobile-First Responsive Design
All UI is mobile-first and responsive. Minimum supported viewport width: 320px. Core layouts must be verified at common breakpoints (mobile, tablet, desktop).

### III. Performance Priority
Prefer minimal client JavaScript; prioritize FCP and LCP. Avoid heavy runtime frameworks or unnecessary third-party scripts. Assets should be optimized and cached.

### IV. Content-Embedded Data
All site data (episodes, metadata, navigation) is stored in content files (Markdown, JSON, or YAML) co-located with the site source. No runtime data stores required.

### V. Build Verification
Every change that affects content or routes must produce a successful static build (`npm run build`) and generate the expected static output.

## Technology Stack (Minimum)

- **Framework**: Next.js configured for Static Site Generation (SSG) or equivalent static-site framework
- **Styling**: CSS Modules, Tailwind, or minimal CSS-in-JS (responsive-first)
- **Content**: Markdown (.md/.mdx) or JSON/YAML files in a content directory
- **Build Output**: Static HTML, CSS, JS artifacts only
- **Hosting**: Must be deployable to static hosts (Vercel, Netlify, GitHub Pages, CDN)

## Development Workflow (Minimum)

- Local development: `npm run dev` (hot-reload for content)
- Pre-merge checks: `npm run build` must succeed and produce expected routes
- Accessibility: basic checks for headings, alt text on images, and keyboard navigation for key pages
- Testing: smoke tests for critical pages (home, episodes list, episode page)

## Governance

This constitution is the source of truth for project constraints. Changes to these requirements require a documented amendment and agreement from maintainers.

**Version**: 1.0.0 | **Ratified**: 2025-12-06 | **Last Amended**: 2025-12-06
# [PROJECT_NAME] Constitution
<!-- Example: Spec Constitution, TaskFlow Constitution, etc. -->

## Core Principles

### [PRINCIPLE_1_NAME]
<!-- Example: I. Library-First -->
[PRINCIPLE_1_DESCRIPTION]
<!-- Example: Every feature starts as a standalone library; Libraries must be self-contained, independently testable, documented; Clear purpose required - no organizational-only libraries -->

### [PRINCIPLE_2_NAME]
<!-- Example: II. CLI Interface -->
[PRINCIPLE_2_DESCRIPTION]
<!-- Example: Every library exposes functionality via CLI; Text in/out protocol: stdin/args → stdout, errors → stderr; Support JSON + human-readable formats -->

### [PRINCIPLE_3_NAME]
<!-- Example: III. Test-First (NON-NEGOTIABLE) -->
[PRINCIPLE_3_DESCRIPTION]
<!-- Example: TDD mandatory: Tests written → User approved → Tests fail → Then implement; Red-Green-Refactor cycle strictly enforced -->

### [PRINCIPLE_4_NAME]
<!-- Example: IV. Integration Testing -->
[PRINCIPLE_4_DESCRIPTION]
<!-- Example: Focus areas requiring integration tests: New library contract tests, Contract changes, Inter-service communication, Shared schemas -->

### [PRINCIPLE_5_NAME]
<!-- Example: V. Observability, VI. Versioning & Breaking Changes, VII. Simplicity -->
[PRINCIPLE_5_DESCRIPTION]
<!-- Example: Text I/O ensures debuggability; Structured logging required; Or: MAJOR.MINOR.BUILD format; Or: Start simple, YAGNI principles -->

## [SECTION_2_NAME]
<!-- Example: Additional Constraints, Security Requirements, Performance Standards, etc. -->

[SECTION_2_CONTENT]
<!-- Example: Technology stack requirements, compliance standards, deployment policies, etc. -->

## [SECTION_3_NAME]
<!-- Example: Development Workflow, Review Process, Quality Gates, etc. -->

[SECTION_3_CONTENT]
<!-- Example: Code review requirements, testing gates, deployment approval process, etc. -->

## Governance
<!-- Example: Constitution supersedes all other practices; Amendments require documentation, approval, migration plan -->

```markdown
# PodSite Constitution

## Core Principles

### I. Static-First Architecture
All content must be statically generated or pre-rendered. Content pages and routes are produced at build time (SSG). No runtime database connections or server-side rendering for content pages.

### II. Mobile-First Responsive Design
All UI is mobile-first and responsive. Minimum supported viewport width: 320px. Core layouts must be verified at common breakpoints (mobile, tablet, desktop).

### III. Performance Priority
Prefer minimal client JavaScript; prioritize FCP and LCP. Avoid heavy runtime frameworks or unnecessary third-party scripts. Assets should be optimized and cached.

### IV. Content-Embedded Data
All site data (episodes, metadata, navigation) is stored in content files (Markdown, JSON, or YAML) co-located with the site source. No runtime data stores required.

### V. Build Verification
Every change that affects content or routes must produce a successful static build (`npm run build`) and generate the expected static output.

## Technology Stack (Minimum)

- **Framework**: Next.js configured for Static Site Generation (SSG) or equivalent static-site framework
- **Styling**: CSS Modules, Tailwind, or minimal CSS-in-JS (responsive-first)
- **Content**: Markdown (.md/.mdx) or JSON/YAML files in a content directory
- **Build Output**: Static HTML, CSS, JS artifacts only
- **Hosting**: Must be deployable to static hosts (Vercel, Netlify, GitHub Pages, CDN)

## Development Workflow (Minimum)

- Local development: `npm run dev` (hot-reload for content)
- Pre-merge checks: `npm run build` must succeed and produce expected routes
- Accessibility: basic checks for headings, alt text on images, and keyboard navigation for key pages
- Testing: smoke tests for critical pages (home, episodes list, episode page)

## Governance

This constitution is the source of truth for project constraints. Changes to these requirements require a documented amendment and agreement from maintainers.

**Version**: 1.0.0 | **Ratified**: 2025-12-06 | **Last Amended**: 2025-12-06

```
