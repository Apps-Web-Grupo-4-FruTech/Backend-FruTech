using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Tasks.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyTaskConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Domain.Model.Aggregate.Task>(entity =>
        {
            entity.ToTable("task");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(t => t.Description).HasColumnName("description").IsRequired();
            entity.Property(t => t.DueDate).HasColumnName("due_date").IsRequired();
            entity.Property(t => t.FieldId).HasColumnName("field_id").IsRequired();
            entity.HasOne<FruTech.Backend.API.Fields.Domain.Model.Entities.Field>()
                  .WithMany()
                  .HasForeignKey(t => t.FieldId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
