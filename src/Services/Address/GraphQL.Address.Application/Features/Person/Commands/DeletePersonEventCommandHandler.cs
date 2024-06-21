using AutoMapper;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.Address.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Features.Person.Commands
{
    public class DeletePersonEventCommandHandler : IRequestHandler<DeletePersonEventDto, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeletePersonEventCommandHandler> _logger;

        public DeletePersonEventCommandHandler(IUnitOfWork unitOfWork, ILogger<DeletePersonEventCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(DeletePersonEventDto request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await _unitOfWork.People.GetByIdAsync(request.PersonId);
                _unitOfWork.People.Delete(person);
                await _unitOfWork.Complete();

                _logger.LogInformation($"Person {person.PersonId} is successfully deleted.");

                return person.PersonId;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Person could not be deleted {request.PersonId}. {ex.Message}");
                throw;
            }
        }
    }
}
