using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Application.Features.Person.Commands;
using GraphQL.Person.Application.Mapping;
using GraphQL.Person.Domain.Contracts;
using GraphQL.Person.Domain.Services.Validation;
using GraphQL.Person.Infrastructure.Exceptions;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.Extensions.Logging;
using ValidationException = GraphQL.Person.Domain.Exceptions.ValidationException;

namespace GraphQL.Person.Application.Tests.Features.Person.Commands
{
    public class PersonCommandHandlerTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly PersonValidator _personValidator = new PersonValidator();

        public PersonCommandHandlerTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });

            _mapper = mapconfig.CreateMapper();
        }

        #region CreatePersonCommandHandler_Constructor

        [Fact]
        public void CreatePersonCommandHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new CreatePersonCommandHandler(null, null, null, null));
        }

        [Fact]
        public void CreatePersonCommandHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var commandHandler = new CreatePersonCommandHandler(mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _personValidator);

            //Assert
            Assert.IsType<CreatePersonCommandHandler>(commandHandler);
        }

        #endregion

        #region UpdatePersonCommandHandler_Constructor

        [Fact]
        public void UpdatePersonCommandHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new UpdatePersonCommandHandler(null, null, null, null));
        }

        [Fact]
        public void UpdatePersonCommandHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var commandHandler = new UpdatePersonCommandHandler(mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _personValidator);

            //Assert
            Assert.IsType<UpdatePersonCommandHandler>(commandHandler);
        }

        #endregion

        #region DeletePersonCommandHandler_Constructor

        [Fact]
        public void DeletePersonCommandHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new DeletePersonCommandHandler(null, null));
        }

        [Fact]
        public void DeletePersonCommandHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var commandHandler = new DeletePersonCommandHandler(mockUnitOfWork, mockDeleteLoggingServiceObject);

            //Assert
            Assert.IsType<DeletePersonCommandHandler>(commandHandler);
        }

        #endregion

        #region CreatePersonCommandHandler

        [Fact]
        public async Task CreatePersonCommandHandler_Handle_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var personDto = new CreatePersonDto()
            {
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            var expected = personDto.FirstName;
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var person = _mapper.Map<Domain.Entities.Person>(personDto);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.AddAsync(_mockingFramework.GetObject<Domain.Entities.Person>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Person>() }, person);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new CreatePersonCommandHandler(mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _personValidator);

            //Act
            var result = await commandHandler.Handle(personDto, new CancellationToken());
            var actual = result.FirstName;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<PersonDto>(result);
        }

        [Fact]
        public async Task CreatePersonCommandHandler_Handle_WhenCalled_Should_Return_Validation_Exception_Result()
        {
            //Arrange
            var personDto = new CreatePersonDto()
            {
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey!",
                Title = "Mr"
            };
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var person = _mapper.Map<Domain.Entities.Person>(personDto);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.AddAsync(_mockingFramework.GetObject<Domain.Entities.Person>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Person>() }, person);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new CreatePersonCommandHandler(mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _personValidator);

            //Assert/Act
            await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(personDto, new CancellationToken()));
        }

        [Fact]
        public async Task CreatePersonCommandHandler_Handle_WhenCalled_Should_Return_Exception_Error()
        {
            //Arrange
            var personDto = new CreatePersonDto()
            {
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            var expected = "Foo";
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<CreatePersonCommandHandler>(new object[] { mockUnitOfWork, _mapper, mockCreateLoggingServiceObject, _personValidator });
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Handle(_mockingFramework.GetObject<CreatePersonDto>(), _mockingFramework.GetObject<CancellationToken>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.Handle(personDto, new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region UpdatePersonCommandHandler

        [Fact]
        public async Task UpdatePersonCommandHandler_Handle_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var updatePerson = new UpdatePersonDto()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };
            var expected = true;
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<UpdatePersonCommandHandler>(new object[] { mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _personValidator });

            var person = _mapper.Map<Domain.Entities.Person>(updatePerson);
            _mockingFramework.SetupReturnsResult(mockObject, x => x.Handle(_mockingFramework.GetObject<UpdatePersonDto>(), _mockingFramework.GetObject<CancellationToken>()), new object[] { _mockingFramework.GetObject<UpdatePersonDto>(), _mockingFramework.GetObject<CancellationToken>() }, true);

            //Act
            var actual = await mockObject.Handle(updatePerson, new CancellationToken());

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdatePersonCommandHandler_Handle_WhenCalled_Should_Return_NotFound_Exception()
        {
            //Arrange
            var updatePerson = new UpdatePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            Domain.Entities.Person person = null;
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);
            _mockingFramework.SetupReturnsNoneResult(mockUnitOfWork.People, x => x.Update(_mockingFramework.GetObject<Domain.Entities.Person>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Person>() });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new UpdatePersonCommandHandler(mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _personValidator);

            //Assert/Act
            await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(updatePerson, new CancellationToken()));
        }

        [Fact]
        public async Task UpdatePersonCommandHandler_Handle_WhenCalled_Should_Return_Validation_Exception_Result()
        {
            //Arrange
            var updatePerson = new UpdatePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey!",
                Title = "Mr"
            };

            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var person = _mapper.Map<Domain.Entities.Person>(updatePerson);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);
            _mockingFramework.SetupReturnsNoneResult(mockUnitOfWork.People, x => x.Update(_mockingFramework.GetObject<Domain.Entities.Person>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Person>() });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new UpdatePersonCommandHandler(mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _personValidator);

            //Assert/Act
            await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(updatePerson, new CancellationToken()));
        }


        [Fact]
        public async Task UpdatePersonCommandHandler_Handle_WhenCalled_Should_Return_Exception_Error()
        {
            //Arrange
            var updatePerson = new UpdatePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            var expected = "Foo";
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<UpdatePersonCommandHandler>(new object[] { mockUnitOfWork, _mapper, mockUpdateLoggingServiceObject, _personValidator });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Handle(_mockingFramework.GetObject<UpdatePersonDto>(), _mockingFramework.GetObject<CancellationToken>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.Handle(updatePerson, new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region DeletePersonCommandHandler

        [Fact]
        public async Task DeletePersonCommandHandler_Handle_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var deletePerson = new DeletePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
            };

            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<DeletePersonCommandHandler>(new object[] { mockUnitOfWork, mockDeleteLoggingServiceObject });

            _mockingFramework.SetupReturnsNoneResult(mockObject, x => x.Handle(_mockingFramework.GetObject<DeletePersonDto>(), _mockingFramework.GetObject<CancellationToken>()), new object[] { _mockingFramework.GetObject<DeletePersonDto>(), _mockingFramework.GetObject<CancellationToken>() });

            //Act
            await mockObject.Handle(deletePerson, new CancellationToken());

            //Assert
            _mockingFramework.VerifyMethodRun(mockObject, c => c.Handle(deletePerson, new CancellationToken()), 1);
        }

        [Fact]
        public async Task DeletePersonCommandHandler_Handle_WhenCalled_Should_Return_NotFound_Exception()
        {
            //Arrange
            var deletePerson = new DeletePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
            };
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            Domain.Entities.Person person = null;
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);
            _mockingFramework.SetupReturnsNoneResult(mockUnitOfWork.People, x => x.Delete(_mockingFramework.GetObject<Domain.Entities.Person>()), new object[] { _mockingFramework.GetObject<Domain.Entities.Person>() });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), 1);
            var commandHandler = new DeletePersonCommandHandler(mockUnitOfWork, mockDeleteLoggingServiceObject);

            //Assert/Act
            await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(deletePerson, new CancellationToken()));
        }

        [Fact]
        public async Task DeletePersonCommandHandler_Handle_WhenCalled_Should_Return_Exception_Error()
        {
            //Arrange
            var deletePerson = new DeletePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
            };

            var person = new Domain.Entities.Person()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
            };

            var expected = "Foo";
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonCommandHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<DeletePersonCommandHandler>(new object[] { mockUnitOfWork, mockDeleteLoggingServiceObject });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Handle(_mockingFramework.GetObject<DeletePersonDto>(), _mockingFramework.GetObject<CancellationToken>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.Handle(deletePerson, new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

    }
}
