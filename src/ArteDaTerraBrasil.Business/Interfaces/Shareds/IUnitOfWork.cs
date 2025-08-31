using ArteDaTerraBrasil.Business.Interfaces.Entities;

namespace ArteDaTerraBrasil.Business.Interfaces.Shareds
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
