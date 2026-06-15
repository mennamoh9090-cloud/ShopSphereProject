using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShopSphere.MVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            _ = password; // Ignore unused parameter for now
            var userEmail = email ?? "user@shopsphere.com";
            var userRole = userEmail.Contains("admin", StringComparison.OrdinalIgnoreCase) ? "Admin" : "User";
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userEmail),
                new(ClaimTypes.Email, userEmail),
                new(ClaimTypes.Role, userRole)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            var anonCart = HttpContext.Session.GetString("CartSession_Anonymous");
            if (!string.IsNullOrEmpty(anonCart))
            {
                HttpContext.Session.SetString($"CartSession_{userEmail}", anonCart);
                HttpContext.Session.Remove("CartSession_Anonymous");
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            if (userRole == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            return RedirectToAction("Dashboard", "User");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string role)
        {
            var userEmail = email ?? "newuser@shopsphere.com";
            var userRole = role ?? "User";
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userEmail),
                new(ClaimTypes.Email, userEmail),
                new(ClaimTypes.Role, userRole)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            var anonCart = HttpContext.Session.GetString("CartSession_Anonymous");
            if (!string.IsNullOrEmpty(anonCart))
            {
                HttpContext.Session.SetString($"CartSession_{userEmail}", anonCart);
                HttpContext.Session.Remove("CartSession_Anonymous");
            }

            if (userRole == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            return RedirectToAction("Dashboard", "User");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
