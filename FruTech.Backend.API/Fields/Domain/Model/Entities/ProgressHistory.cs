using System.Text.Json.Serialization;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities
{
    public partial class ProgressHistory
    {
        public int Id { get; set; }
        
        /// <summary>
        /// ID del campo (relación 1:1)
        /// </summary>
        public int FieldId { get; set; }

        [JsonPropertyName("watered")]
        public DateTime Watered { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("fertilized")]
        public DateTime Fertilized { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("pests")]
        public DateTime Pests { get; set; } = DateTime.UtcNow;
    }
}