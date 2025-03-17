using System.Security.Claims;
using BM_Web.Models;
using BM_Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BM_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Login Page
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var token = await _apiService.LoginAsync(model);
            if (token != null)
            {
                Response.Cookies.Append("AuthToken", token);

                //Create User Identity & Claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email, ClaimTypes.Role), // Store email as identity
                    new Claim("JWT", token) // Store JWT as a claim (optional)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true }; // Keep user logged in

                //Sign in the user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return RedirectToAction("Index", "Home"); // Redirect after login
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // Register Page
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await _apiService.PostAsync("auth/register", model);
            if (success) return RedirectToAction("Login");

            ModelState.AddModelError("", "Registration failed.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // 🔹 Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 🔹 Remove authentication cookie
            Response.Cookies.Delete("AuthToken");

            return RedirectToAction("Login", "Auth"); // Redirect to login page
        }
    }
}
