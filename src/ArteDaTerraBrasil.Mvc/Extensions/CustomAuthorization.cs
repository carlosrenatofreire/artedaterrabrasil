using ArteDaTerraBrasil.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArteDaTerraBrasil.Mvc.Extensions
{
    public static class CustomAuthorization
    {
        public static bool ValidateClaimsByRoles(HttpContext context, string[] roles, MyDbContext dbContext)
        {
            if (!context.User.Identity.IsAuthenticated) return false;

            var username = context.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username)) return false;

            var user = dbContext.E_Users.Include(u => u.Role).FirstOrDefault(u => u.Email == username && u.Activated);

            if (user == null || user.Role == null) return false;

            bool hasValidRole = roles.Any(role => role.Equals(user.Role.Tag, StringComparison.OrdinalIgnoreCase));

            if (!hasValidRole) return false;

            return true; // Para outros usuários, a validação normal ocorre
        }

        public static bool ValidateClaimsByKey(HttpContext context, string moduleNameTag, string claimNameTag )
        {
            // Check if user is authenticated
            if (!context.User.Identity.IsAuthenticated)  return false;

            var claimValue = $"{moduleNameTag}:{claimNameTag}";

            bool validation = context.User.Claims.Any(c => c.Type == "Permissions" && c.Value == claimValue);

            return validation;
        }

        public class ClaimsAuthorizeAttribute : TypeFilterAttribute
        {
            public ClaimsAuthorizeAttribute(params string[] roles) : base(typeof(RequisitoClaimFilter))
            {
                Arguments = new object[] { roles };
            }
        }

        public class RequisitoClaimFilter : IAuthorizationFilter
        {
            private readonly string[] _roles;
            private readonly MyDbContext _dbContext;

            public RequisitoClaimFilter(string[] roles, MyDbContext dbContext)
            {
                _roles = roles;
                _dbContext = dbContext;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                    return;
                }

                if (!ValidateClaimsByRoles(context.HttpContext, _roles, _dbContext))
                {
                    context.Result = new RedirectToActionResult("AccessDenied", "Error", null);
                }
            }
        }

        // New Attribute for Claim-Based Authorization
        public class ClaimsKeyAuthorizeAttribute : TypeFilterAttribute
        {
            public ClaimsKeyAuthorizeAttribute(string moduleNameTag, string claimNameTag) : base(typeof(ClaimKeyRequirementFilter))
            {
                Arguments = new object[] { moduleNameTag, claimNameTag };
            }
        }

        public class ClaimKeyRequirementFilter : IAuthorizationFilter
        {
            private readonly string _moduleNameTag;
            private readonly string _claimNameTag;

            public ClaimKeyRequirementFilter(string moduleNameTag, string claimNameTag)
            {
                _moduleNameTag = moduleNameTag;
                _claimNameTag = claimNameTag;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                    return;
                }

                if (!ValidateClaimsByKey(context.HttpContext, _moduleNameTag, _claimNameTag))
                {
                    context.Result = new RedirectToActionResult("AccessDenied", "Error", null);
                }
            }
        }
    }

}
