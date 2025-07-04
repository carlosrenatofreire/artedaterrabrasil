using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Entities;
using LayeredArch.Business.Models.Parameters;

namespace LayeredArch.Business.Interfaces.Entities
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetAllWithRole();
        Task<User> GetUserAndRoleByUserId(Guid id);
        Task<User> GetUserAndDetailsByUsername(string username);
        Task<IEnumerable<User>> GetAllWithRoleAndSupervisor();
    }
}
