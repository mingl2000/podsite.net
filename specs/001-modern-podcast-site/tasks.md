# Tasks: Modern Podcast Website

**Input**: `spec.md`, `plan.md`, `data-model.md`, `contracts/episode.schema.json`  
**Feature**: `001-modern-podcast-site`  

## Phase 1: Setup (project skeleton and tooling)

- [x] T001 [P] Create site skeleton directory `site/` with subfolders `content/`, `templates/`, `assets/`, `styles/`, `tools/`, and `output/` (path: `site/`)
- [x] T002 [P] Initialize a .NET console project for a minimal static generator at `site/tools/GenerateStaticSite.csproj` (path: `site/tools/GenerateStaticSite.csproj`)
- [x] T003 [P] Add a basic build script `site/tools/build.ps1` that runs the static generator and writes to `site/output/` (path: `site/tools/build.ps1`)
- [x] T004 [P] Add `site/styles/tailwind.config.js` or minimal CSS file `site/styles/main.css` and include a placeholder stylesheet (path: `site/styles/main.css`)
- [x] T005 [P] Add placeholder site metadata file `site/content/site.json` containing `siteTitle`, `siteDescription`, `featuredEpisodeSlug` (path: `site/content/site.json`)
- [x] T006 [P] Add basic CI build workflow for static output (optional) at `.github/workflows/build-static.yml` that runs `site/tools/build.ps1` (path: `.github/workflows/build-static.yml`)

---

## Phase 2: Foundational (blocking prerequisites)

- [x] T007 Create `site/templates/episode.html` template for individual episode pages (path: `site/templates/episode.html`)
- [x] T008 Create `site/templates/episodes-list.html` template for the episodes index page (path: `site/templates/episodes-list.html`)
- [x] T009 Create `site/templates/landing.html` template for the landing page and featured block (path: `site/templates/landing.html`)
- [x] T010 Create `site/templates/simple-page.html` template for static pages (about, faq) (path: `site/templates/simple-page.html`)
- [ ] T011 Add default placeholder image `assets/cover-placeholder.jpg` and favicon (path: `site/assets/cover-placeholder.jpg`)
- [x] T012 Create content folder `site/content/episodes/` and add generator script placeholder `site/tools/generate-mock-episodes.ps1` to generate 20 mocked episode files (path: `site/content/episodes/`, `site/tools/generate-mock-episodes.ps1`)
- [x] T013 Add site pages for About and FAQ as content files `site/content/about.md` and `site/content/faq.md` (path: `site/content/about.md`, `site/content/faq.md`)
- [ ] T014 Add JSON Schema copy `specs/001-modern-podcast-site/contracts/episode.schema.json` to `site/tools/episode.schema.json` for local validation during build (path: `site/tools/episode.schema.json`)
- [x] T015 Implement simple slug-uniqueness check in generator script to fail or warn if duplicate slugs are detected (path: `site/tools/generate-mock-episodes.ps1`)

---

## Phase 3: User Story 1 - Landing Page with Featured Episode (Priority: P1) 🎯 MVP

**Goal**: Landing page `/` with one featured episode block (title, date, short description, cover image, play/link).  
**Independent Test**: After build, open `site/output/index.html` and verify featured block present and links to episode page.

- [x] T016 [US1] Create featured episode metadata in `site/content/site.json` or `site/content/featured.json` and set `featuredEpisodeSlug` (path: `site/content/site.json`)
- [x] T017 [US1] Implement landing page generation logic in the static generator to populate `site/output/index.html` from `site/templates/landing.html` and `site/content/site.json` (path: `site/tools/GenerateStaticSite.csproj` / generation code)
- [x] T018 [US1] Add CSS for featured block in `site/styles/main.css` and ensure responsive layout (path: `site/styles/main.css`)
- [x] T019 [US1] Add a placeholder play button element in the landing template linking to the episode page (path: `site/templates/landing.html`)

---

## Phase 4: User Story 2 - Episodes List & Episode Page (Priority: P1)

**Goal**: `/episodes` lists 20 episodes; each `/episodes/{slug}` is generated and contains full metadata and an audio player placeholder.  
**Independent Test**: After build, confirm `site/output/episodes/index.html` lists 20 items and `site/output/episodes/{slug}.html` exists for sample slugs.

- [x] T020 [US2] Generate 20 mocked episode content files in `site/content/episodes/` named `{slug}.md` or `{slug}.json` (path: `site/content/episodes/`)
- [x] T021 [P] [US2] Implement episodes list generation in static generator to produce `site/output/episodes/index.html` using `site/templates/episodes-list.html` (path: `site/tools/GenerateStaticSite.csproj` / generation code)
- [x] T022 [P] [US2] Implement per-episode page generation to produce `site/output/episodes/{slug}.html` using `site/templates/episode.html` (path: `site/tools/GenerateStaticSite.csproj` / generation code)
- [x] T023 [US2] Add audio player placeholder markup to `site/templates/episode.html` (path: `site/templates/episode.html`)
- [x] T024 [US2] Ensure episode content files include required metadata fields: `title`, `date`, `slug`, `summary` (path: `site/content/episodes/*.md`)

---

## Phase 5: User Story 3 - About Page (Priority: P2)

**Goal**: `/about` page with show description and host bio.  
**Independent Test**: After build, open `site/output/about/index.html` and verify content is present.

- [x] T025 [US3] Add `site/content/about.md` (content) if not already added in foundational tasks (path: `site/content/about.md`)
- [x] T026 [US3] Wire up generator to produce `site/output/about/index.html` using `site/templates/simple-page.html` (path: `site/tools/GenerateStaticSite.csproj` / generation code)
- [x] T027 [US3] Add responsive styling for about page to `site/styles/main.css` (path: `site/styles/main.css`)

---

## Phase 6: User Story 4 - FAQ Page (Priority: P2)

**Goal**: `/faq` page with at least 5 Q/A items.  
**Independent Test**: After build, open `site/output/faq/index.html` and verify at least 5 FAQ items.

- [x] T028 [US4] Add `site/content/faq.md` with 5+ Q/A entries (path: `site/content/faq.md`)
- [x] T029 [US4] Wire up generator to produce `site/output/faq/index.html` using `site/templates/simple-page.html` (path: `site/tools/GenerateStaticSite.csproj` / generation code)

---

## Phase 7: Polish & Cross-Cutting Concerns

- [ ] T030 [P] Add smoke test script `scripts/smoke-check.ps1` that runs `site/tools/build.ps1` and verifies output files exist for `/`, `/episodes`, one episode page, `/about`, `/faq` (path: `scripts/smoke-check.ps1`)
- [ ] T031 [P] Validate episode files against `specs/001-modern-podcast-site/contracts/episode.schema.json` during build and fail/warn on violations (path: `site/tools/episode.schema.json` / validation code)
- [ ] T032 [P] Ensure images have `alt` attributes or default alt text during generation (path: `site/tools/GenerateStaticSite.csproj` / generation code)
- [ ] T033 [P] Optimize assets: add image optimization step or note to use small placeholder images in `site/assets/` (path: `site/assets/`)
- [ ] T034 [P] Update `specs/001-modern-podcast-site/tasks.md` with any new tasks discovered during implementation (path: `specs/001-modern-podcast-site/tasks.md`)

---

## Dependencies & Execution Order

- `Phase 1 (Setup)` tasks can begin immediately and most are parallelizable.
- `Phase 2 (Foundational)` blocks user story implementation and must complete before Phases 3-6.
- User stories (Phases 3-6) are independent after foundational phase and can be implemented in parallel where marked `[P]`.
- Polish tasks (Phase 7) may run in parallel after enough pages are generated.

## Parallel Execution Examples

- While `T007`-`T011` (templates & placeholder assets) are being created, another team member can implement `T020` (generate mock episodes) and `T021` (episodes list generation) in parallel.

## MVP Recommendation

- Implement Phase 1 + Phase 2, then deliver US1 (landing + featured episode) and US2 (episodes list + pages) as the MVP. US3 and US4 can follow in the next iteration.
