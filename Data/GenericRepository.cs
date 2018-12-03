using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> AllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetNonTrackingQuery(includes);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetNonTrackingQuery(includes);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FindByIdAsync(int entityId)
        {
            var lambda = BuildIdLambda(entityId);

            return await dbSet.AsNoTracking().SingleOrDefaultAsync(lambda);
        }
        public async Task<TEntity> FindByIdAsync(int entityId, params Expression<Func<TEntity, object>>[] includes)
        {
            var lambda = BuildIdLambda(entityId);
            var query = GetNonTrackingQuery(includes);
            return await query.SingleOrDefaultAsync(lambda);
        }

        public async Task CreateAsync(TEntity entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int entityId)
        {
            var entity = await FindByIdAsync(entityId);

            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        private Expression<Func<TEntity, bool>> BuildIdLambda(dynamic id)
        {
            var item = Expression.Parameter(typeof(TEntity), "entity");
            var prop = Expression.Property(item, typeof(TEntity).Name + "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(prop, value);
            return Expression.Lambda<Func<TEntity, bool>>(equal, item);
        }
        private IQueryable<TEntity> GetNonTrackingQuery(Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.AsNoTracking();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
