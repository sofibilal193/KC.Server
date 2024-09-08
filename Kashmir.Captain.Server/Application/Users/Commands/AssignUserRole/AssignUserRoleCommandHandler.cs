using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Application.Users.Commands
{
	public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, string>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public AssignUserRoleCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<string> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrEmpty(request.UserId.ToString()) && Enum.IsDefined(typeof(RoleType), request.Role))
			{
				var roleName = request.Role.ToString();

				// Check if role exists in DB
				if (!await _roleManager.RoleExistsAsync(roleName))
				{
					return "Error assigning role.";
				}

				var user = await _userManager.FindByIdAsync(request.UserId.ToString())
						   ?? throw new NotFoundException(nameof(User), request.UserId);

				var userRoles = await _userManager.GetRolesAsync(user);

				if (request.AssignRole)
				{
					// Assign new role
					var addResult = await _userManager.AddToRoleAsync(user, roleName);
					return addResult.Succeeded ? "Role assigned successfully." : string.Join("; ", addResult.Errors.Select(e => e.Description)); ;
				}
				else
				{
					// Remove the role
					var removeResult = await _userManager.RemoveFromRoleAsync(user, roleName);
					return removeResult.Succeeded ? "Role removed successfully." : "Error removing role.";
				}
			}

			return "Error assigning role.";
		}
	}
}