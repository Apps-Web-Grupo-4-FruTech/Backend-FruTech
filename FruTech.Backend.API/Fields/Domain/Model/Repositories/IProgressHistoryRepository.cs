using FruTech.Backend.API.Fields.Domain.Model.Entities;
using FruTech.Backend.API.Shared.Domain.Repositories;

namespace FruTech.Backend.API.Fields.Domain.Model.Repositories
{
    public interface IProgressHistoryRepository : IBaseRepository<ProgressHistory>
    {
        Task<IEnumerable<ProgressHistory>> GetAllAsync();
        Task<ProgressHistory?> GetByIdAsync(int id);
        Task<ProgressHistory?> FindByFieldIdAsync(int fieldId);
        Task AddAsync(ProgressHistory progressHistory);
        void Update(ProgressHistory progressHistory);
    }
}
