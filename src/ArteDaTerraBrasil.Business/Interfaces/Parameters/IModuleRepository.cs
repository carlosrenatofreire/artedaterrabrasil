using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Parameters;

namespace ArteDaTerraBrasil.Business.Interfaces.Parameters
{
    public interface IModuleRepository : IRepository<Module>
    {
        Task<Module> GetModuleAndClaimsById(Guid id);
        Task<Module> GetModuleAndPermissionsById(Guid id);
        Task<IEnumerable<Module>> GetModulesWithClaims();
    }
}
