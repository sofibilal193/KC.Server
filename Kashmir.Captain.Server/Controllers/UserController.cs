using Kashmir.Captain.Server.Application.Users.Commands;
using Kashmir.Captain.Server.Application.Users.Queries;
using Kashmir.Captain.Server.Config;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
		/// Assign role to User
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		[HttpPost("Assignrole")]
		// [Authorize(Policy = nameof(RoleType.SuperAdmin))]
		public async Task<IActionResult> AssignRoleAsync(int userId, RoleType role)
		{
			var response = await _mediator.Send(new AssignUserRoleCommand(userId, role, true));
			return Ok(response);
		}

		/// <summary>
		/// Remove role to User
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		[HttpPost("Removerole")]
		// [Authorize(Policy = nameof(RoleType.SuperAdmin))]
		public async Task<IActionResult> RemoveRoleAsync(int userId, RoleType role)
		{
			var response = await _mediator.Send(new AssignUserRoleCommand(userId, role, false));
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
		// [Authorize(Policy = nameof(RoleType.User))]
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
		[HttpPost("DeleteUser")]
		public async Task<IActionResult> DeleteUserAsync(int userId)
		{
			var command = new DeleteUserCommand();
			command.setId(userId);
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Send Email
		/// </summary>
		[HttpGet("sendMailnew")]
		public async Task SendEmailAsyncAgain()
		{
			var mail = new EmailTemplate
			{
				To = "sofibilal193@gmail.com",
				Subject = "Test Subject",
				Body = "Test Body"
			};
			await _emailService.SendEmailAsync(mail);
		}

		/// <summary>
		/// Get User
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet("GetUser")]
		[Authorize(Policy = nameof(RoleType.SuperAdmin))]
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
		[HttpGet("GetUsers")]
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