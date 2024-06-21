namespace GraphQL.Person.Domain.Contracts
{
    public interface IPersonRepository : IAsyncRepository<Entities.Person>
    {
        Task<IReadOnlyList<Entities.Person>> GetAllPeopleByFirstNameAsync(string firstName);
        Task<IReadOnlyList<Entities.Person>> GetAllPeopleByLastNameAsync(string lastName);
        Task<IReadOnlyList<Entities.Person>> GetAllPeoplebyMiddleInitialAsync(string middleInitial);
        Task<IReadOnlyList<Entities.Person>> GetAllPeoplebyTitleAsync(string title);
        Task<bool> ValidatePersonAsync(Guid personId);
    }
}
