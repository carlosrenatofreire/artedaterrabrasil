using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Data.Contexts;
using ArteDaTerraBrasil.Data.Repository.Shareds;
using Microsoft.EntityFrameworkCore;

namespace ArteDaTerraBrasil.Data.Repository.Parameters
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(MyDbContext context) : base(context) { }

        public async Task<Module> GetModuleAndClaimsById(Guid id)
        {
            return await Db.P_Modules.AsNoTracking()
                .Include(m => m.Claims)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Module> GetModuleAndPermissionsById(Guid id)
        {
            return await Db.P_Modules.AsNoTracking()
                .Include(m => m.Permissions)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Module>> GetModulesWithClaims()
        {
            return await Db.P_Modules.AsNoTracking()
                .OrderBy(e => e.Name)
                .Include(m => m.Claims.Where(c => c.Activated == true))
                .Where(m => m.Activated == true)
                .ToListAsync();
        }
    }
}
