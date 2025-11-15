using FruTech.Backend.API.UpcomingTasks.Domain.Services;
using FruTech.Backend.API.UpcomingTasks.Domain.Model.Queries;
using FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Resources;
using FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Transform;
using FruTech.Backend.API.UpcomingTasks.Domain.Model.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FruTech.Backend.API.UpcomingTasks.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/upcoming-tasks")]
public class UpcomingTasksController : ControllerBase
{
    private readonly IUpcomingTaskCommandService _upcomingTaskCommandService;
    private readonly IUpcomingTaskQueryService _upcomingTaskQueryService;

    public UpcomingTasksController(IUpcomingTaskCommandService upcomingTaskCommandService, IUpcomingTaskQueryService upcomingTaskQueryService)
    {
        _upcomingTaskCommandService = upcomingTaskCommandService;
        _upcomingTaskQueryService = upcomingTaskQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllUpcomingTasksQuery();
        var upcomingTasks = await _upcomingTaskQueryService.Handle(query);
        var resources = upcomingTasks.Select(UpcomingTaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpcomingTaskResource resource)
    {
        var command = CreateUpcomingTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var upcomingTask = await _upcomingTaskCommandService.Handle(command);
        var upcomingTaskResource = UpcomingTaskResourceFromEntityAssembler.ToResourceFromEntity(upcomingTask);
        return Ok(upcomingTaskResource);
    }
}

