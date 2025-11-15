namespace FruTech.Backend.API.User.Domain.Model.Commands;

public record DeleteUserCommand(int Id, string? CurrentPassword = null);
