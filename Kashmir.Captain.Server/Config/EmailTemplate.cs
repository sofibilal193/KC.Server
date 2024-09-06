using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Config
{
	public class EmailTemplate
	{
		public string? To { get; set; }
		public string? Subject { get; set; }
		public string? Body { get; set; }
	}
}