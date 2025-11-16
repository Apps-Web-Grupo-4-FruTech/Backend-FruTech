namespace FruTech.Backend.API.Fields.Domain.Model.Commands;

/// <summary>
/// Comando para actualizar la fecha de fertilizado en ProgressHistory
/// </summary>
public record UpdateFertilizedCommand(int FieldId, DateTime Fertilized);

