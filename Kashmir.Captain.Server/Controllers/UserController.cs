using Kashmir.Captain.Server.Application.Commands;
using Kashmir.Captain.Server.Application.Queries;
using Kashmir.Captain.Server.Config;
using Kashmir.Captain.Server.Entities;
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

		public UserController( IEmailService emailService, IMediator mediator)
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
		[HttpPost("role")]
		[Authorize(Policy = nameof(RoleType.SuperAdmin))]
		public async Task<IActionResult> AssignRoleAsync(int userId, RoleType role)
		{
			var command = new AssignUserRoleCommand();
			command.setId(userId, role);
			var response = await _mediator.Send(command);
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
		[HttpGet("sendMail")]
		public async Task SendEmailAsync()
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
		[HttpPost("GetUser")]
		public async Task<IActionResult> GetUserAsync(int userId)
		{
			var response = await _mediator.Send(new GetUserQuery(userId));
			return Ok(response);
		}

		/// <summary>
		/// Get all Users
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[HttpPost("GetUsers")]
		public async Task<IActionResult> GetAllUserAsync(int userId, int page, int pageSize)
		{
			var response = await _mediator.Send(new GetUsersQuery(userId, page, pageSize));
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