using MediatR;

namespace GraphQL.Address.Application.Dtos.Person
{
    public class DeletePersonEventDto : IRequest<Guid>
    {
        public Guid PersonId { get; set; }
    }
}
