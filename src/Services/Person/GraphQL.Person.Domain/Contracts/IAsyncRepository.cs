using System.Linq.Expressions;

namespace GraphQL.Person.Domain.Contracts
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithChildrenAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            bool disableTracking = true);
        Task<T> GetByIdAsync(Guid Id);
        Task<T> GetByIdWithChildrenAsync(Guid Id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
