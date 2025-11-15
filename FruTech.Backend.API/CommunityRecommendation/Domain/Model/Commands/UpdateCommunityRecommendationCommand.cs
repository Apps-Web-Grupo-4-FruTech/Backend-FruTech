namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates;

/// <summary>
/// Command to update a community recommendation
/// </summary>
public record UpdateCommunityRecommendationCommand(int id, string user, string role,);
    string description);