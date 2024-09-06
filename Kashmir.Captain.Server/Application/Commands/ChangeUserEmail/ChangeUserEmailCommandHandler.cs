using Kashmir.Captain.Server.Entities;
using Kashmir.Captain.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Kashmir.Captain.Server.Client;
using Kashmir.Captain.Server.Application.Queries;

namespace Kashmir.Captain.Server.Application.Commands
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
            var user = await _userManager.FindByIdAsync($"{request.UserId}");
            if (user == null)
                return "User not found.";

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
            var changeEmailLink = _urlHelperService.GenerateUrl("ConfirmEmailChange", "Account", new {userId = user.Id,newEmail = request.NewEmail,  token });

            //await _emailService.SendEmailAsync(user.Email, "Change Email", $"Please confirm your email change by clicking here: {changeEmailLink}");

            // return "A confirmation link has been sent to your new email address.";
			return changeEmailLink;
        }
    }
}
