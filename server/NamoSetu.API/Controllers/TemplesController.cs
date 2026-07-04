using Microsoft.AspNetCore.Mvc;
using NamoSetu.API.DTOs.Temple;
using NamoSetu.API.Services;

namespace NamoSetu.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplesController : ControllerBase
{
    private readonly TempleService _templeService;

    public TemplesController(TempleService templeService)
    {
        _templeService = templeService;
    }

    // GET api/temples
    [HttpGet]
    public async Task<IActionResult> GetTemples([FromQuery] TempleFilterDto filter)
    {
        var result = await _templeService.GetTemplesAsync(filter);
        return Ok(result);
    }

    // GET api/temples/featured
    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured()
    {
        var result = await _templeService.GetFeaturedTemplesAsync();
        return Ok(result);
    }

    // GET api/temples/states
    [HttpGet("states")]
    public async Task<IActionResult> GetStates()
    {
        var result = await _templeService.GetAllStatesAsync();
        return Ok(result);
    }

    // GET api/temples/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTemple(Guid id)
    {
        var result = await _templeService.GetTempleByIdAsync(id);
        if (result == null) return NotFound(new { message = "Temple not found." });
        return Ok(result);
    }
}