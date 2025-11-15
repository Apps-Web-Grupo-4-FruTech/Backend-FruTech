using FruTech.Backend.API.Fields.Domain.Model.Entities;

namespace FruTech.Backend.API.Fields.Domain.Model.Repositories
{
    public interface IProgressHistoryRepository
    {
        Task<IEnumerable<ProgressHistory>> GetAllAsync();
        Task<ProgressHistory?> GetByIdAsync(int id);
        Task AddAsync(ProgressHistory progressHistory);
        void Update(ProgressHistory progressHistory);
    }
}
