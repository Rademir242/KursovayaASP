using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace GamingCatalogue.Pages
{
    public class AuthModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string RegLogin { get; set; }
        [BindProperty]
        public string RegPassword { get; set; }
        [BindProperty]
        public string RegPassword2 { get; set; }
        public string GeneralError { get; set; }
        public bool IsRegisterTab { get; set; } = false;
        private readonly string digits = "0123456789";
        private readonly string lower = "abcdefghijklmnopqrstuvwxyz";
        private readonly string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly string special = "!@#$%^&*()_+-=[]{}|;:',.<>/?";
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Login)
            };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToPage("/Profile");
        }
        public async Task<IActionResult> OnPostRegisterAsync()
        {
            IsRegisterTab = true; 

            if (string.IsNullOrEmpty(RegLogin))
            {
                GeneralError = "Введите логин";
                return Page();
            }

            var passwordErrors = ValidatePassword(RegPassword);
            if (passwordErrors.Any())
            {
                GeneralError = string.Join("<br/>", passwordErrors);
                IsRegisterTab = true;
                return Page();
            }

            if (RegPassword != RegPassword2)
            {
                GeneralError = "Пароли не совпадают";
                IsRegisterTab = true;
                return Page();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, RegLogin)
    };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToPage("/Profile");
        }
        private List<string> ValidatePassword(string password)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(password))
                errors.Add("Введите пароль");

            if (password.Contains(' '))
                errors.Add("Пароль не может содержать пробелы");

            if (password.Length < 8 || password.Length > 20)
                errors.Add("Пароль должен быть от 8 до 20 символов");

            if (!password.Any(c => digits.Contains(c)))
                errors.Add("Пароль должен содержать хотя бы одну цифру");

            if (!password.Any(c => lower.Contains(c)))
                errors.Add("Пароль должен содержать хотя бы одну строчную латинскую букву");


            if (!password.Any(c => upper.Contains(c)))
                errors.Add("Пароль должен содержать хотя бы одну заглавную латинскую букву");

            if (!password.Any(c => special.Contains(c)))
                errors.Add("Пароль должен содержать хотя бы один спецсимвол из списка: [!@#$%^&*()_+-=[]{}|;:',.<>/?]");
            var repeatError = CheckRepeatedCharacters(RegPassword);
            if (repeatError != null)
            {
                errors.Add(repeatError);
            }
            var conError = CheckContiniousCharacters(RegPassword);
            if (conError != null)
                errors.Add(conError);
            return errors;
        }
        private string CheckRepeatedCharacters(string password)
        {
            int count = 1;
            char prev = password[0];

            for (int i = 1; i < password.Length; i++)
            {
                if (password[i] == prev)
                {
                    count++;
                    if (count >= 3)
                        return "Пароль не должен содержать три одинаковых символа подряд";
                }
                else
                {
                    prev = password[i];
                    count = 1; 
                }
            }

            return null; 
        }
        private string CheckContiniousCharacters(string password)
        {

            for (int i = 0; i <= password.Length - 3; i++)
            {
                string window = password.Substring(i, 3); 

                if (digits.Contains(window))
                    return "Пароль не должен содержать последовательность цифр (123, 678)";

                if (lower.Contains(window))
                    return "Пароль не должен содержать последовательность букв (abc, klm)";

                if (upper.Contains(window))
                    return "Пароль не должен содержать последовательность заглавных букв (ABC, XYZ)";
            }

            return null; 
        }
    }
}
