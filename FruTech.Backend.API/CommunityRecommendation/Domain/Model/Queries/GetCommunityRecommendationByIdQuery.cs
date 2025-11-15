namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates;

/// <summary>
/// Represents a query to retrieve a community recommendation by its unique identifier.
/// </summary>
/// <param name ="RecomendationId">
///     The unique identifier of the community recommendation to be retrieved.
/// </param>
public record GetCommunityRecommendationByIdQuery(int RecomendationId);