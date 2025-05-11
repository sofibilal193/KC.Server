using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.KC.Users
{
	public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, string>
	{
		private readonly UserManager<User> _userManager;

		public UpdateUserProfileCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<string> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync($"{request.UserId}")
			?? throw new NotFoundException("");

			user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.PhoneNumber = request.PhoneNumber;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
				return "Profile updated successfully.";

			return "BadRequest(ModelState)";
		}
	}
}