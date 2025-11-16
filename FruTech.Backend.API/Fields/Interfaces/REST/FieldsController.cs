using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Fields.Domain.Model.Commands;
using FruTech.Backend.API.Fields.Domain.Model.Queries;
using FruTech.Backend.API.Fields.Domain.Services;

namespace FruTech.Backend.API.Fields.Interfaces.REST;

/// <summary>
/// Controlador para la gestión de campos agrícolas (Fields)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class FieldsController : ControllerBase
{
    private readonly IFieldCommandService _fieldCommandService;
    private readonly IFieldQueryService _fieldQueryService;

    public FieldsController(
        IFieldCommandService fieldCommandService,
        IFieldQueryService fieldQueryService)
    {
        _fieldCommandService = fieldCommandService;
        _fieldQueryService = fieldQueryService;
    }

    /// <summary>
    /// Crea un nuevo campo y su ProgressHistory asociado automáticamente
    /// </summary>
    /// <param name="command">Datos del comando CreateField (UserId, ImageUrl, Name, Location, FieldSize)</param>
    /// <response code="201">Campo creado correctamente</response>
    /// <response code="400">Datos inválidos</response>
    [HttpPost]
    public async Task<IActionResult> CreateField(
        [FromBody] CreateFieldCommand command)
    {
        try
        {
            var field = await _fieldCommandService.Handle(command);
            return CreatedAtAction(nameof(GetFieldById), new { id = field.Id }, field);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene todos los campos de un usuario
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <response code="200">Lista de campos del usuario</response>
    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetFieldsByUserId(int userId)
    {
        var fields = await _fieldQueryService.Handle(new GetFieldsByUserIdQuery(userId));
        return Ok(fields);
    }

    /// <summary>
    /// Obtiene un campo por ID
    /// </summary>
    /// <param name="id">ID del campo</param>
    /// <response code="200">Campo encontrado</response>
    /// <response code="404">Campo no encontrado</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetFieldById(int id)
    {
        var field = await _fieldQueryService.Handle(new GetFieldByIdQuery(id));
        if (field == null) return NotFound();
        return Ok(field);
    }
}

