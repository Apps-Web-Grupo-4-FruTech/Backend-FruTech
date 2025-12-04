namespace FruTech.Backend.API.Tasks.Domain.Model.Commands;

/// <summary>
/// Command to update an existing task. IDs and audit fields cannot be modified.
/// </summary>
public record EditTaskCommand(int Id, string Description, DateTime DueDate);
