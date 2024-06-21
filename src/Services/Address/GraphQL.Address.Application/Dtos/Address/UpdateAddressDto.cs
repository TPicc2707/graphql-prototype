using MediatR;

namespace GraphQL.Address.Application.Dtos.Address
{
    public class UpdateAddressDto : IRequest<bool>
    {
        public Guid AddressId { get; set; }
        public Guid PersonId { get; set; }
        public string Type { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
