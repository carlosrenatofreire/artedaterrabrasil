using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Data.Contexts;
using LayeredArch.Data.Repository.Shareds;
using Microsoft.EntityFrameworkCore;

namespace LayeredArch.Data.Repository.Parameters
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
