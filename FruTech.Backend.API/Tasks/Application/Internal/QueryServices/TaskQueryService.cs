using FruTech.Backend.API.Tasks.Domain.Model.Queries;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using FruTech.Backend.API.Tasks.Domain.Services;

namespace FruTech.Backend.API.Tasks.Application.Internal.QueryServices;

public class TaskQueryService : ITaskQueryService
{
    private readonly ITaskRepository _taskRepository;

    public TaskQueryService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> Handle(GetAllTasksQuery query)
    {
        return await _taskRepository.GetAllAsync();
    }

    public async Task<Domain.Model.Aggregate.Task?> Handle(GetTaskByIdQuery query)
    {
        return await _taskRepository.GetByIdAsync(query.id);
    }

    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> Handle(GetTasksByFieldQuery query)
    {
        return await _taskRepository.GetByFieldIdAsync(query.fieldId);
    }
}
