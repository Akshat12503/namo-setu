namespace NamoSetu.API.Models;

public class PujaService
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? TempleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? DurationMinutes { get; set; }
    public string? AvailableDays { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Temple? Temple { get; set; }
}