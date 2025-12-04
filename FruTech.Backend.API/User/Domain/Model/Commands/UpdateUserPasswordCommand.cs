namespace FruTech.Backend.API.User.Domain.Model.Commands;
/// <summary>
///  Command to update a user's password.
/// </summary>
/// <param name="Id"></param>
/// <param name="CurrentPassword"></param>
/// <param name="NewPassword"></param>
public record UpdateUserPasswordCommand(int Id, string CurrentPassword, string NewPassword);
