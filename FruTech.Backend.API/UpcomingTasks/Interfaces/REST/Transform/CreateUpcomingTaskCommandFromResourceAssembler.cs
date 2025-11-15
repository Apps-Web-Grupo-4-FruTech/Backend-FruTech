using FruTech.Backend.API.UpcomingTasks.Domain.Model.Commands;
using FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Transform;

public static class CreateUpcomingTaskCommandFromResourceAssembler
{
    public static CreateUpcomingTaskCommand ToCommandFromResource(CreateUpcomingTaskResource resource)
        => new(resource.Date, resource.Name, resource.TaskDescription, resource.TaskId);
}

