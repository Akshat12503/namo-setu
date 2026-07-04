namespace NamoSetu.API.Models;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string BookingType { get; set; } = string.Empty; // travel, hotel, puja
    public Guid? ReferenceId { get; set; }
    public string Status { get; set; } = "pending";
    public decimal? TotalAmount { get; set; }
    public string PaymentStatus { get; set; } = "unpaid";
    public DateTime BookedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User? User { get; set; }
}