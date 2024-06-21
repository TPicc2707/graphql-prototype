using MediatR;

namespace GraphQL.Person.Application.Dtos.Person
{
    public class CreatePersonDto : IRequest<PersonDto>
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
}
