using FruTech.Backend.API.User.Domain.Model.Commands;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Domain.Services;
/// <summary>
///  Service interface for handling user-related commands.
/// </summary>
public interface IUserCommandService
{
    Task<UserAggregate?> Handle(SignUpUserCommand command);
    Task<UserAggregate?> Handle(SignInUserCommand command);
    Task<UserAggregate?> Handle(UpdateUserProfileCommand command);
    Task<bool> Handle(UpdateUserPasswordCommand command);
    Task<bool> Handle(DeleteUserCommand command);
}
