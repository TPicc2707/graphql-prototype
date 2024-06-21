namespace GraphQL.Address.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IPersonRepository People { get; }

        Task<int> Complete();
    }
}
