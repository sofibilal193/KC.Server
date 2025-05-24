using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using KC.Infrastructure.Persistance.Entities;
using KC.KC.Identity;
using KC.Application.Users.Queries;
using KC.Application.Common.Controllers;

namespace KC.Controllers
{
	[Route("api/Account")]
	public class AccountController : BaseController
	{
		#region RegisterUserAsync
		/// <summary>
		/// Register User
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("Register")]
		public async Task<IActionResult> RegisterUserAsync(RegisterUserCommand command)
		{
			var response = await Mediator.Send(command);
			return Ok(response);
		}
		#endregion

		/// <summary>
		/// Confirm EMail Call from Email
		/// </summary>
		/// <param name="token"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet("ConfirmEmail")]
		public async Task<IActionResult> ConfirmEmailAsync(string token, int userId)
		{
			var response = await Mediator.Send(new ConfirmUserEmailQuery(token, userId));
			return Ok(response);
		}


		/// <summary>
		/// Login User
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command)
		{
			var response = await Mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Change Password 
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("ChangePassword")]
		[Authorize(Policy = nameof(RoleType.User))]
		public async Task<IActionResult> ChangePasswordAsync(int userId, ChangeUserPasswordCommand command)
		{
			command.setId(userId);
			var response = await Mediator.Send(command);
			return Ok(response);
		}


		/// <summary>
		/// Forgot Password (Sends token to Email)
		/// </summary>
		/// <returns></returns>
		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotUserPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Internal : Reset Password (Get Token from Email and Call this API )
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns> 
		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPasswordAsync(ResetUserPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Log out User
		/// </summary>
		/// <returns></returns> 
		[HttpPost("logout")]
		public IActionResult LogoutAsync()
		{
			return Ok("Logged out successfully.");
		}

		/// <summary>
		/// Resend Email Confirmation
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpPost("ResendConfirmationEmail")]
		public async Task<IActionResult> ResendConfirmationEmailAsync([FromBody] int userId)
		{
			var command = new ResendUserConfirmationEmailCommand();
			command.setId(userId);
			var response = await Mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Confirm Email Change : Called from Email
		/// </summary>
		/// <param name="token"></param>
		/// <param name="userId"></param>
		/// <param name="newEmail"></param>
		/// <returns></returns>
		[HttpGet("ConfirmEmailChange")]
		public async Task<IActionResult> ConfirmEmailChangeAsync(int userId, string newEmail, string token)
		{
			var response = await Mediator.Send(new ConfirmUserEmailChangeQuery(userId, newEmail, token));
			return Ok(response);
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
			var response = await Mediator.Send(new UpsertRoleCommand(userId, role, AssignRole));
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
			var response = await Mediator.Send(new DeleteUserRoleCommand(userId));
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
			var response = await Mediator.Send(command);
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
			var response = await Mediator.Send(command);
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
			var response = await Mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Get User
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet("Users/{userId}")]
		[Authorize(Policy = nameof(RoleType.User))]
		public async Task<IActionResult> GetUserAsync(int userId)
		{
			var response = await Mediator.Send(new GetUserQuery(userId));
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
		[Authorize]
		public async Task<IActionResult> GetAllUserAsync(int page, int pageSize, string? sort, string? search)
		{
			var response = await Mediator.Send(new GetUsersQuery(page, pageSize, sort, search));
			return Ok(response);
		}
	}
}