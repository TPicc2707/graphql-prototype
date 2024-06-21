using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Types;
using MediatR;

namespace GraphQL.Person.API.GraphQL.Types
{
    public sealed class PersonType : ObjectGraphType<PersonDto>
    {
        public PersonType(IMediator mediator)
        {
            this.Name = nameof(PersonDto);
            this.Description = "Person collection";

            _ = this.Field(m => m.PersonId).Description("Identifier of the person.");
            _ = this.Field(m => m.FirstName).Description("Person's first name.");
            _ = this.Field(m => m.MiddleInitial).Description("Person's middle inital.");
            _ = this.Field(m => m.LastName).Description("Person's last name.");
            _ = this.Field(m => m.Title).Description("Person's title.");
        }
    }
}
