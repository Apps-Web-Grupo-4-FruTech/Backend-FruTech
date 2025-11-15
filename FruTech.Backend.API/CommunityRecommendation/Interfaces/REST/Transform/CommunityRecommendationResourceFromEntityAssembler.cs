using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Commands;
using FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;

namespace FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CommunityRecommendation to CommunityRecommendationResource
/// </summary>
public static class CommunityRecommendationResourceFromEntityAssembler
{
    /// <summary>
    /// Convert CommunityRecommendation to CommunityRecommendationResource
    /// </summary>
    /// <param name="entity">
    /// the <see cref="CommunityRecommendation"/> entity
    /// </param>
    /// the <see cref="CommunityRecommendationResource"/> resource
    /// <returns></returns>
    public static CommunityRecommendationResource ToResourceFromEntity(CommunityRecommendation entity)
    {
        return new CommunityRecommendationResource(
            entity.Id,
            entity.User,
            entity.Role,
            entity.Description);
    }
}