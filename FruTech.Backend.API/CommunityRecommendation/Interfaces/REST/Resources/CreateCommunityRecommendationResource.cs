namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

/// <summary>
/// Create community recommendation resource for REST API
/// <param name="id">
///     The unique identifier of the community recommendation
/// </param>
/// <param name="user">
///     The user who made the recommendation
/// </param>
/// <param name="role">
///     The role of the user
/// </param>
/// <param name="description">
///     The description of the recommendation
/// </summary>
public record CreateCommunityRecommendationResource(int id, string user, string role, string description);