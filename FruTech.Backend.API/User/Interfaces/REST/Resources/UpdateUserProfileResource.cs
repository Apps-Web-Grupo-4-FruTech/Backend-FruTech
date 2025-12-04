using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;
/// <summary>
///  Data Transfer Object for updating user profile information.
/// </summary>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="PhoneNumber"></param>
public record UpdateUserProfileResource(
    [Required][MinLength(3)] string UserName,
    [Required][EmailAddress] string Email,
    [Required][Phone] string PhoneNumber);
