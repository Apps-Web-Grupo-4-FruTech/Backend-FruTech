using FruTech.Backend.API.Fields.Domain.Model.Commands;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Domain.Services;
using FruTech.Backend.API.Shared.Domain.Repositories;

namespace FruTech.Backend.API.Fields.Application.Internal.CommandServices;

/// <summary>
/// Servicio de comandos para Field
/// </summary>
public class FieldCommandService : IFieldCommandService
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IProgressHistoryRepository _progressHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FieldCommandService(
        IFieldRepository fieldRepository,
        IProgressHistoryRepository progressHistoryRepository,
        IUnitOfWork unitOfWork)
    {
        _fieldRepository = fieldRepository;
        _progressHistoryRepository = progressHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Field> Handle(CreateFieldCommand command)
    {
        // Crear el Field
        var field = new Field
        {
            UserId = command.UserId,
            ImageUrl = command.ImageUrl,
            Name = command.Name,
            Location = command.Location,
            FieldSize = command.FieldSize
        };

        await _fieldRepository.AddAsync(field);
        await _unitOfWork.CompleteAsync();

        // Crear ProgressHistory asociado automáticamente (relación 1:1)
        var progressHistory = new ProgressHistory
        {
            FieldId = field.Id,
            Watered = DateTime.UtcNow,
            Fertilized = DateTime.UtcNow,
            Pests = DateTime.UtcNow
        };

        await _progressHistoryRepository.AddAsync(progressHistory);
        await _unitOfWork.CompleteAsync();

        return field;
    }
}

