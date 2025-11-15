using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Tasks.Infrastructure.Persistence.EFC.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> GetAllAsync()
    {
        return await _context.Set<Domain.Model.Aggregate.Task>().ToListAsync();
    }

    public async Task<Domain.Model.Aggregate.Task?> GetByIdAsync(int id)
    {
        return await _context.Set<Domain.Model.Aggregate.Task>()
            .FirstOrDefaultAsync(t => t.id == id);
    }

    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> GetByFieldAsync(string field)
    {
        return await _context.Set<Domain.Model.Aggregate.Task>()
            .Where(t => t.field == field)
            .ToListAsync();
    }

    public async Task<Domain.Model.Aggregate.Task> CreateAsync(Domain.Model.Aggregate.Task task)
    {
        await _context.Set<Domain.Model.Aggregate.Task>().AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<Domain.Model.Aggregate.Task?> UpdateAsync(int id, Domain.Model.Aggregate.Task task)
    {
        var existingTask = await GetByIdAsync(id);
        if (existingTask == null) return null;

        existingTask.description = task.description;
        existingTask.due_date = task.due_date;
        existingTask.field = task.field;

        _context.Set<Domain.Model.Aggregate.Task>().Update(existingTask);
        await _context.SaveChangesAsync();
        return existingTask;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if (task == null) return false;

        _context.Set<Domain.Model.Aggregate.Task>().Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}

