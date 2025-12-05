namespace FruTech.Backend.API.Shared.Domain.Settings;

/// <summary>
/// Configuration settings for JWT token generation and validation.
/// </summary>
public class TokenSettings
{
    /// <summary>
    /// The secret key used to sign tokens (must be at least 32 characters for HMAC SHA256).
    /// </summary>
    public string Secret { get; set; } = string.Empty;

    /// <summary>
    /// Token expiration time in minutes.
    /// </summary>
    public int ExpirationMinutes { get; set; } = 10080; // 7 days by default

    /// <summary>
    /// The issuer claim for the token.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// The audience claim for the token.
    /// </summary>
    public string Audience { get; set; } = string.Empty;
}

