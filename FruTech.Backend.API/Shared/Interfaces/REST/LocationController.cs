using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Shared.Domain.Services;

namespace FruTech.Backend.API.Shared.Interfaces.REST;

/// <summary>
/// Controller for geolocation operations
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IGeoLocationService _geoLocationService;
    private readonly ILogger<LocationController> _logger;

    public LocationController(IGeoLocationService geoLocationService, ILogger<LocationController> logger)
    {
        _geoLocationService = geoLocationService;
        _logger = logger;
    }

    /// <summary>
    /// Gets the user's location based on their IP address
    /// </summary>
    /// <returns>JSON object with ip, city, region_name and country_name</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLocation()
    {
        try
        {
            string? ipAddress = null;
            
            if (HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                var forwardedIps = forwardedFor.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (forwardedIps.Length > 0)
                {
                    ipAddress = forwardedIps[0].Trim();
                    _logger.LogInformation("IP detected from X-Forwarded-For header: {IpAddress}", ipAddress);
                }
            }
            
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                _logger.LogInformation("IP detected from RemoteIpAddress: {IpAddress}", ipAddress);
            }

            if (string.IsNullOrEmpty(ipAddress))
            {
                return StatusCode(500, new { error = "Unable to determine IP address" });
            }

            var locationData = await _geoLocationService.GetLocationAsync(ipAddress);

            if (locationData == null)
            {
                return StatusCode(500, new { error = "Unable to retrieve location information" });
            }
            return Ok(new
            {
                ip = locationData.Ip ?? ipAddress,
                city = locationData.City ?? "Unknown",
                region_name = locationData.Region_Name ?? "Unknown",
                country_name = locationData.Country_Name ?? "Unknown"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving location information");
            return StatusCode(500, new { error = "An error occurred while retrieving location" });
        }
    }
}

