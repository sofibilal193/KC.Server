using Microsoft.AspNetCore.Identity;

namespace KC.Infrastructure.Persistance.Entities
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
