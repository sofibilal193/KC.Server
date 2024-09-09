using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.Application.Users.Commands
{
	public readonly record struct DeleteUserRoleCommand(int UserId) : IRequest<string>;
}