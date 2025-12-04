using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.User.Domain.Model.Commands;
using FruTech.Backend.API.User.Domain.Repositories;
using FruTech.Backend.API.User.Domain.Services;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;
using FruTech.Backend.API.CommunityRecommendation.Domain.Repositories; 

namespace FruTech.Backend.API.User.Application.Internal.CommandServices;
/// <summary>
///  Servicio de comandos para la gestión de usuarios.
/// </summary>
public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommunityRecommendationRepository _communityRecommendationRepository; // new field
    /// <summary>
    ///  Constructor del servicio de comandos de usuario.
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="communityRecommendationRepository"></param>
    public UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork, ICommunityRecommendationRepository communityRecommendationRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _communityRecommendationRepository = communityRecommendationRepository; // assign
    }
    /// <summary>
    ///  Maneja el comando de registro de usuario.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<UserAggregate?> Handle(SignUpUserCommand command)
    {

        var existing = await _userRepository.FindByEmailAsync(command.Email);
        if (existing != null) return null; 
        var existingId = await _userRepository.FindByIdentificatorAsync(command.Identificator);
        if (existingId != null) return null; 

        var user = new UserAggregate(command.UserName, command.Email, command.PhoneNumber, command.Identificator);
        user.HashPassword(command.Password);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return user;
    }
    /// <summary>
    ///  Maneja el comando de inicio de sesión de usuario.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<UserAggregate?> Handle(SignInUserCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);
        if (user == null) return null;

        var verified = user.VerifyPassword(command.Password);
        return verified ? user : null;
    }
    /// <summary>
    ///  Maneja el comando de actualización de perfil de usuario.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<UserAggregate?> Handle(UpdateUserProfileCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null) return null;
        
        var emailOwner = await _userRepository.FindByEmailAsync(command.Email);
        if (emailOwner != null && emailOwner.Id != user.Id) return null; 

        var oldUserName = user.UserName; 

        user.UpdateProfile(command.UserName, command.Email, command.PhoneNumber);
        _userRepository.Update(user);

        if (!string.Equals(oldUserName, command.UserName, StringComparison.Ordinal))
        {
            await _communityRecommendationRepository.UpdateAuthorNameAsync(oldUserName, command.UserName);
        }

        await _unitOfWork.CompleteAsync();
        return user;
    }
    /// <summary>
    /// Maneja el comando de actualización de contraseña de usuario.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateUserPasswordCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null) return false;
        try
        {
            user.ChangePassword(command.CurrentPassword, command.NewPassword);
        }
        catch (InvalidOperationException)
        {
            return false; 
        }
        _userRepository.Update(user);
        await _unitOfWork.CompleteAsync();
        return true;
    }
    /// <summary>
    ///  Maneja el comando de eliminación de usuario.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteUserCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null) return false;
        if (!string.IsNullOrEmpty(command.CurrentPassword))
        {
            if (!user.VerifyPassword(command.CurrentPassword)) return false; 
        }
        _userRepository.Remove(user);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
