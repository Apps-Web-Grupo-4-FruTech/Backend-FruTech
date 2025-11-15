using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities
{
    public class Field
    {
        public int Id { get; set; }

        [JsonPropertyName("image_url")] 
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("product")]
        public string Product { get; set; } = string.Empty;

        [JsonPropertyName("crop")]
        public string Crop { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("field_size")]
        public string FieldSize { get; set; } = string.Empty; // e.g. "5,000 m2"

        // Referencias por Id
        [JsonPropertyName("CropID")]
        public int? CropId { get; set; }


        [JsonPropertyName("ProgressId")]
        public int? ProgressId { get; set; }

        [JsonPropertyName("TaskIds")]
        public List<int> TaskIds { get; set; } = new();
    }
}