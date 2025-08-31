using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Relationships;

namespace ArteDaTerraBrasil.Business.Interfaces.Relationships
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission> GetPermissionsByRoleId(Guid roleId);
        Task<Permission> GetPermissionsByModuleId(Guid moduleId);
        Task<Permission> GetPermissionAndModuleAndRoleById(Guid id);
        Task<IEnumerable<Permission>> GetByRoleIdAsync(Guid roleId);
    }
}
