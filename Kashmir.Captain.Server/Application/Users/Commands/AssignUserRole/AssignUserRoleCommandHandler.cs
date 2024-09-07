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

			if (string.IsNullOrEmpty($"{request.UserId}") || !Enum.IsDefined(typeof(RoleType), request.Role))
			{
				return "User ID and Role Name are required.";
			}

			var roleName = request.Role.ToString();
			var user = await _userManager.FindByIdAsync($"{request.UserId}");
			if (user == null)
			{
				return "User not found.";
			}

			if (!await _roleManager.RoleExistsAsync(roleName))
			{
				return "Role does not exist.";
			}

			var result = await _userManager.AddToRoleAsync(user, roleName);
			if (result.Succeeded)
			{
				return "Role assigned successfully.";
			}

			return "Error assigning role.";

		}
	}
}