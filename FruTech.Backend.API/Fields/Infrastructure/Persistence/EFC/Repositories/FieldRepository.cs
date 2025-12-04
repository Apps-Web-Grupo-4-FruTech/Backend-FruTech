using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories
{
    /// <summary>
    ///  Repository for managing Field entities using Entity Framework Core.
    /// </summary>
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="FieldRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public FieldRepository(AppDbContext context) : base(context)
        {
        }
        /// <summary>
        ///  Retrieves all Field records from the database.
        /// </summary>
        public async Task<IEnumerable<Field>> GetAllAsync()
        {
            return await Context.Fields
                .Include(f => f.CropField)
                .Include(f => f.ProgressHistory)
                .Include(f => f.Tasks)
                .AsNoTracking()
                .ToListAsync();
        }
        /// <summary>
        ///  Retrieves a Field record by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        public async Task<Field?> GetByIdAsync(int id)
        {
            return await Context.Fields
                .Include(f => f.CropField)
                .Include(f => f.ProgressHistory)
                .Include(f => f.Tasks)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        /// <summary>
        ///  Retrieves a Field record by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        public new async Task<Field?> FindByIdAsync(int id)
        {
            return await Context.Fields
                .Include(f => f.CropField)
                .Include(f => f.ProgressHistory)
                .Include(f => f.Tasks)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        /// <summary>
        ///  Retrieves Field records by the associated UserId.
        /// </summary>
        /// <param name="userId"></param>
        public async Task<IEnumerable<Field>> FindByUserIdAsync(int userId)
        {
            return await Context.Fields
                .Include(f => f.CropField)
                .Include(f => f.ProgressHistory)
                .Include(f => f.Tasks)
                .Where(f => f.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
        /// <summary>
        ///  Adds a new Field record to the database.
        /// </summary>
        /// <param name="field"></param>
        public new async Task AddAsync(Field field)
        {
            await Context.Fields.AddAsync(field);
        }
        /// <summary>
        ///  Updates an existing Field record in the database.
        /// </summary>
        /// <param name="field"></param>
        public new void Update(Field field)
        {
            Context.Fields.Update(field);
        }
    }
}
