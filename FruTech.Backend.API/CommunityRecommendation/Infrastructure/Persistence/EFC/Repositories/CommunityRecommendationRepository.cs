using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

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
    ///  Gets all community recommendations with role information joined from User, UserRole, and Role entities.
    /// </summary>
    /// <returns>Enumerable of community recommendations with populated Role property</returns>
    public new async Task<IEnumerable<Domain.Model.Aggregates.CommunityRecommendation>> ListAsync()
    {
        var recommendations = await context.Set<Domain.Model.Aggregates.CommunityRecommendation>()
            .GroupJoin(
                context.Set<UserAggregate>(),
                cr => cr.UserName,
                u => u.UserName,
                (cr, users) => new { Recommendation = cr, Users = users }
            )
            .SelectMany(
                x => x.Users.DefaultIfEmpty(),
                (x, user) => new { x.Recommendation, User = user }
            )
            .GroupJoin(
                context.Set<FruTech.Backend.API.User.Domain.Model.Entities.UserRole>(),
                x => x.User != null ? x.User.Id : 0,
                ur => ur.UserId,
                (x, userRoles) => new { x.Recommendation, x.User, UserRoles = userRoles }
            )
            .SelectMany(
                x => x.UserRoles.DefaultIfEmpty(),
                (x, userRole) => new { x.Recommendation, x.User, UserRole = userRole }
            )
            .GroupJoin(
                context.Set<FruTech.Backend.API.User.Domain.Model.Entities.Role>(),
                x => x.UserRole != null ? x.UserRole.RoleId : 0,
                r => r.Id,
                (x, roles) => new { x.Recommendation, x.User, x.UserRole, Roles = roles }
            )
            .SelectMany(
                x => x.Roles.DefaultIfEmpty(),
                (x, role) => new { x.Recommendation, x.User, x.UserRole, Role = role }
            )
            .Select(x => new Domain.Model.Aggregates.CommunityRecommendation
            {
                Id = x.Recommendation.Id,
                UserName = x.Recommendation.UserName,
                CommentDate = x.Recommendation.CommentDate,
                Comment = x.Recommendation.Comment,
                Role = x.Role != null ? x.Role.Name : "Desconocido"
            })
            .ToListAsync();

        return recommendations;
    }

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
