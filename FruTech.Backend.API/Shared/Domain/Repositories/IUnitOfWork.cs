namespace FruTech.Backend.API.Shared.Domain.Repositories;

/// <summary>
///     Unit of work interface
/// </summary>
/// <remarks>
///     This interface defines the basic operations for a unit of work
/// </remarks>
 
public interface IUnitOfWork
{
    /// <summary>
    ///     Commit all changes to the database.
    /// </summary>
    /// <returns></returns>
    Task CompleteAsync();
}