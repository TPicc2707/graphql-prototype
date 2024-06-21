using MediatR;

namespace GraphQL.Person.Application.Dtos.Person
{
    public class UpdatePersonDto : IRequest<bool>
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
}
