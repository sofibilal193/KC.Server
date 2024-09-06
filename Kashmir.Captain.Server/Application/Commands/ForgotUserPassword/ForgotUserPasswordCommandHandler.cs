using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Commands
{
	public class ForgotUserPasswordCommandHandler : IRequestHandler<ForgotUserPasswordCommand, string>
	{
		private readonly UserManager<User> _userManager;

		public ForgotUserPasswordCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<string> Handle(ForgotUserPasswordCommand request, CancellationToken cancellationToken)
		{

			var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(request.Email));

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			//var resetLink = Url.Action("ResetPassword", "account", new { token, email = user.Email }, Request.Scheme);

			var resetLink = "";

			string subject = "Reset Password";
			string body = $"Please reset your password by clicking here: {resetLink}";

			//await _emailService.SendEmailAsync(user.Email, subject, body);
			return "If the email is registered and confirmed, a reset link has been sent.";

		}
	}
}