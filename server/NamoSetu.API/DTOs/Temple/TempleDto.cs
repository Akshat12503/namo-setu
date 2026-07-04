namespace NamoSetu.API.DTOs.Temple;

public class TempleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Deity { get; set; }
    public string? Location { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Description { get; set; }
    public string? Significance { get; set; }
    public string? DarshanTimings { get; set; }
    public string? DressCode { get; set; }
    public decimal EntryFee { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Rating { get; set; }
    public int TotalReviews { get; set; }
    public bool IsFeatured { get; set; }
}