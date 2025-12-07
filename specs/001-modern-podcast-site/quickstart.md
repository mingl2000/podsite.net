# Quickstart: Build & Run (Modern Podcast Website)

Assumptions: .NET 8 SDK installed (or latest LTS). We recommend using Statiq for a .NET-native static site flow; alternatives are a minimal custom pipeline using `Markdig`.

1. Clone repository and checkout the feature branch (already created):

```pwsh
git checkout 001-modern-podcast-site
```

2. Install Statiq CLI (optional, if choosing Statiq):

```pwsh
# Install Statiq.Tool as a dotnet global tool (optional)
dotnet tool install -g Statiq.Tool
```

3. Development preview (Statiq example):

```pwsh
# run preview server (from site/ folder)
# statiq --preview
```

4. Build static site (Statiq example):

```pwsh
# from repo root or site/ depending on setup
statiq build --output site/output
```

5. Alternative minimal pipeline (no Statiq):

- Use a small .NET console tool that reads `content/episodes/*.md`, parses YAML frontmatter (YAML.Net) and converts Markdown to HTML using `Markdig`, then writes static HTML pages into `site/output/` using simple templates.
- That tool can be a single `dotnet` console project under `site/tools` and run with:

```pwsh
cd site/tools
dotnet run --project GenerateStaticSite.csproj
```

6. Verify

- Confirm `site/output` contains `index.html`, `episodes/index.html`, `episodes/{slug}.html`, `about/index.html`, `faq/index.html`.
- Verify pages render content in `view-source` and responsive checks at 320px/768px/1024px.

7. Deploy

- Upload `site/output` contents to your static host (Vercel/Netlify/GitHub Pages) or serve from a CDN.
