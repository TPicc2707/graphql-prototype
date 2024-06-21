using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByCity
{
    public class GetAllPersonAddressByCityQuery : IRequest<List<AddressDto>>
    {
        public string City { get; set; }

        public GetAllPersonAddressByCityQuery(string city)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));
        }
    }
}
