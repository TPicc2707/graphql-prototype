using GraphQL.Types;

namespace GraphQL.Person.API.GraphQL.InputTypes.Person
{
    public sealed class CreatePersonInputType : InputObjectGraphType
    {
        public CreatePersonInputType()
        {
            Name = "CreatePersonInputType";
            Description = "A person to be created.";

            _ = Field<StringGraphType>("FirstName", "Person's first name.");
            _ = Field<StringGraphType>("MiddleInitial", "Person's middle inital.");
            _ = Field<StringGraphType>("LastName", "Person's last name.");
            _ = Field<StringGraphType>("Title", "Person's title.");

        }
    }
}
