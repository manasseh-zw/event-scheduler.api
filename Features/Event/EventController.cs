using event_scheduler.api.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace event_scheduler.api.Features.Event;


/// <summary>
/// events endpoints
/// </summary>
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

    /// <summary>
    /// create user
    /// </summary>
    /// <param name="userId">the id of the user creating the event</param>
    /// <param name="newEvent">the event to be created</param>
    /// <returns>created event</returns>
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

    /// <summary>
    /// get an event
    /// </summary>
    /// <param name="id">event identifier</param>
    /// <returns>event with specified id</returns>
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

    /// <summary>
    /// updates event
    /// </summary>
    /// <param name="id">event identifier</param>
    /// <param name="updateEvent">object representing updates made to the event</param>
    /// <returns>success message</returns>
    [HttpPatch]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventDto updateEvent)
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

    /// <summary>
    /// delete event
    /// </summary>
    /// <param name="id">event identifier</param>
    /// <returns>success message</returns>
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