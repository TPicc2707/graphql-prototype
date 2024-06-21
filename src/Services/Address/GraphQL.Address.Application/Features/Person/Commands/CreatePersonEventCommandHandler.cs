using AutoMapper;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.Address.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Features.Person.Commands
{
    public class CreatePersonEventCommandHandler : IRequestHandler<CreatePersonEventDto, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonEventCommandHandler> _logger;

        public CreatePersonEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePersonEventCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(CreatePersonEventDto request, CancellationToken cancellationToken)
        {
            try
            {
                var person = _mapper.Map<Domain.Entities.Person>(request);
                var newPerson = await _unitOfWork.People.AddAsync(person);
                await _unitOfWork.Complete();

                _logger.LogInformation($"Person {newPerson.PersonId} is successfully created.");

                return person.PersonId;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Person could not be created {request.PersonId}. {ex.Message}");
                throw;
            }
        }
    }
}
