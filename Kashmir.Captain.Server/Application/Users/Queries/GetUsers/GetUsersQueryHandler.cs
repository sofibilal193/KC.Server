using AutoMapper;
using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Application.Users.Queries
{
	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedList<UserDto>>
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		public GetUsersQueryHandler(UserManager<User> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}
		public async Task<PagedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var query = _userManager.Users;

			var sortExpression = request.Sort?.TrimStart('-')?.ToLower();
			var prefix = request.Sort?.StartsWith('-') == true ? "-" : "";
			if (sortExpression == nameof(UserDto.Email).ToLower())
			{
				sortExpression = prefix + nameof(User.Email);
			}
			else if (sortExpression == nameof(UserDto.FirstName).ToLower())
			{
				sortExpression = prefix + nameof(User.FirstName);
			}
			else
			{
				sortExpression = nameof(User.Id);
			}

			if (!string.IsNullOrEmpty(request.search))
			{
				var search = request.search;
				query = query.Where(d => (d.FirstName ?? "").Contains(search) || (d.LastName ?? "").Contains(search)
										|| (d.Email ?? "").Contains(search) || (d.PhoneNumber ?? "").Contains(search));
			}

			query = query.OrderBy(sortExpression);

			return _mapper.Map<PagedList<UserDto>>(
				await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken));
		}
	}
}