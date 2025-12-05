using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FruTech.Backend.API.User.Domain.Model.Aggregates;
using FruTech.Backend.API.Shared.Domain.Services;
using Microsoft.IdentityModel.Tokens;

namespace FruTech.Backend.API.Shared.Infrastructure.Services;

/// <summary>
/// Service implementation for JWT token generation.
/// </summary>
public class TokenService : ITokenService
{
    private readonly Shared.Domain.Settings.TokenSettings _tokenSettings;
    private readonly ILogger<TokenService> _logger;

    public TokenService(Shared.Domain.Settings.TokenSettings tokenSettings, ILogger<TokenService> logger)
    {
        _tokenSettings = tokenSettings;
        _logger = logger;
    }

    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>A signed JWT token string.</returns>
    public string GenerateToken(FruTech.Backend.API.User.Domain.Model.Aggregates.User user)
    {
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.ExpirationMinutes),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.WriteToken(token);

            _logger.LogInformation("Token generated successfully for user {UserId}", user.Id);
            return encodedToken;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating token for user {UserId}", user.Id);
            throw;
        }
    }
}

