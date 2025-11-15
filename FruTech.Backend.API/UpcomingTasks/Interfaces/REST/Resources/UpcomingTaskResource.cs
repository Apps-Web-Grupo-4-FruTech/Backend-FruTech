namespace FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Resources;

public record UpcomingTaskResource(int Id, DateTime Date, string Name, string TaskDescription, int TaskId);

