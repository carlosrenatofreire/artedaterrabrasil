using LayeredArch.Business.Interfaces.Entities;

namespace LayeredArch.Business.Interfaces.Shareds
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
