using AutoMapper;
using GraphQL.Person.Application.Dtos.Person;
using GraphQL.Person.Application.Mapping;
using GraphQL.Person.Application.Validation;
using GraphQL.Person.Domain.Contracts;
using GraphQL.Person.Domain.Services.Validation;
using GraphQL.Person.Infrastructure.Exceptions;
using GraphQL.Prototype.Tests.Data.TestingFramework;

namespace GraphQL.Person.Application.Tests.Validation
{
    public class PersonBusinessValidatorTests
    {
        private readonly IMockNsubstituteMethods _mockingFramework;
        private readonly IMapper _mapper;
        private readonly PersonValidator _personValidator = new PersonValidator();

        public PersonBusinessValidatorTests()
        {
            _mockingFramework = Helper.GetRequiredService<IMockNsubstituteMethods>() ?? throw new ArgumentNullException(nameof(IMockNsubstituteMethods));
            var mapconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            });

            _mapper = mapconfig.CreateMapper();
        }

        #region ValidatePersonAsync

        [Fact]
        public async Task PersonBusinessValidatorTests_ValidatePersonAsync_When_CreatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Expected_Result()
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
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act
            var result = await PersonBusinessValidator.ValidatePersonAsync(mockUnitOfWork, personDto, _mapper, _personValidator);

            var actual = result.FirstName;

            //Assert
            Assert.IsType<Domain.Entities.Person>(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PersonBusinessValidatorTests_ValidatePersonAsync_When_CreatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Validation_Exception()
        {
            //Arrange
            var personDto = new CreatePersonDto()
            {
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey!",
                Title = "Mr"
            };

            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act/Assert
            await Assert.ThrowsAsync<Domain.Exceptions.ValidationException>(() => PersonBusinessValidator.ValidatePersonAsync(mockUnitOfWork, personDto, _mapper, _personValidator));
        }

        [Fact]
        public async Task PersonBusinessValidatorTests_ValidatePersonAsync_When_UpdatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Expected_Result()
        {
            //Arrange
            var personDto = new UpdatePersonDto()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            var person = new Domain.Entities.Person()
            {
                PersonId = new Guid("bb531538-3bc3-4e88-bbfc-dac101abad2a"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            var expected = personDto.LastName;
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act
            var result = await PersonBusinessValidator.ValidatePersonAsync(mockUnitOfWork, personDto, _mapper, _personValidator);

            var actual = result.LastName;

            //Assert
            Assert.IsType<Domain.Entities.Person>(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PersonBusinessValidatorTests_ValidatePersonAsync_When_UpdatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_NotFound_Exception()
        {
            //Arrange
            var personDto = new UpdatePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            Domain.Entities.Person person = null;

            //Arrange
            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);

            //Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => PersonBusinessValidator.ValidatePersonAsync(mockUnitOfWork, personDto, _mapper, _personValidator));
        }

        [Fact]
        public async Task PersonBusinessValidatorTests_ValidatePersonAsync_When_UpdatePersonLegalNameIdentifictionDto_Object_Called_Should_Return_Validation_Exception()
        {
            //Arrange
            var personDto = new UpdatePersonDto()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey!",
                Title = "Mr"
            };

            var person = new Domain.Entities.Person()
            {
                PersonId = new Guid("3182240f-b07b-4de8-81d3-136e062e4522"),
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey!",
                Title = "Mr"
            };

            var mockUnitOfWork = _mockingFramework.InitializeMockedClass<IUnitOfWork>(new object[] { });

            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.GetByIdAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, person);
            _mockingFramework.SetupReturnsResult(mockUnitOfWork.People, x => x.ValidatePersonAsync(_mockingFramework.GetObject<Guid>()), new object[] { _mockingFramework.GetObject<Guid>() }, true);

            //Act/Assert
            await Assert.ThrowsAsync<Domain.Exceptions.ValidationException>(() => PersonBusinessValidator.ValidatePersonAsync(mockUnitOfWork, personDto, _mapper, _personValidator));
        }



        #endregion

    }
}
