namespace FruTech.Backend.API.User.Domain.Model.Commands;
/// <summary>
///  Command to delete a user.
/// </summary>
/// <param name="Id"></param>
/// <param name="CurrentPassword"></param>
public record DeleteUserCommand(int Id, string? CurrentPassword = null);
