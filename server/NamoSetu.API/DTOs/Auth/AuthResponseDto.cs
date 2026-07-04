namespace NamoSetu.API.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PreferredLanguage { get; set; } = "en";
    public DateTime ExpiresAt { get; set; }
}