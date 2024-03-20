namespace event_scheduler.api.Features.User;

public record EventDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime Date { get; set; }
    public bool Attended { get; set; }
}

public record CreateEventDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime Date { get; set; }
}

public record UpdateEventDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime? Date { get; set; }
}