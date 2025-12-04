using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using FruTech.Backend.API.Tasks.Domain.Services;

namespace FruTech.Backend.API.Tasks.Application.Internal.CommandServices;
/// <summary>
///  Implementation of ITaskCommandService for handling task commands.
/// </summary>
public class TaskCommandService : ITaskCommandService
{
    private readonly ITaskRepository _taskRepository;
    /// <summary>
    ///  Initializes a new instance of the TaskCommandService class.
    /// </summary>
    /// <param name="taskRepository"></param>
    public TaskCommandService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    /// <summary>
    ///  Handles the creation of a new task.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<Domain.Model.Aggregate.Task> Handle(CreateTaskCommand command)
    {
        var task = new Domain.Model.Aggregate.Task(
            command.Description,
            command.DueDate,
            command.FieldId
        )
        {
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null
        };

        return await _taskRepository.CreateAsync(task);
    }
    /// <summary>
    ///  Handles the editing of an existing task.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<Domain.Model.Aggregate.Task?> Handle(EditTaskCommand command)
    {
        var existing = await _taskRepository.GetByIdAsync(command.Id);
        if (existing == null) return null;

        existing.Description = command.Description;
        existing.DueDate = command.DueDate;
        existing.UpdatedDate = DateTimeOffset.Now;

        return await _taskRepository.UpdateAsync(command.Id, existing);
    }
    /// <summary>
    ///  Handles the deletion of a task.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteTaskCommand command)
    {
        return await _taskRepository.DeleteAsync(command.Id);
    }
}
