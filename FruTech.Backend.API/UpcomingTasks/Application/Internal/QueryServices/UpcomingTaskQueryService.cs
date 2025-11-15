using FruTech.Backend.API.UpcomingTasks.Domain.Model.Queries;
using FruTech.Backend.API.UpcomingTasks.Domain.Repositories;
using FruTech.Backend.API.UpcomingTasks.Domain.Services;
using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;

namespace FruTech.Backend.API.UpcomingTasks.Application.Internal.QueryServices;

public class UpcomingTaskQueryService : IUpcomingTaskQueryService
{
    private readonly IUpcomingTaskRepository _upcomingTaskRepository;

    public UpcomingTaskQueryService(IUpcomingTaskRepository upcomingTaskRepository)
    {
        _upcomingTaskRepository = upcomingTaskRepository;
    }

    public async Task<UpcomingTaskAggregate?> Handle(GetUpcomingTaskByIdQuery query)
    {
        return await _upcomingTaskRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<UpcomingTaskAggregate>> Handle(GetAllUpcomingTasksQuery query)
    {
        return await _upcomingTaskRepository.ListAsync();
    }
}

