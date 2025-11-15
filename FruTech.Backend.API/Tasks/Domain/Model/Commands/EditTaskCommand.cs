namespace FruTech.Backend.API.Tasks.Domain.Model.Commands;

public record EditTaskCommand(int id, string description, string due_date, string field);

