using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;
/// <summary>
///  Data Transfer Object for signing up a new user.
/// </summary>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="PhoneNumber"></param>
/// <param name="Identificator"></param>
/// <param name="Password"></param>
public record SignUpUserResource(
    [Required][MinLength(3)] string UserName,
    [Required][EmailAddress] string Email,
    [Required][Phone] string PhoneNumber,
    [Required] string Identificator,
    [Required][MinLength(6)] string Password);
