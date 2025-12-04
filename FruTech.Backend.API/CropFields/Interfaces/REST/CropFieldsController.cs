using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.CropFields.Domain.Model.Commands;
using FruTech.Backend.API.CropFields.Domain.Model.Queries;
using FruTech.Backend.API.CropFields.Domain.Services;
using FruTech.Backend.API.CropFields.Interfaces.REST.Transform;
using FruTech.Backend.API.CropFields.Interfaces.REST.Resources;

namespace FruTech.Backend.API.CropFields.Interfaces.REST;

/// <summary>
/// Controller for managing CropFields (crops associated with fields)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class CropFieldsController : ControllerBase
{
    private readonly ICropFieldCommandService _commandService;
    private readonly ICropFieldQueryService _queryService;

    public CropFieldsController(
        ICropFieldCommandService commandService,
        ICropFieldQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    /// <summary>
    /// Creates a new CropField associated with a Field (1:1 relationship)
    /// </summary>
    /// <param name="command">CropField data</param>
    /// <response code="201">CropField created successfully</response>
    /// <response code="400">A CropField already exists for this Field or invalid data</response>
    [HttpPost]
    public async Task<IActionResult> CreateCropField([FromBody] CreateCropFieldCommand command)
    {
        try
        {
            var cropField = await _commandService.Handle(command);
            return CreatedAtAction(nameof(GetCropFieldById), new { id = cropField.Id }, cropField);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Gets all CropFields
    /// </summary>
    /// <response code="200">List of CropFields</response>
    [HttpGet]
    public async Task<IActionResult> GetAllCropFields()
    {
        var entities = await _queryService.Handle(new GetAllCropFieldsQuery());
        var resources = entities.Select(CropFieldResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    /// <summary>
    /// Gets a CropField by ID
    /// </summary>
    /// <param name="id">CropField ID</param>
    /// <response code="200">CropField found</response>
    /// <response code="404">CropField not found</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCropFieldById(int id)
    {
        var cropField = await _queryService.Handle(new GetCropFieldByIdQuery(id));
        if (cropField == null) return NotFound();
        return Ok(cropField);
    }

    /// <summary>
    /// Gets the CropField associated with a Field (1:1 relationship)
    /// </summary>
    /// <param name="fieldId">Field ID</param>
    /// <response code="200">CropField found</response>
    /// <response code="404">No CropField exists for this Field</response>
    [HttpGet("field/{fieldId:int}")]
    public async Task<IActionResult> GetCropFieldByFieldId(int fieldId)
    {
        var cropField = await _queryService.Handle(new GetCropFieldByFieldIdQuery(fieldId));
        if (cropField == null) return NotFound();
        return Ok(cropField);
    }

    /// <summary>
    /// Gets all CropFields associated with a specific user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <response code="200">List of CropFields for the user</response>
    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetCropFieldsByUserId(int userId)
    {
        var cropFields = await _queryService.Handle(new GetCropFieldsByUserIdQuery(userId));
        var resources = cropFields.Select(CropFieldResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    /// <summary>
    /// Updates attributes of a CropField (supports partial updates): Crop, PlantingDate, HarvestDate, Status. IDs and audit fields cannot be modified.
    /// </summary>
    /// <param name="id">CropField ID</param>
    /// <param name="command">CropField update command (partial values allowed)</param>
    /// <response code="200">CropField updated successfully</response>
    /// <response code="404">CropField not found</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCropField(int id, [FromBody] UpdateCropFieldCommand command)
    {
        var cropField = await _commandService.Handle(id, command);
        if (cropField == null) return NotFound();
        var resource = CropFieldResourceFromEntityAssembler.ToResource(cropField);
        return Ok(resource);
    }

    /// <summary>
    /// Soft-delete a CropField by ID (marks it as deleted). This endpoint is exposed as HTTP DELETE and will appear in Swagger.
    /// </summary>
    /// <param name="id">CropField ID</param>
    /// <response code="204">CropField deleted successfully (soft-delete)</response>
    /// <response code="404">CropField not found</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCropField(int id)
    {
        var cropField = await _commandService.HandleDelete(id);
        if (cropField == null) return NotFound();
        return NoContent();
    }
}
