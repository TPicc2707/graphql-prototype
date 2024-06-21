using MediatR;

namespace GraphQL.Person.Application.Dtos.Person
{
    public class DeletePersonDto : IRequest<bool>
    {
        public Guid PersonId { get; set; }
    }
}
