using KC.Common.Entities;

namespace KC.Infrastructure.Persistance.Entities
{
	public class Worker : SqlEntity
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? PhoneNumber { get; set; }

		public Worker() { }
	}
}