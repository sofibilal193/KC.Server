using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Common.Extensions;
using MediatR;

namespace Kashmir.Captain.Server.Application.Users.Queries
{
	public readonly record struct GetUsersQuery(int UserId, int Page, int PageSize) : IRequest<PagedList<UserDto>>;
}