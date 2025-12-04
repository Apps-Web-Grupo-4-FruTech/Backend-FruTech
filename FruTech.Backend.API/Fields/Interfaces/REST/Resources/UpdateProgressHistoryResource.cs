namespace FruTech.Backend.API.Fields.Interfaces.REST.Resources;

/// <summary>
/// Input resource to update a ProgressHistory. Only these fields can be modified via PUT.
/// </summary>
/// <param name="Watered">Date when watering occurred</param>
/// <param name="Fertilized">Date when fertilizing occurred</param>
/// <param name="Pests">Date when pests were treated</param>
public record UpdateProgressHistoryResource(
    DateTime Watered,
    DateTime Fertilized,
    DateTime Pests
);

