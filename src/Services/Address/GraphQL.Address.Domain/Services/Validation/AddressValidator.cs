using FluentValidation;

namespace GraphQL.Address.Domain.Services.Validation
{
    public class AddressValidator : AbstractValidator<Entities.Address>
    {
        public AddressValidator()
        {
            RuleFor(l => l.Type)
            .NotEmpty().WithMessage("{Type} is required.")
            .NotNull().WithMessage("{Type} is required.")
            .MaximumLength(10).WithMessage("{Type} must not exceed 10 character.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{Type} must not contain any special characters or numbers.");

            RuleFor(l => l.Street)
            .NotEmpty().WithMessage("{Street} is required.")
            .NotNull().WithMessage("{Street} is required.")
            .MaximumLength(100).WithMessage("{Street} must not exceed 100 character.")
            .Matches(@"^[ A-Za-z0-9]+$").WithMessage("{Street} must not contain any special characters.");

            RuleFor(l => l.City)
            .NotEmpty().WithMessage("{City} is required.")
            .NotNull().WithMessage("{City} is required.")
            .MaximumLength(100).WithMessage("{City} must not exceed 100 character.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{City} must not contain any special characters or numbers.");

            RuleFor(l => l.State)
            .NotEmpty().WithMessage("{State} is required.")
            .NotNull().WithMessage("{State} is required.")
            .MaximumLength(100).WithMessage("{State} must not exceed 100 character.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{State} must not contain any special characters or numbers.");

            RuleFor(l => l.ZipCode)
            .NotEmpty().WithMessage("{ZipCode} is required.")
            .NotNull().WithMessage("{ZipCode} is required.")
            .Length(5,5).WithMessage("{ZipCode} must be 5 characters.")
            .Matches(@"^[0-9]+$").WithMessage("{ZipCode} must not contain any special characters or letters.");

        }
    }
}
