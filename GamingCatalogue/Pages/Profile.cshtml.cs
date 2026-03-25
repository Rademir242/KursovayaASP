using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GamingCatalogue.Pages
{
    public class ProfileModel : PageModel
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }

        public void OnGet()
        {
            IsAuthenticated = User.Identity != null && User.Identity.IsAuthenticated;

            if (IsAuthenticated)
            {

                Username = User.FindFirstValue(ClaimTypes.Name)
                           ?? User.Identity.Name
                           ?? "User";
            }
            else
            {
                Username = "Guest";
            }
        }
    }
}
