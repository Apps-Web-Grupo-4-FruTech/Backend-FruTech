using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;
/// <summary>
///  Data Transfer Object for updating user password.
/// </summary>
/// <param name="CurrentPassword"></param>
/// <param name="NewPassword"></param>
public record UpdateUserPasswordResource(
    [Required] string CurrentPassword,
    [Required][MinLength(6)] string NewPassword);
