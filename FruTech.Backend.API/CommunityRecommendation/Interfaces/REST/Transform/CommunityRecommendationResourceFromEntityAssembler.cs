using FruTech.Backend.API.CommunityRecommendation.Interfaces.REST.Resources;
using CommunityRecommendationAggregate = FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates.CommunityRecommendation;

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
    /// the <see cref="CommunityRecommendationAggregate"/> entity
    /// </param>
    /// the <see cref="CommunityRecommendationResource"/> resource
    /// <returns></returns>
    public static CommunityRecommendationResource ToResourceFromEntity(CommunityRecommendationAggregate entity)
    {
        return new CommunityRecommendationResource(
            entity.Id,
            entity.UserName,
            entity.Role,
            entity.CommentDate,
            entity.Comment);
    }
}