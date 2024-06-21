using AutoMapper;
using GraphQL.Address.API.EventBusConsumer;
using GraphQL.Address.API.Mapping;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Application.Dtos.Person;
using GraphQL.Address.Application.Features.Address.Commands;
using GraphQL.Address.Domain.Services.Validation;
using GraphQL.EventBus.Messages.Events;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.API.Tests.EventBusConsumer
{
    public class PersonConsumerTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;

        public PersonConsumerTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });

            _mapper = mapconfig.CreateMapper();
        }

        #region CreatePersonConsumer_Constructor

        [Fact]
        public void CreatePersonConsumer_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new CreatePersonConsumer(null, null, null));
        }

        [Fact]
        public void CreatePersonConsumer_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var commandHandler = new CreatePersonConsumer(mockMediator, _mapper, mockCreateLoggingServiceObject);

            //Assert
            Assert.IsType<CreatePersonConsumer>(commandHandler);
        }

        #endregion

        #region UpdatePersonConsumer_Constructor

        [Fact]
        public void UpdatePersonConsumer_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new UpdatePersonConsumer(null, null, null));
        }

        [Fact]
        public void UpdatePersonConsumer_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var commandHandler = new UpdatePersonConsumer(mockMediator, _mapper, mockUpdateLoggingServiceObject);

            //Assert
            Assert.IsType<UpdatePersonConsumer>(commandHandler);
        }

        #endregion

        #region DeletePersonConsumer_Constructor

        [Fact]
        public void DeletePersonConsumer_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new DeletePersonConsumer(null, null, null));
        }

        [Fact]
        public void DeletePersonConsumer_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var commandHandler = new DeletePersonConsumer(mockMediator, _mapper, mockDeleteLoggingServiceObject);

            //Assert
            Assert.IsType<DeletePersonConsumer>(commandHandler);
        }

        #endregion

        #region CreatePersonConsumer

        [Fact]
        public async Task CreatePersonConsumer_Consume_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var createPersonEvent = new CreatePersonEvent()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mockConsumerContext = _mockingFramework.InitializeMockedClass<ConsumeContext<CreatePersonEvent>>(new object[] { });
            var mockConsumer = _mockingFramework.InitializeMockedClass<CreatePersonConsumer>(new object[] { mockMediator, _mapper, mockCreateLoggingServiceObject });
            var person = _mapper.Map<CreatePersonEventDto>(createPersonEvent);
            _mockingFramework.SetupReturnsResult(mockMediator, x => x.Send(_mockingFramework.GetObject<CreatePersonEventDto>(), _mockingFramework.GetObject<CancellationToken>()), new object[] { _mockingFramework.GetObject<CreatePersonEventDto>(), _mockingFramework.GetObject<CancellationToken>() }, person.PersonId);

            //Act
            await mockConsumer.Consume(mockConsumerContext);

            //Assert
            _mockingFramework.VerifyMethodRun(mockConsumer, l => l.Consume(_mockingFramework.GetObject<ConsumeContext<CreatePersonEvent>>()), 1);
        }

        [Fact]
        public async Task CreatePersonConsumer_Consume_WhenCalled_Should_Return_Exception_Error()
        {
            //Arrange
            var createPersonEvent = new CreatePersonEvent()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };
            var expected = "Foo";
            var mockCreateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<CreatePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mockConsumerContext = _mockingFramework.InitializeMockedClass<ConsumeContext<CreatePersonEvent>>(new object[] { });
            var mockConsumer = _mockingFramework.InitializeMockedClass<CreatePersonConsumer>(new object[] { mockMediator, _mapper, mockCreateLoggingServiceObject });
            var person = _mapper.Map<CreatePersonEventDto>(createPersonEvent);
            mockConsumer = _mockingFramework.SetupThrowsException(mockConsumer, x => x.Consume(_mockingFramework.GetObject<ConsumeContext<CreatePersonEvent>>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockConsumer.Consume(mockConsumerContext);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region UpdatePersonConsumer

        [Fact]
        public async Task UpdatePersonConsumer_Consume_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var updatePersonEvent = new UpdatePersonEvent()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mockConsumerContext = _mockingFramework.InitializeMockedClass<ConsumeContext<UpdatePersonEvent>>(new object[] { });
            var mockConsumer = _mockingFramework.InitializeMockedClass<UpdatePersonConsumer>(new object[] { mockMediator, _mapper, mockUpdateLoggingServiceObject });
            var person = _mapper.Map<UpdatePersonEventDto>(updatePersonEvent);
            _mockingFramework.SetupReturnsResult(mockMediator, x => x.Send(_mockingFramework.GetObject<UpdatePersonEventDto>(), _mockingFramework.GetObject<CancellationToken>()), new object[] { _mockingFramework.GetObject<UpdatePersonEventDto>(), _mockingFramework.GetObject<CancellationToken>() }, person.PersonId);

            //Act
            await mockConsumer.Consume(mockConsumerContext);

            //Assert
            _mockingFramework.VerifyMethodRun(mockConsumer, l => l.Consume(_mockingFramework.GetObject<ConsumeContext<UpdatePersonEvent>>()), 1);
        }

        [Fact]
        public async Task UpdatePersonConsumer_Consume_WhenCalled_Should_Return_Exception_Error()
        {
            //Arrange
            var updatePersonEvent = new UpdatePersonEvent()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };
            var expected = "Foo";
            var mockUpdateLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<UpdatePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mockConsumerContext = _mockingFramework.InitializeMockedClass<ConsumeContext<UpdatePersonEvent>>(new object[] { });
            var mockConsumer = _mockingFramework.InitializeMockedClass<UpdatePersonConsumer>(new object[] { mockMediator, _mapper, mockUpdateLoggingServiceObject });
            var person = _mapper.Map<UpdatePersonEventDto>(updatePersonEvent);
            mockConsumer = _mockingFramework.SetupThrowsException(mockConsumer, x => x.Consume(_mockingFramework.GetObject<ConsumeContext<UpdatePersonEvent>>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockConsumer.Consume(mockConsumerContext);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region DeletePersonConsumer

        [Fact]
        public async Task DeletePersonConsumer_Consume_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var deletePersonEvent = new DeletePersonEvent()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
            };
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mockConsumerContext = _mockingFramework.InitializeMockedClass<ConsumeContext<DeletePersonEvent>>(new object[] { });
            var mockConsumer = _mockingFramework.InitializeMockedClass<DeletePersonConsumer>(new object[] { mockMediator, _mapper, mockDeleteLoggingServiceObject });
            var person = _mapper.Map<DeletePersonEventDto>(deletePersonEvent);
            _mockingFramework.SetupReturnsResult(mockMediator, x => x.Send(_mockingFramework.GetObject<DeletePersonEventDto>(), _mockingFramework.GetObject<CancellationToken>()), new object[] { _mockingFramework.GetObject<DeletePersonEventDto>(), _mockingFramework.GetObject<CancellationToken>() }, person.PersonId);

            //Act
            await mockConsumer.Consume(mockConsumerContext);

            //Assert
            _mockingFramework.VerifyMethodRun(mockConsumer, l => l.Consume(_mockingFramework.GetObject<ConsumeContext<DeletePersonEvent>>()), 1);
        }

        [Fact]
        public async Task DeletePersonConsumer_Consume_WhenCalled_Should_Return_Exception_Error()
        {
            //Arrange
            var deletePersonEvent = new DeletePersonEvent()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
            };
            var expected = "Foo";
            var mockDeleteLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<DeletePersonConsumer>>(new object[] { });
            var mockMediator = _mockingFramework.InitializeMockedClass<IMediator>(new object[] { });
            var mockConsumerContext = _mockingFramework.InitializeMockedClass<ConsumeContext<DeletePersonEvent>>(new object[] { });
            var mockConsumer = _mockingFramework.InitializeMockedClass<DeletePersonConsumer>(new object[] { mockMediator, _mapper, mockDeleteLoggingServiceObject });
            var person = _mapper.Map<DeletePersonEventDto>(deletePersonEvent);
            mockConsumer = _mockingFramework.SetupThrowsException(mockConsumer, x => x.Consume(_mockingFramework.GetObject<ConsumeContext<DeletePersonEvent>>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockConsumer.Consume(mockConsumerContext);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

    }
}
