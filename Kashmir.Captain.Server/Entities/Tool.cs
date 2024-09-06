using Kashmir.Captain.Server.Common.Entities;

namespace Kashmir.Captain.Server.Entities
{
	public class Tool : SqlEntity
	{
		public string? Name { get; set; }
		public string? Brand { get; set; }
		public string? Description { get; set; }
		public Tool() { }
	}
}