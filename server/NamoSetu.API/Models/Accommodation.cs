namespace NamoSetu.API.Models;

public class Accommodation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; } // hotel, dharamshala, guesthouse
    public string? City { get; set; }
    public string? State { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? PricePerNight { get; set; }
    public string? Amenities { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Rating { get; set; } = 0;
    public string? Contact { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}