using FluentValidation.TestHelper;
using GraphQL.Person.Domain.Services.Validation;
using GraphQL.Prototype.Tests.Data;

namespace GraphQL.Person.Domain.Tests.Services.Validation
{
    public class DomainValidationTests
    {
        private readonly PersonValidator _personValidator = new PersonValidator();

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

    }
}
