namespace FruTech.Backend.API.User.Interfaces.REST.Resources;
/// <summary>
///  Data Transfer Object for signing in response.
/// </summary>
/// <param name="Id"></param>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="Token"></param>
public record SignInResponseResource(int Id, string UserName, string Email, string Token);

