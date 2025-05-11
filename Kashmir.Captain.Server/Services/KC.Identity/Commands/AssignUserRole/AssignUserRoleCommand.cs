using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.KC.Users
{
	public readonly record struct UpsertRoleCommand(int UserId, RoleType Role, bool AssignRole) : IRequest<string>;
}