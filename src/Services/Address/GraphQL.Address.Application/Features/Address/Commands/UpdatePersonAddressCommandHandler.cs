using AutoMapper;
using FluentValidation;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Application.Validation;
using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = GraphQL.Address.Domain.Exceptions.ValidationException;

namespace GraphQL.Address.Application.Features.Address.Commands
{
    public class UpdatePersonAddressCommandHandler : IRequestHandler<UpdateAddressDto, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonAddressCommandHandler> _logger;
        private readonly IValidator<Domain.Entities.Address> _validator;


        public UpdatePersonAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdatePersonAddressCommandHandler> logger, IValidator<Domain.Entities.Address> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public virtual async Task<bool> Handle(UpdateAddressDto request, CancellationToken cancellationToken)
        {
            try
            {
                var validatedAddress = await AddressBusinessValidator.ValidateAddressAsync(_unitOfWork, request, _mapper, _validator);

                await _unitOfWork.People.UpdatePersonAddressAsync(validatedAddress);

                await _unitOfWork.Complete();

                var logMessage = string.Format("Person '{0}' has Address '{1}' was updated.", validatedAddress.PersonId, validatedAddress.AddressId);
                _logger.LogInformation(logMessage);

                return true;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw;
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
