using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace GraphQL.Address.Application.Features.Address.Commands
{
    public class DeletePersonAddressCommandHandler : IRequestHandler<DeleteAddressDto, bool>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeletePersonAddressCommandHandler> _logger;

        public DeletePersonAddressCommandHandler(IUnitOfWork unitOfWork, ILogger<DeletePersonAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }


        public virtual async Task<bool> Handle(DeleteAddressDto request, CancellationToken cancellationToken)
        {
            try
            {
                var personAddressDelete = await _unitOfWork.People.GetPersonAddressByIdAsync(request.AddressId);
                if (personAddressDelete == null)
                {
                    _logger.LogInformation("No Address with '{0}' was found.", request.AddressId);
                    throw new NotFoundException("This address could not be found", request);
                }
                else
                {
                    await _unitOfWork.People.DeletePersonAddressAsync(personAddressDelete);

                    await _unitOfWork.Complete();

                    _logger.LogInformation("Person with Address '{0}' was deleted.", request.AddressId);

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
