using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Config;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Accounts.Commands
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
	{
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		private readonly IUrlHelperService _urlHelperService;


		public RegisterUserCommandHandler(UserManager<User> userManager, IEmailService emailService, IUrlHelperService urlHelperService)
		{
			_userManager = userManager;
			_emailService = emailService;
			_urlHelperService = urlHelperService;
		}

		public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var user = new User { UserName = request.Email, Email = request.Email, FirstName = request.FirstName, LastName = request.LastName, PhoneNumber = request.PhoneNumber };

			var result = await _userManager.CreateAsync(user, request.Password);
			if (result.Succeeded)
			{
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var confirmEmailLink = _urlHelperService.GenerateUrl("ConfirmEmail", "Account", new { token, userId = user.Id });

				var mail = new EmailTemplate
				{
					To = user.Email,
					Subject = $"Confirm Email {GlobalConstants.ProjectName}",
					Body = $"Thank You For Rgistration. Please confirm your email by clicking this link: {confirmEmailLink}"
				};

				await _emailService.SendEmailAsync(mail);

				return "User registered successfully. Please check your email to confirm your account.";
			}

			// Return a string message or throw an exception with details
			return "Error registering user.";
		}
	}
}
