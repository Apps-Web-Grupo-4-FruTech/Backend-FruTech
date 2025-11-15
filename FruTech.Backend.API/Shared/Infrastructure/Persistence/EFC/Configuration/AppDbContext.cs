using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User; // alias
using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask; // agregado

namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserAggregate> Users { get; set; } 
    public DbSet<UpcomingTaskAggregate> UpcomingTasks { get; set; } // nuevo DbSet
    
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
