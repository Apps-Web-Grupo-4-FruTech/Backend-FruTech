using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;
using FruTech.Backend.API.UpcomingTasks.Domain.Model.Commands;

namespace FruTech.Backend.API.UpcomingTasks.Domain.Services;

public interface IUpcomingTaskCommandService
{
    Task<UpcomingTaskAggregate> Handle(CreateUpcomingTaskCommand command);
}

