namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record SignUpUserResource(string UserName, string Email, string PhoneNumber, string Identificator, string Password);

