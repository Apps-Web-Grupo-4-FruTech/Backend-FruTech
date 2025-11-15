using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.CropFields.Infrastructure.Persistence.EFC.Repositories
{
    public class CropFieldRepository : ICropFieldRepository
    {
        private readonly AppDbContext _context;

        public CropFieldRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CropField>> GetAllAsync()
        {
            return await _context.CropFields.AsNoTracking().ToListAsync();
        }

        public async Task<CropField?> GetByIdAsync(int id)
        {
            return await _context.CropFields.FindAsync(id);
        }

        public async Task AddAsync(CropField cropField)
        {
            await _context.CropFields.AddAsync(cropField);
        }

        public void Update(CropField cropField)
        {
            _context.CropFields.Update(cropField);
        }

        public void Delete(CropField cropField)
        {
            _context.CropFields.Remove(cropField);
        }
    }
}

