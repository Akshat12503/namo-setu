using Microsoft.AspNetCore.Mvc;
using NamoSetu.API.DTOs.AI;
using NamoSetu.API.Services;

namespace NamoSetu.API.Controllers;

[ApiController]
[Route("api/ai")]
public class AIAssistantController : ControllerBase
{
    private readonly GeminiService _geminiService;

    public AIAssistantController(GeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    // POST api/ai/itinerary
    [HttpPost("itinerary")]
    public async Task<IActionResult> GenerateItinerary(
        [FromBody] ItineraryRequestDto request)
    {
        try
        {
            var result = await _geminiService.GenerateItineraryAsync(request);
            return Ok(new { success = true, data = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    // POST api/ai/chat
    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequestDto request)
    {
        try
        {
            var result = await _geminiService.ChatWithAssistantAsync(request);
            return Ok(new { success = true, message = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    // POST api/ai/recommend
    [HttpPost("recommend")]
    public async Task<IActionResult> RecommendTemples(
        [FromBody] RecommendationRequestDto request)
    {
        try
        {
            var result = await _geminiService.RecommendTemplesAsync(request);
            return Ok(new { success = true, data = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    // POST api/ai/puja-guidance
    [HttpPost("puja-guidance")]
    public async Task<IActionResult> GetPujaGuidance([FromBody] PujaGuidanceRequestDto request)
    {
        try
        {
            var result = await _geminiService.GetPujaGuidanceAsync(
                request.PujaName,
                request.Deity,
                request.Language);
            return Ok(new { success = true, data = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
}