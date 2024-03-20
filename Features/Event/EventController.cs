using event_scheduler.api.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace event_scheduler.api.Features.Event;


[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        var response = await _service.GetEvent(id);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpPatch]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromForm] UpdateEventDto updateEvent)
    {
        if (updateEvent == null)
        {
            return BadRequest("required body params are null");
        }

        var response = await _service.UpdateEvent(id, updateEvent);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var response = await _service.DeleteEvent(id);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}