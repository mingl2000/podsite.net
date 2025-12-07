using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    private static readonly string SITE_ROOT = Path.GetFullPath(Path.Combine(
        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? ".", 
        "..", "..", "..", "..")); // bin/Release/net8.0 -> Release -> bin -> tools -> site
    private static readonly string CONTENT_DIR = Path.Combine(SITE_ROOT, "content");
    private static readonly string EPISODES_DIR = Path.Combine(CONTENT_DIR, "episodes");
    private static readonly string TEMPLATES_DIR = Path.Combine(SITE_ROOT, "templates");
    private static readonly string OUTPUT_DIR = Path.Combine(SITE_ROOT, "output");

    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("🚀 Starting static site generation...");
            Console.WriteLine($"   Site Root: {SITE_ROOT}");
            
            // Ensure output directory exists
            if (Directory.Exists(OUTPUT_DIR))
                Directory.Delete(OUTPUT_DIR, true);
            Directory.CreateDirectory(OUTPUT_DIR);
            
            // Load site metadata
            var siteJson = File.ReadAllText(Path.Combine(CONTENT_DIR, "site.json"));
            var siteMetadata = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(siteJson);
            
            // Load templates
            var baseTemplate = File.ReadAllText(Path.Combine(TEMPLATES_DIR, "base.html"));
            var featuredBlockTemplate = File.ReadAllText(Path.Combine(TEMPLATES_DIR, "featured-block.html"));
            var episodeTemplate = File.ReadAllText(Path.Combine(TEMPLATES_DIR, "episode.html"));
            var episodesListTemplate = File.ReadAllText(Path.Combine(TEMPLATES_DIR, "episodes-list.html"));
            var episodeCardTemplate = File.ReadAllText(Path.Combine(TEMPLATES_DIR, "episode-card.html"));
            var simplePageTemplate = File.ReadAllText(Path.Combine(TEMPLATES_DIR, "simple-page.html"));
            
            // Load all episodes
            var episodes = LoadEpisodes();
            Console.WriteLine($"✓ Loaded {episodes.Count} episodes");
            
            // Generate landing page
            await GenerateLandingPage(siteMetadata, episodes, baseTemplate, featuredBlockTemplate);
            Console.WriteLine("✓ Generated landing page (index.html)");
            
            // Generate episodes directory and pages
            Directory.CreateDirectory(Path.Combine(OUTPUT_DIR, "episodes"));
            
            // Generate episodes list page
            await GenerateEpisodesListPage(episodes, baseTemplate, episodesListTemplate, episodeCardTemplate);
            Console.WriteLine("✓ Generated episodes list page");
            
            // Generate individual episode pages
            foreach (var episode in episodes)
            {
                await GenerateEpisodePage(episode, baseTemplate, episodeTemplate);
            }
            Console.WriteLine($"✓ Generated {episodes.Count} individual episode pages");
            
            // Generate about page
            var aboutContent = File.ReadAllText(Path.Combine(CONTENT_DIR, "about.md"));
            var aboutHtml = MarkdownToHtml(aboutContent);
            var aboutPage = baseTemplate
                .Replace("{TITLE}", "About")
                .Replace("{CONTENT}", $"<div class=\"simple-page\">\n{aboutHtml}\n</div>");
            File.WriteAllText(Path.Combine(OUTPUT_DIR, "about.html"), aboutPage);
            Console.WriteLine("✓ Generated about page");
            
            // Generate FAQ page
            var faqContent = File.ReadAllText(Path.Combine(CONTENT_DIR, "faq.md"));
            var faqHtml = MarkdownToHtml(faqContent);
            var faqPage = baseTemplate
                .Replace("{TITLE}", "FAQ")
                .Replace("{CONTENT}", $"<div class=\"simple-page\">\n{faqHtml}\n</div>");
            File.WriteAllText(Path.Combine(OUTPUT_DIR, "faq.html"), faqPage);
            Console.WriteLine("✓ Generated FAQ page");
            
            // Copy static assets
            CopyDirectory(Path.Combine(SITE_ROOT, "styles"), Path.Combine(OUTPUT_DIR, "styles"));
            Console.WriteLine("✓ Copied styles directory");
            
            Console.WriteLine("\n✅ Static site generation complete!");
            Console.WriteLine($"📁 Output directory: {OUTPUT_DIR}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ Error during generation: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Environment.Exit(1);
        }
    }

    static List<EpisodeMetadata> LoadEpisodes()
    {
        var episodes = new List<EpisodeMetadata>();
        var slugs = new HashSet<string>();

        var episodeFiles = Directory.GetFiles(EPISODES_DIR, "*.md")
            .OrderByDescending(f => Path.GetFileName(f))
            .ToList();

        foreach (var file in episodeFiles)
        {
            try
            {
                var content = File.ReadAllText(file);
                var metadata = ParseMarkdownMetadata(content);
                
                if (metadata == null) continue;
                
                // Validate slug uniqueness
                if (slugs.Contains(metadata.Slug))
                {
                    Console.WriteLine($"⚠️  Warning: Duplicate slug '{metadata.Slug}' in {Path.GetFileName(file)}");
                    continue;
                }
                
                slugs.Add(metadata.Slug);
                metadata.Body = content.Substring(content.IndexOf("---", 3) + 3).Trim();
                episodes.Add(metadata);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error loading episode {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        return episodes;
    }

    static EpisodeMetadata ParseMarkdownMetadata(string content)
    {
        var match = Regex.Match(content, @"^---\s*\n([\s\S]*?)\n---", RegexOptions.Multiline);
        if (!match.Success) return null;

        var frontmatter = match.Groups[1].Value;
        var metadata = new EpisodeMetadata();
        
        var titleMatch = Regex.Match(frontmatter, @"^title:\s*[""']?(.+?)[""']?\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        if (titleMatch.Success) metadata.Title = titleMatch.Groups[1].Value.Trim('"', '\'');

        var slugMatch = Regex.Match(frontmatter, @"^slug:\s*(.+?)\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        if (slugMatch.Success) metadata.Slug = slugMatch.Groups[1].Value.Trim();

        var summaryMatch = Regex.Match(frontmatter, @"^summary:\s*[""']?(.+?)[""']?\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        if (summaryMatch.Success) metadata.Summary = summaryMatch.Groups[1].Value.Trim('"', '\'');

        var dateMatch = Regex.Match(frontmatter, @"^date:\s*(\d{4}-\d{2}-\d{2})", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        if (dateMatch.Success && DateTime.TryParse(dateMatch.Groups[1].Value, out var date))
            metadata.Date = date;

        var durationMatch = Regex.Match(frontmatter, @"^duration:\s*[""']?(.+?)[""']?\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        if (durationMatch.Success) metadata.Duration = durationMatch.Groups[1].Value.Trim('"', '\'');

        var tagsMatch = Regex.Match(frontmatter, @"^tags:\s*\[(.*?)\]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        if (tagsMatch.Success)
        {
            var tagString = tagsMatch.Groups[1].Value;
            metadata.Tags = Regex.Matches(tagString, @"[""']?([^,""'\[\]]+)[""']?")
                .Cast<Match>()
                .Select(m => m.Groups[1].Value.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();
        }

        return metadata;
    }

    static string MarkdownToHtml(string markdown)
    {
        // Basic Markdown to HTML conversion
        var html = markdown
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;");
        
        // Headers
        html = Regex.Replace(html, @"^###### (.+)$", "<h6>$1</h6>", RegexOptions.Multiline);
        html = Regex.Replace(html, @"^##### (.+)$", "<h5>$1</h5>", RegexOptions.Multiline);
        html = Regex.Replace(html, @"^#### (.+)$", "<h4>$1</h4>", RegexOptions.Multiline);
        html = Regex.Replace(html, @"^### (.+)$", "<h3>$1</h3>", RegexOptions.Multiline);
        html = Regex.Replace(html, @"^## (.+)$", "<h2>$1</h2>", RegexOptions.Multiline);
        html = Regex.Replace(html, @"^# (.+)$", "<h1>$1</h1>", RegexOptions.Multiline);
        
        // Lists
        html = Regex.Replace(html, @"^- (.+)$", "<li>$1</li>", RegexOptions.Multiline);
        html = Regex.Replace(html, @"(<li>.*?</li>)", "<ul>$1</ul>", RegexOptions.Singleline);
        html = html.Replace("</ul>\n<ul>", "");
        
        // Bold and italic
        html = Regex.Replace(html, @"\*\*(.+?)\*\*", "<strong>$1</strong>");
        html = Regex.Replace(html, @"\*(.+?)\*", "<em>$1</em>");
        
        // Paragraphs
        var lines = html.Split(new[] { "\n" }, StringSplitOptions.None);
        var paragraphs = new List<string>();
        var currentPara = new StringBuilder();
        
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                if (currentPara.Length > 0)
                {
                    paragraphs.Add("<p>" + currentPara.ToString() + "</p>");
                    currentPara.Clear();
                }
            }
            else if (!trimmed.StartsWith("<"))
            {
                if (currentPara.Length > 0) currentPara.Append(" ");
                currentPara.Append(trimmed);
            }
            else
            {
                if (currentPara.Length > 0)
                {
                    paragraphs.Add("<p>" + currentPara.ToString() + "</p>");
                    currentPara.Clear();
                }
                paragraphs.Add(trimmed);
            }
        }
        
        if (currentPara.Length > 0)
            paragraphs.Add("<p>" + currentPara.ToString() + "</p>");
        
        return string.Join("\n", paragraphs);
    }

    static async Task GenerateLandingPage(Dictionary<string, JsonElement> siteMetadata, 
                                         List<EpisodeMetadata> episodes,
                                         string baseTemplate, string featuredBlockTemplate)
    {
        var featuredSlug = siteMetadata["featuredEpisodeSlug"].GetString();
        var featuredEpisode = episodes.FirstOrDefault(e => e.Slug == featuredSlug);
        
        if (featuredEpisode == null && episodes.Count > 0)
            featuredEpisode = episodes[0];

        var featuredHtml = "";
        if (featuredEpisode != null)
        {
            featuredHtml = featuredBlockTemplate
                .Replace("{EPISODE_TITLE}", HtmlEncode(featuredEpisode.Title))
                .Replace("{EPISODE_SUMMARY}", HtmlEncode(featuredEpisode.Summary))
                .Replace("{EPISODE_DATE}", featuredEpisode.Date.ToString("MMM d, yyyy"))
                .Replace("{EPISODE_SLUG}", featuredEpisode.Slug)
                .Replace("{EPISODE_DURATION}", featuredEpisode.Duration);
        }

        var recentEpisodesHtml = new StringBuilder();
        foreach (var ep in episodes.Take(3))
        {
            recentEpisodesHtml.AppendLine($@"<div class=""episode-item"">
  <h3><a href=""/episodes/{ep.Slug}.html"">{HtmlEncode(ep.Title)}</a></h3>
  <p class=""episode-date"">{ep.Date:MMM d, yyyy}</p>
  <p>{HtmlEncode(ep.Summary)}</p>
</div>");
        }

        var content = $@"<div class=""landing-page"">
  {featuredHtml}
  <section class=""recent-episodes"">
    <h2>Recent Episodes</h2>
    {recentEpisodesHtml}
  </section>
</div>";

        var landingPage = baseTemplate
            .Replace("{TITLE}", "Home")
            .Replace("{CONTENT}", content);
        
        File.WriteAllText(Path.Combine(OUTPUT_DIR, "index.html"), landingPage);
    }

    static async Task GenerateEpisodesListPage(List<EpisodeMetadata> episodes,
                                              string baseTemplate,
                                              string episodesListTemplate,
                                              string episodeCardTemplate)
    {
        var cardsHtml = new StringBuilder();
        foreach (var episode in episodes)
        {
            var cardHtml = episodeCardTemplate
                .Replace("{EPISODE_TITLE}", HtmlEncode(episode.Title))
                .Replace("{EPISODE_DATE}", episode.Date.ToString("MMM d, yyyy"))
                .Replace("{EPISODE_SUMMARY}", HtmlEncode(episode.Summary))
                .Replace("{EPISODE_SLUG}", episode.Slug)
                .Replace("{EPISODE_DURATION}", episode.Duration)
                .Replace("{EPISODE_TAGS}", string.Join(", ", episode.Tags.Select(HtmlEncode)));
            
            cardsHtml.Append(cardHtml);
        }

        var content = episodesListTemplate
            .Replace("{EPISODES_COUNT}", episodes.Count.ToString())
            .Replace("{EPISODES_CARDS}", cardsHtml.ToString());

        var page = baseTemplate
            .Replace("{TITLE}", "Episodes")
            .Replace("{CONTENT}", content);

        File.WriteAllText(Path.Combine(OUTPUT_DIR, "episodes", "index.html"), page);
    }

    static async Task GenerateEpisodePage(EpisodeMetadata episode,
                                         string baseTemplate,
                                         string episodeTemplate)
    {
        var bodyHtml = MarkdownToHtml(episode.Body);
        
        var content = episodeTemplate
            .Replace("{EPISODE_TITLE}", HtmlEncode(episode.Title))
            .Replace("{EPISODE_DATE}", episode.Date.ToString("MMMM d, yyyy"))
            .Replace("{EPISODE_SUMMARY}", HtmlEncode(episode.Summary))
            .Replace("{EPISODE_DURATION}", episode.Duration)
            .Replace("{EPISODE_TAGS}", string.Join(", ", episode.Tags.Select(t => $"<span class=\"tag\">{HtmlEncode(t)}</span>")))
            .Replace("{EPISODE_CONTENT}", bodyHtml);

        var page = baseTemplate
            .Replace("{TITLE}", episode.Title)
            .Replace("{CONTENT}", content);

        File.WriteAllText(Path.Combine(OUTPUT_DIR, "episodes", $"{episode.Slug}.html"), page);
    }

    static string HtmlEncode(string text)
    {
        return System.Net.WebUtility.HtmlEncode(text);
    }

    static void CopyDirectory(string source, string destination)
    {
        if (!Directory.Exists(source)) return;
        
        Directory.CreateDirectory(destination);
        
        foreach (var file in Directory.GetFiles(source))
        {
            File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), true);
        }
        
        foreach (var dir in Directory.GetDirectories(source))
        {
            CopyDirectory(dir, Path.Combine(destination, Path.GetFileName(dir)));
        }
    }
}

class EpisodeMetadata
{
    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Duration { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
    public List<string> Tags { get; set; } = new();
    public string Body { get; set; } = "";
}
