using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Queries;

namespace FruTech.Backend.API.CropFields.Domain.Services;
/// <summary>
///  Service for handling crop field queries such as retrieval operations.
/// </summary>
public interface ICropFieldQueryService
{
    Task<IEnumerable<CropField>> Handle(GetAllCropFieldsQuery query);
    Task<CropField?> Handle(GetCropFieldByIdQuery query);
    Task<CropField?> Handle(GetCropFieldByFieldIdQuery query);
    Task<IEnumerable<CropField>> Handle(GetCropFieldsByUserIdQuery query);
}

