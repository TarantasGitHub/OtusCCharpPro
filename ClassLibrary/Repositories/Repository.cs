using ClassLibraryContracts.Entities;
using ClassLibraryContracts.Models;
using ClassLibraryContracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace ClassLibrary.Repositories
{
    public abstract class Repository<TEntity, U, TSource> : IRepository<TSource, U>
        where TEntity : BaseEntity<U>
        where U : notnull
        where TSource : BaseEntityDto<U>
    {
        protected readonly DbContext _dataContext;
        protected readonly IMapper _mapper;

        public Repository(DbContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<TSource> AddAsync(TSource entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = _mapper.Map<TEntity>(entity);
            var result = await _dataContext
               .Set<TEntity>()
               .AddAsync(dbEntity, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TSource>(result.Entity);
        }

        public Task<IEnumerable<TSource>> FindAsync(Expression<Func<TSource, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TSource?> GetAsync(U id, CancellationToken cancellationToken = default)
        {
            var result = await _dataContext
               .Set<TEntity>()
               .FindAsync(id, cancellationToken);

            if(result == null)
            {
                return null;
            }

            return _mapper.Map<TSource>(result);
        }
    }
}
