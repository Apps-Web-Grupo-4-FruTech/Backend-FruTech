using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using FruTech.Backend.API.Tasks.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;
using CommunityRecommendationAggregate = FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates.CommunityRecommendation;

namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    /// <summary>
    ///  Application database context
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<UserAggregate> Users { get; set; }
        public DbSet<CommunityRecommendationAggregate> CommunityRecommendations { get; set; }
        public DbSet<CropField> CropFields { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<ProgressHistory> ProgressHistories { get; set; }
        public DbSet<Tasks.Domain.Model.Aggregate.Task> Tasks { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }
        
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var now = DateTimeOffset.Now; 

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Shared.Domain.Model.ValueObjects.IEntityWithCreatedUpdatedDate auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedDate = auditable.CreatedDate ?? now;
                        auditable.UpdatedDate = auditable.UpdatedDate ?? now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedDate = now;
                    }
                }
            }
        }
        /// <summary>
        ///  Configures the entity mappings and relationships
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseSnakeCaseNamingConvention();
            
            builder.Entity<UserAggregate>().ToTable("users");
            builder.Entity<UserAggregate>().HasKey(u => u.Id);
            builder.Entity<UserAggregate>().Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Entity<UserAggregate>().Property(u => u.UserName).IsRequired().HasMaxLength(100);
            builder.Entity<UserAggregate>().Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Entity<UserAggregate>().Property(u => u.PhoneNumber).HasMaxLength(20);
            builder.Entity<UserAggregate>().Property(u => u.Identificator).IsRequired().HasMaxLength(50);
            builder.Entity<UserAggregate>().Property(u => u.PasswordHash).IsRequired();
            builder.Entity<UserAggregate>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<UserAggregate>().HasIndex(u => u.Identificator).IsUnique();
            
            builder.Entity<Field>().ToTable("fields");
            builder.Entity<Field>().HasKey(f => f.Id);
            builder.Entity<Field>().Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Entity<Field>().Property(f => f.UserId).IsRequired();
            builder.Entity<Field>().Property(f => f.Name).IsRequired().HasMaxLength(200);
            builder.Entity<Field>().Property(f => f.Location).HasMaxLength(300);
            builder.Entity<Field>().Property(f => f.FieldSize).HasMaxLength(50);
            builder.Entity<Field>().Property(f => f.ImageContent).HasColumnType("longblob");
            builder.Entity<Field>().Property(f => f.ImageContentType).HasMaxLength(100);


            builder.Entity<Field>()
                .HasOne<UserAggregate>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<CropField>().ToTable("crop_fields");
            builder.Entity<CropField>().HasKey(c => c.Id);
            builder.Entity<CropField>().Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Entity<CropField>().Property(c => c.FieldId).IsRequired();
            builder.Entity<CropField>().Property(c => c.Crop).IsRequired().HasMaxLength(200);
            builder.Entity<CropField>().Property(c => c.SoilType).HasMaxLength(100);
            builder.Entity<CropField>().Property(c => c.Watering).HasMaxLength(200);
            builder.Entity<CropField>().Property(c => c.Sunlight).HasMaxLength(100);


            builder.Entity<CropField>()
                .Property(c => c.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(50);


            builder.Entity<Field>()
                .HasOne(f => f.CropField)
                .WithOne(cf => cf.Field)
                .HasForeignKey<CropField>(c => c.FieldId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CropField>().HasIndex(c => c.FieldId).IsUnique();


            builder.Entity<ProgressHistory>().ToTable("progress_histories");
            builder.Entity<ProgressHistory>().HasKey(p => p.Id);
            builder.Entity<ProgressHistory>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<ProgressHistory>().Property(p => p.FieldId).IsRequired();
            builder.Entity<ProgressHistory>().Property(p => p.Watered).IsRequired();
            builder.Entity<ProgressHistory>().Property(p => p.Fertilized).IsRequired();
            builder.Entity<ProgressHistory>().Property(p => p.Pests).IsRequired();
            
            builder.Entity<Field>()
                .HasOne(f => f.ProgressHistory)
                .WithOne()
                .HasForeignKey<ProgressHistory>(p => p.FieldId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<ProgressHistory>().HasIndex(p => p.FieldId).IsUnique();


            builder.Entity<Tasks.Domain.Model.Aggregate.Task>().ToTable("tasks");
            builder.Entity<Tasks.Domain.Model.Aggregate.Task>().HasKey(t => t.Id);
            builder.Entity<Tasks.Domain.Model.Aggregate.Task>().Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Entity<Tasks.Domain.Model.Aggregate.Task>().Property(t => t.FieldId).IsRequired();
            builder.Entity<Tasks.Domain.Model.Aggregate.Task>().Property(t => t.Description).IsRequired().HasMaxLength(500);
            builder.Entity<Tasks.Domain.Model.Aggregate.Task>().Property(t => t.DueDate).IsRequired();
            
            builder.Entity<Field>()
                .HasMany(f => f.Tasks!)
                .WithOne(t => t.Field)
                .HasForeignKey(t => t.FieldId)
                .OnDelete(DeleteBehavior.Cascade);

   
            builder.Entity<CommunityRecommendationAggregate>().ToTable("community_recommendations");
            builder.Entity<CommunityRecommendationAggregate>().HasKey(c => c.Id);
            builder.Entity<CommunityRecommendationAggregate>().Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Entity<CommunityRecommendationAggregate>().Property(c => c.UserName).IsRequired().HasMaxLength(100);
            builder.Entity<CommunityRecommendationAggregate>().Property(c => c.Comment).IsRequired().HasMaxLength(2000);
            builder.Entity<CommunityRecommendationAggregate>().Property(c => c.CommentDate).IsRequired();
        }
    }
}
