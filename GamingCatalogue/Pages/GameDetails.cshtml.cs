using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamingCatalogue.Models;

namespace GamingCatalogue.Pages
{
    public class GameDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GameDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Game Game { get; set; }
        public List<Tag> AllTags { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Game = await _context.Games
                .Include(g => g.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Game == null)
            {
                return NotFound();
            }

            AllTags = await _context.Tags.OrderBy(t => t.Name).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateTagsAsync(int id, int[] selectedTags)
        {
            var gameToUpdate = await _context.Games
                .Include(g => g.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gameToUpdate == null)
            {
                return NotFound();
            }

            gameToUpdate.Tags.Clear();

            if (selectedTags != null)
            {
                foreach (var tagId in selectedTags)
                {
                    var tag = await _context.Tags.FindAsync(tagId);
                    if (tag != null)
                    {
                        gameToUpdate.Tags.Add(tag);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = id });
        }
    }
}