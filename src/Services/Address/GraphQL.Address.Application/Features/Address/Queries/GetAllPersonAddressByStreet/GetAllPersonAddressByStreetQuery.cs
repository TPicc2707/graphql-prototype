using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByStreet
{
    public class GetAllPersonAddressByStreetQuery : IRequest<List<AddressDto>>
    {
        public string Street { get; set; }

        public GetAllPersonAddressByStreetQuery(string street)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
        }
    }
}
