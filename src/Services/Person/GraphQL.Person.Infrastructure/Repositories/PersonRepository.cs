using GraphQL.Person.Domain.Contracts;
using GraphQL.Person.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Infrastructure.Repositories
{
    public class PersonRepository : BaseRepository<Domain.Entities.Person>, IPersonRepository
    {
        private readonly ILogger<Domain.Entities.Person> _logger;

        public PersonRepository(PersonContext personDbContext, ILogger<Domain.Entities.Person> logger) : base(personDbContext, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Person

        public virtual async Task<IReadOnlyList<Domain.Entities.Person>> GetAllPeopleByFirstNameAsync(string firstName)
        {
            try
            {
                return await _personContext.People
                        .Where(s => s.FirstName.Contains(firstName))
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<IReadOnlyList<Domain.Entities.Person>> GetAllPeopleByLastNameAsync(string lastName)
        {
            try
            {
                return await _personContext.People
                        .Where(s => s.LastName.Contains(lastName))
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<IReadOnlyList<Domain.Entities.Person>> GetAllPeoplebyMiddleInitialAsync(string middleInitial)
        {
            try
            {
                return await _personContext.People
                        .Where(s => s.MiddleInitial == middleInitial)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<IReadOnlyList<Domain.Entities.Person>> GetAllPeoplebyTitleAsync(string title)
        {
            try
            {
                return await _personContext.People
                        .Where(s => s.Title.Contains(title))
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> ValidatePersonAsync(Guid personId)
        {
            return await _personContext.People.AnyAsync(i => i.PersonId == personId);
        }

        #endregion

    }
}
