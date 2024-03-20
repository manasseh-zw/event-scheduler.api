using event_scheduler.api.Data.Models;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace event_scheduler.api.Features.Event;

public class EventValidator : AbstractValidator<EventModel>
{
    public EventValidator()
    {
        RuleFor(eventModel => eventModel.Name)
            .NotEmpty()
            .MinimumLength(2)
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("Name must be at least 2 chars and contain alphanumerics only");


        RuleFor(eventModel => eventModel.Description)
            .MinimumLength(2)
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("Description must be at least 2 chars and contain alphanumerics only")
            .When(e => !e.Description.IsNullOrEmpty());


        RuleFor(eventModel => eventModel.Location)
            .NotEmpty()
            .MinimumLength(2)
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("Location must be at least 2 chars and contain alphanumerics only");


        RuleFor(eventModel => eventModel.Date)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("DateTime must be greater than the current date and time.");
    }
}
