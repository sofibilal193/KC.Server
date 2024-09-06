using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.Application.Commands
{
    public class DeleteUserCommand : IRequest<string>
    {
        [JsonIgnore]
		public int? UserId { get; private set; }

		public void setId(int? userId)
		{
			UserId = userId;
		}
    }
}