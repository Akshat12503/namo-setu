using Microsoft.EntityFrameworkCore;
using NamoSetu.API.Data;
using NamoSetu.API.DTOs.Temple;
using NamoSetu.API.Models;

namespace NamoSetu.API.Services;

public class TempleService
{
    private readonly AppDbContext _db;

    public TempleService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<object> GetTemplesAsync(TempleFilterDto filter)
    {
        var query = _db.Temples.AsQueryable();

        // Search
        if (!string.IsNullOrEmpty(filter.Search))
            query = query.Where(t =>
                t.Name.ToLower().Contains(filter.Search.ToLower()) ||
                (t.City != null && t.City.ToLower().Contains(filter.Search.ToLower())) ||
                (t.Deity != null && t.Deity.ToLower().Contains(filter.Search.ToLower())));

        // Filters
        if (!string.IsNullOrEmpty(filter.State))
            query = query.Where(t => t.State == filter.State);

        if (!string.IsNullOrEmpty(filter.City))
            query = query.Where(t => t.City == filter.City);

        if (!string.IsNullOrEmpty(filter.Deity))
            query = query.Where(t => t.Deity != null &&
                t.Deity.ToLower().Contains(filter.Deity.ToLower()));

        if (filter.IsFeatured.HasValue)
            query = query.Where(t => t.IsFeatured == filter.IsFeatured.Value);

        if (filter.MaxEntryFee.HasValue)
            query = query.Where(t => t.EntryFee <= filter.MaxEntryFee.Value);

        // Total count
        var total = await query.CountAsync();

        // Pagination
        var temples = await query
            .OrderByDescending(t => t.IsFeatured)
            .ThenByDescending(t => t.Rating)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new TempleDto
            {
                Id = t.Id,
                Name = t.Name,
                Deity = t.Deity,
                Location = t.Location,
                City = t.City,
                State = t.State,
                Latitude = t.Latitude,
                Longitude = t.Longitude,
                Description = t.Description,
                Significance = t.Significance,
                DarshanTimings = t.DarshanTimings,
                DressCode = t.DressCode,
                EntryFee = t.EntryFee,
                ImageUrl = t.ImageUrl,
                Rating = t.Rating,
                TotalReviews = t.TotalReviews,
                IsFeatured = t.IsFeatured
            })
            .ToListAsync();

        return new
        {
            total,
            page = filter.Page,
            pageSize = filter.PageSize,
            totalPages = (int)Math.Ceiling((double)total / filter.PageSize),
            data = temples
        };
    }

    public async Task<TempleDto?> GetTempleByIdAsync(Guid id)
    {
        var t = await _db.Temples.FindAsync(id);
        if (t == null) return null;

        return new TempleDto
        {
            Id = t.Id,
            Name = t.Name,
            Deity = t.Deity,
            Location = t.Location,
            City = t.City,
            State = t.State,
            Latitude = t.Latitude,
            Longitude = t.Longitude,
            Description = t.Description,
            Significance = t.Significance,
            DarshanTimings = t.DarshanTimings,
            DressCode = t.DressCode,
            EntryFee = t.EntryFee,
            ImageUrl = t.ImageUrl,
            Rating = t.Rating,
            TotalReviews = t.TotalReviews,
            IsFeatured = t.IsFeatured
        };
    }

    public async Task<List<TempleDto>> GetFeaturedTemplesAsync()
    {
        return await _db.Temples
            .Where(t => t.IsFeatured)
            .OrderByDescending(t => t.Rating)
            .Take(10)
            .Select(t => new TempleDto
            {
                Id = t.Id,
                Name = t.Name,
                Deity = t.Deity,
                City = t.City,
                State = t.State,
                ImageUrl = t.ImageUrl,
                Rating = t.Rating,
                TotalReviews = t.TotalReviews,
                IsFeatured = t.IsFeatured,
                DarshanTimings = t.DarshanTimings,
                EntryFee = t.EntryFee
            })
            .ToListAsync();
    }

    public async Task<List<string>> GetAllStatesAsync()
    {
        return await _db.Temples
            .Where(t => t.State != null)
            .Select(t => t.State!)
            .Distinct()
            .OrderBy(s => s)
            .ToListAsync();
    }
}