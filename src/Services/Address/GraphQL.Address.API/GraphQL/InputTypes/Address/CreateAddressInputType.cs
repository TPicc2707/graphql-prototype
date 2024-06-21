using GraphQL.Types;

namespace GraphQL.Address.API.GraphQL.InputTypes.Address
{
    public sealed class CreateAddressInputType : InputObjectGraphType
    {
        public CreateAddressInputType()
        {
            Name = "CreatePersonAddressInputType";
            Description = "A person's address to be created.";

            _ = Field<IdGraphType>("PersonId", "A Person's id.");
            _ = Field<StringGraphType>("Type", "A type of address.");
            _ = Field<StringGraphType>("Street", "Street name of address.");
            _ = Field<StringGraphType>("City", "City of address.");
            _ = Field<StringGraphType>("State", "State of address.");
            _ = Field<StringGraphType>("ZipCode", "Zip Code of address.");

        }
    }
}
