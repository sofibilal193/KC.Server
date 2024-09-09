using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Kashmir.Captain.Server.Application.Accounts.Commands;
using Kashmir.Captain.Server.Application.Accounts.Queries;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;

namespace Kashmir.Captain.Server.Controllers
{
	[ApiController]
	[Route("api/Account")]
	public class AccountController : ControllerBase
	{
		private readonly IMediator _mediator;
		public AccountController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Register User
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("Register")]
		public async Task<IActionResult> RegisterUserAsync(RegisterUserCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Confirm EMail Call from Email
		/// </summary>
		/// <param name="token"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet("ConfirmEmail")]
		public async Task<IActionResult> ConfirmEmailAsync(string token, int userId)
		{
			var response = await _mediator.Send(new ConfirmUserEmailQuery(token, userId));
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
			var response = await _mediator.Send(command);
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
			var response = await _mediator.Send(command);
			return Ok(response);
		}


		/// <summary>
		/// Forgot Password (Sends token to Email)
		/// </summary>
		/// <returns></returns>
		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotUserPasswordCommand command)
		{
			var response = await _mediator.Send(command);
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
			var response = await _mediator.Send(command);
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
			var response = await _mediator.Send(command);
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
			var response = await _mediator.Send(new ConfirmUserEmailChangeQuery(userId, newEmail, token));
			return Ok(response);
		}
	}
}