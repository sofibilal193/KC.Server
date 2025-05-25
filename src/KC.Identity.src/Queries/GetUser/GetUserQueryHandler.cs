using KC.Application.DTO;
using KC.Common.Extensions;
using KC.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KC.Application.Users.Queries
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
			var roles = (await _userManager.GetRolesAsync(user)).ToList();

			var UserDto = new UserDto
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Id = user.Id,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Role = roles,
				ProfilePhoto = Convert.ToBase64String(user.ProfilePhoto)
			};

			return UserDto;
		}
	}
}