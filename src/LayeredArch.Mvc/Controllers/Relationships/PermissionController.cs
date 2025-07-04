using AutoMapper;
using Azure.Core;
using LayeredArch.Business.Enums;
using LayeredArch.Business.Interfaces.Auxiliares;
using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Interfaces.Relationships;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Relationships;
using LayeredArch.Helper.StaticVariables;
using LayeredArch.Mvc.Controllers.Shared;
using LayeredArch.Mvc.ViewModels.Parameters;
using LayeredArch.Mvc.ViewModels.Relationships;
using Microsoft.AspNetCore.Mvc;

namespace LayeredArch.Mvc.Controllers.Relationships
{
    public class PermissionController : BaseController
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionService _permissionService;
        private readonly IModuleRepository _moduleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public PermissionController(
            INotifierService notifier,
            IAccessorService accessorService,
            IAuditLogService auditLogService,
            IPermissionRepository permissionRepository,
            IPermissionService permissionService,
            IModuleRepository moduleRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
            : base(notifier, accessorService, auditLogService)
        {
            _permissionRepository = permissionRepository;
            _permissionService = permissionService;
            _moduleRepository = moduleRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [Route("Permission-Edit/{roleId:guid}")]
        [HttpGet]
        public async Task<IActionResult> Create(Guid roleId)
        {
            var permissionViewModel = await GetFullPermissionViewModel(new PermissionViewModel(), roleId);

            return View(permissionViewModel);
        }

        [HttpPost]
        [Route("Permission-Edit/{roleId}")]
        public async Task<IActionResult> EditPermissions(Guid roleId, [FromForm] List<string> permissions)
        {
            try
            {
                // Step 1: Parse the submitted permissions and prepare to insert them
                var newPermissions = permissions
                    .Select(permission =>
                    {
                        permission = permission.Trim();
                        var parts = permission.Split('_');
                        return new Permission
                        {
                            RoleId = roleId,
                            ModuleId = Guid.Parse(parts[0]),
                            ClaimId = Guid.Parse(parts[1])
                        };
                    })
                    .ToList();


                var existingPermissions = await _permissionRepository.GetByRoleIdAsync(roleId);

                if (!existingPermissions.Any() && newPermissions.Count == 0)
                {
                    return Json(new { success = false, error = MessageHelper.PermissionControllerMessages.NoPermissionsSelected });

                }
                await _permissionService.RemovePermissionsByRoleId(roleId);

                // Step 3: Add the new permissions
                foreach (var newPermission in newPermissions)
                {
                    await _permissionService.AddAsync(newPermission);
                }

                // 5. Log creation success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.PermissionControllerMessages.PermissionEdited);

                return Json(new { success = true, message = MessageHelper.PermissionControllerMessages.PermissionEdited });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }


        private async Task<PermissionViewModel> GetFullPermissionViewModel(PermissionViewModel permissionViewModel, Guid roleId)
        {

            permissionViewModel.ModuleList = _mapper.Map<IEnumerable<ModuleViewModel>>(await _moduleRepository.GetModulesWithClaims());
            permissionViewModel.Role = _mapper.Map<RoleViewModel>(await _roleRepository.GetByIdAsync(roleId));

            // Fetch permissions for the given role
            var permissions = await _permissionRepository.GetByRoleIdAsync(roleId);

            foreach (var module in permissionViewModel.ModuleList)
            {

                foreach (var claim in module.Claims)
                {
                    // If the claim ID matches any permission's ClaimId, set IsChecked to true
                    if (permissions.Any(p => p.ClaimId == claim.Id && p.ModuleId == module.Id))
                    {
                        claim.hasPermission = true;
                    }
                }
            }
            return permissionViewModel;
        }
    }
}
