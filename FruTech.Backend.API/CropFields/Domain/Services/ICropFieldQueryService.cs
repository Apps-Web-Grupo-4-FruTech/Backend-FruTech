using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Queries;

namespace FruTech.Backend.API.CropFields.Domain.Services;

public interface ICropFieldQueryService
{
    Task<IEnumerable<CropField>> Handle(GetAllCropFieldsQuery query);
    Task<CropField?> Handle(GetCropFieldByIdQuery query);
    Task<CropField?> Handle(GetCropFieldByFieldIdQuery query);
}

