using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates;

/// <summary>
/// Repository implementation for CommunityRecommendation entity.
/// </summary>
/// <param name ="context"></param>
public class CommunityRecommendationRepository(AppDBContext context) 
    : BaseRepository<CommunityRecommendation>(context), ICommunityRecommendationRepository;