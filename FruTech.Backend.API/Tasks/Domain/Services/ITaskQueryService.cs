using FruTech.Backend.API.Tasks.Domain.Model.Queries;

namespace FruTech.Backend.API.Tasks.Domain.Services;
/// <summary>
///  Service interface for handling Task queries.
/// </summary>
public interface ITaskQueryService
{
    Task<IEnumerable<Model.Aggregate.Task>> Handle(GetAllTasksQuery query);
    Task<Model.Aggregate.Task?> Handle(GetTaskByIdQuery query);
    Task<IEnumerable<Model.Aggregate.Task>> Handle(GetTasksByFieldQuery query);
    Task<IEnumerable<Model.Aggregate.Task>> Handle(GetTasksByUserIdQuery query);
    Task<IEnumerable<Model.Aggregate.Task>> Handle(GetUpcomingTasksByUserIdQuery query);
}
