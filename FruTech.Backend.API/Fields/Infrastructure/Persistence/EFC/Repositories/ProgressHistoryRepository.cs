using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories
{
    public class ProgressHistoryRepository : BaseRepository<ProgressHistory>, IProgressHistoryRepository
    {
        public ProgressHistoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProgressHistory>> GetAllAsync()
        {
            return await Context.ProgressHistories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProgressHistory?> GetByIdAsync(int id)
        {
            return await Context.ProgressHistories
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProgressHistory?> FindByFieldIdAsync(int fieldId)
        {
            return await Context.ProgressHistories
                .FirstOrDefaultAsync(p => p.FieldId == fieldId);
        }

        public async Task AddAsync(ProgressHistory progressHistory)
        {
            await Context.ProgressHistories.AddAsync(progressHistory);
        }

        public void Update(ProgressHistory progressHistory)
        {
            Context.ProgressHistories.Update(progressHistory);
        }
    }
}

