using LayeredArch.Business.Interfaces.Relationships;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Relationships;
using LayeredArch.Business.Services.Shareds;
using LayeredArch.Business.Validations.Relationships;
using LayeredArch.Helper.StaticVariables;

namespace LayeredArch.Business.Services.Relationship
{
    public class PermissionService : BaseService, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository,
                                    INotifierService notifier,
                                    IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task AddAsync(Permission permission)
        {
            if (!RunValidation(new PermissionValidation(), permission)) return;

            // Check if the entity already exists
            if (_permissionRepository.FindAsync(e => e.ModuleId == permission.ModuleId && e.RoleId == permission.RoleId && e.ClaimId == permission.ClaimId).Result.Any())
            {
                NotifyWithMessage(MessageHelper.GenericErrors.FieldAlreadyExists("Permissão", "Módulo,cargo e declaração", $"{permission.ModuleId} e {permission.RoleId}"));
                return;
            }

            // Add the entity
            await _permissionRepository.AddAsync(permission);
            await Commit();
        }


        public async Task UpdateAsync(Permission permission)
        {
            if (!RunValidation(new PermissionValidation(), permission)) return;

            // Check if the entity already exists
            if (_permissionRepository.FindAsync(e => e.ModuleId == permission.ModuleId && e.RoleId == permission.RoleId && e.Id != permission.Id).Result.Any())
            {
                NotifyWithMessage(MessageHelper.GenericErrors.FieldAlreadyExists("Permissão", "Módulo e Cargo", $"{permission.ModuleId} e {permission.RoleId}"));
                return;
            }

            // Add the entity
            await _permissionRepository.UpdateAsync(permission);
            await Commit();
        }

        public async Task RemovePermissionsByRoleId(Guid RoleId)
        {
            var permissions = await _permissionRepository.GetByRoleIdAsync(RoleId);

            foreach (var permission in permissions)
            {
                await _permissionRepository.RemoveAsync(permission.Id);
            }

            await Commit();
        }

        public void Dispose()
        {
            _permissionRepository?.Dispose();
        }
    }
}
