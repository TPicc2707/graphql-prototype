using GraphQL.Person.Infrastructure.Persistence;
using GraphQL.Person.Infrastructure.Repositories;
using GraphQL.Prototype.Tests.Data;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GraphQL.Person.Infrastructure.Tests.Repositories
{
    public class UnitOfWorkTests
    {
        private readonly DbContextOptions<PersonContext> _dbOptions;
        private readonly IMockNsubstituteMethods _mockingFramework;

        public UnitOfWorkTests()
        {
            _dbOptions = new DbContextOptionsBuilder<PersonContext>()
                .UseInMemoryDatabase(databaseName: "UnitOfWorkDb", s => s.EnableNullChecks(true)).Options;
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
        }

        #region Complete

        [Fact]
        public async Task UnitOfWork_Complete_When_Called_Save_Changes()
        {
            //Arrange
            var expected = 1;
            var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockPersonLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockUnitOfWorkLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<UnitOfWork>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<UnitOfWork>(new object[] { mockPersonContext, mockPersonLoggingObject, mockUnitOfWorkLoggingObject });
            mockUnitOfWork = _mockingFramework.SetupReturnsResult(mockUnitOfWork, x => x.Complete(), expected);

            //Act
            var actual = await mockUnitOfWork.Complete();

            //Assert
            _mockingFramework.VerifyMethodRun(mockPersonContext, x => x.SaveChangesAsync(_mockingFramework.GetObject<CancellationToken>()), 1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UnitOfWork_Complete_When_Called_Shoudl_Return_SqlException_Error()
        {
            //Arrange
            var mockIdentificationContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockIdentificationLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockUnitOfWorkLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<UnitOfWork>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<UnitOfWork>(new object[] { mockIdentificationContext, mockIdentificationLoggingObject, mockUnitOfWorkLoggingObject });

            mockUnitOfWork = _mockingFramework.SetupThrowsException(mockUnitOfWork, x => x.Complete(), CreateSqlException
                .MakeSqlException(@"Data Source=.;Database=GURANTEED_TO_FAIL2;Connection Timeout=1"));

            //Act
            Func<Task> act = () => mockUnitOfWork.Complete();

            //Assert
            var ex = await Assert.ThrowsAsync<SqlException>(act);
            Assert.IsType<SqlException>(ex);
        }

        [Fact]
        public async Task UnitOfWork_Complete_When_Called_Shoudl_Return_DbUpdateConncurrencyException_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockIdentificationContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockIdentificationLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockUnitOfWorkLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<UnitOfWork>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<UnitOfWork>(new object[] { mockIdentificationContext, mockIdentificationLoggingObject, mockUnitOfWorkLoggingObject });

            mockUnitOfWork = _mockingFramework.SetupThrowsException(mockUnitOfWork, x => x.Complete(), new DbUpdateConcurrencyException(expected));

            //Act
            Func<Task> act = () => mockUnitOfWork.Complete();

            //Assert
            var ex = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(act);
            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public async Task UnitOfWork_Complete_When_Called_Shoudl_Return_DbUpdateException_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockIdentificationContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockIdentificationLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockUnitOfWorkLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<UnitOfWork>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<UnitOfWork>(new object[] { mockIdentificationContext, mockIdentificationLoggingObject, mockUnitOfWorkLoggingObject });

            mockUnitOfWork = _mockingFramework.SetupThrowsException(mockUnitOfWork, x => x.Complete(), new DbUpdateException(expected));

            //Act
            Func<Task> act = () => mockUnitOfWork.Complete();

            //Assert
            var ex = await Assert.ThrowsAsync<DbUpdateException>(act);
            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public async Task UnitOfWork_Complete_When_Called_Shoudl_Return_Exception_Error()
        {
            //Arrange
            var expected = "Foo";
            var mockIdentificationContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { _dbOptions });
            var mockIdentificationLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<Domain.Entities.Person>>(new object[] { });
            var mockUnitOfWorkLoggingObject = _mockingFramework.InitializeMockedClass<ILogger<UnitOfWork>>(new object[] { });
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<UnitOfWork>(new object[] { mockIdentificationContext, mockIdentificationLoggingObject, mockUnitOfWorkLoggingObject });

            mockUnitOfWork = _mockingFramework.SetupThrowsException(mockUnitOfWork, x => x.Complete(), new Exception(expected));

            //Act
            Func<Task> act = () => mockUnitOfWork.Complete();

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal(expected, ex.Message);
        }

        #endregion

    }
}
