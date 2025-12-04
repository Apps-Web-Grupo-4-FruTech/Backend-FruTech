namespace FruTech.Backend.API.Shared.Domain.Model.ValueObjects
{
    /// <summary>
    ///  Entity with created and updated date
    /// </summary>
    public interface IEntityWithCreatedUpdatedDate
    {
        DateTimeOffset? CreatedDate { get; set; }
        DateTimeOffset? UpdatedDate { get; set; }
    }
}
