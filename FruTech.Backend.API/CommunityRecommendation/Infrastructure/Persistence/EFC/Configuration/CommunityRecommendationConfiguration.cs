using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunityRecommendationAggregate = FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates.CommunityRecommendation;

namespace FruTech.Backend.API.CommunityRecommendation.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Configuration settings for the Community Recommendation feature.
/// </summary>
public class CommunityRecommendationConfiguration : IEntityTypeConfiguration<CommunityRecommendationAggregate>
{
    public void Configure(EntityTypeBuilder<CommunityRecommendationAggregate> builder)
    {
        builder.ToTable("community_recommendations");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(c => c.User).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Role).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
    }
}