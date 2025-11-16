namespace FruTech.Backend.API.Fields.Domain.Model.Commands;

/// <summary>
/// Comando para actualizar la fecha de control de plagas en ProgressHistory
/// </summary>
public record UpdatePestsCommand(int FieldId, DateTime Pests);

