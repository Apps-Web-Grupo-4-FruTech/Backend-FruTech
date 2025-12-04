using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CommunityRecommendation.Domain.Model.Aggregates;

public partial class CommunityRecommendation : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    ///  Fecha de creación del registro
    /// </summary>
    public DateTimeOffset? CreatedDate { get; set; }
    /// <summary>
    ///  Fecha de última actualización del registro
    /// </summary>
    public DateTimeOffset? UpdatedDate { get; set; }
}

