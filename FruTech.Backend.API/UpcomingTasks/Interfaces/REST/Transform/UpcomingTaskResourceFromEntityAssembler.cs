using UpcomingTaskAggregate = FruTech.Backend.API.UpcomingTasks.Domain.Model.Aggregates.UpcomingTask;
using FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Transform;

public static class UpcomingTaskResourceFromEntityAssembler
{
    public static UpcomingTaskResource ToResourceFromEntity(UpcomingTaskAggregate entity)
        => new(entity.Id, entity.Date, entity.Name, entity.TaskDescription, entity.TaskId);
}

