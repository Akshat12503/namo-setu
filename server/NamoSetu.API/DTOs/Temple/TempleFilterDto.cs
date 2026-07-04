namespace NamoSetu.API.DTOs.Temple;

public class TempleFilterDto
{
    public string? Search { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Deity { get; set; }
    public bool? IsFeatured { get; set; }
    public decimal? MaxEntryFee { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}