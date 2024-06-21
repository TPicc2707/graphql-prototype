using AutoMapper;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.Address.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Features.Person.Commandss
{
    public class UpdatePersonEventCommandHandler : IRequestHandler<UpdatePersonEventDto, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonEventCommandHandler> _logger;

        public UpdatePersonEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdatePersonEventCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(UpdatePersonEventDto request, CancellationToken cancellationToken)
        {
            try
            {
                var person = _mapper.Map<Domain.Entities.Person>(request);
                _unitOfWork.People.Update(person);
                await _unitOfWork.Complete();

                _logger.LogInformation($"Person {person.PersonId} is successfully updated.");

                return person.PersonId;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Person could not be updated {request.PersonId}. {ex.Message}");
                throw;
            }
        }
    }
}
