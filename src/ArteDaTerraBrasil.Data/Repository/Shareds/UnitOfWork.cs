using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Data.Contexts;

namespace ArteDaTerraBrasil.Data.Repository.Shareds
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;

        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
