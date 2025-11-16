using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Queries;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Domain.Services;

namespace FruTech.Backend.API.CropFields.Application.Internal.QueryServices;

public class CropFieldQueryService : ICropFieldQueryService
{
    private readonly ICropFieldRepository _cropFieldRepository;

    public CropFieldQueryService(ICropFieldRepository cropFieldRepository)
    {
        _cropFieldRepository = cropFieldRepository;
    }

    public async Task<IEnumerable<CropField>> Handle(GetAllCropFieldsQuery query)
    {
        return await _cropFieldRepository.ListAsync();
    }

    public async Task<CropField?> Handle(GetCropFieldByIdQuery query)
    {
        return await _cropFieldRepository.FindByIdAsync(query.CropFieldId);
    }

    public async Task<CropField?> Handle(GetCropFieldByFieldIdQuery query)
    {
        return await _cropFieldRepository.FindByFieldIdAsync(query.FieldId);
    }
}

