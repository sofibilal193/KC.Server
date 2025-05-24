using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Infrastructure.Persistance.Entities
{
	public class Role : IdentityRole<int>
	{
		public Role(string roleName)
		{
			Name = roleName;
		}
		public Role()
		{
		}
	}
}
