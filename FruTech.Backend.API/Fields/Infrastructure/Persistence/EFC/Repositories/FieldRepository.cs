using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories
{
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        public FieldRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Field>> GetAllAsync()
        {
            return await Context.Fields
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Field?> GetByIdAsync(int id)
        {
            return await Context.Fields
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public new async Task<Field?> FindByIdAsync(int id)
        {
            return await Context.Fields
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Field>> FindByUserIdAsync(int userId)
        {
            return await Context.Fields
                .Where(f => f.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public new async Task AddAsync(Field field)
        {
            await Context.Fields.AddAsync(field);
        }

        public new void Update(Field field)
        {
            Context.Fields.Update(field);
        }
    }
}
