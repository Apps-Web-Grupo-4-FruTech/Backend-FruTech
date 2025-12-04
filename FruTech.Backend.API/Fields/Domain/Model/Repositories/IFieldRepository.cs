using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Shared.Domain.Repositories;

namespace FruTech.Backend.API.Fields.Domain.Model.Repositories
{
    /// <summary>
    ///  Repository interface for managing Field entities.
    /// </summary>
    public interface IFieldRepository : IBaseRepository<Field>
    {
        Task<IEnumerable<Field>> GetAllAsync();
        Task<Field?> GetByIdAsync(int id);
        Task<Field?> FindByIdAsync(int id);
        Task<IEnumerable<Field>> FindByUserIdAsync(int userId);
        Task AddAsync(Field field);
        void Update(Field field);
    }
}
