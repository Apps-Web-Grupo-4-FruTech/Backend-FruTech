using FruTech.Backend.API.Fields.Domain.Model.Entities;

namespace FruTech.Backend.API.Fields.Domain.Model.Repositories
{
    public interface IFieldRepository
    {
        Task<IEnumerable<Field>> GetAllAsync();
        Task<Field?> GetByIdAsync(int id);
        Task AddAsync(Field field);
        void Update(Field field);
    }
}
