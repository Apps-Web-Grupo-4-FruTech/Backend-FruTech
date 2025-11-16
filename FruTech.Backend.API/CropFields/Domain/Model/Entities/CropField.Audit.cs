using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Entities;

public partial class CropField : IEntityWithCreatedUpdatedDate
{
    [JsonIgnore]
    [BindNever]
    public DateTimeOffset? CreatedDate { get; set; }
    
    [JsonIgnore]
    [BindNever]
    public DateTimeOffset? UpdatedDate { get; set; }
}
