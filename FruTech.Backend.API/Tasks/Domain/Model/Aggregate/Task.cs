namespace FruTech.Backend.API.Tasks.Domain.Model.Aggregate;
public class Task
{
    public int id { get; set; }
    public string description { get; set; }
    public string due_date { get; set; }
    public string field { get; set; }
    public Task()
    {
        description = string.Empty;
        due_date = string.Empty;
        field = string.Empty;
    }
    public Task(string description, string dueDate, string field)
    {
        this.description = description;
        this.due_date = dueDate;
        this.field = field;
    }
}
