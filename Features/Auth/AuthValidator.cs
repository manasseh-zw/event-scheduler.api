using FluentValidation;

namespace event_scheduler.api.Features.Auth;

public class AuthValidator : AbstractValidator<RegisterRequestDto>
{
    public AuthValidator()
    {
        RuleFor(u => u.Fullname)
                 .Matches("^[a-zA-Z ]*$").WithMessage("Full name may contain only letters and spaces")
                 .When(u => !string.IsNullOrEmpty(u.Fullname));

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password)
            .NotNull().NotEmpty().WithMessage("Password must not be empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(60).WithMessage("Password must not exceed 60 characters")
            .Matches(@"^(?=.*[A-Z])(?=.*[^a-zA-Z0-9\s]).+$").WithMessage("Password must have at least one uppercase and a special character excluding spaces");

    }
}
