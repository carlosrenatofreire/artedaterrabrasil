using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Data.Contexts;

namespace LayeredArch.Data.Repository.Shareds
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
