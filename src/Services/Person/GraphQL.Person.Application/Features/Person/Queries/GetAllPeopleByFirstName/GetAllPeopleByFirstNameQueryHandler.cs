using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByFirstName
{
    public class GetAllPeopleByFirstNameQueryHandler : IRequestHandler<GetAllPeopleByFirstNameQuery, List<PersonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPeopleByFirstNameQueryHandler> _logger;

        public GetAllPeopleByFirstNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllPeopleByFirstNameQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<List<PersonDto>> Handle(GetAllPeopleByFirstNameQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var peopleList = await _unitOfWork.People.GetAllPeopleByFirstNameAsync(query.FirstName);
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
