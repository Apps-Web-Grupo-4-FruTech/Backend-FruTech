using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;

public static class TaskResourceFromEntityAssembler
{
    public static TaskResource ToResourceFromEntity(Domain.Model.Aggregate.Task entity)
    {
        return new TaskResource(
            entity.id,
            entity.description,
            entity.due_date,
            entity.field
        );
    }
}

