using AutoMapper;
using GraphQL.Address.Application.Dtos.Address;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddress;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByCity;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByPersonId;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByState;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByStreet;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByType;
using GraphQL.Address.Application.Features.Address.Queries.GetAllPersonAddressByZipCode;
using GraphQL.Address.Application.Features.Address.Queries.GetPersonAddressById;
using GraphQL.Address.Application.Mapping;
using GraphQL.Address.Domain.Contracts;
using GraphQL.Prototype.Tests.Data.Address;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Application.Tests.Features.Address.Queries
{
    public class PersonAddressQueryHandlerTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        public PersonAddressQueryHandlerTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });
            _mapper = mapconfig.CreateMapper();
        }

        #region GetAllPersonAddressQueryHandler_Constructor

        [Fact]
        public void GetAllPersonAddressQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPersonAddressQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPersonAddressQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPersonAddressQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPersonAddressQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPersonAddressByCityQueryHandler_Constructor

        [Fact]
        public void GetAllPersonAddressByCityQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPersonAddressByCityQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPersonAddressByCityQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByCityQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPersonAddressByCityQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPersonAddressByCityQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPersonAddressByPersonIdQueryHandler_Constructor

        [Fact]
        public void GetAllPersonAddressByPersonIdQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPersonAddressByPersonIdQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPersonAddressByPersonIdQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByPersonIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPersonAddressByPersonIdQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPersonAddressByPersonIdQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPersonAddressByStateQueryHandler_Constructor

        [Fact]
        public void GetAllPersonAddressByStateQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPersonAddressByStateQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPersonAddressByStateQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByStateQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPersonAddressByStateQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPersonAddressByStateQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPersonAddressByStreetQueryHandler_Constructor

        [Fact]
        public void GetAllPersonAddressByStreetQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPersonAddressByStreetQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPersonAddressByStreetQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByStreetQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPersonAddressByStreetQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPersonAddressByStreetQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPersonAddressByTypeQueryHandler_Constructor

        [Fact]
        public void GetAllPersonAddressByTypeQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPersonAddressByTypeQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPersonAddressByTypeQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByTypeQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPersonAddressByTypeQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPersonAddressByTypeQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPersonAddressByZipCodeQueryHandler_Constructor

        [Fact]
        public void GetAllPersonAddressByZipCodeQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllPersonAddressByZipCodeQueryHandler(null, null, null));
        }

        [Fact]
        public void GetAllPersonAddressByZipCodeQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByZipCodeQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetAllPersonAddressByZipCodeQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetAllPersonAddressByZipCodeQueryHandler>(queryHandler);
        }

        #endregion

        #region GetPersonAddressByIdQueryHandler_Constructor

        [Fact]
        public void GetPersonAddressByIdQueryHandler_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => new GetPersonAddressByIdQueryHandler(null, null, null));
        }

        [Fact]
        public void GetPersonAddressByIdQueryHandler_Constructor_WhenInitiated_ShouldReturnCorrectType()
        {
            //Arrange/Act
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetPersonAddressByIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            var queryHandler = new GetPersonAddressByIdQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Assert
            Assert.IsType<GetPersonAddressByIdQueryHandler>(queryHandler);
        }

        #endregion

        #region GetAllPersonAddressQueryHandler

        [Fact]
        public async Task GetAllPersonAddressQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var expected = addressData.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPersonAddressesAsync(), addressData);

            var queryHandler = new GetAllPersonAddressQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPersonAddressQuery();
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<AddressDto>>(result);
        }

        [Fact]
        public async Task GetAllPersonAddressQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPersonAddressQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPersonAddressQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPersonAddressQuery(),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPersonAddressByCityQueryHandler

        [Fact]
        public async Task GetAllPersonAddressByCityQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var people = addressData.Where(p => p.City.Contains("Louisville")).ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByCityQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPersonAddressByCityAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPersonAddressByCityQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPersonAddressByCityQuery("Louisville");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<AddressDto>>(result);
        }

        [Fact]
        public async Task GetAllPersonAddressByCityQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByCityQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPersonAddressByCityQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPersonAddressByCityQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPersonAddressByCityQuery("Louisville"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPersonAddressByPersonIdQueryHandler

        [Fact]
        public async Task GetAllPersonAddressByPersonIdQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var addresses = addressData.Where(p => p.PersonId == new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a")).ToList();
            var expected = addresses.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByPersonIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPersonAddressByPersonIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, addresses);

            var queryHandler = new GetAllPersonAddressByPersonIdQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPersonAddressByPersonIdQuery(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"));
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<AddressDto>>(result);
        }

        [Fact]
        public async Task GetAllPersonAddressByPersonIdQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByPersonIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPersonAddressByPersonIdQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPersonAddressByPersonIdQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPersonAddressByPersonIdQuery(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a")),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPersonAddressByStateQueryHandler

        [Fact]
        public async Task GetAllPersonAddressByStateQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var people = addressData.Where(p => p.State.Contains("KY")).ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByStateQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPersonAddressByStateAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPersonAddressByStateQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPersonAddressByStateQuery("KY");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<AddressDto>>(result);
        }

        [Fact]
        public async Task GetAllPersonAddressByStateQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByStateQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPersonAddressByStateQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPersonAddressByStateQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPersonAddressByStateQuery("KY"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPersonAddressByStreetQueryHandler

        [Fact]
        public async Task GetAllPersonAddressByStreetQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var people = addressData.Where(p => p.Street.Contains("Main Street")).ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByStreetQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPersonAddressByStreetAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPersonAddressByStreetQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPersonAddressByStreetQuery("Main Street");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<AddressDto>>(result);
        }

        [Fact]
        public async Task GetAllPersonAddressByStreetQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByStreetQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPersonAddressByStreetQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPersonAddressByStreetQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPersonAddressByStreetQuery("Main Street"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPersonAddressByTypeQueryHandler

        [Fact]
        public async Task GetAllPersonAddressByTypeQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var address = addressData.Where(p => p.Type.Contains("Home")).ToList();
            var expected = address.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByTypeQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPersonAddressByTypeAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, address);

            var queryHandler = new GetAllPersonAddressByTypeQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPersonAddressByTypeQuery("Home");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<AddressDto>>(result);
        }

        [Fact]
        public async Task GetAllPersonAddressByTypeQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByTypeQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPersonAddressByTypeQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPersonAddressByTypeQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPersonAddressByTypeQuery("Home"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetAllPersonAddressByZipCodeQueryHandler

        [Fact]
        public async Task GetAllPersonAddressByZipCodeQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var people = addressData.Where(p => p.ZipCode.Contains("12346")).ToList();
            var expected = people.Count;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByZipCodeQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetAllPersonAddressByZipCodeAsync(_mockingFramework.GetObject<string>()), new object[] { _mockingFramework.GetObject<string>() }, people);

            var queryHandler = new GetAllPersonAddressByZipCodeQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetAllPersonAddressByZipCodeQuery("12346");
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<AddressDto>>(result);
        }

        [Fact]
        public async Task GetAllPersonAddressByZipCodeQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetAllPersonAddressByZipCodeQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetAllPersonAddressByZipCodeQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetAllPersonAddressByZipCodeQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetAllPersonAddressByZipCodeQuery("12346"),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetPersonAddressByIdQueryHandler

        [Fact]
        public async Task GetPersonAddressByIdQueryHandler_Handle_When_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var addressData = AddressTestData.SeedPersonAddressTestData();
            var address = addressData.Where(p => p.AddressId == new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")).FirstOrDefault();
            var expected = address.AddressId;
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetPersonAddressByIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, address);

            var queryHandler = new GetPersonAddressByIdQueryHandler(mockUnitOfWork, _mapper, mockLoggingServiceObject);

            //Act
            var personQuery = new GetPersonAddressByIdQuery(new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"));
            var result = await queryHandler.Handle(personQuery, new CancellationToken());
            var actual = result.AddressId;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<AddressDto>(result);
        }

        [Fact]
        public async Task GetPersonAddressByIdQueryHandler_Handle_When_Called_Should_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockLoggingServiceObject = _mockingFramework.InitializeMockedClass<ILogger<GetPersonAddressByIdQueryHandler>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<GetPersonAddressByIdQueryHandler>(new object[] { mockUnitOfWork, _mapper, mockLoggingServiceObject });
            mockObject = _mockingFramework
                .SetupThrowsException(mockObject,
                x => x.Handle(_mockingFramework.GetObject<GetPersonAddressByIdQuery>(), _mockingFramework.GetObject<CancellationToken>()),
                new Exception(expected));

            //Act
            Func<Task> act = () => mockObject
                                        .Handle(new GetPersonAddressByIdQuery(new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")),
                                        new CancellationToken());

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

    }
}
