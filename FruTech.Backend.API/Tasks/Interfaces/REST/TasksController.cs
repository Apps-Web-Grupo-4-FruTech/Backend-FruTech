using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Domain.Model.Queries;
using FruTech.Backend.API.Tasks.Domain.Services;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;
using FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FruTech.Backend.API.Tasks.Interfaces.REST;

/// <summary>
/// Controlador para la gestión de tareas asociadas a campos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskCommandService _taskCommandService;
    private readonly ITaskQueryService _taskQueryService;

    public TasksController(ITaskCommandService taskCommandService, ITaskQueryService taskQueryService)
    {
        _taskCommandService = taskCommandService;
        _taskQueryService = taskQueryService;
    }

    /// <summary>
    /// Obtiene todas las tareas registradas.
    /// </summary>
    /// <response code="200">Lista de tareas recuperada correctamente.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetAllTasks()
    {
        var query = new GetAllTasksQuery();
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Obtiene una tarea específica por su identificador.
    /// </summary>
    /// <param name="id">Identificador de la tarea.</param>
    /// <response code="200">Tarea encontrada.</response>
    /// <response code="404">No existe una tarea con el identificador proporcionado.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResource>> GetTaskById(int id)
    {
        var query = new GetTaskByIdQuery(id);
        var task = await _taskQueryService.Handle(query);
        
        if (task == null)
            return NotFound();

        var resource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        return Ok(resource);
    }

    /// <summary>
    /// Obtiene las tareas asociadas a un campo específico.
    /// </summary>
    /// <param name="fieldId">Identificador del campo.</param>
    /// <response code="200">Lista de tareas asociadas al campo.</response>
    [HttpGet("field/{fieldId:int}")]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetTasksByField(int fieldId)
    {
        var query = new GetTasksByFieldQuery(fieldId);
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Crea una nueva tarea.
    /// </summary>
    /// <param name="resource">Datos de la tarea a crear.</param>
    /// <response code="201">Tarea creada correctamente.</response>
    /// <response code="400">Datos de entrada inválidos.</response>
    [HttpPost]
    public async Task<ActionResult<TaskResource>> CreateTask([FromBody] CreateTaskResource resource)
    {
        var command = CreateTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var task = await _taskCommandService.Handle(command);
        var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, taskResource);
    }

    /// <summary>
    /// Actualiza los datos de una tarea existente.
    /// </summary>
    /// <param name="id">Identificador de la tarea a actualizar.</param>
    /// <param name="resource">Datos actualizados de la tarea.</param>
    /// <response code="200">Tarea actualizada correctamente.</response>
    /// <response code="404">No existe una tarea con el identificador proporcionado.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskResource>> UpdateTask(int id, [FromBody] EditTaskResource resource)
    {
        var command = EditTaskCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var task = await _taskCommandService.Handle(command);
        
        if (task == null)
            return NotFound();

        var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        return Ok(taskResource);
    }

    /// <summary>
    /// Elimina una tarea.
    /// </summary>
    /// <param name="id">Identificador de la tarea a eliminar.</param>
    /// <response code="204">Tarea eliminada correctamente.</response>
    /// <response code="404">No existe una tarea con el identificador proporcionado.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var command = new DeleteTaskCommand(id);
        var result = await _taskCommandService.Handle(command);
        
        if (!result)
            return NotFound();

        return NoContent();
    }
}
