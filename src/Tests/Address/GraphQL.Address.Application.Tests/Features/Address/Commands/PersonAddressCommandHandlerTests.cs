using AutoMapper;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Application.Features.Address.Commands;
using GraphQL.Address.Application.Mapping;
using GraphQL.Address.Domain.Contracts;
using GraphQL.Address.Domain.Exceptions;
using GraphQL.Address.Domain.Services.Validation;
using GraphQL.Address.Infrastructure.Exceptions;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Tests.Features.Address.Commands
{
    public class PersonAddressCommandHandlerTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly AddressValidator _addressValidator = new AddressValidator();

        public PersonAddressCommandHandlerTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });

            _mapper = mapconfig.CreateMapper();
        }

        #region CreatePersonAddressCommandHandler_Constructor

        [Fact]
        public void CreatePersonAddressCommandHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new CreatePersonAddressCommandHandler(null, null, null, null));
        }

        [Fact]
        public void CreatePersonAddressCommandHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var commandHandler = new CreatePersonAddressCommandHandler(mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _addressValidator);

            //Assert
            Assert.IsType<CreatePersonAddressCommandHandler>(commandHandler);
        }

        #endregion

        #region UpdatePersonAddressCommandHandler_Constructor

        [Fact]
        public void UpdatePersonAddressCommandHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new UpdatePersonAddressCommandHandler(null, null, null, null));
        }

        [Fact]
        public void UpdatePersonAddressCommandHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var commandHandler = new UpdatePersonAddressCommandHandler(mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _addressValidator);

            //Assert
            Assert.IsType<UpdatePersonAddressCommandHandler>(commandHandler);
        }

        #endregion

        #region DeletePersonAddressCommandHandler_Constructor

        [Fact]
        public void DeletePersonAddressCommandHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new DeletePersonAddressCommandHandler(null, null));
        }

        [Fact]
        public void DeletePersonAddressCommandHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var commandHandler = new DeletePersonAddressCommandHandler(mockUnitOfWork, mockDeleteLoggingServiceObject);

            //Assert
            Assert.IsType<DeletePersonAddressCommandHandler>(commandHandler);
        }

        #endregion

        #region CreatePersonAddressCommandHandler

        [Fact]
        public async Task CreatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_Expected_Result()
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
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var address = _mapper.Map<Domain.Entities.Address>(addressDto);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.CreatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Address>() }, address);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new CreatePersonAddressCommandHandler(mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _addressValidator);

            //Act
            var result = await commandHandler.Handle(addressDto, new CancellationToken());
            var actual = result.Street;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<AddressDto>(result);
        }

        [Fact]
        public async Task CreatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_Validation_Exception_Result()
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
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var address = _mapper.Map<Domain.Entities.Address>(addressDto);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.CreatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Address>() }, address);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new CreatePersonAddressCommandHandler(mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _addressValidator);

            //Assert/Act
            await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(addressDto, new CancellationToken()));
        }

        [Fact]
        public async Task CreatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_NotFound_Exception()
        {
            //Arrange
            var addressDto = new CreateAddressDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                Street = "124 Glare Street",
                City = "Louisville!",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            Domain.Entities.Address person = null;
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, false);
            _mockingFramework.SetupReturnsNoneResult(mockUnitOfWork.People, x => x.Update(_mockingFramework.GetObject<Domain.Entities.Person>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Person>() });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new CreatePersonAddressCommandHandler(mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _addressValidator);

            //Assert/Act
            await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(addressDto, new CancellationToken()));
        }


        [Fact]
        public async Task CreatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_Exception_Error()
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

            var expected = "Foo";
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<CreatePersonAddressCommandHandler>(new object[] { mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _addressValidator });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Handle(_mockingFramework.GetObject<CreateAddressDto>(), _mockingFramework.GetObject<CancellationToken>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.Handle(addressDto, new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region UpdatePersonAddressCommandHandler

        [Fact]
        public async Task UpdatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_Expected_Result()
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

            var expected = true;
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<UpdatePersonAddressCommandHandler>(new object[] { mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _addressValidator });

            var address = _mapper.Map<Domain.Entities.Address>(addressDto);
            _mockingFramework.SetupReturnsResult(mockObject, x => x.Handle(_mockingFramework.GetObject<UpdateAddressDto>(), _mockingFramework.GetObject<CancellationToken>()), new object[] { _mockingFramework.GetObject<UpdateAddressDto>(), _mockingFramework.GetObject<CancellationToken>() }, true);

            //Act
            var actual = await mockObject.Handle(addressDto, new CancellationToken());

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_NotFound_Exception()
        {
            //Arrange
            var addressDto = new UpdateAddressDto()
            {
                AddressId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "123 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Home",
                ZipCode = "12345"
            };
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            Domain.Entities.Address address = null;
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);
            _mockingFramework.SetupReturnsNoneResult(mockUnitOfWork.People, x => x.UpdatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Address>() });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new UpdatePersonAddressCommandHandler(mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _addressValidator);

            //Assert/Act
            await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(addressDto, new CancellationToken()));
        }

        [Fact]
        public async Task UpdatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_Validation_Exception_Result()
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

            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var address = _mapper.Map<Domain.Entities.Address>(addressDto);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);
            _mockingFramework.SetupReturnsNoneResult(mockUnitOfWork.People, x => x.UpdatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Address>() });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new UpdatePersonAddressCommandHandler(mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _addressValidator);

            //Assert/Act
            await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(addressDto, new CancellationToken()));
        }


        [Fact]
        public async Task UpdatePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_Exception_Error()
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

            var expected = "Foo";
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<UpdatePersonAddressCommandHandler>(new object[] { mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _addressValidator });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Handle(_mockingFramework.GetObject<UpdateAddressDto>(), _mockingFramework.GetObject<CancellationToken>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.Handle(addressDto, new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region DeletePersonAddressCommandHandler

        [Fact]
        public async Task DeletePersonAddressCommandHandler_Handle_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var deleteAddress = new DeleteAddressDto()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
            };

            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<DeletePersonAddressCommandHandler>(new object[] { mockUnitOfWork, mockDeleteLoggingServiceObject });

            _mockingFramework.SetupReturnsNoneResult(mockObject, x => x.Handle(_mockingFramework.GetObject<DeleteAddressDto>(), _mockingFramework.GetObject<CancellationToken>()), new object[] { _mockingFramework.GetObject<DeleteAddressDto>(), _mockingFramework.GetObject<CancellationToken>() });

            //Act
            await mockObject.Handle(deleteAddress, new CancellationToken());

            //Assert
            _mockingFramework.VerifyMethodRun(mockObject, c => c.Handle(deleteAddress, new CancellationToken()), 1);
        }

        [Fact]
        public async Task DeletePersonCommandHandler_Handle_WhenCalled_Should_Return_NotFound_Exception()
        {
            //Arrange
            var deleteAddress = new DeleteAddressDto()
            {
                AddressId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
            };
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            Domain.Entities.Address address = null;
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);
            _mockingFramework.SetupReturnsNoneResult(mockUnitOfWork.People, x => x.DeletePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Address>() });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new DeletePersonAddressCommandHandler(mockUnitOfWork, mockDeleteLoggingServiceObject);

            //Assert/Act
            await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(deleteAddress, new CancellationToken()));
        }

        [Fact]
        public async Task DeletePersonCommandHandler_Handle_WhenCalled_Should_Return_Exception_Error()
        {
            //Arrange
            var deleteAddress = new DeleteAddressDto()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
            };

            var address = new Domain.Entities.Address()
            {
                AddressId = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"),
            };

            var expected = "Foo";
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonAddressCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<DeletePersonAddressCommandHandler>(new object[] { mockUnitOfWork, mockDeleteLoggingServiceObject });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Handle(_mockingFramework.GetObject<DeleteAddressDto>(), _mockingFramework.GetObject<CancellationToken>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.Handle(deleteAddress, new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

    }
}
