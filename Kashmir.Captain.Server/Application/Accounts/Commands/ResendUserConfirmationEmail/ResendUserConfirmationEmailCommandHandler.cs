using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Accounts.Commands
{
	public class ResendUserConfirmationEmailCommandHandler : IRequestHandler<ResendUserConfirmationEmailCommand, string>
	{
		private readonly UserManager<User> _userManager;

		public ResendUserConfirmationEmailCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<string> Handle(ResendUserConfirmationEmailCommand request, CancellationToken cancellationToken)
		{

			var user = await _userManager.FindByIdAsync($"{request.UserId}");
			if (user == null)
				return "Invalid request.";

			if (user == null || await _userManager.IsEmailConfirmedAsync(user))
			{
				return "If the email is registered and not confirmed, a confirmation link has been sent.";
			}

			//var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			//var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);

			// await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking here: {confirmationLink}");

			return "If the email is registered and not confirmed, a confirmation link has been sent.";

		}
	}
}