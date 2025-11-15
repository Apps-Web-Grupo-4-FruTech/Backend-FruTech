using FruTech.Backend.API.User.Application.Internal.CommandServices;
using FruTech.Backend.API.User.Application.Internal.QueryServices;
using FruTech.Backend.API.User.Domain.Model.Commands;
using FruTech.Backend.API.User.Domain.Model.Queries;
using FruTech.Backend.API.User.Domain.Services;
using FruTech.Backend.API.User.Interfaces.REST.Resources;
using FruTech.Backend.API.User.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FruTech.Backend.API.User.Interfaces.REST.Controllers;

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

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUserResource resource)
    {
        var command = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var user = await _userCommandService.Handle(command);
        if (user == null) return Conflict("Email already exists");

        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);

        return CreatedAtRoute("GetUserById", new { id = userResource.Id }, userResource);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInUserResource resource)
    {
        var command = new SignInUserCommand(resource.Email, resource.Password);
        var user = await _userCommandService.Handle(command);
        if (user == null) return Unauthorized();

        var response = new SignInResponseResource(user.Id, user.UserName, user.Email, string.Empty); // Token placeholder
        return Ok(response);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _userQueryService.Handle(query);
        if (user == null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

    [HttpPut("{id:int}/profile")]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateUserProfileResource resource)
    {
        var command = new UpdateUserProfileCommand(id, resource.UserName, resource.Email, resource.PhoneNumber);
        var updated = await _userCommandService.Handle(command);
        if (updated == null) return Conflict("Email already in use or user not found");
        var updatedResource = UserResourceFromEntityAssembler.ToResourceFromEntity(updated);
        return Ok(updatedResource);
    }

    [HttpPut("{id:int}/password")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdateUserPasswordResource resource)
    {
        var command = new UpdateUserPasswordCommand(id, resource.CurrentPassword, resource.NewPassword);
        var success = await _userCommandService.Handle(command);
        if (!success) return Unauthorized();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromBody] UpdateUserPasswordResource? resource)
    {
        // Reutilizamos recurso para obtener contraseña actual si viene
        var command = new DeleteUserCommand(id, resource?.CurrentPassword);
        var success = await _userCommandService.Handle(command);
        if (!success) return Unauthorized();
        return NoContent();
    }
}
