using FruTech.Backend.API.Shared.Domain.Repositories;
using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;

namespace FruTech.Backend.API.UpcomingTasks.Domain.Repositories;

public interface IUpcomingTaskRepository : IBaseRepository<UpcomingTaskAggregate>
{
    // No métodos adicionales por ahora
}

