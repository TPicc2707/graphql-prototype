using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Types;
using MediatR;

namespace GraphQL.Address.API.GraphQL.Types
{
    public sealed class AddressType : ObjectGraphType<AddressDto>
    {
        public AddressType(IMediator mediator)
        {
            this.Name = nameof(AddressDto);
            this.Description = "Person's Address collection";

            _ = this.Field(m => m.AddressId).Description("Identifier of the address.");
            _ = this.Field(m => m.PersonId).Description("Identifier of the person with address.");
            _ = this.Field(m => m.Type).Description("Type of address.");
            _ = this.Field(m => m.Street).Description("Street address of person.");
            _ = this.Field(m => m.City).Description("City of address for person.");
            _ = this.Field(m => m.State).Description("State of address for person.");
            _ = this.Field(m => m.ZipCode).Description("Zip Code of address for person.");
            _ = this.Field(
                name: "Person",
                description: "Person assoicated to addresses.",
                type: typeof(PersonType),
                resolve: m => m.Source?.Person);

        }
    }
}
