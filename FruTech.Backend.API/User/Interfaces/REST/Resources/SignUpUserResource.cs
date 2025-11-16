using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record SignUpUserResource(
    [Required][MinLength(3)] string UserName,
    [Required][EmailAddress] string Email,
    [Required][Phone] string PhoneNumber,
    [Required] string Identificator,
    [Required][MinLength(6)] string Password);
