using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories
{
    public class ProgressHistoryRepository : IProgressHistoryRepository
    {
        private readonly AppDbContext _context;

        public ProgressHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProgressHistory>> GetAllAsync()
        {
            return await _context.ProgressHistory
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProgressHistory?> GetByIdAsync(int id)
        {
            return await _context.ProgressHistory
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(ProgressHistory progressHistory)
        {
            await _context.ProgressHistory.AddAsync(progressHistory);
        }

        public void Update(ProgressHistory progressHistory)
        {
            _context.ProgressHistory.Update(progressHistory);
        }
    }
}

