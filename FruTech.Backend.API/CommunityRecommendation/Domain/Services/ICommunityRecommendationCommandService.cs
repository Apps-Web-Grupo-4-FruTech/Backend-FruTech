namespace FruTech.Backend.API.CommunityRecommendation.Domain.Services;

using Model.Commands;

/// <summary>
/// Represents the service responsible for handling commands related to community recommendations.
/// </summary>
public interface ICommunityRecommendationCommandService
{
    Task<Model.Aggregates.CommunityRecommendation?> Handle(UpdateCommunityRecommendationCommand command);
}