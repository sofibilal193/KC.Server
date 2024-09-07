using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Config;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Users.Commands
{
	public class ChangeUserEmailCommandHandler : IRequestHandler<ChangeUserEmailCommand, string>
	{
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		private readonly IUrlHelperService _urlHelperService;

		public ChangeUserEmailCommandHandler(
			UserManager<User> userManager,
			IEmailService emailService,
			IUrlHelperService urlHelperFactory)
		{
			_userManager = userManager;
			_emailService = emailService;
			_urlHelperService = urlHelperFactory;
		}

		public async Task<string> Handle(ChangeUserEmailCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync($"{request.UserId}") ?? throw new NotFoundException();

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
			if (!isPasswordValid)
			{
				throw new UnauthorizedAccessException("The provided password is incorrect.");
			}

			var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
			var changeEmailLink = _urlHelperService.GenerateUrl("ConfirmEmailChange", "Account", new { userId = user.Id, newEmail = request.NewEmail, token });

			var mail = new EmailTemplate
			{
				To = request.NewEmail,
				Subject = $"Confirm Email Change {GlobalConstants.ProjectName}",
				Body = $"Please confirm your email change by clicking here: {changeEmailLink}"
			};

			await _emailService.SendEmailAsync(mail);

			return "A confirmation link has been sent to your new email address.";
		}
	}
}
