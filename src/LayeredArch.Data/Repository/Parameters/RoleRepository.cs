using LayeredArch.Business.Interfaces.Entities;
using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Data.Contexts;
using LayeredArch.Data.Repository.Shareds;
using Microsoft.EntityFrameworkCore;

namespace LayeredArch.Data.Repository.Parameters
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
