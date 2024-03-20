using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace event_scheduler.api.Features.Auth;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    /// <summary>
    /// user authectication endpoints
    /// </summary>
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    /// <summary>
    /// registers a user
    /// </summary>
    /// <param name="user">the user with a fullname(optional), email and password</param>
    /// <returns>an jwt token, and the registered userId</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequestDto user)
    {
        if (user == null)
        {
            return BadRequest("required body param is null");
        }

        var response = await _service.Register(user);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    /// <summary>
    /// logins a user in
    /// </summary>
    /// <param name="user">the user with an email and password</param>
    /// <returns>jwt token and the logged-in userId</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Register([FromForm] LoginRequestDto user)
    {
        if (user == null)
        {
            return BadRequest("required body param is null");
        }

        var response = await _service.Login(user);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


}