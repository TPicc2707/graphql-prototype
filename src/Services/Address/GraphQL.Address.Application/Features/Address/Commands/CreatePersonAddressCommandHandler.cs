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
    public class CreatePersonAddressCommandHandler : IRequestHandler<CreateAddressDto, AddressDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonAddressCommandHandler> _logger;
        private readonly IValidator<Domain.Entities.Address> _validator;

        public CreatePersonAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePersonAddressCommandHandler> logger, IValidator<Domain.Entities.Address> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public virtual async Task<AddressDto> Handle(CreateAddressDto request, CancellationToken cancellationToken)
        {
            try
            {
                var validatedAddress = await AddressBusinessValidator.ValidateAddressAsync(_unitOfWork, request, _mapper, _validator);

                var createdPersonAddress = await _unitOfWork.People.CreatePersonAddressAsync(validatedAddress);

                await _unitOfWork.Complete();

                var addressDto = _mapper.Map<AddressDto>(createdPersonAddress);

                var logMessage = string.Format("Person '{0}' has Address '{1}' was created.", addressDto.PersonId, addressDto.AddressId);
                _logger.LogInformation(logMessage);

                return addressDto;

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
