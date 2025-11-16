namespace FruTech.Backend.API.Tasks.Domain.Model.Commands;

/// <summary>
/// Comando para crear una tarea asociada a un Field
/// </summary>
public record CreateTaskCommand(int FieldId, string Description, DateTime DueDate);
