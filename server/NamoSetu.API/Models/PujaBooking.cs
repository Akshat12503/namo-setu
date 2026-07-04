namespace NamoSetu.API.Models;

public class PujaBooking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid? PujaServiceId { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public string? DevoteeName { get; set; }
    public string? Gotra { get; set; }
    public string? SpecialWish { get; set; }
    public string Status { get; set; } = "pending";
    public decimal? Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User? User { get; set; }
    public PujaService? PujaService { get; set; }
}