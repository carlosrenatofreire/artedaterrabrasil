using ArteDaTerraBrasil.Business.Models.Parameters;

namespace ArteDaTerraBrasil.Business.Interfaces.Parameters
{
    public interface IClaimService : IDisposable
    {
        Task AddAsync(Claim claim);
        Task UpdateAsync(Claim claim);
        Task RemoveAsync(Guid id);
        Task ActivatedAsync(Guid id);
    }
}
