using System.Text.Json.Serialization;
using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruTech.Backend.API.CropFields.Domain.Model.Entities
{
    /// <summary>
    ///  Entidad que representa un campo de cultivo con sus propiedades y relaciones.
    /// </summary>
    public partial class CropField
    {
        public int Id { get; set; }
        
        /// <summary>
        /// ID of the field this crop belongs to (1:1 relationship)
        /// </summary>
        public int FieldId { get; set; }
        [ForeignKey("FieldId")]
        public virtual FruTech.Backend.API.Fields.Domain.Model.Entities.Field? Field { get; set; }
        /// <summary>
        ///  Type of crop planted in the field
        /// </summary>
        [JsonPropertyName("crop")]
        public string Crop { get; set; } = string.Empty;
        /// <summary>
        ///  Soil type of the field
        /// </summary>
        [JsonPropertyName("soil_type")]
        public string SoilType { get; set; } = string.Empty;
        /// <summary>
        ///  Sunlight requirements for the crop
        /// </summary>
        [JsonPropertyName("sunlight")]
        public string Sunlight { get; set; } = string.Empty;
        /// <summary>
        ///  Watering needs for the crop
        /// </summary>
        [JsonPropertyName("watering")]
        public string Watering { get; set; } = string.Empty;
        /// <summary>
        ///  Date when the crop was planted
        /// </summary>
        [JsonPropertyName("planting_date")]
        public DateTime? PlantingDate { get; set; }
        /// <summary>
        ///  Date when the crop was harvested
        /// </summary>
        [JsonPropertyName("harvest_date")]
        public DateTime? HarvestDate { get; set; }
        /// <summary>
        ///  Current status of the crop field
        /// </summary>
        [JsonPropertyName("status")]
        public CropFieldStatus Status { get; set; } = CropFieldStatus.Healthy;
        /// <summary>
        ///  Indicates if the crop field record is deleted (soft delete)
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; } = false;
    }
}
