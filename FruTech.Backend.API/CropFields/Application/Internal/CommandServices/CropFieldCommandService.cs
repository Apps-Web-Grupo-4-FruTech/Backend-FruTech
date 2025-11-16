using FruTech.Backend.API.CropFields.Domain.Model.Commands;
using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Domain.Services;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;

namespace FruTech.Backend.API.CropFields.Application.Internal.CommandServices;

public class CropFieldCommandService : ICropFieldCommandService
{
    private readonly ICropFieldRepository _cropFieldRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CropFieldCommandService(ICropFieldRepository cropFieldRepository, IFieldRepository fieldRepository, IUnitOfWork unitOfWork)
    {
        _cropFieldRepository = cropFieldRepository;
        _fieldRepository = fieldRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CropField> Handle(CreateCropFieldCommand command)
    {
        // Validar que no exista ya un CropField para ese Field (relación 1:1)
        var existingCropField = await _cropFieldRepository.FindByFieldIdAsync(command.FieldId);
        if (existingCropField != null)
        {
            throw new InvalidOperationException($"El Field con ID {command.FieldId} ya tiene un CropField asociado.");
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

        // Actualizar el Field para apuntar al CropField creado
        var field = await _fieldRepository.FindByIdAsync(command.FieldId);
        if (field != null)
        {
            field.CropFieldId = cropField.Id;
            _fieldRepository.Update(field);
            await _unitOfWork.CompleteAsync();
        }

        return cropField;
    }

    public async Task<CropField?> Handle(UpdateCropFieldStatusCommand command)
    {
        var cropField = await _cropFieldRepository.FindByIdAsync(command.CropFieldId);
        if (cropField == null) return null;

        cropField.Status = command.Status;
        _cropFieldRepository.Update(cropField);
        await _unitOfWork.CompleteAsync();
        return cropField;
    }
}
