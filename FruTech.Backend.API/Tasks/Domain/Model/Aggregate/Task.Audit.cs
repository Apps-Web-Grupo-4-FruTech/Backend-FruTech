using System.Text.Json.Serialization;
using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.Tasks.Domain.Model.Aggregate;

public partial class Task : IEntityWithCreatedUpdatedDate
{
    [JsonIgnore]
    public DateTimeOffset? CreatedDate { get; set; }
    [JsonIgnore]
    public DateTimeOffset? UpdatedDate { get; set; }
}
