using System.Text.Json.Serialization;
using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities;

public partial class ProgressHistory : IEntityWithCreatedUpdatedDate
{
    [JsonIgnore]
    public DateTimeOffset? CreatedDate { get; set; }
    [JsonIgnore]
    public DateTimeOffset? UpdatedDate { get; set; }
}
