namespace FruTech.Backend.API.User.Domain.Model.Commands;
/// <summary>
///  Command to sign in a user.
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record SignInUserCommand(string Email, string Password);

