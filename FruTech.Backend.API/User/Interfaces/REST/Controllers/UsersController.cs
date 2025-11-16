using FruTech.Backend.API.User.Application.Internal.CommandServices;
using FruTech.Backend.API.User.Application.Internal.QueryServices;
using FruTech.Backend.API.User.Domain.Model.Commands;
using FruTech.Backend.API.User.Domain.Model.Queries;
using FruTech.Backend.API.User.Domain.Services;
using FruTech.Backend.API.User.Interfaces.REST.Resources;
using FruTech.Backend.API.User.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FruTech.Backend.API.User.Interfaces.REST.Controllers;

/// <summary>
/// Controlador para la gestión de usuarios (registro, autenticación y administración de perfil).
/// </summary>
[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
    private readonly IUserQueryService _userQueryService;

    public UsersController(IUserCommandService userCommandService, IUserQueryService userQueryService)
    {
        _userCommandService = userCommandService;
        _userQueryService = userQueryService;
    }

    /// <summary>
    /// Crea un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="resource">Datos de registro del nuevo usuario.</param>
    /// <response code="201">Usuario creado correctamente.</response>
    /// <response code="409">Ya existe un usuario con el mismo correo electrónico.</response>
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUserResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var user = await _userCommandService.Handle(command);
        if (user == null) return Conflict("Email already exists");
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return CreatedAtRoute("GetUserById", new { id = userResource.Id }, userResource);
    }

    /// <summary>
    /// Autentica un usuario y devuelve la información básica de sesión.
    /// </summary>
    /// <param name="resource">Credenciales del usuario (correo y contraseña).</param>
    /// <response code="200">Autenticación exitosa.</response>
    /// <response code="401">Credenciales inválidas.</response>
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInUserResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = new SignInUserCommand(resource.Email, resource.Password);
        var user = await _userCommandService.Handle(command);
        if (user == null) return Unauthorized();
        var response = new SignInResponseResource(user.Id, user.UserName, user.Email, string.Empty); // Token placeholder
        return Ok(response);
    }

    /// <summary>
    /// Obtiene un usuario por su identificador.
    /// </summary>
    /// <param name="id">Identificador único del usuario.</param>
    /// <response code="200">Usuario encontrado.</response>
    /// <response code="404">No existe un usuario con el identificador proporcionado.</response>
    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _userQueryService.Handle(query);
        if (user == null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

    /// <summary>
    /// Actualiza la información de perfil de un usuario existente.
    /// </summary>
    /// <param name="id">Identificador del usuario a actualizar.</param>
    /// <param name="resource">Datos de perfil a actualizar (nombre, correo, teléfono).</param>
    /// <response code="200">Perfil actualizado correctamente.</response>
    /// <response code="409">Correo electrónico ya en uso o usuario no encontrado.</response>
    [HttpPut("{id:int}/profile")]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateUserProfileResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = new UpdateUserProfileCommand(id, resource.UserName, resource.Email, resource.PhoneNumber);
        var updated = await _userCommandService.Handle(command);
        if (updated == null) return Conflict("Email already in use or user not found");
        var updatedResource = UserResourceFromEntityAssembler.ToResourceFromEntity(updated);
        return Ok(updatedResource);
    }

    /// <summary>
    /// Actualiza la contraseña de un usuario.
    /// </summary>
    /// <param name="id">Identificador del usuario.</param>
    /// <param name="resource">Contraseña actual y nueva contraseña.</param>
    /// <response code="204">Contraseña actualizada correctamente.</response>
    /// <response code="401">La contraseña actual es incorrecta.</response>
    [HttpPut("{id:int}/password")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdateUserPasswordResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = new UpdateUserPasswordCommand(id, resource.CurrentPassword, resource.NewPassword);
        var success = await _userCommandService.Handle(command);
        if (!success) return Unauthorized();
        return NoContent();
    }

    /// <summary>
    /// Elimina un usuario del sistema.
    /// </summary>
    /// <param name="id">Identificador del usuario a eliminar.</param>
    /// <param name="resource">Opcionalmente, contraseña actual para validar la eliminación.</param>
    /// <response code="204">Usuario eliminado correctamente.</response>
    /// <response code="401">Credenciales inválidas o contraseña incorrecta.</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromBody] UpdateUserPasswordResource? resource)
    {
        var command = new DeleteUserCommand(id, resource?.CurrentPassword);
        var success = await _userCommandService.Handle(command);
        if (!success) return Unauthorized();
        return NoContent();
    }
}
