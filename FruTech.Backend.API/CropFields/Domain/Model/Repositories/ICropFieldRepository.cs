using FruTech.Backend.API.CropFields.Domain.Model.Entities;
using FruTech.Backend.API.Shared.Domain.Repositories;

namespace FruTech.Backend.API.CropFields.Domain.Model.Repositories
{
    /// <summary>
    ///  Repository for CropField entity
    /// </summary>
    public interface ICropFieldRepository : IBaseRepository<CropField>
    {
        Task<IEnumerable<CropField>> GetAllAsync();
        Task<CropField?> GetByIdAsync(int id);
        new Task<CropField?> FindByIdAsync(int id);
        Task<CropField?> FindByFieldIdAsync(int fieldId);
        Task<CropField?> FindAnyByFieldIdAsync(int fieldId); 
        Task<IEnumerable<CropField>> GetByUserIdAsync(int userId);
        new Task AddAsync(CropField cropField);
        new void Update(CropField cropField);
        void Delete(CropField cropField);
    }
}
