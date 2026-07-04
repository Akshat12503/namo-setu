using Microsoft.EntityFrameworkCore;
using NamoSetu.API.Data;
using NamoSetu.API.DTOs.Auth;
using NamoSetu.API.Helpers;
using NamoSetu.API.Models;

namespace NamoSetu.API.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly JwtHelper _jwtHelper;

    public AuthService(AppDbContext db, JwtHelper jwtHelper)
    {
        _db = db;
        _jwtHelper = jwtHelper;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto dto)
    {
        // Check if email already exists
        var exists = await _db.Users
            .AnyAsync(u => u.Email == dto.Email);

        if (exists) return null;

        // Create new user
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            PreferredLanguage = dto.PreferredLanguage,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var token = _jwtHelper.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email,
            PreferredLanguage = user.PreferredLanguage,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        // Find user by email
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null) return null;

        // Verify password
        var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!isValid) return null;

        var token = _jwtHelper.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email,
            PreferredLanguage = user.PreferredLanguage,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
    }
}