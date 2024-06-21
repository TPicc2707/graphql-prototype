using MediatR;

namespace GraphQL.Address.Application.Dtos.Address
{
    public class CreateAddressDto : IRequest<AddressDto>
    {
        public Guid PersonId { get; set; }
        public string Type { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
