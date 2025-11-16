namespace FruTech.Backend.API.Fields.Interfaces.REST.Resources;

/// <summary>
/// Read resource for Field. Exposes only ID references and audit dates.
/// </summary>
public record FieldResource(
    int Id,
    int UserId,
    string ImageUrl,
    string Name,
    string Location,
    string FieldSize,
    int? ProgressHistoryId,
    int? CropFieldId,
    IReadOnlyList<int> TaskIds,
    DateTimeOffset? CreatedDate,
    DateTimeOffset? UpdatedDate
);

