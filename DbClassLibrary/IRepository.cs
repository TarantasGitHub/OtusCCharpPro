using System.Linq.Expressions;

namespace DbClassLibrary
{
    public interface IRepository<T, U> where T : BaseEntity<U> where U : notnull
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(T[] entites, CancellationToken cancellationToken = default);

        Task DeleteAsync(U id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T?> GetAsync(U id, CancellationToken cancellationToken = default);

        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);                
    }
}