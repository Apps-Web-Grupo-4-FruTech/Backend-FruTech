using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;
using FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Transform;

/// <summary>
/// Converts CreateCommunityRecommendationResource to CreateCommunityRecommendationCommand
/// </summary>
public static class CreateCommunityRecommendationCommandFromResourceAssembler
{
    public static CreateCommunityRecommendationCommand ToCommandFromResource(CreateCommunityRecommendationResource resource)
    {
        return new CreateCommunityRecommendationCommand(
            resource.UserName,
            resource.Comment
        );
    }
}