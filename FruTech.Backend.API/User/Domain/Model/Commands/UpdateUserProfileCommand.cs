namespace FruTech.Backend.API.User.Domain.Model.Commands;
/// <summary>
///  Command to update a user's profile.
/// </summary>
/// <param name="Id"></param>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="PhoneNumber"></param>
public record UpdateUserProfileCommand(int Id, string UserName, string Email, string PhoneNumber);
