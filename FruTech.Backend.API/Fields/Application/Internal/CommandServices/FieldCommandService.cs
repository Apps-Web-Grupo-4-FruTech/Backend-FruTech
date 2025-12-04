using FruTech.Backend.API.Fields.Domain.Model.Commands;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Domain.Services;
using FruTech.Backend.API.Shared.Domain.Repositories;

namespace FruTech.Backend.API.Fields.Application.Internal.CommandServices;

/// <summary>
/// Command service for Field
/// </summary>
public class FieldCommandService : IFieldCommandService
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IProgressHistoryRepository _progressHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="fieldRepository"></param>
    /// <param name="progressHistoryRepository"></param>
    /// <param name="unitOfWork"></param>
    public FieldCommandService(
        IFieldRepository fieldRepository,
        IProgressHistoryRepository progressHistoryRepository,
        IUnitOfWork unitOfWork)
    {
        _fieldRepository = fieldRepository;
        _progressHistoryRepository = progressHistoryRepository;
        _unitOfWork = unitOfWork;
    }
    /// <summary>
    ///  Creates a new Field
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<Field> Handle(CreateFieldCommand command)
    {

        var field = new Field
        {
            UserId = command.UserId,
            ImageContent = command.ImageContent,
            ImageContentType = command.ImageContentType,
            Name = command.Name,
            Location = command.Location,
            FieldSize = command.FieldSize
        };

        await _fieldRepository.AddAsync(field);
        await _unitOfWork.CompleteAsync();
        
        var progressHistory = new ProgressHistory
        {
            FieldId = field.Id,
            Watered = DateTime.UtcNow,
            Fertilized = DateTime.UtcNow,
            Pests = DateTime.UtcNow,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null
        };

        await _progressHistoryRepository.AddAsync(progressHistory);
        await _unitOfWork.CompleteAsync();

        return field;
    }
}
