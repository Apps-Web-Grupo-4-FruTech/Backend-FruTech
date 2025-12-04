namespace FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

/// <summary>
/// Task response resource
/// </summary>
/// <param name="Id">Task ID</param>
/// <param name="FieldId">Associated field ID</param>
/// <param name="FieldName">Name of the associated field</param>
/// <param name="Description">Task description</param>
/// <param name="DueDate">Due date</param>
/// <param name="CreatedDate">Date when task was created</param>
/// <param name="UpdatedDate">Date when task was last updated</param>
public record TaskResource(
    int Id, 
    int FieldId,
    string FieldName,
    string Description, 
    DateTime DueDate,
    DateTimeOffset? CreatedDate,
    DateTimeOffset? UpdatedDate
);
