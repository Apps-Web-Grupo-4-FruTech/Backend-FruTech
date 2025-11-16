using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.CropFields.Infrastructure.Persistence.EFC.Repositories
{
    public class CropFieldRepository : BaseRepository<CropField>, ICropFieldRepository
    {
        public CropFieldRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CropField>> GetAllAsync()
        {
            return await Context.CropFields.AsNoTracking().ToListAsync();
        }

        public async Task<CropField?> GetByIdAsync(int id)
        {
            return await Context.CropFields.FindAsync(id);
        }

        public async Task<CropField?> FindByIdAsync(int id)
        {
            return await Context.CropFields
                .FirstOrDefaultAsync(cf => cf.Id == id);
        }

        public async Task<CropField?> FindByFieldIdAsync(int fieldId)
        {
            return await Context.CropFields
                .FirstOrDefaultAsync(cf => cf.FieldId == fieldId);
        }

        public async Task AddAsync(CropField cropField)
        {
            await Context.CropFields.AddAsync(cropField);
        }

        public void Update(CropField cropField)
        {
            Context.CropFields.Update(cropField);
        }

        public void Delete(CropField cropField)
        {
            Context.CropFields.Remove(cropField);
        }
    }
}

