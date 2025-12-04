using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
/// <summary>
///  Assembler to convert EditTaskResource to EditTaskCommand.
/// </summary>
public static class EditTaskCommandFromResourceAssembler
{
    /// <summary>
    ///  Converts an EditTaskResource to an EditTaskCommand.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    public static EditTaskCommand ToCommandFromResource(int id, EditTaskResource resource)
    {
        return new EditTaskCommand(
            id,
            resource.Description,
            resource.DueDate
        );
    }
}
