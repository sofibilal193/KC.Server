using MediatR;
using System.Text.Json.Serialization;

namespace Kashmir.Captain.Server.Application.Accounts.Commands
{
	public class ForgotUserPasswordCommand : IRequest<string>
	{
		public string Email { get; init; } = "";
	}
}