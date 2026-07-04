namespace NamoSetu.API.DTOs.AI;

public class RecommendationRequestDto
{
    public string? Deity { get; set; }
    public string? State { get; set; }
    public decimal? Budget { get; set; }
    public string? Occasion { get; set; }
    public string? TravelMonth { get; set; }
    public int NumTemples { get; set; } = 5;
}