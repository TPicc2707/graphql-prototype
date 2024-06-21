using MediatR;

namespace GraphQL.Address.Application.Dtos.Address
{
    public class DeleteAddressDto : IRequest<bool>
    {
        public Guid AddressId { get; set; }
    }
}
