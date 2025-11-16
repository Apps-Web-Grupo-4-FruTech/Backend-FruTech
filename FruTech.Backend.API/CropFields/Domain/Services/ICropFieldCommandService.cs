using FruTech.Backend.API.CropFields.Domain.Model.Commands;
using FruTech.Backend.API.CropFields.Domain.Model.Entities;

namespace FruTech.Backend.API.CropFields.Domain.Services;

public interface ICropFieldCommandService
{
    Task<CropField> Handle(CreateCropFieldCommand command);
    Task<CropField?> Handle(UpdateCropFieldStatusCommand command);
}

