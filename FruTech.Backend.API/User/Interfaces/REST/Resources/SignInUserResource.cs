using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;
/// <summary>
///  Data Transfer Object for signing in a user.
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record SignInUserResource(
    [Required][EmailAddress] string Email,
    [Required][MinLength(6)] string Password);
