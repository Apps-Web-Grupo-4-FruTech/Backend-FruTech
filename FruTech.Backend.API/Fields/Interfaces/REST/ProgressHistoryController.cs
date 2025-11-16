using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Interfaces.REST
{
    /// <summary>
    /// Controlador para la gestión del historial de progreso de los cultivos.
    /// </summary>
    [ApiController]
    [Route("api/v1/progress")]
    public class ProgressHistoryController : ControllerBase
    {
        private readonly IProgressHistoryRepository _progressRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProgressHistoryController(IProgressHistoryRepository progressRepo, IUnitOfWork unitOfWork)
        {
            _progressRepo = progressRepo;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene todos los registros de historial de progreso.
        /// </summary>
        /// <response code="200">Lista de historiales recuperada correctamente.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _progressRepo.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// Obtiene un historial de progreso por su identificador.
        /// </summary>
        /// <param name="id">Identificador del historial.</param>
        /// <response code="200">Historial encontrado.</response>
        /// <response code="404">No existe un historial con el identificador proporcionado.</response>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _progressRepo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Crea un nuevo registro de historial de progreso.
        /// </summary>
        /// <param name="progressHistory">Datos del historial de progreso a crear.</param>
        /// <response code="201">Historial creado correctamente.</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgressHistory progressHistory)
        {
            var scope = HttpContext.RequestServices.CreateScope();
            var db = scope.ServiceProvider.GetService<FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext>();

            // Si FieldId es 0 o no se especificó, buscar el último Field creado
            if (progressHistory.FieldId <= 0 && db != null)
            {
                var lastField = await db.Fields.OrderByDescending(f => f.Id).FirstOrDefaultAsync();
                if (lastField != null)
                {
                    progressHistory.FieldId = lastField.Id; // Asignar automáticamente el último Field
                }
            }

            await _progressRepo.AddAsync(progressHistory);
            await _unitOfWork.CompleteAsync();

            // Eliminado: asignación de ProgressHistoryId inexistente. La relación se resuelve por FieldId en ProgressHistory.

            return CreatedAtAction(nameof(GetById), new { id = progressHistory.Id }, progressHistory);
        }

        /// <summary>
        /// Actualiza un registro de historial de progreso.
        /// </summary>
        /// <param name="id">Identificador del historial a actualizar.</param>
        /// <param name="progressHistory">Datos actualizados del historial.</param>
        /// <response code="204">Historial actualizado correctamente.</response>
        /// <response code="404">No existe un historial con el identificador proporcionado.</response>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProgressHistory progressHistory)
        {

            var existing = await _progressRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Actualizar propiedades
            existing.FieldId = progressHistory.FieldId;
            existing.Watered = progressHistory.Watered;
            existing.Fertilized = progressHistory.Fertilized;
            existing.Pests = progressHistory.Pests;

            _progressRepo.Update(existing);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
