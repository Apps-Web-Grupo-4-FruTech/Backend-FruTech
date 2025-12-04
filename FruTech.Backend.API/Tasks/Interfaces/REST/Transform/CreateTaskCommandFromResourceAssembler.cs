using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
/// <summary>
///  Assembler to convert CreateTaskResource to CreateTaskCommand.
/// </summary>
public static class CreateTaskCommandFromResourceAssembler
{
    /// <summary>
    ///  Converts a CreateTaskResource to a CreateTaskCommand.
    /// </summary>
    /// <param name="resource"></param>
    public static CreateTaskCommand ToCommandFromResource(CreateTaskResource resource)
    {
        return new CreateTaskCommand(
            resource.FieldId,
            resource.Description,
            resource.DueDate
        );
    }
}
