using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.CropFields.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/crop_fields")]
    public class CropFieldsController : ControllerBase
    {
        private readonly ICropFieldRepository _cropFieldRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CropFieldsController(ICropFieldRepository cropFieldRepository, IUnitOfWork unitOfWork)
        {
            _cropFieldRepository = cropFieldRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropField>>> GetAllAsync()
        {
            var items = await _cropFieldRepository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CropField>> GetByIdAsync(int id)
        {
            var item = await _cropFieldRepository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CropField>> PostAsync([FromBody] CropField cropField)
        {
            var scope = HttpContext.RequestServices.CreateScope();
            var db = scope.ServiceProvider.GetService<FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext>();
            
            // Si Field es 0 o no se especificó, buscar el último Field creado
            if (cropField.Field <= 0 && db != null)
            {
                var lastField = await db.Fields.OrderByDescending(f => f.Id).FirstOrDefaultAsync();
                if (lastField != null)
                {
                    cropField.Field = lastField.Id; // Asignar automáticamente el último Field
                }
            }

            await _cropFieldRepository.AddAsync(cropField);
            await _unitOfWork.CompleteAsync();

            // Vincular automáticamente con Field si el campo 'Field' contiene un Id válido (>0)
            if (cropField.Field > 0 && db != null)
            {
                var fieldEntity = await db.Fields.FirstOrDefaultAsync(f => f.Id == cropField.Field);
                if (fieldEntity != null)
                {
                    fieldEntity.CropId = cropField.Id; // vincular
                    if (string.IsNullOrWhiteSpace(fieldEntity.Product)) fieldEntity.Product = cropField.Title;
                    if (string.IsNullOrWhiteSpace(fieldEntity.Crop)) fieldEntity.Crop = cropField.Title;
                    db.Fields.Update(fieldEntity);
                    await db.SaveChangesAsync();
                }
            }

            return Ok(cropField); // cambiado de CreatedAtAction para evitar warning de acción no resuelta
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] CropField input)
        {
            var existing = await _cropFieldRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = input.Title;
            existing.PlantingDate = input.PlantingDate;
            existing.HarvestDate = input.HarvestDate;
            existing.Field = input.Field;
            existing.Status = input.Status;
            existing.Days = input.Days;

            _cropFieldRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var existing = await _cropFieldRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _cropFieldRepository.Delete(existing);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
