using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace event_scheduler.api.Features.User;


/// <summary>
/// user endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{

    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }

    /// <summary>
    /// gets a user
    /// </summary>
    /// <param name="id">the user identifier</param>
    /// <returns>user with specified id</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var response = await _service.GetUser(id);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    /// <summary>
    /// gets user events
    /// </summary>
    /// <param name="id">the user identifier</param>
    /// <returns>List of events for user with specified id</returns>
    [HttpGet("{id:guid}/events")]
    public async Task<IActionResult> GetEvents(Guid id)
    {
        var response = await _service.GetEvents(id);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

}