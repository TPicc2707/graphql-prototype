using GraphQL.Person.Application.Dtos.Person;
using MediatR;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByFirstName
{
    public class GetAllPeopleByFirstNameQuery : IRequest<List<PersonDto>>
    {
        public string FirstName { get; set; }

        public GetAllPeopleByFirstNameQuery(string firstName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        }
    }
}
