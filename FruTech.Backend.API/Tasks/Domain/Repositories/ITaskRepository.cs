using FruTech.Backend.API.Tasks.Domain.Model.Aggregate;

namespace FruTech.Backend.API.Tasks.Domain.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<Model.Aggregate.Task>> GetAllAsync();
    Task<Model.Aggregate.Task?> GetByIdAsync(int id);
    Task<IEnumerable<Model.Aggregate.Task>> GetByFieldAsync(string field);
    Task<Model.Aggregate.Task> CreateAsync(Model.Aggregate.Task task);
    Task<Model.Aggregate.Task?> UpdateAsync(int id, Model.Aggregate.Task task);
    Task<bool> DeleteAsync(int id);
}

