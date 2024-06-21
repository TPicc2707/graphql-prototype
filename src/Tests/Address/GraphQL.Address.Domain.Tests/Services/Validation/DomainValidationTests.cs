using FluentValidation.TestHelper;
using GraphQL.Address.Domain.Services.Validation;
using GraphQL.Prototype.Tests.Data;

namespace GraphQL.Address.Domain.Tests.Services.Validation
{
    public class DomainValidationTests
    {
        private readonly PersonValidator _personValidator = new PersonValidator();
        private readonly AddressValidator _addressValidator = new AddressValidator();

        #region PersonValidator

        [Fact]
        public void Person_Validator_Give_Valid_Values_Should_Not_Return_Error()
        {
            //Arrange
            var person = new Entities.Person()
            {
                FirstName = "Robert",
                MiddleInitial = "J",
                LastName = "Downey",
                Title = "Mr"
            };

            //Act
            var personResult = _personValidator.TestValidate(person);

            //Assert
            personResult.ShouldNotHaveValidationErrorFor(l => l.FirstName);
            personResult.ShouldNotHaveValidationErrorFor(l => l.MiddleInitial);
            personResult.ShouldNotHaveValidationErrorFor(l => l.LastName);
            personResult.ShouldNotHaveValidationErrorFor(l => l.Title);
        }

        [Fact]
        public void Person_Validator_Given_A_Blank_Value_Should_Return_Error()
        {
            //Arrange
            var personFirstName = new Entities.Person { FirstName = "" };
            var personLastName = new Entities.Person { LastName = "" };
            var personTitle = new Entities.Person { Title = "" };

            var expectedFirstNameMessage = "{FirstName} is required.";
            var expectedLastNameMessage = "{LastName} is required.";
            var expectedTitleMessage = "{Title} is required.";


            //Act
            var firstNameResult = _personValidator.TestValidate(personFirstName);
            var lastNameResult = _personValidator.TestValidate(personLastName);
            var titleResult = _personValidator.TestValidate(personTitle);

            //Assert
            var actualFirstNameMessage = firstNameResult
                                                    .ShouldHaveValidationErrorFor(l => l.FirstName)
                                                    .FirstOrDefault();
            var actualLastNameMessage = lastNameResult
                                                    .ShouldHaveValidationErrorFor(l => l.LastName)
                                                    .FirstOrDefault();
            var actualTitleMessage = titleResult
                                            .ShouldHaveValidationErrorFor(l => l.Title)
                                            .FirstOrDefault();

            Assert.Equal(expectedFirstNameMessage, actualFirstNameMessage.ErrorMessage);
            Assert.Equal(expectedLastNameMessage, actualLastNameMessage.ErrorMessage);
            Assert.Equal(expectedTitleMessage, actualTitleMessage.ErrorMessage);

        }

        [Fact]
        public void Person_Validator_Given_A_Null_Value_Should_Return_Error()
        {
            //Arrange
            var firstName = new Entities.Person { FirstName = null };
            var lastName = new Entities.Person { LastName = null };
            var title = new Entities.Person { Title = null };

            var firstNameMessage = "{FirstName} is required.";
            var lastNameMessage = "{LastName} is required.";
            var titleMessage = "{Title} is required.";


            //Act
            var firstNameResult = _personValidator.TestValidate(firstName);
            var lastNameResult = _personValidator.TestValidate(lastName);
            var titleResult = _personValidator.TestValidate(title);

            //Assert
            var actualFirstNameMessage = firstNameResult
                                                    .ShouldHaveValidationErrorFor(l => l.FirstName)
                                                    .FirstOrDefault();
            var actualLastNameMessage = lastNameResult
                                                    .ShouldHaveValidationErrorFor(l => l.LastName)
                                                    .FirstOrDefault();
            var actualTitleMessage = titleResult
                                            .ShouldHaveValidationErrorFor(l => l.Title)
                                            .FirstOrDefault();

            Assert.Equal(firstNameMessage, actualFirstNameMessage.ErrorMessage);
            Assert.Equal(lastNameMessage, actualLastNameMessage.ErrorMessage);
            Assert.Equal(titleMessage, actualTitleMessage.ErrorMessage);

        }

        [Fact]
        public void Person_Validator_Given_A_Exceeding_Length_Value_Should_Return_Error()
        {
            //Arrange
            var generator = new RandomGenerator();

            var firstName = new Entities.Person { FirstName = generator.RandomString(41) };
            var middleInitial = new Entities.Person { MiddleInitial = generator.RandomString(2) };
            var lastName = new Entities.Person { LastName = generator.RandomString(51) };
            var title = new Entities.Person { Title = generator.RandomString(5) };

            var expectedFirstNameMessage = "{FirstName} must not exceed 40 character.";
            var expectedMiddleInitialMessage = "{MiddleInitial} must not exceed 1 character.";
            var expectedLastNameMessage = "{LastName} must not exceed 50 character.";
            var expectedTitleMessage = "{Title} must not exceed 4 character.";


            //Act
            var firstNameResult = _personValidator.TestValidate(firstName);
            var middleInitialResult = _personValidator.TestValidate(middleInitial);
            var lastNameResult = _personValidator.TestValidate(lastName);
            var titleResult = _personValidator.TestValidate(title);

            //Assert
            var actualFirstNameMessageResult = firstNameResult
                                                            .ShouldHaveValidationErrorFor(l => l.FirstName)
                                                            .FirstOrDefault();
            var actualMiddleInitialMessageResult = middleInitialResult
                                                            .ShouldHaveValidationErrorFor(l => l.MiddleInitial)
                                                            .FirstOrDefault();
            var actualLastNameMessageResult = lastNameResult
                                                        .ShouldHaveValidationErrorFor(l => l.LastName)
                                                        .FirstOrDefault();
            var actualTitleMessageResult = titleResult
                                                    .ShouldHaveValidationErrorFor(l => l.Title)
                                                    .FirstOrDefault();

            Assert.Equal(expectedFirstNameMessage, actualFirstNameMessageResult.ErrorMessage);
            Assert.Equal(expectedMiddleInitialMessage, actualMiddleInitialMessageResult.ErrorMessage);
            Assert.Equal(expectedLastNameMessage, actualLastNameMessageResult.ErrorMessage);
            Assert.Equal(expectedTitleMessage, actualTitleMessageResult.ErrorMessage);

        }

        [Fact]
        public void Person_Validator_When_Text_Contains_Special_Characters_Should_Return_Error()
        {
            //Arrange
            var person = new Entities.Person()
            {
                FirstName = "Robert!",
                MiddleInitial = "!",
                LastName = "Downey!",
                Title = "Mr!"
            };

            var expectedFirstNameErrorMessage = "{FirstName} must not contain any special characters or numbers.";
            var expectedMiddleInitialErrorMessage = "{MiddleInitial} must not contain any special characters or numbers.";
            var expectedLastNameErrorMessage = "{LastName} must not contain any special characters or numbers.";
            var expectedTitleErrorMessage = "{Title} must not contain any special characters or numbers.";


            //Act
            var personResult = _personValidator.TestValidate(person);

            //Assert
            var actualFirstNameErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.FirstName)
                                                            .FirstOrDefault();
            var actualMiddleInitialErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.MiddleInitial)
                                                            .FirstOrDefault();
            var actualLastNameErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.LastName)
                                                            .FirstOrDefault();
            var actualTitleErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.Title)
                                                .FirstOrDefault();

            Assert.Equal(expectedFirstNameErrorMessage, actualFirstNameErrorMessage.ErrorMessage);
            Assert.Equal(expectedMiddleInitialErrorMessage, actualMiddleInitialErrorMessage.ErrorMessage);
            Assert.Equal(expectedLastNameErrorMessage, actualLastNameErrorMessage.ErrorMessage);
            Assert.Equal(expectedTitleErrorMessage, actualTitleErrorMessage.ErrorMessage);

        }

        [Fact]
        public void Person_Validator_When_Text_Contains_Numbers_Should_Return_Error()
        {
            //Arrange
            var person = new Entities.Person()
            {
                FirstName = "Robert9",
                MiddleInitial = "9",
                LastName = "Downey9",
                Title = "Mr9"
            };

            var expectedFirstNameErrorMessage = "{FirstName} must not contain any special characters or numbers.";
            var expectedMiddleInitialErrorMessage = "{MiddleInitial} must not contain any special characters or numbers.";
            var expectedLastNameErrorMessage = "{LastName} must not contain any special characters or numbers.";
            var expectedTitleErrorMessage = "{Title} must not contain any special characters or numbers.";


            //Act
            var personResult = _personValidator.TestValidate(person);

            //Assert
            var actualFirstNameErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.FirstName)
                                                            .FirstOrDefault();
            var actualMiddleInitialErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.MiddleInitial)
                                                            .FirstOrDefault();
            var actualMiddleLegalNameInputTextErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.LastName)
                                                            .FirstOrDefault();
            var actualTitleErrorMessage = personResult.ShouldHaveValidationErrorFor(l => l.Title)
                                                .FirstOrDefault();

            Assert.Equal(expectedFirstNameErrorMessage, actualFirstNameErrorMessage.ErrorMessage);
            Assert.Equal(expectedMiddleInitialErrorMessage, actualMiddleInitialErrorMessage.ErrorMessage);
            Assert.Equal(expectedLastNameErrorMessage, actualMiddleLegalNameInputTextErrorMessage.ErrorMessage);
            Assert.Equal(expectedTitleErrorMessage, actualTitleErrorMessage.ErrorMessage);

        }

        #endregion

        #region AddressValidator

        [Fact]
        public void Address_Validator_Give_Valid_Values_Should_Not_Return_Error()
        {
            //Arrange
            var address = new Entities.Address()
            {
                Street = "124 Glare Street",
                City = "Louisville",
                State = "KY",
                Type = "Work",
                ZipCode = "12345"
            };

            //Act
            var addressResult = _addressValidator.TestValidate(address);

            //Assert
            addressResult.ShouldNotHaveValidationErrorFor(l => l.Street);
            addressResult.ShouldNotHaveValidationErrorFor(l => l.City);
            addressResult.ShouldNotHaveValidationErrorFor(l => l.State);
            addressResult.ShouldNotHaveValidationErrorFor(l => l.Type);
            addressResult.ShouldNotHaveValidationErrorFor(l => l.ZipCode);
        }

        [Fact]
        public void Address_Validator_Given_A_Blank_Value_Should_Return_Error()
        {
            //Arrange
            var addressStreet = new Entities.Address { Street = "" };
            var addressCity = new Entities.Address { City = "" };
            var addressState = new Entities.Address { State = "" };
            var addressType = new Entities.Address { Type = "" };
            var addressZipCode = new Entities.Address { ZipCode = "" };

            var expectedStreetMessage = "{Street} is required.";
            var expectedCityMessage = "{City} is required.";
            var expectedStateMessage = "{State} is required.";
            var expectedTypeMessage = "{Type} is required.";
            var expectedZipCodeMessage = "{ZipCode} is required.";


            //Act
            var streetResult = _addressValidator.TestValidate(addressStreet);
            var cityResult = _addressValidator.TestValidate(addressCity);
            var stateResult = _addressValidator.TestValidate(addressState);
            var typeResult = _addressValidator.TestValidate(addressType);
            var zipCodeResult = _addressValidator.TestValidate(addressZipCode);

            //Assert
            var actualStreetMessage = streetResult
                                                .ShouldHaveValidationErrorFor(l => l.Street)
                                                .FirstOrDefault();
            var actualCityMessage = cityResult
                                            .ShouldHaveValidationErrorFor(l => l.City)
                                            .FirstOrDefault();
            var actualStateMessage = stateResult
                                            .ShouldHaveValidationErrorFor(l => l.State)
                                            .FirstOrDefault();
            var actualTypeMessage = typeResult
                                        .ShouldHaveValidationErrorFor(l => l.Type)
                                        .FirstOrDefault();
            var actualZipCodeMessage = zipCodeResult
                                        .ShouldHaveValidationErrorFor(l => l.ZipCode)
                                        .FirstOrDefault();

            Assert.Equal(expectedStreetMessage, actualStreetMessage.ErrorMessage);
            Assert.Equal(expectedCityMessage, actualCityMessage.ErrorMessage);
            Assert.Equal(expectedStateMessage, actualStateMessage.ErrorMessage);
            Assert.Equal(expectedTypeMessage, actualTypeMessage.ErrorMessage);
            Assert.Equal(expectedZipCodeMessage, actualZipCodeMessage.ErrorMessage);

        }

        [Fact]
        public void Address_Validator_Given_A_Null_Value_Should_Return_Error()
        {
            //Arrange
            var addressStreet = new Entities.Address { Street = null };
            var addressCity = new Entities.Address { City = null };
            var addressState = new Entities.Address { State = null };
            var addressType = new Entities.Address { Type = null };
            var addressZipCode = new Entities.Address { ZipCode = null };

            var expectedStreetMessage = "{Street} is required.";
            var expectedCityMessage = "{City} is required.";
            var expectedStateMessage = "{State} is required.";
            var expectedTypeMessage = "{Type} is required.";
            var expectedZipCodeMessage = "{ZipCode} is required.";


            //Act
            var streetResult = _addressValidator.TestValidate(addressStreet);
            var cityResult = _addressValidator.TestValidate(addressCity);
            var stateResult = _addressValidator.TestValidate(addressState);
            var typeResult = _addressValidator.TestValidate(addressType);
            var zipCodeResult = _addressValidator.TestValidate(addressZipCode);

            //Assert
            var actualStreetMessage = streetResult
                                                .ShouldHaveValidationErrorFor(l => l.Street)
                                                .FirstOrDefault();
            var actualCityMessage = cityResult
                                            .ShouldHaveValidationErrorFor(l => l.City)
                                            .FirstOrDefault();
            var actualStateMessage = stateResult
                                            .ShouldHaveValidationErrorFor(l => l.State)
                                            .FirstOrDefault();
            var actualTypeMessage = typeResult
                                            .ShouldHaveValidationErrorFor(l => l.Type)
                                            .FirstOrDefault();
            var actualZipCodeMessage = zipCodeResult
                                            .ShouldHaveValidationErrorFor(l => l.ZipCode)
                                            .FirstOrDefault();

            Assert.Equal(expectedStreetMessage, actualStreetMessage.ErrorMessage);
            Assert.Equal(expectedCityMessage, actualCityMessage.ErrorMessage);
            Assert.Equal(expectedStateMessage, actualStateMessage.ErrorMessage);
            Assert.Equal(expectedTypeMessage, actualTypeMessage.ErrorMessage);
            Assert.Equal(expectedZipCodeMessage, actualZipCodeMessage.ErrorMessage);

        }

        [Fact]
        public void Address_Validator_Given_A_Exceeding_Length_Value_Should_Return_Error()
        {
            //Arrange
            var generator = new RandomGenerator();

            var street = new Entities.Address { Street = generator.RandomString(101) };
            var city = new Entities.Address { City = generator.RandomString(101) };
            var state = new Entities.Address { State = generator.RandomString(101) };
            var type = new Entities.Address { Type = generator.RandomString(11) };
            var zipCode = new Entities.Address { ZipCode = generator.RandomString(6) };

            var expectedStreetMessage = "{Street} must not exceed 100 character.";
            var expectedCityMessage = "{City} must not exceed 100 character.";
            var expectedStateMessage = "{State} must not exceed 100 character.";
            var expectedTypeMessage = "{Type} must not exceed 10 character.";
            var expectedZipCodeMessage = "{ZipCode} must be 5 characters.";


            //Act
            var streetResult = _addressValidator.TestValidate(street);
            var cityResult = _addressValidator.TestValidate(city);
            var stateResult = _addressValidator.TestValidate(state);
            var typeResult = _addressValidator.TestValidate(type);
            var zipCodeResult = _addressValidator.TestValidate(zipCode);

            //Assert
            var actualStreetMessage = streetResult
                                                .ShouldHaveValidationErrorFor(l => l.Street)
                                                .FirstOrDefault();
            var actualCityMessage = cityResult
                                            .ShouldHaveValidationErrorFor(l => l.City)
                                            .FirstOrDefault();
            var actualStateMessage = stateResult
                                            .ShouldHaveValidationErrorFor(l => l.State)
                                            .FirstOrDefault();
            var actualTypeMessage = typeResult
                                            .ShouldHaveValidationErrorFor(l => l.Type)
                                            .FirstOrDefault();
            var actualZipCodeMessage = zipCodeResult
                                            .ShouldHaveValidationErrorFor(l => l.ZipCode)
                                            .FirstOrDefault();

            Assert.Equal(expectedStreetMessage, actualStreetMessage.ErrorMessage);
            Assert.Equal(expectedCityMessage, actualCityMessage.ErrorMessage);
            Assert.Equal(expectedStateMessage, actualStateMessage.ErrorMessage);
            Assert.Equal(expectedTypeMessage, actualTypeMessage.ErrorMessage);
            Assert.Equal(expectedZipCodeMessage, actualZipCodeMessage.ErrorMessage);

        }

        [Fact]
        public void Address_Validator_When_Text_Contains_Special_Characters_Should_Return_Error()
        {
            //Arrange
            var address = new Entities.Address()
            {
                Street = "124 Glare Street!",
                City = "Louisville!",
                State = "KY!",
                Type = "Work!",
                ZipCode = "1234!"
            };

            var expectedStreetErrorMessage = "{Street} must not contain any special characters.";
            var expectedCityErrorMessage = "{City} must not contain any special characters or numbers.";
            var expectedStateErrorMessage = "{State} must not contain any special characters or numbers.";
            var expectedTypeErrorMessage = "{Type} must not contain any special characters or numbers.";
            var expectedZipCodeErrorMessage = "{ZipCode} must not contain any special characters or letters.";


            //Act
            var addressResult = _addressValidator.TestValidate(address);

            //Assert
            var actualStreetErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.Street)
                                                            .FirstOrDefault();
            var actualCityErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.City)
                                                            .FirstOrDefault();
            var actualStateErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.State)
                                                            .FirstOrDefault();
            var actualTypeErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.Type)
                                                .FirstOrDefault();
            var actualZipCodeErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.ZipCode)
                                    .FirstOrDefault();

            Assert.Equal(expectedStreetErrorMessage, actualStreetErrorMessage.ErrorMessage);
            Assert.Equal(expectedCityErrorMessage, actualCityErrorMessage.ErrorMessage);
            Assert.Equal(expectedStateErrorMessage, actualStateErrorMessage.ErrorMessage);
            Assert.Equal(expectedTypeErrorMessage, actualTypeErrorMessage.ErrorMessage);
            Assert.Equal(expectedZipCodeErrorMessage, actualZipCodeErrorMessage.ErrorMessage);

        }

        [Fact]
        public void Address_Validator_When_Text_Contains_Numbers_Should_Return_Error()
        {
            //Arrange
            var address = new Entities.Address()
            {
                City = "Louisville9",
                State = "KY9",
                Type = "Work9",
            };

            var expectedCityErrorMessage = "{City} must not contain any special characters or numbers.";
            var expectedStateErrorMessage = "{State} must not contain any special characters or numbers.";
            var expectedTypeErrorMessage = "{Type} must not contain any special characters or numbers.";


            //Act
            var addressResult = _addressValidator.TestValidate(address);

            //Assert
            var actualCityErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.City)
                                                            .FirstOrDefault();
            var actualStateErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.State)
                                                            .FirstOrDefault();
            var actualTypeErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.Type)
                                                            .FirstOrDefault();

            Assert.Equal(expectedCityErrorMessage, actualCityErrorMessage.ErrorMessage);
            Assert.Equal(expectedStateErrorMessage, actualStateErrorMessage.ErrorMessage);
            Assert.Equal(expectedTypeErrorMessage, actualTypeErrorMessage.ErrorMessage);

        }

        [Fact]
        public void Address_Validator_When_Text_Contains_Letters_Should_Return_Error()
        {
            //Arrange
            var address = new Entities.Address()
            {
                ZipCode = "12W12",
            };

            var expectedZipCodeErrorMessage = "{ZipCode} must not contain any special characters or letters.";


            //Act
            var addressResult = _addressValidator.TestValidate(address);

            //Assert
            var actualZipCodeErrorMessage = addressResult.ShouldHaveValidationErrorFor(l => l.ZipCode)
                                                            .FirstOrDefault();

            Assert.Equal(expectedZipCodeErrorMessage, actualZipCodeErrorMessage.ErrorMessage);

        }


        #endregion

    }
}
