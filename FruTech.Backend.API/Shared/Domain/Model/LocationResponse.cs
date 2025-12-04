namespace FruTech.Backend.API.Shared.Domain.Model;

/// <summary>
/// Response from ipapi.com geolocation API
/// </summary>
public record LocationResponse
{
    public string? Ip { get; init; }
    public string? City { get; init; }
    public string? Region_Name { get; init; }
    public string? Country_Name { get; init; }
}

