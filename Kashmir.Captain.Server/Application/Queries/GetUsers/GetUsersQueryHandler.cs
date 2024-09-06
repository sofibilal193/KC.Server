using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Queries
{
	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedList<UserDto>>
	{
		private readonly UserManager<User> _userManager;
		public GetUsersQueryHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<PagedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			return await _userManager.Users.Select(user => new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Role = _userManager.GetRolesAsync(user).Result.ToList()
			}).ToPagedListAsync(request.Page, request.PageSize, cancellationToken);

		}
	}
}