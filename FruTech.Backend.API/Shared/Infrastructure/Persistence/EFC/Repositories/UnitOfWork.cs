using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories
{
    /// <summary>
    ///  Unit of Work implementation for managing database transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///  Database context
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        ///  Commits all changes made in the context to the database
        /// </summary>
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
