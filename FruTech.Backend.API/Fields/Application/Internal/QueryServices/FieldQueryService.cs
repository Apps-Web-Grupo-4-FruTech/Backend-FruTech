using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Queries;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Domain.Services;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;
using FruTech.Backend.API.Fields.Interfaces.REST.Transform;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;

namespace FruTech.Backend.API.Fields.Application.Internal.QueryServices;

/// <summary>
/// Query service for Field
/// </summary>
public class FieldQueryService : IFieldQueryService
{
    private readonly IFieldRepository _fieldRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IProgressHistoryRepository _progressHistoryRepository;
    private readonly ICropFieldRepository _cropFieldRepository;

    public FieldQueryService(
        IFieldRepository fieldRepository,
        ITaskRepository taskRepository,
        IProgressHistoryRepository progressHistoryRepository,
        ICropFieldRepository cropFieldRepository)
    {
        _fieldRepository = fieldRepository;
        _taskRepository = taskRepository;
        _progressHistoryRepository = progressHistoryRepository;
        _cropFieldRepository = cropFieldRepository;
    }

    public async Task<IEnumerable<FieldResource>> Handle(GetFieldsByUserIdQuery query)
    {
        var fields = await _fieldRepository.FindByUserIdAsync(query.UserId);

        var tasks = fields.Select(async f => new
        {
            Field = f,
            TaskIds = (await _taskRepository.GetByFieldIdAsync(f.Id)).Select(t => t.Id).ToList(),
            ProgressHistoryId = (await _progressHistoryRepository.FindByFieldIdAsync(f.Id))?.Id,
            CropFieldId = (await _cropFieldRepository.FindByFieldIdAsync(f.Id))?.Id
        });

        var results = await Task.WhenAll(tasks);
        return results.Select(r => new FieldResource(
            r.Field.Id,
            r.Field.UserId,
            r.Field.ImageUrl,
            r.Field.Name,
            r.Field.Location,
            r.Field.FieldSize,
            r.ProgressHistoryId,
            r.CropFieldId,
            r.TaskIds,
            r.Field.CreatedDate,
            r.Field.UpdatedDate
        ));
    }

    public async Task<FieldResource?> Handle(GetFieldByIdQuery query)
    {
        var field = await _fieldRepository.FindByIdAsync(query.FieldId);
        if (field is null) return null;

        var tasks = await _taskRepository.GetByFieldIdAsync(field.Id);
        var progress = await _progressHistoryRepository.FindByFieldIdAsync(field.Id);
        var cropField = await _cropFieldRepository.FindByFieldIdAsync(field.Id);

        return new FieldResource(
            field.Id,
            field.UserId,
            field.ImageUrl,
            field.Name,
            field.Location,
            field.FieldSize,
            progress?.Id,
            cropField?.Id,
            tasks.Select(t => t.Id).ToList(),
            field.CreatedDate,
            field.UpdatedDate
        );
    }
}
