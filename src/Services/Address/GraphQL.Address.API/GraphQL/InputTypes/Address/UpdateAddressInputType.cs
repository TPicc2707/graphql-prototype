using GraphQL.Types;

namespace GraphQL.Address.API.GraphQL.InputTypes.Address
{
    public sealed class UpdateAddressInputType : InputObjectGraphType
    {
        public UpdateAddressInputType()
        {
            Name = "UpdatePersonAddressInputType";
            Description = "A person's address to be updated.";

            _ = Field<IdGraphType>("AddressId", "Id of the address.");
            _ = Field<IdGraphType>("PersonId", "Id of the person.");
            _ = Field<StringGraphType>("Type", "A type of address.");
            _ = Field<StringGraphType>("Street", "Street name of address.");
            _ = Field<StringGraphType>("City", "City of address.");
            _ = Field<StringGraphType>("State", "State of address.");
            _ = Field<StringGraphType>("ZipCode", "Zip Code of address.");

        }
    }
}
