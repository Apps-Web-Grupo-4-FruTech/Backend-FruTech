using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Commands;

/// <summary>
/// Comando para crear un CropField asociado a un Field existente
/// </summary>
public record CreateCropFieldCommand(
    int FieldId,
    string Crop,
    string SoilType,
    string Sunlight,
    string Watering,
    DateTime? PlantingDate,
    DateTime? HarvestDate,
    CropFieldStatus Status
);

