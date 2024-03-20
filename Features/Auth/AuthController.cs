using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace event_scheduler.api.Features.Auth;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto user)
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

    [HttpPost("/login")]
    public async Task<IActionResult> Register([FromBody] LoginRequestDto user)
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