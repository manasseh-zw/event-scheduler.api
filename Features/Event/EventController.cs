using event_scheduler.api.Features.User;
using Microsoft.AspNetCore.Mvc;

namespace event_scheduler.api.Features.Event;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _service;
    public EventController(IEventService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(Guid userId, [FromForm] CreateEventDto newEvent)
    {
        if (newEvent == null)
        {
            return BadRequest("required body params are null");
        }

        var response = await _service.CreateEvent(userId, newEvent);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

}