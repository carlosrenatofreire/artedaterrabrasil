using LayeredArch.Business.Models.Relationships;

namespace LayeredArch.Business.Interfaces.Relationships
{
    public interface IPermissionService : IDisposable
    {
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task RemovePermissionsByRoleId(Guid RoleId);
    }
}
