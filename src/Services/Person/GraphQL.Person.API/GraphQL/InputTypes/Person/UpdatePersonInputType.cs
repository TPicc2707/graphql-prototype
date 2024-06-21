using GraphQL.Types;

namespace GraphQL.Person.API.GraphQL.InputTypes.Person
{
    public sealed class UpdatePersonInputType : InputObjectGraphType
    {
        public UpdatePersonInputType()
        {
            Name = "UpdatePersonInputType";
            Description = "A person to be updated.";

            _ = Field<IdGraphType>("PersonId", "Id of the person.");
            _ = Field<StringGraphType>("FirstName", "Person's first name.");
            _ = Field<StringGraphType>("MiddleInitial", "Person's middle inital.");
            _ = Field<StringGraphType>("LastName", "Person's last name.");
            _ = Field<StringGraphType>("Title", "Person's title.");

        }
    }
}
