using GraphQL.Person.Infrastructure.Persistence;
using GraphQL.Person.Infrastructure.Repositories;
using GraphQL.Prototype.Tests.Data.Person;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Infrastructure.Tests.Repositories
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

        #region GetAllPeopleByFirstNameAsync

        [Fact]
        public async Task PersonRepository_GetAllPeopleByFirstNameAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.People.Where(s => s.FirstName == "Anthony").Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPeopleByFirstNameAsync("Anthony");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

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
            var result = await repository.GetAllPeopleByFirstNameAsync(string.Empty);
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPeopleByFirstNameAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPeopleByFirstNameAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPeopleByFirstNameAsync("Anthonyt");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllPeopleByLastNameAsync

        [Fact]
        public async Task PersonRepository_GetAllPeopleByLastNameAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.People.Where(s => s.LastName == "Piccirilli").Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPeopleByLastNameAsync("Piccirilli");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPeopleByLastNameAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPeopleByLastNameAsync(string.Empty);
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPeopleByLastNameAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPeopleByLastNameAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPeopleByLastNameAsync("Piccirilli3");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }


        #endregion

        #region GetAllPeoplebyMiddleInitialAsync

        [Fact]
        public async Task PersonRepository_GetAllPeoplebyMiddleInitialAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.People.Where(s => s.MiddleInitial == "N").Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPeoplebyMiddleInitialAsync("N");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPeoplebyMiddleInitialAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPeoplebyMiddleInitialAsync(string.Empty);
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPeoplebyMiddleInitialAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPeoplebyMiddleInitialAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPeoplebyMiddleInitialAsync("3");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }


        #endregion

        #region GetAllPeoplebyTitleAsync

        [Fact]
        public async Task PersonRepository_GetAllPeoplebyTitleAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.People.Where(s => s.Title.Contains("Mr")).Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPeoplebyTitleAsync("Mr");
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task PersonRepository_GetAllPeoplebyTitleAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new PersonRepository(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllPeoplebyTitleAsync(string.Empty);
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

        }

        [Fact]
        public async Task PersonRepository_GetAllPeoplebyTitleAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<PersonRepository>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllPeoplebyTitleAsync(_mockingFramework.GetObject<string>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllPeoplebyTitleAsync("Miss3");

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }


        #endregion


    }
}
