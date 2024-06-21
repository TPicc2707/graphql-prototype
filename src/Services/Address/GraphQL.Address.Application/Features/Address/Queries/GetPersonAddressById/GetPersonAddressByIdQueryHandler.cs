using AutoMapper;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Features.Address.Queries.GetPersonAddressById
{
    public class GetPersonAddressByIdQueryHandler : IRequestHandler<GetPersonAddressByIdQuery, AddressDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonAddressByIdQueryHandler> _logger;

        public GetPersonAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetPersonAddressByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<AddressDto> Handle(GetPersonAddressByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _unitOfWork.People.GetPersonAddressByIdAsync(query.AddressId);

                if (address == null)
                {
                    Console.WriteLine("Address '{0}' was not found", query.AddressId);

                    return null;
                }

                var addressDto = _mapper.Map<AddressDto>(address);

                return addressDto;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
    }
}
