namespace FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

/// <summary>
/// Resource to update an existing task via PUT. IDs and audit fields are not allowed.
/// </summary>
/// <param name="Description">Task description</param>
/// <param name="DueDate">Due date</param>
public record EditTaskResource(string Description, DateTime DueDate);
