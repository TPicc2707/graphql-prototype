using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByZipCode
{
    public class GetAllPersonAddressByZipCodeQuery : IRequest<List<AddressDto>>
    {
        public string ZipCode { get; set; }

        public GetAllPersonAddressByZipCodeQuery(string zipCode)
        {
            ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        }
    }
}
