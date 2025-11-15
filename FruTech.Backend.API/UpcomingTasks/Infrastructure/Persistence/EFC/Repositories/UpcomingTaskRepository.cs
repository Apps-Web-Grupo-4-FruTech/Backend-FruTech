using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;
using FruTech.Backend.API.UpcomingTasks.Domain.Repositories;

namespace FruTech.Backend.API.UpcomingTasks.Infrastructure.Persistence.EFC.Repositories;

public class UpcomingTaskRepository : BaseRepository<UpcomingTaskAggregate>, IUpcomingTaskRepository
{
    public UpcomingTaskRepository(AppDbContext context) : base(context)
    {
    }
}

