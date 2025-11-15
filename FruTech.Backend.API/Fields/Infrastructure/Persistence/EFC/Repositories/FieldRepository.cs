using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly AppDbContext _context;

        public FieldRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Field>> GetAllAsync()
        {
            return await _context.Fields
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Field?> GetByIdAsync(int id)
        {
            return await _context.Fields
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Field field)
        {
            await _context.Fields.AddAsync(field);
        }

        public void Update(Field field)
        {
            _context.Fields.Update(field);
        }
    }
}
