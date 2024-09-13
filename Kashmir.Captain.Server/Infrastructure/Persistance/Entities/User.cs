using Kashmir.Captain.Server.Common.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Kashmir.Captain.Server.Infrastructure.Persistance.Entities
{
	public class User : IdentityUser<int>
	{
		[Key]
		public override int Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public override string? PhoneNumber { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? ModifiedBy { get; set; }

		public User() { }

		public User(string email, string? firstName, string? lastName, string? phoneNumber)
		{
			UserName = email;
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phoneNumber;
			CreatedDate = DateTime.UtcNow;
			CreatedBy = "";
		}

		public void Update(string? firstName, string? lastName, string? phoneNumber)
		{
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phoneNumber;
			ModifiedDate = DateTime.UtcNow;
			ModifiedBy = "";
		}
	}
}