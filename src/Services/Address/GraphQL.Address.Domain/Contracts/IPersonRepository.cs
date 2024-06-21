using GraphQL.Address.Domain.Entities;

namespace GraphQL.Address.Domain.Contracts
{
    public interface IPersonRepository : IAsyncRepository<Person>
    {
        Task<Entities.Address> CreatePersonAddressAsync(Entities.Address address);
        Task UpdatePersonAddressAsync(Entities.Address address);
        Task DeletePersonAddressAsync(Entities.Address address);
        Task<Entities.Address> GetPersonAddressByIdAsync(Guid addressID);
        Task<IReadOnlyList<Entities.Address>> GetAllPersonAddressesAsync();
        Task<IReadOnlyList<Entities.Address>> GetAllPersonAddressByPersonIdAsync(Guid personId);
        Task<IReadOnlyList<Entities.Address>> GetAllPersonAddressByTypeAsync(string type);
        Task<IReadOnlyList<Entities.Address>> GetAllPersonAddressByStreetAsync(string street);
        Task<IReadOnlyList<Entities.Address>> GetAllPersonAddressByCityAsync(string city);
        Task<IReadOnlyList<Entities.Address>> GetAllPersonAddressByStateAsync(string state);
        Task<IReadOnlyList<Entities.Address>> GetAllPersonAddressByZipCodeAsync(string zipCode);
        Task<bool> ValidatePersonAsync(Guid personId);
    }
}
