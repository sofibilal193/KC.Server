using Kashmir.Captain.Server.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kashmir.Captain.Server.Infrastructure.Persistance.Entities
{
	public class User : IdentityUser<int>
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public override string? PhoneNumber { get; set; }

		public User() { }

		public User(string? firstName, string? lastName, string? phoneNumber)
		{
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phoneNumber;
		}

		public void Update(string? firstName, string? lastName, string? phoneNumber)
		{
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phoneNumber;
		}
	}
}