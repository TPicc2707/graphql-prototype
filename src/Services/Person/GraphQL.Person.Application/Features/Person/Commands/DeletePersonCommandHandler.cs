using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Domain.Contracts;
using GraphQL.Person.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace GraphQL.Person.Application.Features.Person.Commands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonDto, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeletePersonCommandHandler> _logger;

        public DeletePersonCommandHandler(IUnitOfWork unitOfWork, ILogger<DeletePersonCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public virtual async Task<bool> Handle(DeletePersonDto request, CancellationToken cancellationToken)
        {
            try
            {
                var personDelete = await _unitOfWork.People.GetByIdAsync(request.PersonId);
                if (personDelete == null)
                {
                    _logger.LogInformation("No Person with '{0}' was found.", request.PersonId);
                    throw new NotFoundException("This person could not be found", request);
                }
                else
                {
                    _unitOfWork.People.Delete(personDelete);

                    await _unitOfWork.Complete();

                    _logger.LogInformation("Person '{0}' was deleted.", request.PersonId);

                    return true;
                }
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
