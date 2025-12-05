using FruTech.Backend.API.Shared.Domain.Repositories;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Domain.Repositories;
/// <summary>
///  Repository interface for User aggregate.
/// </summary>
public interface IUserRepository : IBaseRepository<UserAggregate>
{
    Task<UserAggregate?> FindByEmailAsync(string email);
    Task<UserAggregate?> FindByIdentificatorAsync(string identificator);
    
    /// <summary>
    ///  Finds a UserAggregate by ID including related UserRole.
    ///  This overrides the generic implementation to include role information.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    new Task<UserAggregate?> FindByIdAsync(int id);
}
