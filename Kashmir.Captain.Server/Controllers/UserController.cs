using Kashmir.Captain.Server.Application.Users.Commands;
using Kashmir.Captain.Server.Application.Users.Queries;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kashmir.Captain.Server.Controllers
{
	[ApiController]
	[Route("api/Users")]
	public class UserController : ControllerBase
	{
		private readonly IEmailService _emailService;
		private readonly IMediator _mediator;

		public UserController(IEmailService emailService, IMediator mediator)
		{
			_emailService = emailService;
			_mediator = mediator;
		}

		/// <summary>
		/// Upsert role to User
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="role"></param>
		/// <param name="AssignRole"> True: Add-Role, False: Delete-Role</param>
		/// <returns></returns>
		[HttpPost("Upsertrole")]
		[Authorize(Policy = nameof(RoleType.SuperAdmin))]
		public async Task<IActionResult> UpsertRoleAsync(int userId, RoleType role, bool AssignRole)
		{
			var response = await _mediator.Send(new UpsertRoleCommand(userId, role, AssignRole));
			return Ok(response);
		}

		/// <summary>
		/// Remove role to User
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpDelete("Deleterole")]
		[Authorize(Policy = nameof(RoleType.SuperAdmin))]
		public async Task<IActionResult> DeleteRoleAsync(int userId)
		{
			var response = await _mediator.Send(new DeleteUserRoleCommand(userId));
			return Ok(response);
		}

		/// <summary>
		/// Update User Profile
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("UpdateProfile")]
		[Authorize(Policy = nameof(RoleType.User))]
		public async Task<IActionResult> UpdateProfileAsync(int userId, [FromBody] UpdateUserProfileCommand command)
		{
			command.setId(userId);
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Change Email Address
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("ChangeEmail")]
		[Authorize(Policy = nameof(RoleType.User))]
		public async Task<IActionResult> ChangeEmailAsync(int userId, ChangeUserEmailCommand command)
		{
			command.setId(userId);
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Delete a User
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpDelete("DeleteUser")]
		[Authorize(Policy = nameof(RoleType.SuperAdmin))]
		public async Task<IActionResult> DeleteUserAsync(int userId)
		{
			var command = new DeleteUserCommand();
			command.setId(userId);
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Get User
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet("User")]
		[Authorize(Policy = nameof(RoleType.User))]
		public async Task<IActionResult> GetUserAsync(int userId)
		{
			var response = await _mediator.Send(new GetUserQuery(userId));
			return Ok(response);
		}

		/// <summary>
		/// Get all Users
		/// </summary>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="sort"></param>
		/// <param name="search"></param>
		/// <returns></returns>
		[HttpGet("Users")]
		[Authorize(Policy = nameof(RoleType.SuperAdmin))]
		public async Task<IActionResult> GetAllUserAsync(int page, int pageSize, string? sort, string? search)
		{
			var response = await _mediator.Send(new GetUsersQuery(page, pageSize, sort, search));
			return Ok(response);
		}
	}
}

// GetProfile
// UpdateProfile
// DeleteUser
// GetAllUsers
// GetUserById
// AssignRole
// RemoveRole
// ChangeUserStatus
// UpdateUserRoles
// GetUserRoles