using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetPersonAddressById
{
    public class GetPersonAddressByIdQuery : IRequest<AddressDto>
    {
        public Guid AddressId { get; set; }

        public GetPersonAddressByIdQuery(Guid addressId)
        {
            AddressId = addressId;
        }
    }
}
