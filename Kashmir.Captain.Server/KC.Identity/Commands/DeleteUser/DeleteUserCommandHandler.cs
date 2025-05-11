using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.KC.Users
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
	{
		private readonly UserManager<User> _userManager;

		public DeleteUserCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync($"{request.UserId}")
				?? throw new NotFoundException(nameof(User), $"{request.UserId}");

			var result = await _userManager.DeleteAsync(user);

			if (!result.Succeeded)
			{
				return $"{result.Errors}";
			}

			return $"{result.Succeeded}";
		}
	}
}