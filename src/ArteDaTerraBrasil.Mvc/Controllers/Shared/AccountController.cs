using ArteDaTerraBrasil.Business.Interfaces.Entities;
using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Interfaces.Relationships;
using ArteDaTerraBrasil.Infra.Interfaces.Security;
using ArteDaTerraBrasil.Mvc.ViewModels.Shareds;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace ArteDaTerraBrasil.Mvc.Controllers.Shared
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPasswordService _passwordService;

        public AccountController(IUserRepository userRepository, 
                                 IPasswordService passwordService, 
                                 IPermissionRepository permissionRepository, 
                                 IRoleRepository roleRepository, 
                                 IModuleRepository moduleRepository)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _moduleRepository = moduleRepository;
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

            // Obter permissoes por perfil
            var roleId = user.RoleId;
            var roleTag = (await _roleRepository.GetByIdAsync(roleId)).Tag;
            var permissions = await _permissionRepository.GetByRoleIdAsync(roleId);

            // Criar claims
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Name),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role?.Tag ?? "user"),
                new System.Security.Claims.Claim("UserId", user.Id.ToString())
            };

            var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var permission in permissions)
            {
                var module = await _moduleRepository.GetByIdAsync(permission.ModuleId);

                if (module != null) identity.AddClaim(new System.Security.Claims.Claim("Permissions", $"{module.Tag}:{permission.Claim.Tag}"));
            }

            var principal = new System.Security.Claims.ClaimsPrincipal(identity);


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
