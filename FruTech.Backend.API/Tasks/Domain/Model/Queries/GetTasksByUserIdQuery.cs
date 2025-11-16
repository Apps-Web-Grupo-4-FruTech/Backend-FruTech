namespace FruTech.Backend.API.Tasks.Domain.Model.Queries;

/// <summary>
/// Query para obtener todas las tareas de un usuario (a través de sus Fields)
/// </summary>
public record GetTasksByUserIdQuery(int UserId);

