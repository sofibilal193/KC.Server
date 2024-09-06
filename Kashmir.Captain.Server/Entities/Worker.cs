using Kashmir.Captain.Server.Common.Entities;

namespace Kashmir.Captain.Server.Entities
{
	public class Worker : SqlEntity
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? PhoneNumber { get; set; }

		public Worker() { }
	}
}