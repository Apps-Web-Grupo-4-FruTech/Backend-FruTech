using FruTech.Backend.API.User.Domain.Model.Queries;
using FruTech.Backend.API.User.Domain.Repositories;
using FruTech.Backend.API.User.Domain.Services;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Application.Internal.QueryServices;
/// <summary>
///  Servicio de consultas para la gestión de usuarios.
/// </summary>
public class UserQueryService : IUserQueryService
{
    /// <summary>
    ///  Repositorio de usuarios.
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    ///  Constructor del servicio de consultas de usuario.
    /// </summary>
    /// <param name="userRepository"></param>
    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    /// <summary>
    ///  Maneja la consulta para obtener un usuario por su ID.
    /// </summary>
    /// <param name="query"></param>
    public async Task<UserAggregate?> Handle(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.Id);
    }
}
