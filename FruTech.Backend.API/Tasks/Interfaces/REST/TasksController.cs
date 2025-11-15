using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Domain.Model.Queries;
using FruTech.Backend.API.Tasks.Domain.Services;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;
using FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FruTech.Backend.API.Tasks.Interfaces.REST;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetAllTasks()
    {
        var query = new GetAllTasksQuery();
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

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

    [HttpGet("field/{field}")]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetTasksByField(string field)
    {
        var query = new GetTasksByFieldQuery(field);
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResource>> CreateTask([FromBody] CreateTaskResource resource)
    {
        var command = CreateTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var task = await _taskCommandService.Handle(command);
        var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.id }, taskResource);
    }

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

