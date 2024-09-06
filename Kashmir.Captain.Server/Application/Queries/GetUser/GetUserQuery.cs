using Kashmir.Captain.Server.Application.DTO;
using MediatR;

namespace Kashmir.Captain.Server.Application.Queries
{
     public readonly record struct GetUserQuery(int UserId) : IRequest<UserDto>;
}