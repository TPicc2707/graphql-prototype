using GraphQL.Types;

namespace GraphQL.Address.API.GraphQL.InputTypes.Address
{
    public sealed class DeleteAddressInputType : InputObjectGraphType
    {
        public DeleteAddressInputType()
        {
            Name = "DeletePersonAddressInputType";
            Description = "A person's address to be deleted.";

            _ = Field<IdGraphType>("AddressId", "Id of the address.");

        }

    }
}
