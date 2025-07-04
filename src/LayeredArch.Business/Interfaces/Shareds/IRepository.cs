using LayeredArch.Business.Models.Shareds;
using System.Linq.Expressions;

namespace LayeredArch.Business.Interfaces.Shareds
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        /* Persistence methods (read and write) */

        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task<int> SaveChanges();

    }
}
