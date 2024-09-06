using Microsoft.AspNetCore.Identity;
using System;

namespace Kashmir.Captain.Server.Entities
{
	public class Role : IdentityRole<int>
	{
		public Role(string roleName) {
			Name = roleName;
		}
		public Role() {
		}
	}
}
