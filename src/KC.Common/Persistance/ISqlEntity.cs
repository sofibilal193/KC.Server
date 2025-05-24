using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KC.Common.Persistance
{
	public interface ISqlEntity
	{
		int Id { get; set; }
		byte[]? Timestamp { get; set; }
		dynamic GetId();
		bool IsTransient();
	}

}