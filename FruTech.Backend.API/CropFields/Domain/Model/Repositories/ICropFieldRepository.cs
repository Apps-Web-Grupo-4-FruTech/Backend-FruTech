using FruTech.Backend.API.CropFields.Domain.Model.Entities;

namespace FruTech.Backend.API.CropFields.Domain.Model.Repositories
{
    public interface ICropFieldRepository
    {
        Task<IEnumerable<CropField>> GetAllAsync();
        Task<CropField?> GetByIdAsync(int id);
        Task AddAsync(CropField cropField);
        void Update(CropField cropField);
        void Delete(CropField cropField);
    }
}
