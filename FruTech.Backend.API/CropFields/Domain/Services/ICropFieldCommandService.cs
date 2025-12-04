using FruTech.Backend.API.CropFields.Domain.Model.Commands;
using FruTech.Backend.API.CropFields.Domain.Model.Entities;

namespace FruTech.Backend.API.CropFields.Domain.Services;
/// <summary>
///  Service for handling crop field commands such as create, update, and delete.
/// </summary>
public interface ICropFieldCommandService
{
    Task<CropField> Handle(CreateCropFieldCommand command);
    Task<CropField?> Handle(int cropFieldId, UpdateCropFieldCommand command);
    Task<CropField?> HandleDelete(int cropFieldId);
}
