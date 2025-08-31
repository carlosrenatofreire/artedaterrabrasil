using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Parameters;

namespace ArteDaTerraBrasil.Business.Interfaces.Parameters
{
    public interface IClaimRepository : IRepository<Claim>
    {
        Task<List<Claim>> GetAllWithModule();
        Task<Claim> GetClaimAndModuleById(Guid id);
    }
}
