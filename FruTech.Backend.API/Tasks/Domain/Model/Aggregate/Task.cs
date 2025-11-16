using System.Text.Json.Serialization;

namespace FruTech.Backend.API.Tasks.Domain.Model.Aggregate;

public partial class Task
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// ID del campo asociado a esta tarea
    /// </summary>
    [JsonPropertyName("field_id")]
    public int FieldId { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; set; } = DateTime.UtcNow.Date;
    
    public Task() { }
    
    public Task(string description, DateTime dueDate, int fieldId)
    {
        Description = description;
        DueDate = dueDate;
        FieldId = fieldId;
    }
}
