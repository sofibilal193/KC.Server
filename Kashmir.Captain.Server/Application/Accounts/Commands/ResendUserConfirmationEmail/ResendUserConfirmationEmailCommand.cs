using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.Application.Accounts.Commands
{
	public class ResendUserConfirmationEmailCommand : IRequest<string>
	{
		[JsonIgnore]
		public int UserId { get; private set; }

		public void setId(int userId)
		{
			UserId = userId;
		}
	}
}