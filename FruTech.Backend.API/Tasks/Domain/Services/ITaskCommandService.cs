using FruTech.Backend.API.Tasks.Domain.Model.Commands;

namespace FruTech.Backend.API.Tasks.Domain.Services;
/// <summary>
///  Service interface for handling Task commands.
/// </summary>
public interface ITaskCommandService
{
    Task<Model.Aggregate.Task> Handle(CreateTaskCommand command);
    Task<Model.Aggregate.Task?> Handle(EditTaskCommand command);
    Task<bool> Handle(DeleteTaskCommand command);
}

