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
        builder.Property(c => c.UserName).IsRequired().HasMaxLength(100).HasColumnName("user_name");
        builder.Property(c => c.CommentDate).IsRequired().HasColumnName("comment_date");
        builder.Property(c => c.Comment).IsRequired().HasMaxLength(1000).HasColumnName("comment");
    }
}