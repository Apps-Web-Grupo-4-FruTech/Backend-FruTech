namespace FruTech.Backend.API.Fields.Domain.Model.Commands;

/// <summary>
/// Command to create a field (Field) and its associated ProgressHistory automatically
/// </summary>
public record CreateFieldCommand(
    int UserId,
    byte[]? ImageContent,
    string? ImageContentType,
    string Name,
    string Location,
    string FieldSize
);
