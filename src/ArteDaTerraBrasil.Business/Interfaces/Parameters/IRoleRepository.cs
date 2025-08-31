using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Parameters;

namespace ArteDaTerraBrasil.Business.Interfaces.Parameters
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRoleAndUsersByRoleId(Guid id);
        Task<Role> GetRoleAndDetailsById(Guid id);
    }
}
