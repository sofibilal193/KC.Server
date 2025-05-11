using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Config;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.KC.Identity
{
	public class ForgotUserPasswordCommandHandler : IRequestHandler<ForgotUserPasswordCommand, string>
	{
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		private readonly IUrlHelperService _urlHelperService;

		public ForgotUserPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService, IUrlHelperService urlHelperService)
		{
			_userManager = userManager;
			_emailService = emailService;
			_urlHelperService = urlHelperService;
		}
		public async Task<string> Handle(ForgotUserPasswordCommand request, CancellationToken cancellationToken)
		{

			var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(request.Email));
			if (string.IsNullOrEmpty(user.Email))
			{
				throw new InvalidOperationException("User's email is not set.");
			}
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var resetLink = _urlHelperService.GenerateUrl("ResetPassword", "Account", new { token, email = user.Email });

			var mail = new EmailTemplate()
			{
				To = user.Email,
				Subject = $"Reset Password {GlobalConstants.ProjectName}",
			};
			mail.GetResetPasswordBody(resetLink);

			await _emailService.SendEmailAsync(mail);
			return "If the email is registered and confirmed, a reset link has been sent.";

		}
	}
}