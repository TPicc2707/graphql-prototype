using MediatR;

namespace GraphQL.Address.Application.Dtos.Person
{
    public class UpdatePersonEventDto : IRequest<Guid>
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
}
