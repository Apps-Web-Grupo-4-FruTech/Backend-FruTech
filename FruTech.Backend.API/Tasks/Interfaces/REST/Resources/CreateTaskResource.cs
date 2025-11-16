namespace FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

/// <summary>
/// Resource para crear una tarea
/// </summary>
/// <param name="FieldId">ID del campo asociado</param>
/// <param name="Description">Descripción de la tarea</param>
/// <param name="DueDate">Fecha de vencimiento</param>
public record CreateTaskResource(int FieldId, string Description, DateTime DueDate);
