using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories
{
    /// <summary>
    ///  Repository for managing ProgressHistory entities using Entity Framework Core.
    /// </summary>
    public class ProgressHistoryRepository : BaseRepository<ProgressHistory>, IProgressHistoryRepository
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="ProgressHistoryRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ProgressHistoryRepository(AppDbContext context) : base(context)
        {
        }
        /// <summary>
        ///  Retrieves all ProgressHistory records from the database.
        /// </summary>
        public async Task<IEnumerable<ProgressHistory>> GetAllAsync()
        {
            return await Context.ProgressHistories
                .AsNoTracking()
                .ToListAsync();
        }
        /// <summary>
        ///  Retrieves a ProgressHistory record by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        public async Task<ProgressHistory?> GetByIdAsync(int id)
        {
            return await Context.ProgressHistories
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        /// <summary>
        ///  Retrieves a ProgressHistory record by its associated FieldId.
        /// </summary>
        /// <param name="fieldId"></param>
        public async Task<ProgressHistory?> FindByFieldIdAsync(int fieldId)
        {
            return await Context.ProgressHistories
                .FirstOrDefaultAsync(p => p.FieldId == fieldId);
        }
        /// <summary>
        ///  Adds a new ProgressHistory record to the database.
        /// </summary>
        /// <param name="progressHistory"></param>
        public async Task AddAsync(ProgressHistory progressHistory)
        {
            await Context.ProgressHistories.AddAsync(progressHistory);
        }
        /// <summary>
        ///  Updates an existing ProgressHistory record in the database.
        /// </summary>
        /// <param name="progressHistory"></param>
        public void Update(ProgressHistory progressHistory)
        {
            Context.ProgressHistories.Update(progressHistory);
        }
    }
}

