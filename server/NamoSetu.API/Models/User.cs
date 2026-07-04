namespace NamoSetu.API.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string PreferredLanguage { get; set; } = "en";
    public string? ProfileImage { get; set; }
    public bool IsVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
}