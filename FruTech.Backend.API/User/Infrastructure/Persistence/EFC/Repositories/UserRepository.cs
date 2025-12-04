using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Infrastructure.Persistence.EFC.Repositories;
/// <summary>
///  Repository for managing UserAggregate entities.
/// </summary>
public class UserRepository : BaseRepository<UserAggregate>, IUserRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    ///  Initializes a new instance of the UserRepository class.
    /// </summary>
    /// <param name="context"></param>
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    /// <summary>
    ///  Finds a UserAggregate by email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<UserAggregate?> FindByEmailAsync(string email)
    {
        return await _context.Set<UserAggregate>().FirstOrDefaultAsync(u => u.Email == email);
    }
    /// <summary>
    ///  Finds a UserAggregate by identificator.
    /// </summary>
    /// <param name="identificator"></param>
    /// <returns></returns>
    public async Task<UserAggregate?> FindByIdentificatorAsync(string identificator)
    {
        return await _context.Set<UserAggregate>().FirstOrDefaultAsync(u => u.Identificator == identificator);
    }
}
