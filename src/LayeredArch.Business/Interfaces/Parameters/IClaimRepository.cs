using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Parameters;

namespace LayeredArch.Business.Interfaces.Parameters
{
    public interface IClaimRepository : IRepository<Claim>
    {
        Task<List<Claim>> GetAllWithModule();
        Task<Claim> GetClaimAndModuleById(Guid id);
    }
}
