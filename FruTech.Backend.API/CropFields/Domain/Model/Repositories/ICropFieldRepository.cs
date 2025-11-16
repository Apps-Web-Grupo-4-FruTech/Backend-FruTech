using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.Shared.Domain.Repositories;

namespace FruTech.Backend.API.CropFields.Domain.Model.Repositories
{
    public interface ICropFieldRepository : IBaseRepository<CropField>
    {
        Task<IEnumerable<CropField>> GetAllAsync();
        Task<CropField?> GetByIdAsync(int id);
        Task<CropField?> FindByIdAsync(int id);
        Task<CropField?> FindByFieldIdAsync(int fieldId);
        Task AddAsync(CropField cropField);
        void Update(CropField cropField);
        void Delete(CropField cropField);
    }
}
