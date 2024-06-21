using GraphQL.Person.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using GraphQL.Prototype.Tests.Data.Person;
using GraphQL.Person.Infrastructure.Repositories;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace GraphQL.Person.Infrastructure.Tests.Repositories
{
    public class BaseRepositoryTests
    {
        private readonly PersonContext _personContext;
        private readonly DbContextOptions<PersonContext> _dbOptions;
        private readonly IMockNsubstituteMethods _mockingFramework;

        public BaseRepositoryTests()
        {
            _dbOptions = new DbContextOptionsBuilder<PersonContext>().UseInMemoryDatabase(databaseName: "BaseDb", s => s.EnableNullChecks(true)).Options;
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
        public void BaseRepository_Constructor_WhenInitiated_ShouldReturnArgumentNullException()
        {
            //Arrange
            //Act/Assert
            Assert.Throws<ArgumentNullException>(() => new BaseRepository<Domain.Entities.Person>(null, null));
        }

        [Fact]
        public void BaseRepository_Constructor_WhenInitiated_ShouldReturnBaseRepositoryType()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act/Assert
            Assert.IsType<BaseRepository<Domain.Entities.Person>>(repository);
        }

        #endregion

        #region Add

        [Fact]
        public async Task BaseRepository_Add_WhenCalled_ShouldReturnExpectedResult()
        {
            //Arrange
            await SetupDb();

            var expected = new Guid("119d7fd2-d3ab-4ec5-a5a6-ad1b0a9ebaa9");
            var expectedCount = _personContext.People.Count() + 1;

            var testData = new Domain.Entities.Person()
            {
                PersonId = new Guid("119d7fd2-d3ab-4ec5-a5a6-ad1b0a9ebaa9"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"

            };
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.AddAsync(testData);
            await _personContext.SaveChangesAsync();

            var actual = result.PersonId;
            var actualCount = _personContext.People.Count();

            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal(expectedCount, actualCount);
            Assert.IsType<Domain.Entities.Person>(result);

            await DeleteData();
        }

        [Fact]
        public async Task BaseRepository_Add_WhenCalled_ShouldReturn_InvalidOperationException_Error()
        {
            //Arrange
            var testData = new Domain.Entities.Person()
            {
                PersonId = new Guid("119d7fd2-d3ab-4ec5-a5a6-ad1b0a9ebaa9"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.AddAsync(_mockingFramework.GetObject<Domain.Entities.Person>()), new InvalidOperationException(expected));

            //Act
            Func<Task> act = () => mockObject.AddAsync(testData);

            //Assert
            InvalidOperationException ex = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Contains(expected, ex.Message);

        }

        [Fact]
        public async Task BaseRepository_Add_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var testData = new Domain.Entities.Person()
            {
                PersonId = new Guid("119d7fd2-d3ab-4ec5-a5a6-ad1b0a9ebaa9"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.AddAsync(_mockingFramework.GetObject<Domain.Entities.Person>()), new Exception(expected));
            //Act
            Func<Task> act = () => mockObject.AddAsync(testData);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region Delete

        [Fact]
        public async Task BaseRepository_Delete_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();

            var expectedCount = _personContext.People.Count() - 1;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            var person = await repository.GetByIdAsync(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"));

            //Act
            repository.Delete(person);
            await _personContext.SaveChangesAsync();

            var result = await repository.GetByIdAsync(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"));
            var actualCount = _personContext.People.Count();

            //Assert
            Assert.Null(result);
            Assert.Equal(expectedCount, actualCount);

            await DeleteData();
        }

        [Fact]
        public async Task BaseRepository_Delete_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            await SetupDb();

            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            var person = await repository.GetByIdAsync(new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"));

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Delete(_mockingFramework.GetObject<Domain.Entities.Person>()), new Exception(expected));

            //Act
            Action act = () => mockObject.Delete(person);

            //Assert
            Exception ex = Assert.Throws<Exception>(act);
            Assert.Equal(expected, ex.Message);

            await DeleteData();
        }


        #endregion

        #region GetAllAsync

        [Fact]
        public async Task BaseRepository_GetAllAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.People.Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllAsync();
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task BaseRepository_GetAllAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllAsync();
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);
        }

        [Fact]
        public async Task BaseRepository_GetAllAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, l => l.GetAllAsync(), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllAsync();

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetAllWithChildrenAsync

        [Fact]
        public async Task BaseRepository_GetAllWithChildrenAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = _personContext.People.Count();
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });


            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllWithChildrenAsync();
            var actual = result.Count;


            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);

            await DeleteData();
        }

        [Fact]
        public async Task BaseRepository_GetAllWithChildrenAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var expected = 0;
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetAllWithChildrenAsync();
            var actual = result.Count;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsAssignableFrom<IReadOnlyList<Domain.Entities.Person>>(result);
        }

        [Fact]
        public async Task BaseRepository_GetAllWithChildrenAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetAllWithChildrenAsync(
                _mockingFramework.GetObject<Expression<Func<Domain.Entities.Person, bool>>>(),
                _mockingFramework.GetObject<Func<IQueryable<Domain.Entities.Person>, IOrderedQueryable<Domain.Entities.Person>>>(),
                _mockingFramework.GetObject<bool>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetAllWithChildrenAsync();

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region GetByIdAsync

        [Fact]
        public async Task BaseRepository_GetByIdAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = new Guid("1a4e32e0-479d-4110-ae8d-292cd63c8bdf");
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetByIdAsync(new Guid("1a4e32e0-479d-4110-ae8d-292cd63c8bdf"));
            var actual = result.PersonId;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<Domain.Entities.Person>(result);

            await DeleteData();
        }

        [Fact]
        public async Task BaseRepository_GetByIdAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetByIdAsync(new Guid("1a4e32e0-479d-4110-ae8d-292cd63c8bdf"));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task BaseRepository_GetByIdAsync_WhenCalled_ShouldReturn_FormatException_Error()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act/Assert
            await Assert.ThrowsAsync<FormatException>(() => repository.GetByIdAsync(new Guid("")));

        }

        [Fact]
        public async Task BaseRepository_GetByIdAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetByIdAsync(new Guid("1a4e32e0-479d-4110-ae8d-292cd63c8bdf"));

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);
        }

        #endregion

        #region GetByIdWithChildrenAsync

        [Fact]
        public async Task BaseRepository_GetByIdWithChildrenAsync_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = new Guid("f60e17e3-c781-466d-9e6a-511fb3b3d2a3");
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetByIdWithChildrenAsync(new Guid("f60e17e3-c781-466d-9e6a-511fb3b3d2a3"));
            var actual = result.PersonId;


            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<Domain.Entities.Person>(result);

            await DeleteData();
        }

        [Fact]
        public async Task BaseRepository_GetByIdWithChildrenAsync_WhenCalled_ShouldReturn_NoResult()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetByIdWithChildrenAsync(new Guid("f60e17e3-c781-466d-9e6a-511fb3b3d2a3"));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task BaseRepository_GetByIdWithChildrenAsync_WhenCalled_ShouldReturn_FormatException_Error()
        {
            //Arrange
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act/Assert
            await Assert.ThrowsAsync<FormatException>(() => repository.GetByIdWithChildrenAsync(new Guid("")));

        }

        [Fact]
        public async Task BaseRepository_GetByIdWithChildrenAsync_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.GetByIdWithChildrenAsync(_mockingFramework.GetObject<Guid>()), new Exception(expected));

            //Act
            Func<Task> act = () => mockObject.GetByIdWithChildrenAsync(new Guid("f60e17e3-c781-466d-9e6a-511fb3b3d2a3"));

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(expected, ex.Message);

        }

        #endregion

        #region Update

        [Fact]
        public async Task BaseRepository_Update_WhenCalled_ShouldReturn_ExpectedResult()
        {
            //Arrange
            await SetupDb();
            var expected = "Bob";
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });

            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetByIdAsync(new Guid("5311f4ad-3a9c-485f-bdab-dc6ed066c960"));
            result.FirstName = expected;

            repository.Update(result);
            await _personContext.SaveChangesAsync();

            var updatedResult = await repository.GetByIdAsync(new Guid("5311f4ad-3a9c-485f-bdab-dc6ed066c960"));

            var actual = updatedResult.FirstName;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<Domain.Entities.Person>(result);

            await DeleteData();
        }

        [Fact]
        public async Task BaseRepository_Update_WhenCalled_ShouldReturn_Exception_Error()
        {
            //Arrange
            await SetupDb();
            var expected = "Foo";
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockObject = _mockingFramework.InitializeMockedClass<BaseRepository<Domain.Entities.Person>>(new object[] { mockPersonContext, mockLoggingObject });

            mockObject = _mockingFramework.SetupThrowsException(mockObject, x => x.Update(_mockingFramework.GetObject<Domain.Entities.Person>()), new Exception(expected));
            var repository = new BaseRepository<Domain.Entities.Person>(_personContext, mockLoggingObject);

            //Act
            var result = await repository.GetByIdAsync(new Guid("5311f4ad-3a9c-485f-bdab-dc6ed066c960"));
            result.FirstName = "Bob";

            Action act = () => mockObject.Update(result);

            //Assert
            Exception ex = Assert.Throws<Exception>(act);
            Assert.Contains(expected, ex.Message);

            await DeleteData();
        }

        #endregion
    }
}
