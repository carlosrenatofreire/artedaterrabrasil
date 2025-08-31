using ArteDaTerraBrasil.Business.Interfaces.Entities;
using ArteDaTerraBrasil.Business.Models.Entities;
using ArteDaTerraBrasil.Data.Contexts;
using ArteDaTerraBrasil.Data.Repository.Shareds;
using Microsoft.EntityFrameworkCore;

namespace ArteDaTerraBrasil.Data.Repository.Entities
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context) { }

        public async Task<List<User>> GetAllWithRole()
        {
            return await DbSet
                .AsNoTracking()
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> GetUserAndRoleByUserId(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(u => u.Role)
                .ThenInclude(r => r.Supervisor)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserAndDetailsByUsername(string username)
        {
            return await DbSet
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == username);
        }

        public async Task<IEnumerable<User>> GetAllWithRoleAndSupervisor()
        {
            return await DbSet
                .AsNoTracking()
                .Include(u => u.Role)
                .ThenInclude(r => r.Supervisor)
                .ToListAsync();
        }
    }
}
