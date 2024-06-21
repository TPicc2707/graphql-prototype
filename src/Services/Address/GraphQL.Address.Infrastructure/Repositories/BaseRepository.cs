using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace GraphQL.Address.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        internal PersonContext _personContext;
        internal DbSet<T> dbSet;
        private readonly ILogger<T> _logger;

        public BaseRepository(PersonContext personContext, ILogger<T> logger)
        {
            _personContext = personContext ?? throw new ArgumentNullException(nameof(personContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            dbSet = _personContext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                _logger.LogInformation($"Object was added to table.");
                return entity;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
                _logger.LogInformation($"Object was deleted from table.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<IReadOnlyList<T>> GetAllWithChildrenAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, bool disableTracking = true)
        {
            try
            {
                IQueryable<T> query = _personContext.Set<T>();
                if (disableTracking) query = query.AsNoTracking();

                foreach (var property in _personContext.Model.FindEntityType(typeof(T)).GetNavigations())
                {
                    query = query.Include(property.Name);
                }

                if (predicate != null) query = query.Where(predicate);

                if (orderby != null)
                    return await orderby(query).ToListAsync();

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid Id)
        {
            try
            {
                return await dbSet.FindAsync(Id);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<T> GetByIdWithChildrenAsync(Guid Id)
        {
            try
            {
                var model = await dbSet.FindAsync(Id);

                if (model != null)
                {
                    foreach (var property in _personContext.Model.FindEntityType(typeof(T)).GetNavigations())
                    {
                        //Look more into this issue
                        //if (property.PropertyInfo.PropertyType.GetInterfaces().Contains(typeof(IEnumerable<>))) 
                        //    await _personContext.Entry(model).Collection(property.Name).LoadAsync();
                        //else
                        //    await _personContext.Entry(model).Reference(property.Name).LoadAsync();

                        await _personContext.Entry(model).Collection(property.Name).LoadAsync();
                    }

                    return model;
                }

                return model;
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                dbSet.Attach(entity);
                _personContext.Entry(entity).State = EntityState.Modified;
                _logger.LogInformation($"Object was updated in table.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
