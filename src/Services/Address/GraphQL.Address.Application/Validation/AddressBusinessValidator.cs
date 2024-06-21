using AutoMapper;
using FluentValidation;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Infrastructure.Exceptions;

namespace GraphQL.Address.Application.Validation
{
    public static class AddressBusinessValidator
    {
        public static async Task<Domain.Entities.Address> ValidateAddressAsync(IUnitOfWork unitOfWork, object address, IMapper mapper, IValidator<Domain.Entities.Address> addressValidator)
        {
            Domain.Entities.Address validatedAddress = new();

            if (address is CreateAddressDto)
                validatedAddress = mapper.Map<Domain.Entities.Address>(address);
            else
            {
                var updateAddressDto = mapper.Map<UpdateAddressDto>(address);

                validatedAddress = await unitOfWork.People.GetPersonAddressByIdAsync(updateAddressDto.AddressId);

                if (validatedAddress == null)
                    throw new NotFoundException("AddressId", updateAddressDto.AddressId);

                mapper.Map(updateAddressDto, validatedAddress, typeof(UpdateAddressDto), typeof(Domain.Entities.Address));
            }

            await CheckForExistingPersonAsync(unitOfWork, validatedAddress.PersonId);

            await ValidateAddressAsync(addressValidator, validatedAddress);

            return validatedAddress;
        }

        private static async Task CheckForExistingPersonAsync(IUnitOfWork unitOfWork, Guid personId)
        {
            if (!await unitOfWork.People.ValidatePersonAsync(personId))
                throw new NotFoundException("PersonId", personId);
        }

        private static async Task ValidateAddressAsync(IValidator<Domain.Entities.Address> validator, Domain.Entities.Address address)
        {
            var addressValidationResult = await validator.ValidateAsync(address);

            if (!addressValidationResult.IsValid)
                throw new Domain.Exceptions.ValidationException(addressValidationResult.Errors);
        }

    }
}
