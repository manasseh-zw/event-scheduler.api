using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_scheduler.api.Data.Models;

public class Event
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User? User { get; set; }
}