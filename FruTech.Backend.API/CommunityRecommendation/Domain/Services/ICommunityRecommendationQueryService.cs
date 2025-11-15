namespace FruTech.Backend.API.CommunityRecommendation.Domain.Services;

using Model.Queries;

/// <summary>
/// Represents the service responsible for handling queries related to community recommendations.
/// </summary>
public interface ICommunityRecommendationQueryService
{
    Task<Model.Aggregates.CommunityRecommendation?> Handle(GetCommunityRecommendationByIdQuery query);
    Task<IEnumerable<Model.Aggregates.CommunityRecommendation>> Handle(GetAllCommunityRecommendationsQuery query);
}