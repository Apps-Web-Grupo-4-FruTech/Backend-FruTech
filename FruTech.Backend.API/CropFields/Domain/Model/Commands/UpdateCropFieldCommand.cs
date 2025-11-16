namespace FruTech.Backend.API.CropFields.Domain.Model.Commands;

/// <summary>
/// Command to update the crop attribute of a CropField
/// </summary>
public record UpdateCropFieldCommand(
    int CropFieldId,
    string Crop
);

