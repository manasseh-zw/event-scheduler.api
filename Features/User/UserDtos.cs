namespace event_scheduler.api.Features.User;

public record UserDto
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}