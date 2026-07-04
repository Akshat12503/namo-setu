namespace NamoSetu.API.DTOs.AI;

public class ItineraryRequestDto
{
    public string Destination { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumTravelers { get; set; } = 1;
    public decimal Budget { get; set; }
    public string? Deity { get; set; }
    public string? SpecialRequirements { get; set; }
    public string Language { get; set; } = "en";
    public bool IncludeNearbyTemples { get; set; } = true;
}