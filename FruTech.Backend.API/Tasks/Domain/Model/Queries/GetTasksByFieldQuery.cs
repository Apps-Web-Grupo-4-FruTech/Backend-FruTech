namespace FruTech.Backend.API.Tasks.Domain.Model.Queries;
/// <summary>
///  GetTasksByFieldQuery record represents a query to retrieve tasks associated with a specific field identifier.
/// </summary>
/// <param name="fieldId"></param>
public record GetTasksByFieldQuery(int fieldId);
