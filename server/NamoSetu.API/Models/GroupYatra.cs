namespace NamoSetu.API.Models;

public class GroupYatra
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CreatedBy { get; set; }
    public string? GroupName { get; set; }
    public string? Destination { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MaxMembers { get; set; }
    public int CurrentMembers { get; set; } = 1;
    public string? Description { get; set; }
    public string Status { get; set; } = "open";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<GroupYatraMember> Members { get; set; } = new List<GroupYatraMember>();
}