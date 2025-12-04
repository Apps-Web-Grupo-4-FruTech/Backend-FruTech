using FruTech.Backend.API.CropFields.Domain.Model.Commands;
using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Domain.Services;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;

namespace FruTech.Backend.API.CropFields.Application.Internal.CommandServices;

/// <summary>
///  Implementation of the command service for managing CropField entities.
/// </summary>
public class CropFieldCommandService : ICropFieldCommandService
{
    private readonly ICropFieldRepository _cropFieldRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    ///  Initializes a new instance of the <see cref="CropFieldCommandService"/> class.
    /// </summary>
    /// <param name="cropFieldRepository"></param>
    /// <param name="fieldRepository"></param>
    /// <param name="unitOfWork"></param>
    public CropFieldCommandService(ICropFieldRepository cropFieldRepository, IFieldRepository fieldRepository, IUnitOfWork unitOfWork)
    {
        _cropFieldRepository = cropFieldRepository;
        _fieldRepository = fieldRepository;
        _unitOfWork = unitOfWork;
    }
    /// <summary>
    ///  Handles the creation of a new CropField.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<CropField> Handle(CreateCropFieldCommand command)
    {
        var existing = await _cropFieldRepository.FindAnyByFieldIdAsync(command.FieldId);
        if (existing != null)
        {
            if (!existing.Deleted)
            {
                throw new InvalidOperationException($"El Field con ID {command.FieldId} ya tiene un CropField activo.");
            }


            existing.Crop = command.Crop;
            existing.SoilType = command.SoilType;
            existing.Sunlight = command.Sunlight;
            existing.Watering = command.Watering;
            existing.PlantingDate = command.PlantingDate;
            existing.HarvestDate = command.HarvestDate;
            existing.Status = command.Status;
            existing.Deleted = false;
            existing.DeletedDate = null;
            existing.UpdatedDate = DateTimeOffset.Now;

            _cropFieldRepository.Update(existing);
            await _unitOfWork.CompleteAsync();
            
            var field = await _fieldRepository.FindByIdAsync(command.FieldId);
            if (field != null)
            {
                field.CropFieldId = existing.Id;
                _fieldRepository.Update(field);
                await _unitOfWork.CompleteAsync();
            }

            return existing;
        }
        
        var cropField = new CropField
        {
            FieldId = command.FieldId,
            Crop = command.Crop,
            SoilType = command.SoilType,
            Sunlight = command.Sunlight,
            Watering = command.Watering,
            PlantingDate = command.PlantingDate,
            HarvestDate = command.HarvestDate,
            Status = command.Status
        };

        await _cropFieldRepository.AddAsync(cropField);
        await _unitOfWork.CompleteAsync();
        
        var fieldNew = await _fieldRepository.FindByIdAsync(command.FieldId);
        if (fieldNew != null)
        {
            fieldNew.CropFieldId = cropField.Id;
            _fieldRepository.Update(fieldNew);
            await _unitOfWork.CompleteAsync();
        }

        return cropField;
    }
    /// <summary>
    ///  Handles the update of an existing CropField.
    /// </summary>
    /// <param name="cropFieldId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<CropField?> Handle(int cropFieldId, UpdateCropFieldCommand command)
    {
        var cropField = await _cropFieldRepository.FindByIdAsync(cropFieldId);
        if (cropField == null) return null;

        if (command.Crop is not null)
            cropField.Crop = command.Crop;
        if (command.PlantingDate.HasValue)
            cropField.PlantingDate = command.PlantingDate.Value.UtcDateTime;
        if (command.HarvestDate.HasValue)
            cropField.HarvestDate = command.HarvestDate.Value.UtcDateTime;
        if (command.Status.HasValue)
            cropField.Status = command.Status.Value;

        cropField.UpdatedDate = DateTimeOffset.Now;
        _cropFieldRepository.Update(cropField);
        await _unitOfWork.CompleteAsync();
        return cropField;
    }
    /// <summary>
    ///  Handles the deletion of a CropField (soft delete).
    /// </summary>
    /// <param name="cropFieldId"></param>
    /// <returns></returns>
    public async Task<CropField?> HandleDelete(int cropFieldId)
    {
        var cropField = await _cropFieldRepository.FindByIdAsync(cropFieldId);
        if (cropField == null) return null;
        
        cropField.Deleted = true;
        cropField.DeletedDate = DateTimeOffset.UtcNow;
        cropField.UpdatedDate = DateTimeOffset.UtcNow;

        _cropFieldRepository.Update(cropField);
        await _unitOfWork.CompleteAsync();
        return cropField;
    }
}
