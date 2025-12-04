using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Commands;

/// <summary>
/// Command to create a CropField associated with an existing Field
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
