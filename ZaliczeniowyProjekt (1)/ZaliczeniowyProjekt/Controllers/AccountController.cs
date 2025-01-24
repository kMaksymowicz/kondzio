using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ZaliczeniowyProjekt.Models;
using ZaliczeniowyProjekt.ViewModels;
using System.Threading.Tasks;

namespace ZaliczeniowyProjekt.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Logujemy od razu użytkownika po rejestracji
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Dodajemy komunikat sukcesu
                TempData["SuccessMessage"] = "Rejestracja przebiegła pomyślnie! Jesteś teraz zalogowany.";

                // Przekierowujemy na stronę główną
                return RedirectToAction("Index", "Home");
            }

            // Jeżeli wystąpiły błędy walidacji w Identity:
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Próba logowania
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // Komunikat o sukcesie
                TempData["SuccessMessage"] = "Zalogowałeś się pomyślnie!";

                // Przekierowanie na stronę główną
                return RedirectToAction("Index", "Home");
            }

            // Jeśli logowanie się nie powiedzie:
            ModelState.AddModelError("", "Nieudana próba logowania. Sprawdź dane i spróbuj ponownie.");
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // Po wylogowaniu wracamy na Home/Index
            return RedirectToAction("Index", "Home");
        }
    }
}
