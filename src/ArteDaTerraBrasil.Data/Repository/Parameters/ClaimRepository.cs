using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Data.Contexts;
using ArteDaTerraBrasil.Data.Repository.Shareds;
using Microsoft.EntityFrameworkCore;

namespace ArteDaTerraBrasil.Data.Repository.Parameters
{
    public class ClaimRepository : Repository<Claim>, IClaimRepository
    {
        public ClaimRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<List<Claim>> GetAllWithModule()
        {
            return await Db.P_Claims.AsNoTracking()
                .Include(c => c.Module)
                .ToListAsync();

        }

        public async Task<Claim> GetClaimAndModuleById(Guid id)
        {
            return await Db.P_Claims.AsNoTracking()
                .Include(c => c.Module)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
