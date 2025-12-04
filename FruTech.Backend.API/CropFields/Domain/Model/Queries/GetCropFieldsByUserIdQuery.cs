namespace FruTech.Backend.API.CropFields.Domain.Model.Queries;

/// <summary>
/// Query para obtener todos los CropFields de un usuario específico
/// </summary>
public record GetCropFieldsByUserIdQuery(int UserId);

