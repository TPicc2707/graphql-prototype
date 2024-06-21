using AutoMapper;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByType
{
    public class GetAllPersonAddressByTypeQueryHandler : IRequestHandler<GetAllPersonAddressByTypeQuery, List<AddressDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPersonAddressByTypeQueryHandler> _logger;


        public GetAllPersonAddressByTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllPersonAddressByTypeQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<List<AddressDto>> Handle(GetAllPersonAddressByTypeQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var addressList = await _unitOfWork.People.GetAllPersonAddressByTypeAsync(query.Type);
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
