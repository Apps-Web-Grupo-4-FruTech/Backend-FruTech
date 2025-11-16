using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.CropFields.Domain.Model.Commands;
using FruTech.Backend.API.CropFields.Domain.Model.Queries;
using FruTech.Backend.API.CropFields.Domain.Services;

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
        var cropFields = await _queryService.Handle(new GetAllCropFieldsQuery());
        return Ok(cropFields);
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
    /// Updates the crop attribute of a CropField
    /// </summary>
    /// <param name="id">CropField ID</param>
    /// <param name="command">New crop value</param>
    /// <response code="200">Crop updated successfully</response>
    /// <response code="404">CropField not found</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCropField(int id, [FromBody] UpdateCropFieldCommand command)
    {
        if (id != command.CropFieldId)
            return BadRequest(new { message = "ID mismatch" });


        var cropField = await _commandService.Handle(command);
        if (cropField == null) return NotFound();
        return Ok(cropField);
    }
}

