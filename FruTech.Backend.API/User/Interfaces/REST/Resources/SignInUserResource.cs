using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record SignInUserResource(
    [Required][EmailAddress] string Email,
    [Required][MinLength(6)] string Password);
