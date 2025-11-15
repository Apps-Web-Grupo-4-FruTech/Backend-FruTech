namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record SignInResponseResource(int Id, string UserName, string Email, string Token);

