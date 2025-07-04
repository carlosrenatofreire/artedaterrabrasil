using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Relationships;

namespace LayeredArch.Business.Interfaces.Relationships
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission> GetPermissionsByRoleId(Guid roleId);
        Task<Permission> GetPermissionsByModuleId(Guid moduleId);
        Task<Permission> GetPermissionAndModuleAndRoleById(Guid id);
        Task<IEnumerable<Permission>> GetByRoleIdAsync(Guid roleId);
    }
}
