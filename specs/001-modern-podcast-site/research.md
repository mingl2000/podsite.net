# Research: Decisions & Alternatives

Decision: Use the .NET ecosystem for static site generation (primary choice: Statiq or lightweight Markdig-based pipeline)  
Rationale: The user specified ASP.NET Core/.NET. Statiq is a mature .NET static-site generator that fits the requirement to produce static HTML from Markdown and templates while staying in the .NET ecosystem. If maintainers prefer minimal dependencies, a small build script that uses `Markdig` to convert Markdown files into pre-rendered Razor/HTML is also viable.

Alternatives considered:
- Next.js (React): Excellent SSG features, but not in the requested .NET ecosystem. Rejected based on user preference.
- Hugo/Jekyll: Fast static-site generators; would add a different toolchain (Go/Ruby). Rejected to keep the stack .NET-first.
- Blazor WebAssembly (pre-rendered): Could achieve .NET-first client app, but would increase JS payload and complexity; not ideal for static-minimal approach.

Dependency decisions (best guesses):
- Use .NET 8 SDK (or latest LTS) as the runtime and tooling baseline.
- For Markdown parsing: `Markdig` (if custom script) or Statiq's pipeline (if using Statiq).
- Styling: Tailwind CSS for fast, responsive design, or plain CSS Modules if minimal tooling is desired.

Deployment targets:
- Artifacts should be compatible with Vercel, Netlify, GitHub Pages, or any CDN that serves static HTML.

Security & privacy:
- No external network calls at build-time for required content. Mock audio URLs acceptable. No PII storage.

Conclusion: Proceed with a .NET-native static-generation approach (Statiq preferred) unless maintainers request an alternative.
