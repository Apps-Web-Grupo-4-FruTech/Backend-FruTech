using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Interfaces.REST
{
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _progressRepo.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _progressRepo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

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

            // Vincular automáticamente con Field si FieldId > 0
            if (progressHistory.FieldId > 0 && db != null)
            {
                var fieldEntity = await db.Fields.FirstOrDefaultAsync(f => f.Id == progressHistory.FieldId);
                if (fieldEntity != null)
                {
                    fieldEntity.ProgressId = progressHistory.Id;
                    db.Fields.Update(fieldEntity);
                    await db.SaveChangesAsync();
                }
            }

            return CreatedAtAction(nameof(GetById), new { id = progressHistory.Id }, progressHistory);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProgressHistory progressHistory)
        {

            var existing = await _progressRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Actualizar propiedades
            existing.FieldId = progressHistory.FieldId;
            if (progressHistory.Watered.HasValue) existing.Watered = progressHistory.Watered;
            if (progressHistory.Fertilized.HasValue) existing.Fertilized = progressHistory.Fertilized;
            if (progressHistory.Pests.HasValue) existing.Pests = progressHistory.Pests;

            _progressRepo.Update(existing);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
