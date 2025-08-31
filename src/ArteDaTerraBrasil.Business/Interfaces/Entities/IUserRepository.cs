using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Entities;
using ArteDaTerraBrasil.Business.Models.Parameters;

namespace ArteDaTerraBrasil.Business.Interfaces.Entities
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetAllWithRole();
        Task<User> GetUserAndRoleByUserId(Guid id);
        Task<User> GetUserAndDetailsByUsername(string username);
        Task<IEnumerable<User>> GetAllWithRoleAndSupervisor();
    }
}
