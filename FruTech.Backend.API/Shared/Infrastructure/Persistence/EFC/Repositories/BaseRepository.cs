using FruTech.Backend.API.Shared.Domain.Model.ValueObjects;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;


namespace FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Base repository for all repositories
/// </summary>
/// <remarks>
///     This class implements the basic CRUD operations for all entities.
///     It requires the entity type to be passed as a generic parameter.
///     It also requires the context to be passed in the constructor.
/// </remarks>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;
    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="context"></param>
    protected BaseRepository(AppDbContext context)
    {
        Context = context;
    }
    /// <summary>
    ///  Adds a new entity to the repository
    /// </summary>
    /// <param name="entity"></param>
    public async Task AddAsync(TEntity entity)
    {

        if (entity is IEntityWithCreatedUpdatedDate auditable)
        {
            var now = DateTimeOffset.Now;
            auditable.CreatedDate ??= now;
            auditable.UpdatedDate ??= now;
        }

        await Context.Set<TEntity>().AddAsync(entity);
    }
    /// <summary>
    ///  Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity"></param>
    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }
    /// <summary>
    ///  Removes an entity from the repository
    /// </summary>
    /// <param name="entity"></param>
    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
    /// <summary>
    ///  Finds an entity by its ID
    /// </summary>
    /// <param name="id"></param>
    public async Task<TEntity?> FindByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }
    /// <summary>
    ///  Lists all entities in the repository
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }
}