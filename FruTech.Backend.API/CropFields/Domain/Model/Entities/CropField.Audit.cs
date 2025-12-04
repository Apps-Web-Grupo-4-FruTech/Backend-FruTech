using Microsoft.AspNetCore.Mvc.ModelBinding;
using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;

namespace FruTech.Backend.API.CropFields.Domain.Model.Entities;
/// <summary>
///  Partial class for CropField to handle audit properties like CreatedDate and UpdatedDate.
/// </summary>
public partial class CropField : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    ///  Date and time when the crop field record was created.
    /// </summary>
    [BindNever]
    public DateTimeOffset? CreatedDate { get; set; }
    /// <summary>
    ///  Date and time when the crop field record was last updated.
    /// </summary>
    [BindNever]
    public DateTimeOffset? UpdatedDate { get; set; }
    /// <summary>
    ///  Date and time when the crop field record was deleted (soft delete).
    /// </summary>
    [BindNever]
    public DateTimeOffset? DeletedDate { get; set; }
}
