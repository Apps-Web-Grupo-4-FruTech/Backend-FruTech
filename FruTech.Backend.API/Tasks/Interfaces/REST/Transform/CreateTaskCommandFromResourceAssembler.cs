using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;

public static class CreateTaskCommandFromResourceAssembler
{
    public static CreateTaskCommand ToCommandFromResource(CreateTaskResource resource)
    {
        return new CreateTaskCommand(
            resource.FieldId,
            resource.Description,
            resource.DueDate
        );
    }
}
