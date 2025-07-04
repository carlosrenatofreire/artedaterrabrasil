using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Parameters;

namespace LayeredArch.Business.Interfaces.Parameters
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRoleAndUsersByRoleId(Guid id);
        Task<Role> GetRoleAndDetailsById(Guid id);
    }
}
