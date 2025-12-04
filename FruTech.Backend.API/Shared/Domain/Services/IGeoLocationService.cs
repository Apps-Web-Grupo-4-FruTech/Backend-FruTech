using FruTech.Backend.API.Shared.Domain.Model;

namespace FruTech.Backend.API.Shared.Domain.Services;

/// <summary>
/// Service for obtaining geolocation information based on IP address
/// </summary>
public interface IGeoLocationService
{
    /// <summary>
    /// Gets location information for the given IP address
    /// </summary>
    /// <param name="ip">IP address to geolocate</param>
    /// <returns>Location information including region and country</returns>
    Task<LocationResponse?> GetLocationAsync(string ip);
}

