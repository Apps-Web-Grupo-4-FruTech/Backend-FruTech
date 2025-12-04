using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities
{
    public partial class Field
    {
        public int Id { get; set; }

        /// <summary>
        /// ID of the user who owns the field
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// Raw image bytes (BLOB) stored in database. Nullable.
        /// </summary>
        public byte[]? ImageContent { get; set; }

        /// <summary>
        /// MIME content type for the image (e.g. "image/png"). Nullable.
        /// </summary>
        public string? ImageContentType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("field_size")]
        public string FieldSize { get; set; } = string.Empty; // e.g. "5,000 m2"

        /// <summary>
        /// Optional FK to CropField (will be updated when CropField 1:1 is created)
        /// Not mapped in DB to avoid errors if the column doesn't exist.
        /// </summary>
        [NotMapped]
        [JsonPropertyName("crop_field_id")]
        public int? CropFieldId { get; set; }

        /// <summary>
        /// 1:1 relationship with ProgressHistory
        /// </summary>
        public ProgressHistory? ProgressHistory { get; set; }
        /// <summary>
        /// 1:1 relationship with CropField
        /// </summary>
        public FruTech.Backend.API.CropFields.Domain.Model.Entities.CropField? CropField { get; set; }
        /// <summary>
        /// 1:N relationship with Tasks
        /// </summary>
        [JsonPropertyName("tasks")]
        public ICollection<FruTech.Backend.API.Tasks.Domain.Model.Aggregate.Task>? Tasks { get; set; }
    }
}