using AutoMapper;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByPersonId
{
    public class GetAllPersonAddressByPersonIdQueryHandler : IRequestHandler<GetAllPersonAddressByPersonIdQuery, List<AddressDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPersonAddressByPersonIdQueryHandler> _logger;

        public GetAllPersonAddressByPersonIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllPersonAddressByPersonIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<List<AddressDto>> Handle(GetAllPersonAddressByPersonIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var addressList = await _unitOfWork.People.GetAllPersonAddressByPersonIdAsync(query.PersonId);
                return _mapper.Map<List<AddressDto>>(addressList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
