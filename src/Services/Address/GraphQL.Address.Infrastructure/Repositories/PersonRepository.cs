using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Domain.Entities;
using GraphQL.Address.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Infrastructure.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly ILogger<Person> _logger;

        public PersonRepository(PersonContext personDbContext, ILogger<Person> logger) : base(personDbContext, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Address

        public virtual async Task<IReadOnlyList<Domain.Entities.Address>> GetAllPersonAddressesAsync()
        {
            try
            {
                return await _personContext.Addresses.Include(a => a.Person).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<Domain.Entities.Address> GetPersonAddressByIdAsync(Guid addressId)
        {
            try
            {
                return await _personContext.Addresses
                        .Where(s => s.AddressId == addressId)
                        .Include(s => s.Person)
                        .FirstOrDefaultAsync();
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

        public virtual async Task<IReadOnlyList<Domain.Entities.Address>> GetAllPersonAddressByCityAsync(string city)
        {
            try
            {
                return await _personContext.Addresses
                        .Where(s => s.City.Contains(city))
                        .Include(s => s.Person)
                        .ToListAsync();
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

        public virtual async Task<IReadOnlyList<Domain.Entities.Address>> GetAllPersonAddressByPersonIdAsync(Guid personId)
        {
            try
            {
                return await _personContext.Addresses
                        .Where(s => s.PersonId == personId)
                        .Include(s => s.Person)
                        .ToListAsync();
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

        public virtual async Task<IReadOnlyList<Domain.Entities.Address>> GetAllPersonAddressByStateAsync(string state)
        {
            try
            {
                return await _personContext.Addresses
                        .Where(s => s.State.Contains(state))
                        .Include(s => s.Person)
                        .ToListAsync();
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

        public virtual async Task<IReadOnlyList<Domain.Entities.Address>> GetAllPersonAddressByStreetAsync(string street)
        {
            try
            {
                return await _personContext.Addresses
                        .Where(s => s.Street.Contains(street))
                        .Include(s => s.Person)
                        .ToListAsync();
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

        public virtual async Task<IReadOnlyList<Domain.Entities.Address>> GetAllPersonAddressByTypeAsync(string type)
        {
            try
            {
                return await _personContext.Addresses
                        .Where(s => s.Type == type)
                        .Include(s => s.Person)
                        .ToListAsync();
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

        public virtual async Task<IReadOnlyList<Domain.Entities.Address>> GetAllPersonAddressByZipCodeAsync(string zipCode)
        {
            try
            {
                return await _personContext.Addresses
                        .Where(s => s.ZipCode.Contains(zipCode))
                        .Include(s => s.Person)
                        .ToListAsync();
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

        public virtual async Task<Domain.Entities.Address> CreatePersonAddressAsync(Domain.Entities.Address address)
        {
            try
            {
                var person = await GetByIdWithChildrenAsync(address.PersonId);
                person.Addresses.Add(address);

                _logger.LogInformation($"Object {address.AddressId} was added to table.");

                return address;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (NullReferenceException ex)
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

        public virtual async Task UpdatePersonAddressAsync(Domain.Entities.Address address)
        {
            try
            {
                var person = await GetByIdWithChildrenAsync(address.PersonId);
                var existingAddress = person.Addresses.Where(i => i.AddressId == address.AddressId).FirstOrDefault();
                person.Addresses.Remove(existingAddress);
                person.Addresses.Add(address);

                _personContext.Entry(person).State = EntityState.Modified;

                _logger.LogInformation($"Object {address.AddressId} was updated in table.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (NullReferenceException ex)
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

        public virtual async Task DeletePersonAddressAsync(Domain.Entities.Address address)
        {
            try
            {
                var person = await GetByIdWithChildrenAsync(address.PersonId);
                person.Addresses.Remove(address);
                _logger.LogInformation($"Object {address.AddressId} was deleted from table.");
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
