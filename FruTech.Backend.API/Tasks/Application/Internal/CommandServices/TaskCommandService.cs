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
            command.description,
            command.due_date,
            command.field
        );

        return await _taskRepository.CreateAsync(task);
    }

    public async Task<Domain.Model.Aggregate.Task?> Handle(EditTaskCommand command)
    {
        var task = new Domain.Model.Aggregate.Task
        {
            id = command.id,
            description = command.description,
            due_date = command.due_date,
            field = command.field
        };

        return await _taskRepository.UpdateAsync(command.id, task);
    }

    public async Task<bool> Handle(DeleteTaskCommand command)
    {
        return await _taskRepository.DeleteAsync(command.id);
    }
}

