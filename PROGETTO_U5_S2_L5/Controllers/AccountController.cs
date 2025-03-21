using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROGETTO_U5_S2_L5.Models;
using PROGETTO_U5_S2_L5.Services;
using PROGETTO_U5_S2_L5.ViewModels;

namespace PROGETTO_U5_S2_L5.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly PrenotazioniService _prenotazioniService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, PrenotazioniService prenotazioniService) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _prenotazioniService = prenotazioniService;
        }
        private bool VerifyAuth() {
            return User.Identity.IsAuthenticated;
        }

        //[AllowAnonymous]
        public async Task<IActionResult> Register() {
            //if (VerifyAuth()) {
            //    return RedirectToAction("Index", "Home");
            //}

            if (!User.IsInRole("Admin")) {
                return RedirectToAction("Index", "Home");
            }

            var roles = await _prenotazioniService.GetAllRolesAsync();

            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Compila correttamente i campi.");
                return View(model);
            }

            var user = new ApplicationUser {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                BirthDate = model.BirthDate
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            //if (!result.Succeeded) {
            //    return View();
            //}

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            var userForRole = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(userForRole, model.Role);

            return RedirectToAction("Index", "Home");
        }

        //[AllowAnonymous]
        public IActionResult Login() {
            if (VerifyAuth()) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null) {
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            if (!signInResult.Succeeded) {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
