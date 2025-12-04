using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities;
/// <summary>
///  Audit properties for ProgressHistory entity
/// </summary>
public partial class ProgressHistory : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}
