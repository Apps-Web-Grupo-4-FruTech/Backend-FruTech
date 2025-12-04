namespace FruTech.Backend.API.Fields.Interfaces.REST.Resources;

/// <summary>
/// Resource for ProgressHistory data (Watered, Fertilized, Pests)
/// </summary>
public record ProgressHistoryResource(
    int Id,
    DateTime Watered,
    DateTime Fertilized,
    DateTime Pests
);

