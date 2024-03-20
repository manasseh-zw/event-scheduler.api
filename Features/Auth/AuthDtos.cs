namespace event_scheduler.api.Features.Auth;

public record RegisterRequestDto
{
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public record LoginRequestDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public record AuthResponseDto
{
    public string? Token { get; set; }
    public Guid UserId { get; set; }
}