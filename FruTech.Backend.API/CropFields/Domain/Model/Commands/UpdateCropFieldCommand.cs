using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Commands;

/// <summary>
/// Command to update attributes of a CropField (supports partial updates). The identifier comes from the route.
/// </summary>
public record UpdateCropFieldCommand(
    string? Crop,
    DateTimeOffset? PlantingDate,
    DateTimeOffset? HarvestDate,
    CropFieldStatus? Status
);
