using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamingCatalogue.Models;

namespace GamingCatalogue.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Game> Games { get; set; } = new();
        public List<Genre> AllGenres { get; set; } = new();
        public List<Tag> AllTags { get; set; } = new();


        [BindProperty(SupportsGet = true)] public string? SelectedGenre { get; set; }
        [BindProperty(SupportsGet = true)] public string? SelectedTag { get; set; }
        [BindProperty(SupportsGet = true)] public int? MinGameplay { get; set; }
        [BindProperty(SupportsGet = true)] public int? MinGraphics { get; set; }
        [BindProperty(SupportsGet = true)] public int? MinStory { get; set; }
        [BindProperty(SupportsGet = true)] public int? MinMusic { get; set; }

        public async Task OnGetAsync()
        {
            AllGenres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            AllTags = await _context.Tags.OrderBy(t => t.Name).ToListAsync();

            IQueryable<Game> query = _context.Games
                .Include(g => g.Genres)
                .Include(g => g.Tags);

            if (!string.IsNullOrEmpty(SelectedGenre))
            {
                query = query.Where(g => g.Genres.Any(genre => genre.Name == SelectedGenre));
            }

            if (!string.IsNullOrEmpty(SelectedTag))
            {
                query = query.Where(g => g.Tags.Any(tag => tag.Name == SelectedTag));
            }
            if (MinGameplay.HasValue && MinGameplay > 0)
                query = query.Where(g => g.GameplayScore >= MinGameplay);

            if (MinGraphics.HasValue && MinGraphics > 0)
                query = query.Where(g => g.GraphicsScore >= MinGraphics);

            if (MinStory.HasValue && MinStory > 0)
                query = query.Where(g => g.StoryScore >= MinStory);

            if (MinMusic.HasValue && MinMusic > 0)
                query = query.Where(g => g.MusicScore >= MinMusic);
            Games = await query.ToListAsync();
        }
    }
}
