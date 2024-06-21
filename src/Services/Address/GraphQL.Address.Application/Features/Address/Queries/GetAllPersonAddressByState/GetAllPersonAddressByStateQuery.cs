using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByState
{
    public class GetAllPersonAddressByStateQuery : IRequest<List<AddressDto>>
    {
        public string State { get; set; }
        public GetAllPersonAddressByStateQuery(string state)
        {

            State = state ?? throw new ArgumentNullException(nameof(state));

        }
    }
}
