using FruTech.Backend.API.Tasks.Domain.Model.Aggregate;

namespace FruTech.Backend.API.Tasks.Domain.Repositories;
/// <summary>
///  Repository interface for managing Task entities.
/// </summary>
public interface ITaskRepository
{
    Task<IEnumerable<Model.Aggregate.Task>> GetAllAsync();
    Task<Model.Aggregate.Task?> GetByIdAsync(int id);
    Task<IEnumerable<Model.Aggregate.Task>> GetByFieldIdAsync(int fieldId);
    Task<IEnumerable<Model.Aggregate.Task>> GetByUserIdAsync(int userId);
    Task<Model.Aggregate.Task> CreateAsync(Model.Aggregate.Task task);
    Task<Model.Aggregate.Task?> UpdateAsync(int id, Model.Aggregate.Task task);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Model.Aggregate.Task>> GetUpcomingTasksByUserIdAsync(int userId, int count);
}
