namespace FruTech.Backend.API.CropFields.Interfaces.REST.Resources;

/// <summary>
/// Recurso de lectura para CropField expuesto al frontend.
/// </summary>
public record CropFieldResource(
    int Id,
    string Title,
    string FieldName,
    string PlantingDate,
    string HarvestDate,
    string Status,
    string Days
);

