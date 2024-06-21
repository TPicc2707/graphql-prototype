using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByLastName
{
    public class GetAllPeopleByLastNameQueryHandler : IRequestHandler<GetAllPeopleByLastNameQuery, List<PersonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPeopleByLastNameQueryHandler> _logger;

        public GetAllPeopleByLastNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllPeopleByLastNameQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<List<PersonDto>> Handle(GetAllPeopleByLastNameQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var peopleList = await _unitOfWork.People.GetAllPeopleByLastNameAsync(query.LastName);
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
