using Kashmir.Captain.Server.Config;
using Kashmir.Captain.Server.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Commands
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
	{
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		// private readonly IUrlHelper _urlHelper;

		public RegisterUserCommandHandler(UserManager<User> userManager,IEmailService emailService
		)
		{
			_userManager = userManager;
			_emailService = emailService;
			// _urlHelper = urlHelper;
		}

		public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var user = new User{UserName = request.Email,Email = request.Email,FirstName = request.FirstName,LastName = request.LastName,PhoneNumber = request.PhoneNumber};

			var result = await _userManager.CreateAsync(user, request.Password);
			if (result.Succeeded)
			{
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				// var callbackUrl = _urlHelper.Action("ConfirmEmail", "Account",
				// 	new { token, email = user.Email },
				// 	protocol: "https");

					var callbackUrl = "xyz";
				var mail = new EmailTemplate
				{
					To = user.Email,
					Subject = "Confirm Email",
					Body = $"Please confirm your email by clicking this link: {callbackUrl}"
				};

				await _emailService.SendEmailAsync(mail);

				return "User registered successfully. Please check your email to confirm your account.";
			}

			// Return a string message or throw an exception with details
			return "Error registering user.";
		}
	}
}
