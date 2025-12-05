namespace FruTech.Backend.API.Shared.Domain.Services;

/// <summary>
/// Service interface for JWT token generation.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>A signed JWT token string.</returns>
    string GenerateToken(FruTech.Backend.API.User.Domain.Model.Aggregates.User user);
}

