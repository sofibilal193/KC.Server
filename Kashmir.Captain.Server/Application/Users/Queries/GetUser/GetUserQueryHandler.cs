using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Users.Queries
{
	public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
	{
		private readonly UserManager<User> _userManager;

		public GetUserQueryHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync($"{request.UserId}") ?? throw new NotFoundException();
			var roles = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

			return new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Role = roles
			};
		}
	}
}