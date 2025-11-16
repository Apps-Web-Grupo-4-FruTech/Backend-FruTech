using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities
{
    public partial class Field
    {
        public int Id { get; set; }

        /// <summary>
        /// ID del usuario propietario del campo
        /// </summary>
        public int UserId { get; set; }

        [JsonPropertyName("image_url")] 
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("field_size")]
        public string FieldSize { get; set; } = string.Empty; // e.g. "5,000 m2"

        /// <summary>
        /// Relación 1:1 con ProgressHistory
        /// </summary>
        public ProgressHistory? ProgressHistory { get; set; }
        /// <summary>
        /// Relación 1:1 con CropField
        /// </summary>
        public FruTech.Backend.API.CropFields.Domain.Model.Entities.CropField? CropField { get; set; }
        /// <summary>
        /// Relación 1:N con Tasks
        /// </summary>
        public ICollection<FruTech.Backend.API.Tasks.Domain.Model.Aggregate.Task>? Tasks { get; set; }
    }
}