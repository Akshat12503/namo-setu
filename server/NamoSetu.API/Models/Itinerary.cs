namespace NamoSetu.API.Models;

public class Itinerary
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Destination { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int NumTravelers { get; set; } = 1;
    public decimal? Budget { get; set; }
    public string? AiGeneratedPlan { get; set; }
    public bool IsSaved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User? User { get; set; }
}