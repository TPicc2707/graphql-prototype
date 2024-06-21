using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeoplebyMiddleInitial
{
    public class GetAllPeopleByMiddleInitialQueryHandler : IRequestHandler<GetAllPeopleByMiddleInitialQuery, List<PersonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPeopleByMiddleInitialQueryHandler> _logger;

        public GetAllPeopleByMiddleInitialQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllPeopleByMiddleInitialQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<List<PersonDto>> Handle(GetAllPeopleByMiddleInitialQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var peopleList = await _unitOfWork.People.GetAllPeoplebyMiddleInitialAsync(query.MiddleInitial);
                return _mapper.Map<List<PersonDto>>(peopleList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
