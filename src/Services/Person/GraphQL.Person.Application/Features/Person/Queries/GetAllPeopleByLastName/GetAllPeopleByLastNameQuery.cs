using GraphQL.Person.Application.Dtos.Person;
using MediatR;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByLastName
{
    public class GetAllPeopleByLastNameQuery : IRequest<List<PersonDto>>
    {
        public string LastName { get; set; }
        public GetAllPeopleByLastNameQuery(string lastName)
        {
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }
    }
}
