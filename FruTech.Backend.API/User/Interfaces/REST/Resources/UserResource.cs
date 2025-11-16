namespace FruTech.Backend.API.User.Interfaces.REST.Resources;

public record UserResource(int Id, string UserName, string Email, string PhoneNumber, string Identificator, DateTimeOffset? CreatedDate, DateTimeOffset? UpdatedDate);
