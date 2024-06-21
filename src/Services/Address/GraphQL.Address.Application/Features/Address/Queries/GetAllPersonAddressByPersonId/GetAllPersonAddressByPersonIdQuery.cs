using GraphQL.Address.Application.Dtos.Address;
using MediatR;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByPersonId
{
    public class GetAllPersonAddressByPersonIdQuery : IRequest<List<AddressDto>>
    {
        public Guid PersonId { get; set; }

        public GetAllPersonAddressByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
