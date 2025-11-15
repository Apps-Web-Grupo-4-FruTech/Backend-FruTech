using FruTech.Backend.API.User.Domain.Model.Queries;
using FruTech.Backend.API.User.Domain.Repositories;
using FruTech.Backend.API.User.Domain.Services;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Application.Internal.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;

    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserAggregate?> Handle(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.Id);
    }
}
