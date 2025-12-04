using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruTech.Backend.API.Tasks.Infrastructure.Persistence.EFC.Repositories;
/// <summary>
///  Repository implementation for managing Task entities.
/// </summary>
public class TaskRepository : ITaskRepository
{
    /// <summary>
    ///  The database context.
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    ///  Initializes a new instance of the <see cref="TaskRepository"/> class.
    /// </summary>
    /// <param name="context"></param>
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    ///  Gets all tasks asynchronously.
    /// </summary>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> GetAllAsync()
    {
        return await _context.Set<Domain.Model.Aggregate.Task>()
            .Include(t => t.Field)
            .ToListAsync();
    }
    /// <summary>
    ///  Gets a task by its ID asynchronously.
    /// </summary>
    /// <param name="id"></param>
    public async Task<Domain.Model.Aggregate.Task?> GetByIdAsync(int id)
    {
        return await _context.Set<Domain.Model.Aggregate.Task>()
            .Include(t => t.Field)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    /// <summary>
    ///  Gets tasks by field ID asynchronously.
    /// </summary>
    /// <param name="fieldId"></param>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> GetByFieldIdAsync(int fieldId)
    {
        return await _context.Set<Domain.Model.Aggregate.Task>()
            .Include(t => t.Field)
            .Where(t => t.FieldId == fieldId)
            .ToListAsync();
    }
    /// <summary>
    ///  Gets tasks by user ID asynchronously.
    /// </summary>
    /// <param name="userId"></param>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> GetByUserIdAsync(int userId)
    {
        return await _context.Set<Domain.Model.Aggregate.Task>()
            .Include(t => t.Field)
            .Where(t => t.Field != null && t.Field.UserId == userId)
            .ToListAsync();
    }
    /// <summary>
    ///  Gets upcoming tasks by user ID asynchronously.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="count"></param>
    public async Task<IEnumerable<Domain.Model.Aggregate.Task>> GetUpcomingTasksByUserIdAsync(int userId, int count)
    {
        var today = DateTime.UtcNow.Date;
        return await _context.Set<Domain.Model.Aggregate.Task>()
            .Include(t => t.Field)
            .Where(t => t.Field != null && t.Field.UserId == userId && t.DueDate >= today)
            .OrderBy(t => t.DueDate)
            .Take(count)
            .ToListAsync();
    }
    /// <summary>
    ///  Creates a new task asynchronously.
    /// </summary>
    /// <param name="task"></param>
    public async Task<Domain.Model.Aggregate.Task> CreateAsync(Domain.Model.Aggregate.Task task)
    {
        await _context.Set<Domain.Model.Aggregate.Task>().AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }
    /// <summary>
    ///  Updates an existing task asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="task"></param>
    public async Task<Domain.Model.Aggregate.Task?> UpdateAsync(int id, Domain.Model.Aggregate.Task task)
    {
        var existingTask = await GetByIdAsync(id);
        if (existingTask == null) return null;

        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.FieldId = task.FieldId;

        _context.Set<Domain.Model.Aggregate.Task>().Update(existingTask);
        await _context.SaveChangesAsync();
        return existingTask;
    }
    /// <summary>
    ///  Deletes a task by its ID asynchronously.
    /// </summary>
    /// <param name="id"></param>
    public async Task<bool> DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if (task == null) return false;

        _context.Set<Domain.Model.Aggregate.Task>().Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}
