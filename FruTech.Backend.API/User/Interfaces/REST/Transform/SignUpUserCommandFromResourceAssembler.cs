using FruTech.Backend.API.User.Domain.Model.Commands;
using FruTech.Backend.API.User.Interfaces.REST.Resources;

namespace FruTech.Backend.API.User.Interfaces.REST.Transform;
/// <summary>
///  Assembler for converting SignUpUserResource DTOs to SignUpUserCommand commands.
/// </summary>
public static class SignUpUserCommandFromResourceAssembler
{
    public static SignUpUserCommand ToCommandFromResource(SignUpUserResource resource)
        => new(resource.UserName, resource.Email, resource.PhoneNumber, resource.Identificator, resource.Password, resource.RoleId);
}

