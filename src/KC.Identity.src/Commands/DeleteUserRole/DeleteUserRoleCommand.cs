using KC.Infrastructure.Persistance.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace KC.KC.Identity
{
	public readonly record struct DeleteUserRoleCommand(int UserId) : IRequest<string>;
}