using ClassLibraryContracts.Models;
using System.Linq.Expressions;

namespace ClassLibraryContracts.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntityDto<TKey> where TKey : notnull
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    }
}
