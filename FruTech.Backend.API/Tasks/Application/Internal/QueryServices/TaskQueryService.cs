using FruTech.Backend.API.Tasks.Domain.Model.Queries;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using FruTech.Backend.API.Tasks.Domain.Services;

namespace FruTech.Backend.API.Tasks.Application.Internal.QueryServices;
/// <summary>
///  Implementation of ITaskQueryService for handling task queries.
/// </summary>
public class TaskQueryService : ITaskQueryService
{
    private readonly ITaskRepository _taskRepository;
    /// <summary>
    ///  Initializes a new instance of the TaskQueryService class.
    /// </summary>
    /// <param name="taskRepository"></param>
    public TaskQueryService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    /// <summary>
    ///  Handles retrieving all tasks.
    /// </summary>
    /// <param name="query"></param>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> Handle(GetAllTasksQuery query)
    {
        return await _taskRepository.GetAllAsync();
    }
    /// <summary>
    ///  Handles retrieving a task by its ID.
    /// </summary>
    /// <param name="query"></param>
    public async Task<Domain.Model.Aggregate.Task?> Handle(GetTaskByIdQuery query)
    {
        return await _taskRepository.GetByIdAsync(query.id);
    }
    /// <summary>
    ///  Handles retrieving tasks by field ID.
    /// </summary>
    /// <param name="query"></param>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> Handle(GetTasksByFieldQuery query)
    {
        return await _taskRepository.GetByFieldIdAsync(query.fieldId);
    }
    /// <summary>
    ///  Handles retrieving tasks by user ID.
    /// </summary>
    /// <param name="query"></param>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> Handle(GetTasksByUserIdQuery query)
    {
        return await _taskRepository.GetByUserIdAsync(query.UserId);
    }
    /// <summary>
    ///  Handles retrieving upcoming tasks by user ID.
    /// </summary>
    /// <param name="query"></param>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> Handle(GetUpcomingTasksByUserIdQuery query)
    {
        return await _taskRepository.GetUpcomingTasksByUserIdAsync(query.UserId, query.Count);
    }
}
