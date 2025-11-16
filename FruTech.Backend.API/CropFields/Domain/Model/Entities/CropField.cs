using System.Text.Json.Serialization;
using FruTech.Backend.API.CropFields.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Entities
{
    public partial class CropField
    {
        public int Id { get; set; }
        
        /// <summary>
        /// ID del campo al que pertenece este cultivo (relación 1:1)
        /// </summary>
        public int FieldId { get; set; }
        
        [JsonPropertyName("crop")]
        public string Crop { get; set; } = string.Empty;
        
        [JsonPropertyName("soil_type")]
        public string SoilType { get; set; } = string.Empty;
        
        [JsonPropertyName("sunlight")]
        public string Sunlight { get; set; } = string.Empty;
        
        [JsonPropertyName("watering")]
        public string Watering { get; set; } = string.Empty;
        
        [JsonPropertyName("planting_date")]
        public DateTime? PlantingDate { get; set; }
        
        [JsonPropertyName("harvest_date")]
        public DateTime? HarvestDate { get; set; }
        
        [JsonPropertyName("status")]
        public CropFieldStatus Status { get; set; } = CropFieldStatus.Healthy;
    }
}
