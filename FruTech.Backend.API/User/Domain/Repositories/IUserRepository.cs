using FruTech.Backend.API.Shared.Domain.Repositories;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Domain.Repositories;

public interface IUserRepository : IBaseRepository<UserAggregate>
{
    Task<UserAggregate?> FindByEmailAsync(string email);
}
