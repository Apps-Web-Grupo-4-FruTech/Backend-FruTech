namespace FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

/// <summary>
/// Resource de respuesta de tarea
/// </summary>
/// <param name="Id">ID de la tarea</param>
/// <param name="FieldId">ID del campo asociado</param>
/// <param name="Description">Descripción de la tarea</param>
/// <param name="DueDate">Fecha de vencimiento</param>
public record TaskResource(int Id, int FieldId, string Description, DateTime DueDate);
