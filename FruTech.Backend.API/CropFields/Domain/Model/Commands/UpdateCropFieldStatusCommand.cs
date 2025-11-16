using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Commands;

/// <summary>
/// Comando para actualizar el status de un CropField
/// </summary>
public record UpdateCropFieldStatusCommand(
    int CropFieldId,
    CropFieldStatus Status
);

