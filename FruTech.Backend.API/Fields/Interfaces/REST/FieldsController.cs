using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Fields.Domain.Model.Commands;
using FruTech.Backend.API.Fields.Domain.Model.Queries;
using FruTech.Backend.API.Fields.Domain.Services;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;
using FruTech.Backend.API.Fields.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Http;

namespace FruTech.Backend.API.Fields.Interfaces.REST;

/// <summary>
/// Controller for managing agricultural fields
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class FieldsController : ControllerBase
{
    private readonly IFieldCommandService _fieldCommandService;
    private readonly IFieldQueryService _fieldQueryService;
    /// <summary>
    ///  Constructor for FieldsController
    /// </summary>
    /// <param name="fieldCommandService"></param>
    /// <param name="fieldQueryService"></param>
    public FieldsController(
        IFieldCommandService fieldCommandService,
        IFieldQueryService fieldQueryService)
    {
        _fieldCommandService = fieldCommandService;
        _fieldQueryService = fieldQueryService;
    }
    /// <summary>
    ///  Request model for creating a new field
    /// </summary>
    public class CreateFieldRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string FieldSize { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }

    /// <summary>
    /// Creates a new field and its associated ProgressHistory automatically
    /// </summary>
    /// <param name="request">CreateField form data (UserId, Name, Location, FieldSize, Image)</param>
    /// <response code="201">Field created successfully</response>
    /// <response code="400">Invalid data</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateField(
        [FromForm] CreateFieldRequest request)
    {
        try
        {
            byte[]? imageBytes = null;
            string? contentType = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                using var ms = new MemoryStream();
                await request.Image.CopyToAsync(ms);
                imageBytes = ms.ToArray();
                contentType = request.Image.ContentType;
            }

            var command = new CreateFieldCommand(
                request.UserId,
                imageBytes,
                contentType,
                request.Name,
                request.Location,
                request.FieldSize
            );

            var field = await _fieldCommandService.Handle(command);
            var resource = FieldResourceFromEntityAssembler.ToResource(field);
            return CreatedAtAction(nameof(GetFieldById), new { id = field.Id }, resource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Gets all fields for a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <response code="200">List of user fields</response>
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<FieldResource>>> GetFieldsByUserId(int userId)
    {
        var resources = await _fieldQueryService.Handle(new GetFieldsByUserIdQuery(userId));
        return Ok(resources);
    }

    /// <summary>
    /// Gets a field by ID
    /// </summary>
    /// <param name="id">Field ID</param>
    /// <response code="200">Field found</response>
    /// <response code="404">Field not found</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FieldResource>> GetFieldById(int id)
    {
        var resource = await _fieldQueryService.Handle(new GetFieldByIdQuery(id));
        if (resource == null) return NotFound();
        return Ok(resource);
    }
}
