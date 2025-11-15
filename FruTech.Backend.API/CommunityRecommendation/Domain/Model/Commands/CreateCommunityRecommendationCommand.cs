namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;

/// <summary>
/// Command to create a new community recommendation
/// </summary>
public record CreateCommunityRecommendationCommand(int id,  string user, string role, string description);