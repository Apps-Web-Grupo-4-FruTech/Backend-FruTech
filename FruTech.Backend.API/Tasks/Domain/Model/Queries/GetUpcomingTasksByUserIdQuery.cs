namespace FruTech.Backend.API.Tasks.Domain.Model.Queries;

/// <summary>
/// Query para obtener las próximas tareas (ordenadas por DueDate ascendente) de un usuario, limitado por count.
/// </summary>
/// <param name="UserId">Identificador del usuario dueño de los Fields</param>
/// <param name="Count">Cantidad máxima de tareas a devolver</param>
public record GetUpcomingTasksByUserIdQuery(int UserId, int Count);
