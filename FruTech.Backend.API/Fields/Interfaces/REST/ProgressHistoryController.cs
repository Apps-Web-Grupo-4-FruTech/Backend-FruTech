using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using FruTech.Backend.API.Fields.Interfaces.REST.Resources;

namespace FruTech.Backend.API.Fields.Interfaces.REST
{
    /// <summary>
    /// Controller for managing progress history records.
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
        /// Gets all progress history records.
        /// </summary>
        /// <response code="200">List of histories retrieved successfully.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _progressRepo.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// Gets a progress history by its identifier.
        /// </summary>
        /// <param name="id">History identifier.</param>
        /// <response code="200">History found.</response>
        /// <response code="404">No history found with the provided identifier.</response>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _progressRepo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Creates a new progress history record.
        /// </summary>
        /// <param name="progressHistory">Progress history data to create.</param>
        /// <response code="201">History created successfully.</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgressHistory progressHistory)
        {
            var scope = HttpContext.RequestServices.CreateScope();
            var db = scope.ServiceProvider.GetService<FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext>();
            
            if (progressHistory.FieldId <= 0 && db != null)
            {
                var lastField = await db.Fields.OrderByDescending(f => f.Id).FirstOrDefaultAsync();
                if (lastField != null)
                {
                    progressHistory.FieldId = lastField.Id; 
                }
            }

            await _progressRepo.AddAsync(progressHistory);
            await _unitOfWork.CompleteAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = progressHistory.Id }, progressHistory);
        }

        /// <summary>
        /// Updates a progress history record.
        /// </summary>
        /// <param name="id">Identifier of the history to update.</param>
        /// <param name="resource">Updated history data (only Watered, Fertilized, Pests).</param>
        /// <response code="204">History updated successfully.</response>
        /// <response code="404">No history found with the provided identifier.</response>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProgressHistoryResource resource)
        {
            var existing = await _progressRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();
            
            existing.Watered = resource.Watered;
            existing.Fertilized = resource.Fertilized;
            existing.Pests = resource.Pests;

            _progressRepo.Update(existing);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
