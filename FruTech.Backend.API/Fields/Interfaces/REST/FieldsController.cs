// csharp
using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/fields")]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepo;
        private readonly ICropFieldRepository _cropRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public FieldsController(IFieldRepository fieldRepo, ICropFieldRepository cropRepo, IUnitOfWork unitOfWork, AppDbContext context)
        {
            _fieldRepo = fieldRepo;
            _cropRepo = cropRepo;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Fields
                .AsNoTracking()
                .ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.Fields
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Field field)
        {
            // Considerar 0 como no enviado
            if (field.CropId.HasValue && field.CropId.Value <= 0)
                field.CropId = null;
            if (field.ProgressId.HasValue && field.ProgressId.Value <= 0)
                field.ProgressId = null;

            if (field.CropId.HasValue && field.CropId.Value > 0)
            {
                var crop = await _cropRepo.GetByIdAsync(field.CropId.Value);
                if (crop != null)
                {
                    if (string.IsNullOrWhiteSpace(field.Name)) field.Name = crop.Title;
                    field.Product = string.IsNullOrWhiteSpace(field.Product) ? crop.Title : field.Product;
                    field.Crop = string.IsNullOrWhiteSpace(field.Crop) ? crop.Title : field.Crop;
                }
                // Si el CropId es inválido, ignorar la vinculación (no lanzar error)
            }

            if (field.ProgressId.HasValue && field.ProgressId.Value > 0)
            {
                var progress = await _context.ProgressHistory.AsNoTracking().FirstOrDefaultAsync(p => p.Id == field.ProgressId.Value);
                if (progress == null)
                {
                    // Ignorar si no existe
                    field.ProgressId = null;
                }
            }

            await _fieldRepo.AddAsync(field);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = field.Id }, field);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] Field patch)
        {
            var existing = await _context.Fields.FirstOrDefaultAsync(f => f.Id == id);
            if (existing == null) return NotFound();

            // Actualizaciones básicas
            if (!string.IsNullOrWhiteSpace(patch.ImageUrl)) existing.ImageUrl = patch.ImageUrl;
            if (!string.IsNullOrWhiteSpace(patch.Name)) existing.Name = patch.Name;
            if (!string.IsNullOrWhiteSpace(patch.Location)) existing.Location = patch.Location;
            if (!string.IsNullOrWhiteSpace(patch.FieldSize)) existing.FieldSize = patch.FieldSize;
            if (!string.IsNullOrWhiteSpace(patch.Product)) existing.Product = patch.Product;
            if (!string.IsNullOrWhiteSpace(patch.Crop)) existing.Crop = patch.Crop;

            // Tratar 0 como null
            if (patch.CropId.HasValue && patch.CropId.Value <= 0)
                patch.CropId = null;
            if (patch.ProgressId.HasValue && patch.ProgressId.Value <= 0)
                patch.ProgressId = null;

            if (patch.CropId.HasValue && patch.CropId != existing.CropId && patch.CropId.Value > 0)
            {
                var crop = await _cropRepo.GetByIdAsync(patch.CropId.Value);
                if (crop != null)
                {
                    existing.CropId = crop.Id;
                    if (string.IsNullOrWhiteSpace(existing.Name)) existing.Name = crop.Title;
                    if (string.IsNullOrWhiteSpace(patch.Product)) existing.Product = crop.Title;
                    if (string.IsNullOrWhiteSpace(patch.Crop)) existing.Crop = crop.Title;
                }
                // Ignorar si no existe
            }

            if (patch.ProgressId.HasValue && patch.ProgressId != existing.ProgressId && patch.ProgressId.Value > 0)
            {
                var progress = await _context.ProgressHistory.AsNoTracking().FirstOrDefaultAsync(p => p.Id == patch.ProgressId.Value);
                if (progress != null)
                {
                    existing.ProgressId = progress.Id;
                }
                // Ignorar si no existe
            }

            if (patch.TaskIds.Count > 0)
            {
                foreach (var taskId in patch.TaskIds)
                {
                    if (taskId > 0 && !existing.TaskIds.Contains(taskId)) existing.TaskIds.Add(taskId);
                }
            }

            _fieldRepo.Update(existing);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
