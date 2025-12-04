namespace FruTech.Backend.API.CommunityRecommendation.Domain.Services;

using Model.Commands;

/// <summary>
/// Represents the service responsible for handling commands related to community recommendations.
/// </summary>
public interface ICommunityRecommendationCommandService
{
    Task<Model.Aggregates.CommunityRecommendation?> Handle(UpdateCommunityRecommendationCommand command);
    Task<Model.Aggregates.CommunityRecommendation> Handle(CreateCommunityRecommendationCommand command);
    Task<Model.Aggregates.CommunityRecommendation?> HandleUpdateContent(int id, string newComment);
}