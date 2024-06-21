using FluentValidation;

namespace GraphQL.Person.Domain.Services.Validation
{
    public class PersonValidator : AbstractValidator<Entities.Person>
    {
        public PersonValidator()
        {
            RuleFor(l => l.FirstName)
            .NotEmpty().WithMessage("{FirstName} is required.")
            .NotNull().WithMessage("{FirstName} is required.")
            .MaximumLength(40).WithMessage("{FirstName} must not exceed 40 character.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{FirstName} must not contain any special characters or numbers.");

            RuleFor(l => l.MiddleInitial)
            .MaximumLength(1).WithMessage("{MiddleInitial} must not exceed 1 character.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{MiddleInitial} must not contain any special characters or numbers.");

            RuleFor(l => l.LastName)
            .NotEmpty().WithMessage("{LastName} is required.")
            .NotNull().WithMessage("{LastName} is required.")
            .MaximumLength(50).WithMessage("{LastName} must not exceed 50 character.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{LastName} must not contain any special characters or numbers.");

            RuleFor(l => l.Title)
            .NotEmpty().WithMessage("{Title} is required.")
            .NotNull().WithMessage("{Title} is required.")
            .MaximumLength(4).WithMessage("{Title} must not exceed 4 character.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{Title} must not contain any special characters or numbers.");


        }
    }
}
