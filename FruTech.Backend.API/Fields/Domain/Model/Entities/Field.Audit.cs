using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.Fields.Domain.Model.Entities;
/// <summary>
///  Audit properties for Field entity
/// </summary>
public partial class Field : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}
