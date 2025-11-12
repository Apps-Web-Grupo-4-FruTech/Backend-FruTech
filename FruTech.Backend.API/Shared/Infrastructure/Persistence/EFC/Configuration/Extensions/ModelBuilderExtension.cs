using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extension
/// </summary>
/// <remarks>
///     This class contains extension methods for the ModelBuilder class to customize the model configuration.
///     It includes a method to apply snake case naming conventions to the database schema.
///     It also pluralizes table names.
/// </remarks>
public static class ModelBuilderExtension
{
    /// <summary>
    ///     Use snake case naming convention
    /// </summary>
    /// <remarks>
    ///     This method sets the naming convention for tables, columns, keys, foreign keys, and indexes
    ///     to snake case format.
    /// </remarks>
    /// <param name="builder"></param>
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName)) entity.SetTableName(tableName.ToPlural().ToSnakeCase());
            
            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().ToSnakeCase());

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (!string.IsNullOrEmpty(keyName)) key.SetName(keyName.ToSnakeCase());
            }

            foreach (var key in entity.GetForeignKeys())
            {
                var foreignKeyName = key.GetConstraintName();
                if (!string.IsNullOrEmpty(foreignKeyName)) 
                    key.SetConstraintName(foreignKeyName.ToSnakeCase());
            }
            
            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if (!string.IsNullOrEmpty(indexName)) 
                    index.SetDatabaseName(indexName.ToSnakeCase());
            }
        }
    }
}