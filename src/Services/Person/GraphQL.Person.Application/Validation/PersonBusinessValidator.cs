using AutoMapper;
using FluentValidation;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Domain.Contracts;
using GraphQL.Person.Infrastructure.Exceptions;
using ValidationException = GraphQL.Person.Domain.Exceptions.ValidationException;

namespace GraphQL.Person.Application.Validation
{
    public static class PersonBusinessValidator
    {
        public static async Task<Domain.Entities.Person> ValidatePersonAsync(IUnitOfWork unitOfWork, object person, IMapper mapper, IValidator<Domain.Entities.Person> personValidator)
        {
            Domain.Entities.Person validatedPerson = new();

            if (person is CreatePersonDto)
                validatedPerson = mapper.Map<Domain.Entities.Person>(person);
            else
            {
                var updatePersonDto = mapper.Map<UpdatePersonDto>(person);

                validatedPerson = await unitOfWork.People.GetByIdAsync(updatePersonDto.PersonId);

                if (validatedPerson == null)
                    throw new NotFoundException("PersonId", updatePersonDto.PersonId);

                mapper.Map(updatePersonDto, validatedPerson, typeof(UpdatePersonDto), typeof(Domain.Entities.Person));
            }

            await ValidatePersonAsync(personValidator, validatedPerson);

            return validatedPerson;
        }

        private static async Task CheckForExistingPersonAsync(IUnitOfWork unitOfWork, Guid personId)
        {
            if (!await unitOfWork.People.ValidatePersonAsync(personId))
                throw new NotFoundException("PersonId", personId);
        }

        private static async Task ValidatePersonAsync(IValidator<Domain.Entities.Person> validator, Domain.Entities.Person person)
        {
            var personValidationResult = await validator.ValidateAsync(person);

            if (!personValidationResult.IsValid)
                throw new ValidationException(personValidationResult.Errors);
        }

    }
}
