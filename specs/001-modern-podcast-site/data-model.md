# Data Model: Episode

Entity: Episode

Fields
- `title` (string) — required, non-empty
- `date` (date) — required, ISO 8601 or yyyy-MM-dd
- `slug` (string) — required, URL-safe, unique
- `summary` (string) — required, short description for list pages
- `description` (string) — optional, full HTML or Markdown for episode page
- `duration` (string) — optional, human-readable (e.g., "32:15")
- `coverImage` (string) — optional, path to image asset; if absent, use placeholder
- `audioUrl` (string) — optional, mocked URL or placeholder
- `tags` (array of strings) — optional

Validation rules
- `title`, `date`, `slug`, and `summary` must be present for each episode file.
- `slug` must be unique across all episodes; build-time validation should fail or emit a warning for duplicates.
- `date` should parse as a valid date; future dates allowed (tolerated), but flagged if significantly in the future.
- `coverImage` should reference an existing file in `assets/` or be left empty for placeholder handling.

Storage
- Files in `content/episodes/` named `{slug}.md` or `{slug}.json`.
- Prefer frontmatter for Markdown variant (YAML frontmatter) or JSON files for structured data.

State transitions
- N/A — episodes are static content. No runtime state machine required.
