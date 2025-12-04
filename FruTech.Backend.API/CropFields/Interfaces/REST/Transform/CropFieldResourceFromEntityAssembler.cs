using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Interfaces.REST.Resources;

namespace FruTech.Backend.API.CropFields.Interfaces.REST.Transform;
/// <summary>
///  Transform of entity to resource.
/// </summary>
public static class CropFieldResourceFromEntityAssembler
{
    /// <summary>
    ///  Transform a CropField entity to a CropFieldResource.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static CropFieldResource ToResource(CropField entity)
    {
        var fieldName = entity.Field?.Name ?? "Unknown Field";
        var plantingDate = entity.PlantingDate?.ToString("yyyy-MM-dd") ?? string.Empty;
        var harvestDate = entity.HarvestDate?.ToString("yyyy-MM-dd") ?? string.Empty;
        
        var days = entity.PlantingDate.HasValue && entity.HarvestDate.HasValue 
            ? ((int)(entity.HarvestDate.Value - entity.PlantingDate.Value).TotalDays).ToString() 
            : string.Empty;
        
        var status = entity.Status.ToString();
        var title = string.IsNullOrWhiteSpace(entity.Crop) ? "(Sin cultivo)" : entity.Crop;

        return new CropFieldResource(
            entity.Id,
            title,
            fieldName,
            plantingDate,
            harvestDate,
            status,
            days
        );
    }
}

