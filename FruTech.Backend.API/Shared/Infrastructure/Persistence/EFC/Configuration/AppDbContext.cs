using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using CommunityRecommendationAggregate = FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates.CommunityRecommendation;

namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // DbSets
    public DbSet<CommunityRecommendationAggregate> CommunityRecommendations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        //builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Create all entities configurations
        
        //Publishing Context
        //builder.ApplyPublishingConfiguration();
        

        builder.UseSnakeCaseNamingConvention();
    }
}
