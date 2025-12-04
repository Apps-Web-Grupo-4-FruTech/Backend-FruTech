using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.Tasks.Domain.Model.Aggregate;
/// <summary>
///  Audit properties for Task entity.
/// </summary>
public partial class Task : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}
