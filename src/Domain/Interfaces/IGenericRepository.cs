using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TeamHolidayPlanner.Domain
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> AllAsync();
        Task<IEnumerable<TEntity>> AllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindByIdAsync(int entityId);
        Task<TEntity> FindByIdAsync(int entityId, params Expression<Func<TEntity, object>>[] includes);

        Task UpdateAsync(TEntity entity);
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(int entityId);
    }
}