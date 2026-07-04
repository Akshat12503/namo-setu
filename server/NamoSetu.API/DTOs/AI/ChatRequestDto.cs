namespace NamoSetu.API.DTOs.AI;

public class ChatRequestDto
{
    public string Message { get; set; } = string.Empty;
    public string? Language { get; set; } = "en";
    public List<ChatHistoryItem> History { get; set; } = new();
}

public class ChatHistoryItem
{
    public string Role { get; set; } = string.Empty; // "user" or "assistant"
    public string Message { get; set; } = string.Empty;
}