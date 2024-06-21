using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByType
{
    public class GetAllPersonAddressByTypeQuery : IRequest<List<AddressDto>>
    {
        public string Type { get; set; }

        public GetAllPersonAddressByTypeQuery(string type)
        {

            Type = type ?? throw new ArgumentNullException(nameof(type));

        }
    }
}
