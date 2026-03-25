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

        public async Task OnGetAsync()
        {
            Games = await _context.Games.ToListAsync();
        }
    }
}