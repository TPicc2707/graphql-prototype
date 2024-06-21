using GraphQL.Person.Application.Dtos.Person;
using MediatR;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeople
{
    public class GetAllPeopleQuery : IRequest<List<PersonDto>>
    {
        public GetAllPeopleQuery()
        {
            
        }
    }
}
