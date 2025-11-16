using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FruTech.Backend.API.CommunityRecommendation.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for CommunityRecommendation entity.
/// </summary>
/// <param name ="context"></param>
public class CommunityRecommendationRepository(AppDbContext context) 
    : BaseRepository<Domain.Model.Aggregates.CommunityRecommendation>(context),
        Domain.Repositories.ICommunityRecommendationRepository
{
}
