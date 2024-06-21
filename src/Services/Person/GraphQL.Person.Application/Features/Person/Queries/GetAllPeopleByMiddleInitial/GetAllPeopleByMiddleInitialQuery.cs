using GraphQL.Person.Application.Dtos.Person;
using MediatR;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeoplebyMiddleInitial
{
    public class GetAllPeopleByMiddleInitialQuery : IRequest<List<PersonDto>>
    {
        public string MiddleInitial { get; set; }

        public GetAllPeopleByMiddleInitialQuery(string middleInitial)
        {
            MiddleInitial = middleInitial ?? throw new ArgumentNullException(nameof(middleInitial));
        }
    }
}
