using System.Linq.Expressions;

namespace StudentTracking.Data.EntityFramework.Base
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void AddOrUpdate<T>(T entity) where T : class, new();
        void AddOrUpdateRange<T>(IEnumerable<T> entities) where T : class, new();
        void ClearTrackingAndUpdate(T entity);
        void ClearTrackingAndDelete(T entity);

        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindListAsNoTrackingAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsNoTrackingAsync();
        IQueryable<T> Queryable();
    }
}
