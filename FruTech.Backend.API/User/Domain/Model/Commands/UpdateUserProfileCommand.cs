namespace FruTech.Backend.API.User.Domain.Model.Commands;

public record UpdateUserProfileCommand(int Id, string UserName, string Email, string PhoneNumber);
