using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
/// <summary>
///  Assembler to convert Task entity to TaskResource.
/// </summary>
public static class TaskResourceFromEntityAssembler
{
    /// <summary>
    ///  Converts a Task entity to a TaskResource.
    /// </summary>
    /// <param name="entity"></param>
    public static TaskResource ToResourceFromEntity(Domain.Model.Aggregate.Task entity)
    {
        var fieldName = entity.Field?.Name ?? "Unknown";
        
        return new TaskResource(
            entity.Id,
            entity.FieldId,
            fieldName,
            entity.Description,
            entity.DueDate,
            entity.CreatedDate,
            entity.UpdatedDate
        );
    }
}
