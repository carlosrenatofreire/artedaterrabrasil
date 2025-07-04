using LayeredArch.Business.Models.Parameters;

namespace LayeredArch.Business.Interfaces.Parameters
{
    public interface IModuleService : IDisposable
    {
        Task AddAsync(Module module);
        Task UpdateAsync(Module module);
        Task RemoveAsync(Guid id);
        Task ActivatedAsync(Guid id);
    }
}
