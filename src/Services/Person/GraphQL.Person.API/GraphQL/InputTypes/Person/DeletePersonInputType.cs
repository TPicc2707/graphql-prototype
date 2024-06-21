using GraphQL.Types;

namespace GraphQL.Person.API.GraphQL.InputTypes.Person
{
    public class DeletePersonInputType : InputObjectGraphType
    {
        public DeletePersonInputType()
        {
            Name = "DeletePersonInputType";
            Description = "A person to be deleted.";

            _ = Field<IdGraphType>("PersonId", "Id of the person.");

        }
    }
}
