namespace FruTech.Backend.API.Shared.Domain.Model.ValueObjects
{
    public interface IEntityWithCreatedUpdatedDate
    {
        DateTimeOffset? CreatedDate { get; set; }
        DateTimeOffset? UpdatedDate { get; set; }
    }
}
