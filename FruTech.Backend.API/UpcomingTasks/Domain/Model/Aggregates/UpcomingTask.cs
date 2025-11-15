namespace FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates;

public class UpcomingTask
{
    public int Id { get; private set; }
    public int TaskId { get; private set; }
    public string TaskDescription { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }

    // Constructor sin parámetros requerido por EF Core
    private UpcomingTask() { }

    public UpcomingTask(DateTime date, string name, string taskDescription, int taskId)
    {
        Date = date;
        Name = name;
        TaskDescription = taskDescription;
        TaskId = taskId;
    }
}
