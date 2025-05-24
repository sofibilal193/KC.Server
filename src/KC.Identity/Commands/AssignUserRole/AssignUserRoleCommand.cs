using KC.Infrastructure.Persistance.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace KC.KC.Identity
{
	public readonly record struct UpsertRoleCommand(int UserId, RoleType Role, bool AssignRole) : IRequest<string>;
}