using System;
using System.Text.Json.Serialization;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities
{
    public class FieldTask
    {
        public int Id { get; set; }
        public int FieldId { get; set; }

        [JsonPropertyName("date")]
        // Usar DateTime? para la fecha de la tarea
        public DateTime? Date { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("taskDescription")]
        // Renombrado a TaskDescription para mayor claridad; si prefieres mantener "task" cambia el nombre
        public string TaskDescription { get; set; } = string.Empty;

        // Se elimina la propiedad de navegación `Field`
        // public Field Field { get; set; } = null!;
    }
}