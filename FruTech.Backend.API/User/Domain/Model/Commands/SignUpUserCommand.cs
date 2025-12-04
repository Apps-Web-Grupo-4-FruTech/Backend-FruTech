namespace FruTech.Backend.API.User.Domain.Model.Commands;
/// <summary>
///  Command to sign up a new user.
/// </summary>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="PhoneNumber"></param>
/// <param name="Identificator"></param>
/// <param name="Password"></param>
public record SignUpUserCommand(string UserName, string Email, string PhoneNumber, string Identificator, string Password);

