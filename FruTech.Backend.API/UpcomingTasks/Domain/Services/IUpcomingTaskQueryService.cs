using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;
using FruTech.Backend.API.UpcomingTasks.Domain.Model.Queries;

namespace FruTech.Backend.API.UpcomingTasks.Domain.Services;

public interface IUpcomingTaskQueryService
{
    Task<UpcomingTaskAggregate?> Handle(GetUpcomingTaskByIdQuery query);
    Task<IEnumerable<UpcomingTaskAggregate>> Handle(GetAllUpcomingTasksQuery query);
}

