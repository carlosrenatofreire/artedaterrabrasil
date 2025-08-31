using ArteDaTerraBrasil.Business.Models.Relationships;

namespace ArteDaTerraBrasil.Business.Interfaces.Relationships
{
    public interface IPermissionService : IDisposable
    {
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task RemovePermissionsByRoleId(Guid RoleId);
    }
}
