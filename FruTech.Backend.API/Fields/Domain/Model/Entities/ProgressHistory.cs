using System;
using System.Text.Json.Serialization;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities
{
    public class ProgressHistory
    {
        public int Id { get; set; }
        public int FieldId { get; set; }

        // Convertidos a DateTime? para poder calcular fácilmente
        [JsonPropertyName("watered")]
        public DateTime? Watered { get; set; }

        [JsonPropertyName("fertilized")]
        public DateTime? Fertilized { get; set; }

        [JsonPropertyName("pests")]
        public DateTime? Pests { get; set; }

        // Se elimina la propiedad de navegación `Field` para que la relación sea por FieldId
        // public Field Field { get; set; } = null!;
    }
}