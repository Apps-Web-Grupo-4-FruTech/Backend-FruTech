namespace FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Resources;

public record CreateUpcomingTaskResource(DateTime Date, string Name, string TaskDescription, int TaskId);

