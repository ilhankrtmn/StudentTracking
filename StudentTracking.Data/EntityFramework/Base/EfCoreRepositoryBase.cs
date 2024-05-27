using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StudentTracking.Data.EntityFramework.Base
{
    public class EfCoreRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _context;

        public EfCoreRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }


        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().Where(expression).SingleOrDefaultAsync();
        }

        public async Task<TEntity> FindAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>()
            .Where(expression)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindListAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _context.Set<TEntity>()
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _context.Set<TEntity>()
                .Where(expression)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void ClearTrackingAndUpdate(TEntity entity)
        {
            _context.ChangeTracker.Clear();
            _context.Set<TEntity>().Update(entity);
        }

        public void ClearTrackingAndDelete(TEntity entity)
        {
            _context.ChangeTracker.Clear();
            _context.Set<TEntity>().Remove(entity);
        }

        public void AddOrUpdate<TEntity>(TEntity entity) where TEntity : class, new()
        {
            AddOrUpdateRange(new[] { entity });
        }

        public void AddOrUpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            foreach (var entity in entities)
            {
                var IdProperty = entity.GetType().GetProperty("Id").GetValue(entity, null);
                var id = IdProperty as object[];

                var tracked = (id != null)
                    ? _context.Set<TEntity>().Find(id)
                    : _context.Set<TEntity>().Find(IdProperty);

                if (tracked != null)
                {
                    _context.Entry(tracked).CurrentValues.SetValues(entity);
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Added;
                }
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> Queryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }
}
