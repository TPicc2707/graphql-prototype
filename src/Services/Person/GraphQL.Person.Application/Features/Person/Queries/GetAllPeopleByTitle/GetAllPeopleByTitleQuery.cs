using GraphQL.Person.Application.Dtos.Person;
using MediatR;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeoplebyTitle
{
    public class GetAllPeopleByTitleQuery : IRequest<List<PersonDto>>
    {
        public string Title { get; set; }

        public GetAllPeopleByTitleQuery(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));   
        }
    }
}
