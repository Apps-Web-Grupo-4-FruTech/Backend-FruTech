using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using FruTech.Backend.API.Tasks.Domain.Services;

namespace FruTech.Backend.API.Tasks.Application.Internal.CommandServices;

public class TaskCommandService : ITaskCommandService
{
    private readonly ITaskRepository _taskRepository;

    public TaskCommandService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Domain.Model.Aggregate.Task> Handle(CreateTaskCommand command)
    {
        var task = new Domain.Model.Aggregate.Task(
            command.Description,
            command.DueDate,
            command.FieldId
        );

        return await _taskRepository.CreateAsync(task);
    }

    public async Task<Domain.Model.Aggregate.Task?> Handle(EditTaskCommand command)
    {
        var task = new Domain.Model.Aggregate.Task
        {
            Id = command.Id,
            Description = command.Description,
            DueDate = command.DueDate,
            FieldId = command.FieldId
        };

        return await _taskRepository.UpdateAsync(command.Id, task);
    }

    public async Task<bool> Handle(DeleteTaskCommand command)
    {
        return await _taskRepository.DeleteAsync(command.Id);
    }
}
