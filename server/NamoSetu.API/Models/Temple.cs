namespace NamoSetu.API.Models;

public class Temple
{
    public Guid Id { get; set; } = Guid.NewGuid();
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
    public decimal EntryFee { get; set; } = 0;
    public string? ImageUrl { get; set; }
    public decimal Rating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public bool IsFeatured { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<PujaService> PujaServices { get; set; } = new List<PujaService>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}