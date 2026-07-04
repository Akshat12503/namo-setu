namespace NamoSetu.API.DTOs.AI;

public class PujaGuidanceRequestDto
{
    public string PujaName { get; set; } = string.Empty;
    public string Deity { get; set; } = string.Empty;
    public string? Language { get; set; } = "en";
}