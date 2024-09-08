using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.Application.Users.Commands
{
	public readonly record struct AssignUserRoleCommand(int UserId, RoleType Role, bool? AssignRole) : IRequest<string>;
}