using IdentityModel;
using LayeredArch.Business.Interfaces.Shareds;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LayeredArch.Business.Services.Shareds
{
    public class AccessorService : IAccessorService
    {
        private readonly IHttpContextAccessor _accessor;

        public AccessorService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid GetUserId()
        {
            // NOTE:
            // Searches for the name in the cookie or JWT 
            // (if it doesn't find it, it returns one (Empty Guid)

            if (!IsAuthenticated()) return Guid.Empty;

            var claim = _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (string.IsNullOrEmpty(claim))
            //    claim = _accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            return claim is null ? Guid.Empty : Guid.Parse(claim);
        }

        public string GetUsername()
        {
            var username = _accessor.HttpContext?.User.FindFirst("username")?.Value;
            if (!string.IsNullOrEmpty(username)) return username;

            username = _accessor.HttpContext?.User.Identity?.Name;
            if (!string.IsNullOrEmpty(username)) return username;

            username = _accessor.HttpContext?.User.FindFirst(JwtClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(username)) return username;

            username = _accessor.HttpContext?.User.FindFirst(JwtClaimTypes.GivenName)?.Value;
            if (!string.IsNullOrEmpty(username)) return username;

            var sub = _accessor.HttpContext?.User.FindFirst(JwtClaimTypes.Subject);
            if (sub != null) return sub.Value;

            return string.Empty;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext != null && _accessor.HttpContext.User.IsInRole(role);
        }

        public string GetLocalIpAddress()
        {
            return _accessor.HttpContext?.Connection.LocalIpAddress?.ToString();
        }

        public string GetRemoteIpAddress()
        {
            return _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }

    }
}
