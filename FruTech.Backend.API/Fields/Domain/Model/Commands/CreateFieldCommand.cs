namespace FruTech.Backend.API.Fields.Domain.Model.Commands;

/// <summary>
/// Comando para crear un campo (Field) y su ProgressHistory asociado automáticamente
/// </summary>
public record CreateFieldCommand(
    int UserId,
    string ImageUrl,
    string Name,
    string Location,
    string FieldSize
);

