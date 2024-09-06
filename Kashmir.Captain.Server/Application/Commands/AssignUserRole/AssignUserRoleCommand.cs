using Kashmir.Captain.Server.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.Application.Commands
{
    public class AssignUserRoleCommand :  IRequest<string>
    {
        [JsonIgnore]
		public int UserId { get; private set; }
		public RoleType Role { get; private set; }

		public void setId(int userId, RoleType role)
		{
			UserId = userId;
			Role = role;
		}
       
    }
}