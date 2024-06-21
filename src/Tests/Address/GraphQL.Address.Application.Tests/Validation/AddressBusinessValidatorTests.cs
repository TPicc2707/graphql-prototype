using AutoMapper;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Application.Mapping;
using GraphQL.Address.Application.Validation;
using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Domain.Services.Validation;
using GraphQL.Address.Infrastructure.Exceptions;
using GraphQL.Prototype.Tests.Data.TestingFramework;

namespace GraphQL.Address.Application.Tests.Validation
{
    public class AddressBusinessValidatorTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly AddressValidator _addressValidator = new AddressValidator();

        public AddressBusinessValidatorTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });

            _mapper = mapconfig.CreateMapper();
        }

        #region ValidateAddressAsync

        [Fact]
        public async Task AddressBusinessValidatorTests_ValidateAddressAsync_When_CreatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressDto = new CreateAddressDto()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "124 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };

            var expected = addressDto.Street;
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act
            var result = await AddressBusinessValidator.ValidateAddressAsync(mockUnitOfWork, addressDto, _mapper, _addressValidator);

            var actual = result.Street;

            //Assert
            Assert.IsType<Domain.Entities.Address>(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddressBusinessValidatorTests_ValidateAddressAsync_When_CreatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Validation_Exception()
        {
            //Arrange
            var addressDto = new CreateAddressDto()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "124 Glare Street",
                City = "Louisville!",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };

            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act/Assert
            await Assert.ThrowsAsync<Domain.Exceptions.ValidationException>(() => AddressBusinessValidator.ValidateAddressAsync(mockUnitOfWork, addressDto, _mapper, _addressValidator));
        }

        [Fact]
        public async Task AddressBusinessValidatorTests_ValidateAddressAsync_When_UpdatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressDto = new UpdateAddressDto()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "123 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Home",
                ZipCode = "12345"
            };

            var address = new Domain.Entities.Address()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "123 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Home",
                ZipCode = "12345"
            };

            var expected = addressDto.Street;
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act
            var result = await AddressBusinessValidator.ValidateAddressAsync(mockUnitOfWork, addressDto, _mapper, _addressValidator);

            var actual = result.Street;

            //Assert
            Assert.IsType<Domain.Entities.Address>(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddressBusinessValidatorTests_ValidateAddressAsync_When_UpdatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_NotFound_Exception()
        {
            //Arrange
            var addressDto = new UpdateAddressDto()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                Street = "123 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Home",
                ZipCode = "12345"
            };

            Domain.Entities.Address address = null;

            //Arrange
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);

            //Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => AddressBusinessValidator.ValidateAddressAsync(mockUnitOfWork, addressDto, _mapper, _addressValidator));
        }

        [Fact]
        public async Task AddressBusinessValidatorTests_ValidateAddressAsync_When_UpdatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Validation_Exception()
        {
            //Arrange
            var addressDto = new UpdateAddressDto()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "123 Glare Street",
                City = "Louisville!",
                State = "KY",
                Type = "Home",
                ZipCode = "12345"
            };

            var address = new Domain.Entities.Address()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "123 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Home",
                ZipCode = "12345"
            };

            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act/Assert
            await Assert.ThrowsAsync<Domain.Exceptions.ValidationException>(() => AddressBusinessValidator.ValidateAddressAsync(mockUnitOfWork, addressDto, _mapper, _addressValidator));
        }



        #endregion

    }
}
