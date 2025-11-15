namespace FruTech.Backend.API.Tasks.Domain.Model.Commands;

public record CreateTaskCommand(string description, string due_date, string field);

