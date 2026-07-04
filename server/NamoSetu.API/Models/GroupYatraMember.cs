namespace NamoSetu.API.Models;

public class GroupYatraMember
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = "member";
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public GroupYatra? GroupYatra { get; set; }
    public User? User { get; set; }
}