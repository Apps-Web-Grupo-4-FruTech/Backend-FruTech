namespace FruTech.Backend.API.User.Domain.Model.Commands;

public record UpdateUserPasswordCommand(int Id, string CurrentPassword, string NewPassword);
