using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Common.Kashmir.Captain.Server.Common;
using Kashmir.Captain.Server.Config;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Accounts.Commands
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApiResponse<string>>
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

		public async Task<ApiResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			byte[] imageBytes = Convert.FromBase64String(request.ProfilePhoto);
			var user = new User(request.Email, request.FirstName, request.LastName, request.PhoneNumber, imageBytes);

			var createdResult = await _userManager.CreateAsync(user, request.Password);
			if (createdResult.Succeeded)
			{
				var addRoleResult = await _userManager.AddToRoleAsync(user, nameof(RoleType.User));
				if (addRoleResult.Succeeded)
				{
					var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var confirmEmailLink = _urlHelperService.GenerateUrl("ConfirmEmail", "Account", new { token, userId = user.Id });

					var mail = new EmailTemplate()
					{
						To = user.Email!,
						Subject = $"Confirm Email {GlobalConstants.ProjectName}",
					};
					mail.GetEmailRegistrationBody(user.FirstName, user.LastName, confirmEmailLink);

					await _emailService.SendEmailAsync(mail);

					return new ApiResponse<string> { IsSuccess = true, Message = "User registered successfully. Please check your email to confirm your account." };
				}
				else // RollBack
				{
					await _userManager.DeleteAsync(user);
					return new ApiResponse<string> { IsSuccess = false, Message = $"{addRoleResult.Errors.First()}" };
				}
			}
			return new ApiResponse<string> { IsSuccess = false, Message = $"{createdResult.Errors.First()}" };
		}
	}
}
