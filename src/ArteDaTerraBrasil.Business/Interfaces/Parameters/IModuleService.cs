using ArteDaTerraBrasil.Business.Models.Parameters;

namespace ArteDaTerraBrasil.Business.Interfaces.Parameters
{
    public interface IModuleService : IDisposable
    {
        Task AddAsync(Module module);
        Task UpdateAsync(Module module);
        Task RemoveAsync(Guid id);
        Task ActivatedAsync(Guid id);
    }
}
