using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;

public static class EditTaskCommandFromResourceAssembler
{
    public static EditTaskCommand ToCommandFromResource(int id, EditTaskResource resource)
    {
        return new EditTaskCommand(
            id,
            resource.FieldId,
            resource.Description,
            resource.DueDate
        );
    }
}
