using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Tasks.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyTaskConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Domain.Model.Aggregate.Task>(entity =>
        {
            entity.ToTable("task");
            entity.HasKey(t => t.id);
            entity.Property(t => t.id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(t => t.description).HasColumnName("description").IsRequired();
            entity.Property(t => t.due_date).HasColumnName("due_date").IsRequired();
            entity.Property(t => t.field).HasColumnName("field").IsRequired();
        });
    }
}

