namespace FruTech.Backend.API.CropFields.Domain.Model.Queries;

/// <summary>
/// Query para obtener un CropField por FieldId (relación 1:1)
/// </summary>
public record GetCropFieldByFieldIdQuery(int FieldId);

