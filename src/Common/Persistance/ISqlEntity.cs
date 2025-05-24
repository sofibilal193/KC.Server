using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Common.Persistance
{
	public interface ISqlEntity
	{
		int Id { get; set; }
		byte[]? Timestamp { get; set; }
		dynamic GetId();
		bool IsTransient();
	}

}