using FruTech.Backend.API.Shared.Domain.Model;
using FruTech.Backend.API.Shared.Domain.Services;
using System.Text.Json;

namespace FruTech.Backend.API.Shared.Infrastructure.Services;

/// <summary>
/// Service for obtaining geolocation information using ipapi.com
/// </summary>
public class GeoLocationService : IGeoLocationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GeoLocationService> _logger;
    private const string AccessKey = "fa7cfc7586d347d5f8338192c1960405";
    private const string TestIp = "161.185.160.93"; 

    public GeoLocationService(HttpClient httpClient, ILogger<GeoLocationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <summary>
    /// Gets location information for the given IP address
    /// </summary>
    /// <param name="ip">IP address to geolocate</param>
    /// <returns>Location information including region and country</returns>
    public async Task<LocationResponse?> GetLocationAsync(string ip)
    {
        try
        {
            if (string.IsNullOrEmpty(ip) || ip == "::1" || ip == "127.0.0.1" || ip.Contains("localhost"))
            {
                _logger.LogInformation("Local IP detected ({Ip}), using test IP: {TestIp}", ip, TestIp);
                ip = TestIp;
            }

            var url = $"http://api.ipapi.com/api/{ip}?access_key={AccessKey}";
            _logger.LogInformation("Requesting geolocation for IP: {Ip}", ip);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var location = JsonSerializer.Deserialize<LocationResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _logger.LogInformation("Geolocation retrieved: {Region}, {Country}", 
                location?.Region_Name, location?.Country_Name);

            return location;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtaining geolocation for IP: {Ip}", ip);
            return null;
        }
    }
}

