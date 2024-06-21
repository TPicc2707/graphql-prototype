using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddress
{
    public class GetAllPersonAddressQuery : IRequest<List<AddressDto>>
    {
        public GetAllPersonAddressQuery()
        {
            
        }
    }
}
