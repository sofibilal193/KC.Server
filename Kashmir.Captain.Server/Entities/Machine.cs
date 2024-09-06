using Kashmir.Captain.Server.Common.Entities;

namespace Kashmir.Captain.Server.Entities
{
	public class Machine : SqlEntity
	{
		public string? MachineName { get; set; }
		public string? MachineBrand { get; set; }
		public string? MachineDescription { get; set; }
		public string? Version { get; set; }

		public Machine() { }
	}
}