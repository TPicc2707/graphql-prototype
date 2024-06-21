using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Application.Features.Person.Queries.GetPersonById
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonByIdQueryHandler> _logger;

        public GetPersonByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetPersonByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<PersonDto> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var person = await _unitOfWork.People.GetByIdAsync(query.PersonId);

                if(person == null)
                {
                    _logger.LogInformation("Person '{0}' was not found", query.PersonId);

                    return null;
                }

                var personDto = _mapper.Map<PersonDto>(person);

                return personDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
