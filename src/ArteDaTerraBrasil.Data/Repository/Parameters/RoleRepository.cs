using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Business.Interfaces.Entities;
using ArteDaTerraBrasil.Data.Contexts;
using ArteDaTerraBrasil.Data.Repository.Shareds;
using Microsoft.EntityFrameworkCore;

namespace ArteDaTerraBrasil.Data.Repository.Parameters
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(MyDbContext context) : base(context) { }

        public async Task<Role> GetRoleAndDetailsById(Guid id)
        {
            return await Db.P_Roles.AsNoTracking()
                .Include(r => r.Users)
                .Include(r => r.Supervisor)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> GetRoleAndUsersByRoleId(Guid id)
        {
            return await Db.P_Roles.AsNoTracking()
               .Include(r => r.Users)
               .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
