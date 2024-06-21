using AutoMapper;
using FluentValidation;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Application.Validation;
using GraphQL.Person.Domain.Contracts;
using GraphQL.Person.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = GraphQL.Person.Domain.Exceptions.ValidationException;

namespace GraphQL.Person.Application.Features.Person.Commands
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonDto, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonCommandHandler> _logger;
        private readonly IValidator<Domain.Entities.Person> _validator;

        public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdatePersonCommandHandler> logger,
                                          IValidator<Domain.Entities.Person> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public virtual async Task<bool> Handle(UpdatePersonDto request, CancellationToken cancellationToken)
        {
            try
            {
                var validatedPerson = await PersonBusinessValidator.ValidatePersonAsync(_unitOfWork, request, _mapper, _validator);

                _unitOfWork.People.Update(validatedPerson);

                await _unitOfWork.Complete();

                var logMessage = string.Format("Person '{0}' was updated.", validatedPerson.PersonId);
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
