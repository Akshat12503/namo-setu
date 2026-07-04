using Microsoft.AspNetCore.Mvc;
using NamoSetu.API.DTOs.Auth;
using NamoSetu.API.Services;

namespace NamoSetu.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // POST api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(dto);

        if (result == null)
            return Conflict(new { message = "Email already registered." });

        return Ok(result);
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized(new { message = "Invalid email or password." });

        return Ok(result);
    }
}