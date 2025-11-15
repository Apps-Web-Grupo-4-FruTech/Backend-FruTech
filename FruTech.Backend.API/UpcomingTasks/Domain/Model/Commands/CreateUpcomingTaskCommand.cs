namespace FruTech.Backend.API.UpcomingTasks.Domain.Model.Commands;

public record CreateUpcomingTaskCommand(DateTime Date, string Name, string TaskDescription, int TaskId);

