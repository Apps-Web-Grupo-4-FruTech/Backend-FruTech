using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;

public static class CreateTaskCommandFromResourceAssembler
{
    public static CreateTaskCommand ToCommandFromResource(CreateTaskResource resource)
    {
        return new CreateTaskCommand(
            resource.description,
            resource.due_date,
            resource.field
        );
    }
}

