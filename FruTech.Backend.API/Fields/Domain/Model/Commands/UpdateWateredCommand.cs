namespace FruTech.Backend.API.Fields.Domain.Model.Commands;

/// <summary>
/// Comando para actualizar la fecha de regado en ProgressHistory
/// </summary>
public record UpdateWateredCommand(int FieldId, DateTime Watered);

