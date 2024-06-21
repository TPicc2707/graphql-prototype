using GraphQL.Address.Infrastructure.Persistence;
using GraphQL.Prototype.Tests.Data.TestingFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GraphQL.Address.Infrastructure.Tests.Persistence
{
    public class PersonSeedDataTests
    {
        private readonly PersonContext _context;
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly DbContextOptions<PersonContext> _dbOptions;

        public PersonSeedDataTests()
        {
            _dbOptions = new DbContextOptionsBuilder<PersonContext>().UseInMemoryDatabase(databaseName: "PersonSeedDataDb", b => b.EnableNullChecks(true)).Options;
            _context = new PersonContext(_dbOptions);
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
        }

        public async Task DeleteData()
        {
            _context.People.RemoveRange(_context.People.ToList());
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Person_Seed_Data_Async_WhenCalled_Should_Return_Expected_Result()
        {
            //Arrange
            var expectedPeople = 4;
            var expectedAddresses = 6;
            var mockObject = _mockingFramework.InitializeMockedClass<ILogger<PersonSeedData>>(new object[] { });

            //Act
            await PersonSeedData.SeedAsync(_context, mockObject, "true");
            var actualPeople = _context.People.Count();
            var actualAddresses = _context.Addresses.Count();

            //Assert
            Assert.Equal(expectedPeople, actualPeople);
            Assert.Equal(expectedAddresses, actualAddresses);

            await DeleteData();
        }

        [Fact]
        public async Task Person_Seed_Data_Async_WhenCalled_Should_Return_ArgumentNullException_Error()
        {
            //Arrange
            var mockObject = _mockingFramework.InitializeMockedClass<ILogger<PersonSeedData>>(new object[] { });
            _context.People = null;

            //Act/Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => PersonSeedData.SeedAsync(_context, mockObject, "true"));
        }

        //[Fact]
        //public async Task Identification_Seed_Data_Async_WhenCalled_Should_Return_Exception_Error()
        //{
        //    //Arrange
        //    var mockObject = _mockingFramework.InitializeMockedClass<ILogger<PersonSeedData>>(new object[] { });
        //    var mockPersonContext = _mockingFramework.InitializeMockedClass<PersonContext>(new object[] { });

        //    mockPersonContext = _mockingFramework.SetupThrowsException(mockPersonContext, l => l.LogInformation(_mockingFramework.GetObject<string>()), new Exception());

        //    //Act/Assert
        //    await Assert.ThrowsAsync<Exception>(() => PersonSeedData.SeedAsync(_context, mockObject, "true"));

        //    await DeleteData();
        //}
    }
}
