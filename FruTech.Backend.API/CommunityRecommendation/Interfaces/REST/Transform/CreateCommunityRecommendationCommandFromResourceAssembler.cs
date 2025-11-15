using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;
using FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform CreateCommunityRecommendationResource to CreateCommunityRecommendationCommand
/// </summary>
public static class CreateCommunityRecommendationCommandFromResourceAssembler
{
    public static CreateCommunityRecommendationCommand ToCommandFromResource(CreateCommunityRecommendationResource resource)
    {
        return new CreateCommunityRecommendationCommand(
            resource.User,
            resource.Role,
            resource.Description);
    }
}