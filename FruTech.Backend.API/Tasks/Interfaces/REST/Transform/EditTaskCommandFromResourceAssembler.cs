using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;

public static class EditTaskCommandFromResourceAssembler
{
    public static EditTaskCommand ToCommandFromResource(int id, EditTaskResource resource)
    {
        return new EditTaskCommand(
            id,
            resource.description,
            resource.due_date,
            resource.field
        );
    }
}

