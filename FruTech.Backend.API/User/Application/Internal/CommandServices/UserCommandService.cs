using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.User.Domain.Model.Commands;
using FruTech.Backend.API.User.Domain.Repositories;
using FruTech.Backend.API.User.Domain.Services;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Application.Internal.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserAggregate?> Handle(SignUpUserCommand command)
    {
        // Verificar duplicado
        var existing = await _userRepository.FindByEmailAsync(command.Email);
        if (existing != null) return null; // o lanzar excepción personalizada

        var user = new UserAggregate(command.UserName, command.Email, command.PhoneNumber, command.Identificator);
        user.HashPassword(command.Password);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return user;
    }

    public async Task<UserAggregate?> Handle(SignInUserCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);
        if (user == null) return null;

        var verified = user.VerifyPassword(command.Password);
        return verified ? user : null;
    }

    public async Task<UserAggregate?> Handle(UpdateUserProfileCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null) return null;

        // Validar si email nuevo ya está en uso por otro usuario
        var emailOwner = await _userRepository.FindByEmailAsync(command.Email);
        if (emailOwner != null && emailOwner.Id != user.Id) return null; // Indica conflicto

        user.UpdateProfile(command.UserName, command.Email, command.PhoneNumber);
        _userRepository.Update(user);
        await _unitOfWork.CompleteAsync();
        return user;
    }

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
            return false; // contraseña actual incorrecta
        }
        _userRepository.Update(user);
        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> Handle(DeleteUserCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null) return false;
        if (!string.IsNullOrEmpty(command.CurrentPassword))
        {
            if (!user.VerifyPassword(command.CurrentPassword)) return false; // contraseña actual inválida
        }
        _userRepository.Remove(user);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
