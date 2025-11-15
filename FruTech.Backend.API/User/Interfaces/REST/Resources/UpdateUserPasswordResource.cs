namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record UpdateUserPasswordResource(string CurrentPassword, string NewPassword);
