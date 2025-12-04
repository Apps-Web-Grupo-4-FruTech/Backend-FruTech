using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Queries;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Domain.Services;

namespace FruTech.Backend.API.CropFields.Application.Internal.QueryServices;
/// <summary>
///  Implementation of the crop field query service.
/// </summary>
public class CropFieldQueryService : ICropFieldQueryService
{
    private readonly ICropFieldRepository _cropFieldRepository;
    /// <summary>
    ///  Constructor for CropFieldQueryService.
    /// </summary>
    /// <param name="cropFieldRepository"></param>
    public CropFieldQueryService(ICropFieldRepository cropFieldRepository)
    {
        _cropFieldRepository = cropFieldRepository;
    }
    /// <summary>
    ///  Get all crop fields.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CropField>> Handle(GetAllCropFieldsQuery query)
    {
        return await _cropFieldRepository.GetAllAsync();
    }
     /// <summary>
     ///  Get crop field by id.
     /// </summary>
     /// <param name="query"></param>
     /// <returns></returns>
    public async Task<CropField?> Handle(GetCropFieldByIdQuery query)
    {
        return await _cropFieldRepository.FindByIdAsync(query.CropFieldId);
    }
    /// <summary>
    ///  Get crop field by field id.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<CropField?> Handle(GetCropFieldByFieldIdQuery query)
    {
        return await _cropFieldRepository.FindByFieldIdAsync(query.FieldId);
    }
     /// <summary>
     ///  Get crop fields by user id.
     /// </summary>
     /// <param name="query"></param>
     /// <returns></returns>
    public async Task<IEnumerable<CropField>> Handle(GetCropFieldsByUserIdQuery query)
    {
        return await _cropFieldRepository.GetByUserIdAsync(query.UserId);
    }
}
