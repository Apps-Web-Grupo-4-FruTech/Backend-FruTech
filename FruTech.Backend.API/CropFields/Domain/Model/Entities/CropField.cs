using System.Text.Json.Serialization;

namespace FruTech.Backend.API.CropFields.Domain.Model.Entities
{
    public class CropField
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime? PlantingDate { get; set; }
        public DateTime? HarvestDate { get; set; }
        public int Field { get; set; } // ahora entero para vincular con Field.Id
        public string Status { get; set; } = string.Empty;
        public int Days { get; set; }
        
        [JsonPropertyName("soil_type")]
        public string SoilType { get; set; } = string.Empty;
        
        [JsonPropertyName("watering")]
        public string Watering { get; set; } = string.Empty;
        
        [JsonPropertyName("sunlight")]
        public string Sunlight { get; set; } = string.Empty;
    }
}
