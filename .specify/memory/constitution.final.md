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
