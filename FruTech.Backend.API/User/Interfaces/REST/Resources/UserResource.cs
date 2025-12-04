namespace FruTech.Backend.API.User.Interfaces.REST.Resources;
/// <summary>
///  Data Transfer Object representing a User.
/// </summary>
/// <param name="Id"></param>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="PhoneNumber"></param>
/// <param name="Identificator"></param>
/// <param name="CreatedDate"></param>
/// <param name="UpdatedDate"></param>
public record UserResource(int Id, string UserName, string Email, string PhoneNumber, string Identificator, DateTimeOffset? CreatedDate, DateTimeOffset? UpdatedDate);
