using GraphQL.Address.Infrastructure.Persistence;
using GraphQL.Address.Infrastructure.Repositories;
using GraphQL.Prototype.Tests.Data.Address;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Infrastructure.Tests.Repositories
{
    public class PersonRepositoryTests
    {
        private readonly DbContextOptions<PersonContext> _dbOptions;
        private readonly PersonContext _personContext;
        private readonly IMockNsubstituteMethods _mockingFramework;

        public PersonRepositoryTests()
        {
            _dbOptions = new DbContextOptionsBuilder<PersonContext>()
            .UseInMemoryDatabase(databaseName: "PersonDb", s => s.EnableNullChecks(true)).Options;
            _personContext = new PersonContext(_dbOptions);
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));

        }

        public async Task SetupDb()
        {
            await SetupInMemoryDb.SetupDb(_personContext);
        }

        public async Task DeleteData()
        {
            await SetupInMemoryDb.DeleteData(_personContext);
        }

        #region Constructor

        [Fact]
        public void PersonRepository_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange
            //Act/Assert
            Assert.Throws<ArgumentNullException>(() => new PersonRepository(null, null));
        }

        [Fact]
        public void PersonRepository_Constructor_WhenInitiated_ShouldReturnSsnRepositoryType()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act/Assert
            Assert.IsType<PersonRepository>(repository);
        }

        #endregion

        #region CreatePersonAddressAsync

        [Fact]
        public async Task PersonRepository_CreatePersonAddressAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();

            var expected = "124 Glare Street";
            var expectedCount = _personContext.Addresses.Count() + 1;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            var address = new Domain.Entities.Address()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "124 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };

            //Act
            var result = await repository.CreatePersonAddressAsync(address);
            var person = await _personContext.People.Where(i => i.PersonId == address.PersonId).FirstOrDefaultAsync();

            await _personContext.SaveChangesAsync();

            var addedAddress = person.Addresses.Where(s => s.AddressId == address.AddressId).FirstOrDefault();
            var actual = addedAddress.Street;
            var actualCount = _personContext.Addresses.Count();

            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal(expectedCount, actualCount);
            Assert.IsType<Domain.Entities.Address>(addedAddress);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_CreatePersonAddressAsync_WhenCalled_ShouldReturn_InvalidOperationException_Error()
        {
            //Arrange
            await SetupDb();

            var address = new Domain.Entities.Address()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "124 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };

            var expected = "Foo";
            var mockIdentificationContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockIdentificationContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.CreatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new InvalidOperationException(expected));

            //Act
            Func<Task> act = () => mockObject.CreatePersonAddressAsync(address);

            //Assert
            InvalidOperationException ex = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Contains(expected, ex.Message);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_CreatePersonAddressAsync_WhenCalled_ShouldReturn_NullReference_Error()
        {
            //Arrange
            await SetupDb();

            var address = new Domain.Entities.Address()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "124 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };

            var expected = "Foo";
            var mockIdentificationContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockIdentificationContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.CreatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new NullReferenceException(expected));

            //Act
            Func<Task> act = () => mockObject.CreatePersonAddressAsync(address);

            //Assert
            NullReferenceException ex = await Assert.ThrowsAsync<NullReferenceException>(act);
            Assert.Contains(expected, ex.Message);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_CreatePersonAddressAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            await SetupDb();

            var address = new Domain.Entities.Address()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                Street = "124 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };

            var expected = "Foo";
            var mockIdentificationContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockIdentificationContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.CreatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.CreatePersonAddressAsync(address);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

            await DeleteData();
        }


        #endregion

        #region UpdatePersonAddressAsync

        [Fact]
        public async Task PersonRepository_UpdatePersonAddressAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();

            var expected = "1234 Main Street";
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);


            //Act
            var result = await repository.GetPersonAddressByIdAsync(new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"));
            result.Street = "1234 Main Street";

            await repository.UpdatePersonAddressAsync(result);
            await _personContext.SaveChangesAsync();

            var updatedResult = await repository.GetPersonAddressByIdAsync(new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"));

            var actual = updatedResult.Street;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<Domain.Entities.Address>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_UpdatePersonAddressAsync_WhenCalled_ShouldReturn_InvalidOperationException_Error()
        {
            //Arrange
            await SetupDb();

            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            var addressData = await _personContext.Addresses.Where(s => s.AddressId == new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")).FirstOrDefaultAsync();
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.UpdatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new InvalidOperationException(expected));

            //Act
            Func<Task> act = () => mockObject.UpdatePersonAddressAsync(addressData);

            //Assert
            InvalidOperationException ex = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Contains(expected, ex.Message);
            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_UpdatePersonAddressAsync_WhenCalled_ShouldReturn_NullReference_Error()
        {
            //Arrange
            await SetupDb();

            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            var addressData = await _personContext.Addresses.Where(s => s.AddressId == new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")).FirstOrDefaultAsync();
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.UpdatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new NullReferenceException(expected));

            //Act
            Func<Task> act = () => mockObject.UpdatePersonAddressAsync(addressData);

            //Assert
            NullReferenceException ex = await Assert.ThrowsAsync<NullReferenceException>(act);
            Assert.Contains(expected, ex.Message);
            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_UpdatePersonAddressAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            await SetupDb();

            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            var addressData = await _personContext.Addresses.Where(s => s.AddressId == new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")).FirstOrDefaultAsync();
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.UpdatePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.UpdatePersonAddressAsync(addressData);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
            await DeleteData();
        }

        #endregion

        #region DeletePersonAddressAsync

        [Fact]
        public async Task PersonRepository_DeletePersonAddressAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();

            var expected = _personContext.Addresses.ToList().Count - 1;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetPersonAddressByIdAsync(new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"));
            await repository.DeletePersonAddressAsync(result);
            await _personContext.SaveChangesAsync();

            var actual = _personContext.Addresses.ToList().Count;

            //Assert
            Assert.Equal(expected, actual);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_DeletePersonAddressAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            await SetupDb();

            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            var addressData = await _personContext.Addresses.Where(s => s.AddressId == new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e")).FirstOrDefaultAsync();
            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.DeletePersonAddressAsync(_mockingFramework.GetObject<Domain.Entities.Address>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.DeletePersonAddressAsync(addressData);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
            await DeleteData();
        }

        #endregion

        #region GetAllPersonAddressesAsync

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressesAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.Addresses.Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressesAsync();
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPeopleByFirstNameAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressesAsync();
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPeopleByFirstNameAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPersonAddressesAsync(), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPersonAddressesAsync();

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetPersonAddressByIdAsync

        [Fact]
        public async Task PersonRepository_GetPersonAddressByIdAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e");
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetPersonAddressByIdAsync(new Guid("50ca7492-6771-4e79-b7d8-59f996344e2e"));
            var actual = result.AddressId;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<Domain.Entities.Address>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetPersonAddressByIdAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            await SetupDb();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetPersonAddressByIdAsync(new Guid("ae45413c-d16c-45e1-a178-4ce00a52f237"));

            //Assert
            Assert.Null(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetPersonAddressByIdAsync_WhenCalled_ShouldReturn_FormatException_Error()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act/Assert
            await Assert.ThrowsAsync<FormatException>(() => repository.GetPersonAddressByIdAsync(new Guid("")));

        }

        [Fact]
        public async Task PersonRepository_GetPersonAddressByIdAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetPersonAddressByIdAsync(_mockingFramework.GetObject<Guid>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetPersonAddressByIdAsync(new Guid("101096d3-7782-49b4-bcd9-c570067bcc07"));

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllPersonAddressByPersonIdAsync

        [Fact]
        public async Task PersonRepository_GGetAllPersonAddressByPersonIdAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.Addresses.Where(s => s.PersonId == new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a")).Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByPersonIdAsync(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"));
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<List<Domain.Entities.Address>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByPersonIdAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByPersonIdAsync(new Guid("90366ae2-74d7-4e20-8f79-f88727335fa4"));
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByPersonIdAsync_WhenCalled_ShouldReturn_FormatException_Error()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act/Assert
            await Assert.ThrowsAsync<FormatException>(() => repository.GetAllPersonAddressByPersonIdAsync(new Guid("")));

        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByPersonIdAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPersonAddressByPersonIdAsync(_mockingFramework.GetObject<Guid>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPersonAddressByPersonIdAsync(new Guid("101096d3-7782-49b4-bcd9-c570067bcc07"));

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllPersonAddressByCityAsync

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByCityAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.Addresses.Where(s => s.City == "Louisville").Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByCityAsync("Louisville");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByCityAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByCityAsync(string.Empty);
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByCityAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPersonAddressByCityAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPersonAddressByCityAsync("Louisville!");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllPersonAddressByStateAsync

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByStateAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.Addresses.Where(s => s.State == "KY").Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByStateAsync("KY");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByStateAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByStateAsync("TA");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByStateAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPersonAddressByStateAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPersonAddressByStateAsync("KY!");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllPersonAddressByStreetAsync

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByStreetAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.Addresses.Where(s => s.Street.Contains("Red Station")).Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByStreetAsync("Red Station");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByStreetAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByStreetAsync(string.Empty);
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByStreetAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPersonAddressByStreetAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPersonAddressByStreetAsync("Red Station!");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllPersonAddressByTypeAsync

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByTypeAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.Addresses.Where(s => s.Type == "Home").Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByTypeAsync("Home");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByTypeAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByTypeAsync(string.Empty);
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByTypeAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPersonAddressByTypeAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPersonAddressByTypeAsync("Home!");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllPersonAddressByZipCodeAsync

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByZipCodeAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.Addresses.Where(s => s.ZipCode.Contains("12346")).Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByZipCodeAsync("12346");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByZipCodeAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPersonAddressByZipCodeAsync("00000");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Address>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPersonAddressByZipCodeAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPersonAddressByZipCodeAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPersonAddressByZipCodeAsync("01");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion


    }
}
