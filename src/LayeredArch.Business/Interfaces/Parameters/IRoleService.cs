using LayeredArch.Business.Models.Parameters;

namespace LayeredArch.Business.Interfaces.Parameters
{
    public interface IRoleService : IDisposable
    {
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task RemoveAsync(Guid id);
        Task ActivateAsync(Guid id);
    }
}
