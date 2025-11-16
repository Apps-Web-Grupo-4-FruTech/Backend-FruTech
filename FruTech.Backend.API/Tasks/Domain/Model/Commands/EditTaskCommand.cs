namespace FruTech.Backend.API.Tasks.Domain.Model.Commands;

/// <summary>
/// Comando para actualizar una tarea existente
/// </summary>
public record EditTaskCommand(int Id, int FieldId, string Description, DateTime DueDate);
