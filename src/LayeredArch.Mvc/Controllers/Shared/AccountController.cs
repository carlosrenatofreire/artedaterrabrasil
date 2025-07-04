using LayeredArch.Business.Interfaces.Entities;
using LayeredArch.Business.Models.Entities;
using LayeredArch.Infra.Interfaces.Security;
using LayeredArch.Mvc.ViewModels.Shareds;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LayeredArch.Mvc.Controllers.Shared
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public AccountController(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return Json(new { success = false, message = "Dados inválidos." });

            var user = await _userRepository.GetUserAndDetailsByUsername(model.Email);

            if (user == null || !user.Activated || user.Deleted) return Json(new { success = false, message = "Email ou palavra-passe inválidos." });

            var isValid = _passwordService.VerifyPassword(user, model.Password);
            
            if (!isValid) return Json(new { success = false, message = "Credenciais invalidas." });

            // Criar claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.Tag ?? "user"),
                new Claim("UserId", user.Id.ToString())
                // Adicione mais claims se necessário
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Json(new { success = true, message = "Login efetuado com sucesso!" });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
