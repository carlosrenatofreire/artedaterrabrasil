using LayeredArch.Business.Models.Entities;

namespace LayeredArch.Business.Interfaces.Entities
{
    public interface IUserService : IDisposable
    {
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(Guid id);
        Task ActivatedAsync(Guid id);
    }
}
