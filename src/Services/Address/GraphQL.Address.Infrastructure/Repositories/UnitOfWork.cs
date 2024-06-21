using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Domain.Entities;
using GraphQL.Address.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private PersonContext _personContext;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly ILogger<Person> _personLogger;

        public UnitOfWork(PersonContext personContext, ILogger<Person> personLogger, ILogger<UnitOfWork> logger)
        {
            _personContext = personContext ?? throw new ArgumentNullException(nameof(personContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personLogger = personLogger ?? throw new ArgumentNullException(nameof(personLogger));
            InitializeRepositories();
        }

        public IPersonRepository People { get; private set; } = null!;

        public virtual async Task<int> Complete()
        {
            try
            {
                var saved = await _personContext.SaveChangesAsync();
                _logger.LogInformation("Data was saved to database.");
                return saved;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
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

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _personContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void InitializeRepositories()
        {
            People = new PersonRepository(_personContext, _personLogger);
        }


    }
}
