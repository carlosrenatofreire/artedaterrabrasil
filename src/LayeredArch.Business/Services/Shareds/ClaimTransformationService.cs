using LayeredArch.Business.Interfaces.Entities;
using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Interfaces.Relationships;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace LayeredArch.Business.Services.Shareds
{
    public class ClaimsTransformationService : IClaimsTransformation
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public ClaimsTransformationService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IModuleRepository moduleRepository
        )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _moduleRepository = moduleRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Check if the identity already contains claims
            if (principal.Identity is ClaimsIdentity identity && !identity.Claims.Any(c => c.Type == ClaimTypes.Role || c.Type == "Permissions"))
            {
                // Get the username from the identity
                var username = identity.Name;

                // Fetch the user from the database
                var user = ( await _userRepository.FindAsync(x => x.Email == username) ).FirstOrDefault();
                if (user != null)
                {
                    // Fetch the user's role
                    var role = user.RoleId;
                    var roleTag = (await _roleRepository.GetByIdAsync(role)).Tag;

                    // Add the user's role as a claim
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleTag));

                    // Fetch all permissions for the user's role
                    var permissions = await _permissionRepository.GetByRoleIdAsync(role);

                    foreach (var permission in permissions)
                    {
                        // Fetch the module associated with the permission
                        var module = await _moduleRepository.GetByIdAsync(permission.ModuleId);
                        if (module != null)
                        {
                            // Add the claim for the module and permission
                            identity.AddClaim(
                                new Claim("Permissions", $"{module.Tag}:{permission.Claim.Tag}")
                            );
                        }
                    }
                }
            }
            return principal;
        }
    }
}
