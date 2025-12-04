using FruTech.Backend.API.CommunityRecommendation.Domain.Model.Queries;
using FruTech.Backend.API.CommunityRecommendation.Domain.Repositories;
using  FruTech.Backend.API.CommunityRecommendation.Domain.Services;

namespace FruTech.Backend.API.CommunityRecommendation.Application.Internal.QueryServices;

/// <summary>
/// Query Service for Community Recommendations
/// </summary>
public class CommunityRecommendationQueryService(ICommunityRecommendationRepository communityRecommendationRepository)
    : ICommunityRecommendationQueryService
{
    /// <summary>
    ///  Get all Community Recommendations
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Domain.Model.Aggregates.CommunityRecommendation>> Handle(GetAllCommunityRecommendationsQuery query)
    {
        return await communityRecommendationRepository.ListAsync();
    }

    /// <summary>
    ///  Get Community Recommendation by Id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<Domain.Model.Aggregates.CommunityRecommendation?> Handle(GetCommunityRecommendationByIdQuery query)
    {
        return await communityRecommendationRepository.FindByIdAsync(query.RecomendationId);
    }
}