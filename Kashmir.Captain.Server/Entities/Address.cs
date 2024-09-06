using Kashmir.Captain.Server.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Entities
{
	public class Address
	{
		public string Name { get; set; } = "";
		public string Address1 { get; set; } = "";
		public string Address2 { get; set; } = "";
		public string City { get; set; } = "";
		public string State { get; set; } = "";
		public string Country { get; set; } = "";
		public string ZipCode { get; set; } = "";

		public Address() {}
	}
}