using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.CropFields.Domain.Model.Commands;
using FruTech.Backend.API.CropFields.Domain.Model.Queries;
using FruTech.Backend.API.CropFields.Domain.Services;

namespace FruTech.Backend.API.CropFields.Interfaces.REST;

/// <summary>
/// Controlador para la gestión de CropFields (cultivos asociados a campos)
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
    /// Crea un nuevo CropField asociado a un Field (relación 1:1)
    /// </summary>
    /// <param name="command">Datos del CropField</param>
    /// <response code="201">CropField creado correctamente</response>
    /// <response code="400">Ya existe un CropField para ese Field o datos inválidos</response>
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
    /// Obtiene todos los CropFields
    /// </summary>
    /// <response code="200">Lista de CropFields</response>
    [HttpGet]
    public async Task<IActionResult> GetAllCropFields()
    {
        var cropFields = await _queryService.Handle(new GetAllCropFieldsQuery());
        return Ok(cropFields);
    }

    /// <summary>
    /// Obtiene un CropField por ID
    /// </summary>
    /// <param name="id">ID del CropField</param>
    /// <response code="200">CropField encontrado</response>
    /// <response code="404">CropField no encontrado</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCropFieldById(int id)
    {
        var cropField = await _queryService.Handle(new GetCropFieldByIdQuery(id));
        if (cropField == null) return NotFound();
        return Ok(cropField);
    }

    /// <summary>
    /// Obtiene el CropField asociado a un Field (relación 1:1)
    /// </summary>
    /// <param name="fieldId">ID del Field</param>
    /// <response code="200">CropField encontrado</response>
    /// <response code="404">No existe CropField para ese Field</response>
    [HttpGet("field/{fieldId:int}")]
    public async Task<IActionResult> GetCropFieldByFieldId(int fieldId)
    {
        var cropField = await _queryService.Handle(new GetCropFieldByFieldIdQuery(fieldId));
        if (cropField == null) return NotFound();
        return Ok(cropField);
    }

    /// <summary>
    /// Actualiza el status de un CropField (Healthy, Attention, Critical)
    /// </summary>
    /// <param name="id">ID del CropField</param>
    /// <param name="command">Nuevo status</param>
    /// <response code="200">Status actualizado correctamente</response>
    /// <response code="404">CropField no encontrado</response>
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateCropFieldStatus(int id, [FromBody] UpdateCropFieldStatusCommand command)
    {
        if (id != command.CropFieldId)
            return BadRequest(new { message = "El ID no coincide" });

        var cropField = await _commandService.Handle(command);
        if (cropField == null) return NotFound();
        return Ok(cropField);
    }
}

