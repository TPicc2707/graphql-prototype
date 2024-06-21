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
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonDto, PersonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IValidator<Domain.Entities.Person> _validator;

        public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePersonCommandHandler> logger, 
                                          IValidator<Domain.Entities.Person> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public virtual async Task<PersonDto> Handle(CreatePersonDto request, CancellationToken cancellationToken)
        {
            try
            {
                var validatedPerson = await PersonBusinessValidator.ValidatePersonAsync(_unitOfWork, request, _mapper, _validator);

                var createdPerson = await _unitOfWork.People.AddAsync(validatedPerson);

                await _unitOfWork.Complete();

                var personDto = _mapper.Map<PersonDto>(createdPerson);

                var logMessage = string.Format("Person '{0}' was created.", personDto.PersonId);
                _logger.LogInformation(logMessage);

                return personDto;

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
