using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeople;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByFirstName;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeopleByLastName;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeoplebyMiddleInitial;
using GraphQL.Person.Application.Features.Person.Queries.GetAllPeoplebyTitle;
using GraphQL.Person.Application.Features.Person.Queries.GetPersonById;
using GraphQL.Person.Application.Mapping;
using GraphQL.Person.Domain.Contracts;
using GraphQL.Prototype.Tests.Data.Person;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Application.Tests.Features.Person.Queries
{
    public class PersonQueryHandlerTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        public PersonQueryHandlerTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });
            _mapper = mapconfig.CreateMapper();
        }

        #region GetAllPeopleQueryHandler_Constructor

        [Fact]
        public void GetAllPeopleQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPeopleQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPeopleQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPeopleQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPeopleQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPeopleByFirstNameQueryHandler_Constructor

        [Fact]
        public void GetAllPeopleByFirstNameQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPeopleByFirstNameQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPeopleByFirstNameQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByFirstNameQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPeopleByFirstNameQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPeopleByFirstNameQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPeopleByLastNameQueryHandler_Constructor

        [Fact]
        public void GetAllPeopleByLastNameQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPeopleByLastNameQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPeopleByLastNameQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByLastNameQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPeopleByLastNameQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPeopleByLastNameQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPeopleByMiddleInitialQueryHandler_Constructor

        [Fact]
        public void GetAllPeopleByMiddleInitialQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPeopleByMiddleInitialQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPeopleByMiddleInitialQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByMiddleInitialQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPeopleByMiddleInitialQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPeopleByMiddleInitialQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPeopleByTitleQueryHandler_Constructor

        [Fact]
        public void GetAllPeopleByTitleQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPeopleByTitleQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPeopleByTitleQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByTitleQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPeopleByTitleQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPeopleByTitleQueryHandler>(queryHandler);
        }

        #endregion

        #region GetPersonByIdQueryHandler_Constructor

        [Fact]
        public void GetPersonByIdQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetPersonByIdQueryHandler(null, null, null));
        }

        [Fact]
        public void GetPersonByIdQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetPersonByIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetPersonByIdQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetPersonByIdQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPeopleQueryHandler

        [Fact]
        public async Task GetAllPeopleQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var personData = PersonTestData.SeedPersonTestData();
            var expected = personData.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

           _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllAsync(), personData);

            var queryHandler = new GetAllPeopleQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPeopleQuery();
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<PersonDto>>(result);
        }

        [Fact]
        public async Task GetAllPeopleQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPeopleQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPeopleQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPeopleQuery(),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPeopleByFirstNameQueryHandler

        [Fact]
        public async Task GetAllPeopleByFirstNameQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var personData = PersonTestData.SeedPersonTestData();
            var people = personData.Where(p => p.FirstName == "Anthony").ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByFirstNameQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPeopleByFirstNameAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPeopleByFirstNameQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPeopleByFirstNameQuery("Anthony");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<PersonDto>>(result);
        }

        [Fact]
        public async Task GetAllPeopleByFirstNameQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByFirstNameQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPeopleByFirstNameQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPeopleByFirstNameQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPeopleByFirstNameQuery("Anthony"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPeopleByLastNameQueryHandler

        [Fact]
        public async Task GetAllPeopleByLastNameQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var personData = PersonTestData.SeedPersonTestData();
            var people = personData.Where(p => p.LastName == "Piccirilli").ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByLastNameQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPeopleByLastNameAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPeopleByLastNameQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPeopleByLastNameQuery("Anthony");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<PersonDto>>(result);
        }

        [Fact]
        public async Task GetAllPeopleByLastNameQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByLastNameQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPeopleByLastNameQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPeopleByLastNameQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPeopleByLastNameQuery("Anthony"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPeopleByMiddleInitialQueryHandler

        [Fact]
        public async Task GetAllPeopleByMiddleInitialQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var personData = PersonTestData.SeedPersonTestData();
            var people = personData.Where(p => p.MiddleInitial == "N").ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByMiddleInitialQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPeoplebyMiddleInitialAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPeopleByMiddleInitialQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPeopleByMiddleInitialQuery("N");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<PersonDto>>(result);
        }

        [Fact]
        public async Task GetAllPeopleByMiddleInitialQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByMiddleInitialQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPeopleByMiddleInitialQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPeopleByMiddleInitialQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPeopleByMiddleInitialQuery("N"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPeopleByTitleQueryHandler

        [Fact]
        public async Task GetAllPeopleByTitleQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var personData = PersonTestData.SeedPersonTestData();
            var people = personData.Where(p => p.Title == "Mr").ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByTitleQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPeoplebyTitleAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPeopleByTitleQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPeopleByTitleQuery("Mr");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<PersonDto>>(result);
        }

        [Fact]
        public async Task GetAllPeopleByTitleQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPeopleByTitleQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPeopleByTitleQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPeopleByTitleQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPeopleByTitleQuery("Mr"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetPersonByIdQueryHandler

        [Fact]
        public async Task GetPersonByIdQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var personData = PersonTestData.SeedPersonTestData();
            var person = personData.Where(p => p.PersonId == new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a")).FirstOrDefault();
            var expected = person.PersonId;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetPersonByIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);

            var queryHandler = new GetPersonByIdQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetPersonByIdQuery(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"));
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.PersonId;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<PersonDto>(result);
        }

        [Fact]
        public async Task GetPersonByIdQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetPersonByIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetPersonByIdQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetPersonByIdQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetPersonByIdQuery(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a")),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

    }
}
