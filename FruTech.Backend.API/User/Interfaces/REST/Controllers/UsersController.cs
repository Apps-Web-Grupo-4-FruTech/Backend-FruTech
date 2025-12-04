using FruTech.Backend.API.User.Domain.Model.Commands;
using FruTech.Backend.API.User.Domain.Model.Queries;
using FruTech.Backend.API.User.Domain.Services;
using FruTech.Backend.API.User.Interfaces.REST.Resources;
using FruTech.Backend.API.User.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FruTech.Backend.API.User.Interfaces.REST.Controllers;

/// <summary>
/// Controller for user management (registration, authentication, and profile management).
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
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="resource">New user registration data.</param>
    /// <response code="201">User created successfully.</response>
    /// <response code="409">A user with the same email already exists.</response>
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUserResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var user = await _userCommandService.Handle(command);
        if (user == null) return Conflict("Email or Identificator already exists");
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return CreatedAtRoute("GetUserById", new { id = userResource.Id }, userResource);
    }

    /// <summary>
    /// Authenticates a user and returns a basic session payload.
    /// </summary>
    /// <param name="resource">User credentials (email and password).</param>
    /// <response code="200">Authentication succeeded.</response>
    /// <response code="401">Invalid credentials.</response>
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
    /// Gets a user by id.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <response code="200">User found.</response>
    /// <response code="404">No user found with the provided id.</response>
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
    /// Updates the profile of an existing user. IDs and audit fields cannot be modified.
    /// </summary>
    /// <param name="id">Identifier of the user to update.</param>
    /// <param name="resource">Profile data to update (userName, email, phoneNumber).</param>
    /// <response code="200">Profile updated successfully.</response>
    /// <response code="409">Email already in use or user not found.</response>
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
    /// Updates a user's password.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="resource">Current and new password.</param>
    /// <response code="204">Password updated successfully.</response>
    /// <response code="401">The current password is incorrect.</response>
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
    /// Deletes a user from the system.
    /// </summary>
    /// <param name="id">Identifier of the user to delete.</param>
    /// <param name="resource">Optionally, the current password to validate deletion.</param>
    /// <response code="204">User deleted successfully.</response>
    /// <response code="401">Invalid credentials or incorrect password.</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromBody] UpdateUserPasswordResource? resource)
    {
        var command = new DeleteUserCommand(id, resource?.CurrentPassword);
        var success = await _userCommandService.Handle(command);
        if (!success) return Unauthorized();
        return NoContent();
    }
}
