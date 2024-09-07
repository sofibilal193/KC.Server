using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Common.Entities
{
	public interface IBaseEntity
	{
		DateTime? CreatedDate { get; set; }
		string? CreatedBy { get; set; }
		DateTime? ModifiedDate { get; set; }
		string? ModifiedBy { get; set; }
	}

}