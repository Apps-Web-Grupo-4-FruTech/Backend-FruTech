using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;
using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;
using CommunityRecommendationAggregate = FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates.CommunityRecommendation;

namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppDbContext : DbContext
    // DbSets
    public DbSet<CommunityRecommendationAggregate> CommunityRecommendations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // DbSets de User y UpcomingTasks
        public DbSet<UserAggregate> Users { get; set; }
        public DbSet<UpcomingTaskAggregate> UpcomingTasks { get; set; }
        
        // DbSets de CropFields y Fields
        public DbSet<CropField> CropFields { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<ProgressHistory> ProgressHistory { get; set; }
        public DbSet<FieldTask> FieldTasks { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración CropField
            builder.Entity<CropField>().ToTable("CropFields");
            builder.Entity<CropField>().HasKey(p => p.Id);
            builder.Entity<CropField>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<CropField>().Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Entity<CropField>().Property(p => p.Field).IsRequired();
            builder.Entity<CropField>().Property(p => p.Status).IsRequired().HasMaxLength(100);
            builder.Entity<CropField>().Property(p => p.SoilType).HasMaxLength(100);
            builder.Entity<CropField>().Property(p => p.Watering).HasMaxLength(200);
            builder.Entity<CropField>().Property(p => p.Sunlight).HasMaxLength(100);

            // Configuración Field
            builder.Entity<Field>().ToTable("Fields");
            builder.Entity<Field>().HasKey(f => f.Id);
            builder.Entity<Field>().Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Entity<Field>().Property(f => f.Name).IsRequired().HasMaxLength(200);
            builder.Entity<Field>().Property(f => f.Location).HasMaxLength(300);
            builder.Entity<Field>().Property(f => f.ImageUrl).HasMaxLength(500);
            builder.Entity<Field>().Property(f => f.FieldSize).HasMaxLength(50);
            builder.Entity<Field>().Property(f => f.Product).HasMaxLength(150);
            builder.Entity<Field>().Property(f => f.Crop).HasMaxLength(150);

            // Configuración ProgressHistory
            builder.Entity<ProgressHistory>().ToTable("ProgressHistory");
            builder.Entity<ProgressHistory>().HasKey(p => p.Id);
            builder.Entity<ProgressHistory>().Property(p => p.Id).ValueGeneratedOnAdd();

            // Configuración FieldTask
            builder.Entity<FieldTask>().ToTable("FieldTasks");
            builder.Entity<FieldTask>().HasKey(t => t.Id);
            builder.Entity<FieldTask>().Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Entity<FieldTask>().Property(t => t.Name).HasMaxLength(200);
            builder.Entity<FieldTask>().Property(t => t.TaskDescription).HasMaxLength(500);
        }
    }
}
