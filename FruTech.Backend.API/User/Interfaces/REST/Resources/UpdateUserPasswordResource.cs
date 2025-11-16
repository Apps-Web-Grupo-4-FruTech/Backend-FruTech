using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record UpdateUserPasswordResource(
    [Required] string CurrentPassword,
    [Required][MinLength(6)] string NewPassword);
