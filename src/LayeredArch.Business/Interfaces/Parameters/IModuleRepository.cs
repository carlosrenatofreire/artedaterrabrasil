using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Parameters;

namespace LayeredArch.Business.Interfaces.Parameters
{
    public interface IModuleRepository : IRepository<Module>
    {
        Task<Module> GetModuleAndClaimsById(Guid id);
        Task<Module> GetModuleAndPermissionsById(Guid id);
        Task<IEnumerable<Module>> GetModulesWithClaims();
    }
}
