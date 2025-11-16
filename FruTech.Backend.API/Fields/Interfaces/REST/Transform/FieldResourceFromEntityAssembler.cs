using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Fields.Interfaces.REST.Transform;

public static class FieldResourceFromEntityAssembler
{
    public static FieldResource ToResource(Field entity)
    {
        // Extract IDs without loading heavy collections
        var progressHistoryId = entity.ProgressHistory?.Id;
        var cropFieldId = entity.CropFieldId; // explicitly mapped in AppDbContext
        var taskIds = entity.Tasks?.Select(t => t.Id).ToList() ?? new List<int>();

        return new FieldResource(
            entity.Id,
            entity.UserId,
            entity.ImageUrl,
            entity.Name,
            entity.Location,
            entity.FieldSize,
            progressHistoryId,
            cropFieldId,
            taskIds,
            entity.CreatedDate,
            entity.UpdatedDate
        );
    }

    public static FieldResource ToResource(Field entity, IReadOnlyList<int> taskIds, int? progressHistoryId)
    {
        return new FieldResource(
            entity.Id,
            entity.UserId,
            entity.ImageUrl,
            entity.Name,
            entity.Location,
            entity.FieldSize,
            progressHistoryId,
            entity.CropFieldId,
            taskIds,
            entity.CreatedDate,
            entity.UpdatedDate
        );
    }
}
