using FruTech.Backend.API.Tasks.Domain.Model.Commands;
using FruTech.Backend.API.Tasks.Domain.Model.Queries;
using FruTech.Backend.API.Tasks.Domain.Services;
using FruTech.Backend.API.Tasks.Interfaces.REST.Resources;
using FruTech.Backend.API.Tasks.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FruTech.Backend.API.Tasks.Interfaces.REST;

/// <summary>
/// Controller for managing tasks associated with fields.
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
    /// Gets all registered tasks.
    /// </summary>
    /// <response code="200">Task list retrieved successfully.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetAllTasks()
    {
        var query = new GetAllTasksQuery();
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Gets a specific task by its identifier.
    /// </summary>
    /// <param name="id">Task identifier.</param>
    /// <response code="200">Task found.</response>
    /// <response code="404">No task found with the provided identifier.</response>
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
    /// Gets tasks associated with a specific field.
    /// </summary>
    /// <param name="fieldId">Field identifier.</param>
    /// <response code="200">List of tasks associated with the field.</response>
    [HttpGet("field/{fieldId:int}")]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetTasksByField(int fieldId)
    {
        var query = new GetTasksByFieldQuery(fieldId);
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Gets all tasks associated with a specific user (through their fields).
    /// </summary>
    /// <param name="userId">User identifier owning the fields</param>
    /// <response code="200">List of tasks for the user retrieved successfully.</response>
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetTasksByUserId(int userId)
    {
        var query = new GetTasksByUserIdQuery(userId);
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Gets upcoming tasks for a user ordered by due date ascending and limited by count.
    /// </summary>
    /// <param name="userId">User identifier owning the fields</param>
    /// <param name="count">Max number of tasks to retrieve</param>
    /// <response code="200">Upcoming tasks retrieved successfully.</response>
    [HttpGet("user/{userId:int}/upcoming/{count:int}")]
    public async Task<ActionResult<IEnumerable<TaskResource>>> GetUpcomingTasksByUser(int userId, int count)
    {
        var query = new GetUpcomingTasksByUserIdQuery(userId, count);
        var tasks = await _taskQueryService.Handle(query);
        var resources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="resource">Task data to create.</param>
    /// <response code="201">Task created successfully.</response>
    /// <response code="400">Invalid input data.</response>
    [HttpPost]
    public async Task<ActionResult<TaskResource>> CreateTask([FromBody] CreateTaskResource resource)
    {
        var command = CreateTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var task = await _taskCommandService.Handle(command);
        var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, taskResource);
    }

    /// <summary>
    /// Updates data of an existing task. IDs and audit fields cannot be changed via PUT.
    /// </summary>
    /// <param name="id">Task identifier to update.</param>
    /// <param name="resource">Updated task data.</param>
    /// <response code="200">Task updated successfully.</response>
    /// <response code="404">No task found with the provided identifier.</response>
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
    /// Deletes a task.
    /// </summary>
    /// <param name="id">Task identifier to delete.</param>
    /// <response code="204">Task deleted successfully.</response>
    /// <response code="404">No task found with the provided identifier.</response>
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
