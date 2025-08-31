using ArteDaTerraBrasil.Business.Interfaces.Relationships;
using ArteDaTerraBrasil.Business.Models.Relationships;
using ArteDaTerraBrasil.Data.Contexts;
using ArteDaTerraBrasil.Data.Repository.Shareds;
using Microsoft.EntityFrameworkCore;

namespace ArteDaTerraBrasil.Data.Repository.Relationships
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(MyDbContext context) : base(context) { }

        public async Task<Permission> GetPermissionAndModuleAndRoleById(Guid id)
        {
            return await Db.R_Permissions.AsNoTracking()
                            .Include(p => p.Module)
                            .Include(p => p.Role)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Permission> GetPermissionsByModuleId(Guid moduleId)
        {
            return await Db.R_Permissions.AsNoTracking()
                            .FirstOrDefaultAsync(p => p.ModuleId == moduleId);
        }

        public async Task<Permission> GetPermissionsByRoleId(Guid roleId)
        {
            return await Db.R_Permissions.AsNoTracking()
                            .FirstOrDefaultAsync(p => p.RoleId == roleId);
        }

        async Task<IEnumerable<Permission>> IPermissionRepository.GetByRoleIdAsync(Guid roleId)
        {
            return await Db.R_Permissions.AsNoTracking()
                            .Include(p => p.Role)
                            .Include(p => p.Module)
                            .Include(p => p.Claim)
                            .Where(p => p.RoleId == roleId)
                            .ToListAsync();
        }
    }
}
