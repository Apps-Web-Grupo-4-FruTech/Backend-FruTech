using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.CommunityRecommendation.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for CommunityRecommendation entity.
/// </summary>
/// <param name ="context"></param>
public class CommunityRecommendationRepository(AppDbContext context) 
    : BaseRepository<Domain.Model.Aggregates.CommunityRecommendation>(context),
        Domain.Repositories.ICommunityRecommendationRepository
{
    /// <summary>
    ///  Updates the author name in all community recommendations.
    /// </summary>
    /// <param name="oldUserName"></param>
    /// <param name="newUserName"></param>
    public async Task UpdateAuthorNameAsync(string oldUserName, string newUserName)
    {
        if (string.IsNullOrWhiteSpace(oldUserName) || string.IsNullOrWhiteSpace(newUserName) || oldUserName == newUserName)
            return;
        var recommendations = await context.Set<Domain.Model.Aggregates.CommunityRecommendation>()
            .Where(r => r.UserName == oldUserName)
            .ToListAsync();
        if (!recommendations.Any()) return;
        foreach (var r in recommendations)
        {
            r.UserName = newUserName;
        }
    }
}
