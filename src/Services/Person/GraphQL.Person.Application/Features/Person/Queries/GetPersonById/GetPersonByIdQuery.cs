using GraphQL.Person.Application.Dtos.Person;
using MediatR;

namespace GraphQL.Person.Application.Features.Person.Queries.GetPersonById
{
    public class GetPersonByIdQuery : IRequest<PersonDto>
    {
        public Guid PersonId { get; set; }

        public GetPersonByIdQuery(Guid personId)
        {
            PersonId = personId;   
        }
    }
}
