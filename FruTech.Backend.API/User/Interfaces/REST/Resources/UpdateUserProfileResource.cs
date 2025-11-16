using System.ComponentModel.DataAnnotations;
namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record UpdateUserProfileResource(
    [Required][MinLength(3)] string UserName,
    [Required][EmailAddress] string Email,
    [Required][Phone] string PhoneNumber);
