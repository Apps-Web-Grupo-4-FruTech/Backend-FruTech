namespace FruTech.Backend.API.User.Domain.Model.Commands;

public record SignUpUserCommand(string UserName, string Email, string PhoneNumber, string Identificator, string Password);

