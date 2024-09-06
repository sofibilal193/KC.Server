using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Application.DTO
{
    public class CurrentUserDetailDto
    {
        public int Id { get; set; }
		public string? Email { get; set; }
		public string? Role { get; set; }
    }
}