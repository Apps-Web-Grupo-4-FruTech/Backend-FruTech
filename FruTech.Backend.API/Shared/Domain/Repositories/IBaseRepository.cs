namespace FruTech.Backend.API.Shared.Domain.Repositories;

/// <summary>
///     Base repository interface for all repositories.
/// </summary>
/// <remarks>
///     This interface defines the basic CRUD operations that all repository implementations must provide.
/// </remarks>
/// <typeparam name="TEntity">The Entity Type</typeparam>
public interface IBaseRepository<TEntity>
{
    /// <summary>
    ///     Add entity to the repository.
    /// </summary>
    /// <param name="entity">Entity object to add</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);
    
    /// <summary>
    ///     Updates an entity.
    /// </summary>
    /// <param name="entity">The entity object to update</param>
    void Update(TEntity entity);
    
    /// <summary>
    ///     Removes an entity.
    /// </summary>
    /// <param name="entity">The entity object to remove</param>
    void Remove(TEntity entity);
    
    /// <summary>
    ///     Find entity by Id.
    /// </summary>
    /// <param name="id">The entity ID to find</param>
    /// <returns>Entity object if found</returns>
    Task<TEntity?> FindByIdAsync(int id);
    
    /// <summary>
    ///     Gets all entities.
    /// </summary>
    /// <returns>An Enumerable containing all entity objects</returns>
    Task<IEnumerable<TEntity>> ListAsync();
}