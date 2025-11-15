using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : BaseRepository<UserAggregate>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserAggregate?> FindByEmailAsync(string email)
    {
        return await _context.Set<UserAggregate>().FirstOrDefaultAsync(u => u.Email == email);
    }
}
