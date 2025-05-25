using KC.Application.DTO;
using MediatR;

namespace KC.Application.Users.Queries
{
	public readonly record struct GetUserQuery(int UserId) : IRequest<UserDto>;
}